using UnityEngine;

/// <summary>
/// Fly 值道具
/// 玩家收集后增加 fly 值
/// 实现 ICollectible 接口，支持扩展
/// </summary>
public class FlyValueItem : MonoBehaviour, ICollectible
{
    #region 序列化字段（在 Inspector 中可配置）

    [Header("道具参数")]
    [Tooltip("收集时增加的 fly 值")]
    [SerializeField] private float flyValueToAdd = 5f;

    [Tooltip("收集动画持续时间")]
    [SerializeField] private float collectDuration = 0.3f;

    [Tooltip("收集后是否自动销毁")]
    [SerializeField] private bool autoDestroy = true;

    [Tooltip("收集后销毁延迟时间")]
    [SerializeField] private float destroyDelay = 0.5f;

    [Header("视觉效果")]
    [Tooltip("道具旋转速度（度/秒）")]
    [SerializeField] private float rotateSpeed = 90f;

    [Tooltip("上下浮动速度")]
    [SerializeField] private float bobSpeed = 2f;

    [Tooltip("上下浮动幅度")]
    [SerializeField] private float bobAmount = 0.1f;

    [Header("组件引用")]
    [Tooltip("精灵渲染器（可选，用于淡出效果）")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    #endregion

    #region 私有字段

    // 是否已被收集
    private bool isCollected = false;

    // 初始位置（用于浮动效果）
    private Vector3 initialPosition;

    // 浮动计时器
    private float bobTimer = 0f;

    // 收集动画计时器
    private float collectTimer = 0f;

    // 初始缩放
    private Vector3 initialScale;

    // 初始颜色
    private Color initialColor;

    #endregion

    #region 公开属性

    /// <summary>
    /// 是否可以被收集
    /// </summary>
    public bool CanBeCollected => !isCollected;

    #endregion

    #region Unity 生命周期方法

    private void Awake()
    {
        // 记录初始位置
        initialPosition = transform.position;
        initialScale = transform.localScale;

        // 如果未指定 SpriteRenderer，尝试获取
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // 记录初始颜色
        if (spriteRenderer != null)
        {
            initialColor = spriteRenderer.color;
        }
    }

    private void Update()
    {
        // 如果已被收集，播放收集动画
        if (isCollected)
        {
            PlayCollectAnimation();
            return;
        }

        // 播放浮动动画
        PlayBobAnimation();

        // 播放旋转动画
        PlayRotateAnimation();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查是否可以被收集
        if (!CanBeCollected) return;

        // 检查碰撞对象是否是玩家
        if (other.CompareTag("Player"))
        {
            OnCollected(other.gameObject);
        }
    }

    #endregion

    #region ICollectible 接口实现

    /// <summary>
    /// 当道具被收集时调用
    /// </summary>
    /// <param name="collector">收集者对象</param>
    public void OnCollected(GameObject collector)
    {
        // 检查是否可以被收集
        if (!CanBeCollected) return;

        // 标记为已收集
        isCollected = true;

        // 获取 PlayerFlight 组件并增加 fly 值
        PlayerFlight playerFlight = collector.GetComponent<PlayerFlight>();
        if (playerFlight != null)
        {
            playerFlight.AddFlyValue(flyValueToAdd);
            Debug.Log($"FlyValueItem: 玩家收集道具，获得 {flyValueToAdd} fly 值");
        }
        else
        {
            Debug.LogWarning("FlyValueItem: 收集者没有 PlayerFlight 组件");
        }

        // 禁用碰撞器，防止重复收集
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // 开始收集动画
        collectTimer = 0f;
    }

    #endregion

    #region 私有方法

    /// <summary>
    /// 播放浮动动画
    /// </summary>
    private void PlayBobAnimation()
    {
        bobTimer += Time.deltaTime * bobSpeed;
        float yOffset = Mathf.Sin(bobTimer) * bobAmount;
        transform.position = initialPosition + new Vector3(0f, yOffset, 0f);
    }

    /// <summary>
    /// 播放旋转动画
    /// </summary>
    private void PlayRotateAnimation()
    {
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 播放收集动画
    /// </summary>
    private void PlayCollectAnimation()
    {
        collectTimer += Time.deltaTime;
        float progress = collectTimer / collectDuration;

        if (progress >= 1f)
        {
            // 动画完成
            CompleteCollection();
            return;
        }

        // 缩放动画：从小到大再消失
        float scaleProgress = 1f - progress;
        Vector3 newScale = initialScale * scaleProgress;
        transform.localScale = newScale;

        // 淡出动画
        if (spriteRenderer != null)
        {
            Color newColor = initialColor;
            newColor.a = 1f - progress;
            spriteRenderer.color = newColor;
        }

        // 向上移动效果
        float yOffset = progress * 0.5f;
        transform.position = initialPosition + new Vector3(0f, yOffset, 0f);
    }

    /// <summary>
    /// 完成收集
    /// </summary>
    private void CompleteCollection()
    {
        if (autoDestroy)
        {
            Destroy(gameObject, destroyDelay);
        }
        else
        {
            // 禁用对象而不是销毁
            gameObject.SetActive(false);
        }
    }

    #endregion

    #region 公开方法

    /// <summary>
    /// 重置道具状态
    /// 用于对象池复用
    /// </summary>
    public void ResetItem()
    {
        isCollected = false;
        bobTimer = 0f;
        collectTimer = 0f;
        transform.position = initialPosition;
        transform.localScale = initialScale;
        transform.rotation = Quaternion.identity;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = initialColor;
        }

        // 启用碰撞器
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = true;
        }
    }

    /// <summary>
    /// 设置初始位置（用于动态生成的道具）
    /// </summary>
    /// <param name="position">新的初始位置</param>
    public void SetInitialPosition(Vector3 position)
    {
        initialPosition = position;
        transform.position = position;
    }

    /// <summary>
    /// 设置 fly 值（用于动态配置道具）
    /// </summary>
    /// <param name="value">增加的 fly 值</param>
    public void SetFlyValue(float value)
    {
        flyValueToAdd = value;
    }

    #endregion
}