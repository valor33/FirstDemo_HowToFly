using UnityEngine;

/// <summary>
/// 弹跳平台
/// 玩家从上方落下时自动弹跳
/// 通过调用 PlayerJump.ForceJump 实现弹跳效果
/// </summary>
public class BouncePlatform : MonoBehaviour
{
    #region 序列化字段（在 Inspector 中可配置）

    [Header("弹跳参数")]
    [Tooltip("弹跳力度（-1 使用玩家默认跳跃力）")]
    [SerializeField] private float bounceForce = -1f;

    [Tooltip("弹跳力度倍率")]
    [SerializeField] private float bounceMultiplier = 1.5f;

    [Header("视觉效果")]
    [Tooltip("平台颜色")]
    [SerializeField] private Color platformColor = new Color(0.5f, 1f, 0.5f, 1f);

    [Tooltip("弹跳动画时长")]
    [SerializeField] private float animationDuration = 0.2f;

    [Tooltip("弹跳时缩放倍数")]
    [SerializeField] private float bounceScale = 0.8f;

    [Header("组件引用")]
    [Tooltip("平台渲染器")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    #endregion

    #region 私有字段

    // 初始缩放
    private Vector3 initialScale;

    // 动画计时器
    private float animationTimer = 0f;

    // 是否正在播放动画
    private bool isAnimating = false;

    // 弹跳冷却时间（防止重复触发）
    private float cooldownTimer = 0f;

    // 冷却时间
    private const float COOLDOWN_TIME = 0.1f;

    #endregion

    #region Unity 生命周期方法

    private void Awake()
    {
        // 记录初始缩放
        initialScale = transform.localScale;

        // 如果未指定 SpriteRenderer，尝试获取
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // 设置平台颜色
        if (spriteRenderer != null)
        {
            spriteRenderer.color = platformColor;
        }
    }

    private void Update()
    {
        // 更新冷却计时器
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // 播放弹跳动画
        if (isAnimating)
        {
            PlayBounceAnimation();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"[BouncePlatform] OnCollisionEnter2D: {collision.gameObject.name}, Tag: {collision.gameObject.tag}");
        HandleCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log($"[BouncePlatform] OnCollisionStay2D: {collision.gameObject.name}, Tag: {collision.gameObject.tag}");
        HandleCollision(collision);
    }

    private void HandleCollision(Collision2D collision)
    {
        // 检查冷却时间
        if (cooldownTimer > 0f)
        {
            Debug.Log("[BouncePlatform] 冷却中，跳过");
            return;
        }

        // 检查是否是玩家
        if (!collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("[BouncePlatform] 不是玩家，跳过");
            return;
        }

        // 检查玩家是否从上方落下
        bool fromAbove = IsPlayerFromAbove(collision);
        Debug.Log($"[BouncePlatform] IsPlayerFromAbove: {fromAbove}");
        if (!fromAbove) return;

        // 执行弹跳
        BouncePlayer(collision.gameObject);
    }

    #endregion

    #region 私有方法

    /// <summary>
    /// 检查玩家是否从上方落下
    /// </summary>
    private bool IsPlayerFromAbove(Collision2D collision)
    {
        // 方法1：检查玩家速度方向
        Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            // 如果玩家正在下落（速度向下），说明是从上方来的
            bool isFalling = playerRb.velocity.y <= 0f;
            Debug.Log($"[BouncePlatform] 玩家速度 Y: {playerRb.velocity.y}, 正在下落: {isFalling}");
            return isFalling;
        }

        // 方法2：检查碰撞接触点法线（备用）
        ContactPoint2D[] contacts = collision.contacts;
        if (contacts.Length == 0)
        {
            Debug.Log("[BouncePlatform] 没有碰撞接触点");
            return false;
        }

        Vector2 normal = contacts[0].normal;
        Debug.Log($"[BouncePlatform] 接触点法线: {normal}, normal.y = {normal.y}");
        return normal.y > 0.5f;
    }

    /// <summary>
    /// 弹跳玩家
    /// </summary>
    private void BouncePlayer(GameObject player)
    {
        // 获取 PlayerJump 组件
        PlayerJump playerJump = player.GetComponent<PlayerJump>();
        if (playerJump == null)
        {
            Debug.LogWarning("BouncePlatform: 玩家没有 PlayerJump 组件");
            return;
        }

        // 计算弹跳力度
        float force = bounceForce;
        if (force < 0f)
        {
            // 使用默认跳跃力乘以倍率
            force = playerJump.JumpForce * bounceMultiplier;
        }

        // 执行强制跳跃
        playerJump.ForceJump(force);

        // 播放弹跳动画
        StartBounceAnimation();

        // 设置冷却时间
        cooldownTimer = COOLDOWN_TIME;

        Debug.Log($"BouncePlatform: 弹跳玩家，力度: {force}");
    }

    /// <summary>
    /// 开始弹跳动画
    /// </summary>
    private void StartBounceAnimation()
    {
        isAnimating = true;
        animationTimer = 0f;
    }

    /// <summary>
    /// 播放弹跳动画
    /// </summary>
    private void PlayBounceAnimation()
    {
        animationTimer += Time.deltaTime;
        float progress = animationTimer / animationDuration;

        if (progress >= 1f)
        {
            // 动画完成
            transform.localScale = initialScale;
            isAnimating = false;
            return;
        }

        // 缩放动画：压缩后恢复
        float scaleMultiplier;
        if (progress < 0.5f)
        {
            // 前半段压缩
            scaleMultiplier = Mathf.Lerp(1f, bounceScale, progress * 2f);
        }
        else
        {
            // 后半段恢复
            scaleMultiplier = Mathf.Lerp(bounceScale, 1f, (progress - 0.5f) * 2f);
        }

        // 只在 Y 轴压缩
        Vector3 newScale = initialScale;
        newScale.y *= scaleMultiplier;
        transform.localScale = newScale;
    }

    #endregion

    #region 公开方法

    /// <summary>
    /// 设置弹跳力度
    /// </summary>
    public void SetBounceForce(float force)
    {
        bounceForce = force;
    }

    /// <summary>
    /// 设置弹跳倍率
    /// </summary>
    public void SetBounceMultiplier(float multiplier)
    {
        bounceMultiplier = multiplier;
    }

    #endregion

    #region 编辑器可视化

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        // 绘制平台范围
        Gizmos.color = Color.green;

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            Bounds bounds = collider.bounds;
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }
    }
#endif

    #endregion
}