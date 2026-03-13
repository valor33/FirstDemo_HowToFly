using UnityEngine;

/// <summary>
/// 检查点
/// 玩家触发后更新重生点位置
/// 提供视觉反馈表示已被激活
/// </summary>
public class Checkpoint : MonoBehaviour
{
    #region 序列化字段（在 Inspector 中可配置）

    [Header("检查点参数")]
    [Tooltip("重生点的偏移位置（相对于检查点中心）")]
    [SerializeField] private Vector3 respawnOffset = Vector3.zero;

    [Tooltip("是否只能触发一次")]
    [SerializeField] private bool singleUse = true;

    [Header("视觉效果")]
    [Tooltip("未激活时的颜色")]
    [SerializeField] private Color inactiveColor = new Color(0.5f, 0.5f, 0.5f, 1f);

    [Tooltip("激活后的颜色")]
    [SerializeField] private Color activeColor = new Color(0f, 1f, 0.5f, 1f);

    [Tooltip("激活时的缩放动画时间")]
    [SerializeField] private float activationDuration = 0.3f;

    [Tooltip("激活时的缩放倍数")]
    [SerializeField] private float activationScale = 1.3f;

    [Header("组件引用")]
    [Tooltip("检查点渲染器（可选，用于颜色变化）")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Tooltip("激活特效预制体（可选）")]
    [SerializeField] private GameObject activationEffect;

    #endregion

    #region 私有字段

    // 是否已被激活
    private bool isActivated = false;

    // 初始缩放
    private Vector3 initialScale;

    // 动画计时器
    private float animationTimer = 0f;

    // 是否正在播放激活动画
    private bool isAnimating = false;

    #endregion

    #region 公开属性

    /// <summary>
    /// 是否已被激活
    /// </summary>
    public bool IsActivated => isActivated;

    /// <summary>
    /// 获取重生点位置
    /// </summary>
    public Vector3 RespawnPosition => transform.position + respawnOffset;

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

        // 设置初始颜色
        UpdateVisual(false);
    }

    private void Update()
    {
        // 播放激活动画
        if (isAnimating)
        {
            PlayActivationAnimation();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查是否是玩家
        if (!other.CompareTag("Player")) return;

        // 检查是否可以触发
        if (singleUse && isActivated) return;

        // 激活检查点
        Activate(other.gameObject);
    }

    #endregion

    #region 公开方法

    /// <summary>
    /// 激活检查点
    /// </summary>
    /// <param name="player">触发玩家对象</param>
    public void Activate(GameObject player)
    {
        // 标记为已激活
        isActivated = true;

        // 获取 PlayerRespawn 组件
        PlayerRespawn respawn = player.GetComponent<PlayerRespawn>();
        if (respawn != null)
        {
            respawn.SetRespawnPoint(RespawnPosition);
            Debug.Log($"Checkpoint: 检查点 {gameObject.name} 已激活，重生点: {RespawnPosition}");
        }
        else
        {
            Debug.LogWarning("Checkpoint: 玩家没有 PlayerRespawn 组件");
        }

        // 更新视觉
        UpdateVisual(true);

        // 播放激活动画
        StartActivationAnimation();

        // 播放特效
        PlayActivationEffect();
    }

    /// <summary>
    /// 重置检查点状态
    /// </summary>
    public void ResetCheckpoint()
    {
        isActivated = false;
        isAnimating = false;
        animationTimer = 0f;
        transform.localScale = initialScale;
        UpdateVisual(false);
    }

    #endregion

    #region 私有方法

    /// <summary>
    /// 更新视觉效果
    /// </summary>
    private void UpdateVisual(bool activated)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = activated ? activeColor : inactiveColor;
        }
    }

    /// <summary>
    /// 开始激活动画
    /// </summary>
    private void StartActivationAnimation()
    {
        isAnimating = true;
        animationTimer = 0f;
    }

    /// <summary>
    /// 播放激活动画
    /// </summary>
    private void PlayActivationAnimation()
    {
        animationTimer += Time.deltaTime;
        float progress = animationTimer / activationDuration;

        if (progress >= 1f)
        {
            // 动画完成
            transform.localScale = initialScale;
            isAnimating = false;
            return;
        }

        // 缩放动画：放大然后缩回
        float scaleMultiplier;
        if (progress < 0.5f)
        {
            // 前半段放大
            scaleMultiplier = Mathf.Lerp(1f, activationScale, progress * 2f);
        }
        else
        {
            // 后半段缩小
            scaleMultiplier = Mathf.Lerp(activationScale, 1f, (progress - 0.5f) * 2f);
        }

        transform.localScale = initialScale * scaleMultiplier;
    }

    /// <summary>
    /// 播放激活特效
    /// </summary>
    private void PlayActivationEffect()
    {
        if (activationEffect == null) return;

        GameObject effect = Instantiate(activationEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
    }

    #endregion

    #region 编辑器可视化

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        // 绘制重生点位置
        Gizmos.color = Color.green;
        Vector3 respawnPos = transform.position + respawnOffset;
        Gizmos.DrawWireSphere(respawnPos, 0.3f);

        // 绘制从检查点到重生点的连线
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, respawnPos);
    }
#endif

    #endregion
}