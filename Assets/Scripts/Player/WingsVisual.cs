using UnityEngine;

/// <summary>
/// 翅膀视觉效果控制器
/// 根据 fly 值显示翅膀的成长过程
/// - 0-34: 无翅膀（不显示）
/// - 35-49: 未成熟翅膀（小、半透明、浅色）
/// - 50+: 成熟翅膀（大、不透明、亮色）
/// </summary>
public class WingsVisual : MonoBehaviour
{
    #region 序列化字段（在 Inspector 中可配置）

    [Header("组件引用")]
    [Tooltip("PlayerFlight 组件引用")]
    [SerializeField] private PlayerFlight playerFlight;

    [Tooltip("左翅膀渲染器")]
    [SerializeField] private SpriteRenderer leftWing;

    [Tooltip("右翅膀渲染器")]
    [SerializeField] private SpriteRenderer rightWing;

    [Header("颜色配置")]
    [Tooltip("未成熟翅膀颜色")]
    [SerializeField] private Color immatureColor = new Color(0.5f, 0.7f, 1f, 0.6f);

    [Tooltip("成熟翅膀颜色")]
    [SerializeField] private Color matureColor = new Color(0.3f, 0.6f, 1f, 1f);

    [Header("大小配置")]
    [Tooltip("未成熟翅膀缩放")]
    [SerializeField] private float immatureScale = 0.5f;

    [Tooltip("成熟翅膀缩放")]
    [SerializeField] private float matureScale = 1f;

    [Header("动画配置")]
    [Tooltip("翅膀状态切换动画时间")]
    [SerializeField] private float transitionDuration = 0.3f;

    #endregion

    #region 私有字段

    // 当前的动画过渡进度
    private float currentTransitionProgress = 1f;

    // 目标翅膀状态
    private WingState targetState = WingState.None;

    // 当前翅膀状态
    private WingState currentState = WingState.None;

    // 目标缩放
    private float targetScale = 0f;

    // 目标颜色
    private Color targetColor = Color.clear;

    // 当前成长进度（用于 Immature 状态内部渐变）
    private float currentGrowthProgress = 0f;

    #endregion

    #region Unity 生命周期方法

    private void Awake()
    {
        // 如果未指定 PlayerFlight，尝试从父对象获取
        if (playerFlight == null)
        {
            playerFlight = GetComponentInParent<PlayerFlight>();
        }

        // 检查必要组件
        if (playerFlight == null)
        {
            Debug.LogError("WingsVisual: 缺少 PlayerFlight 组件引用！");
        }

        if (leftWing == null || rightWing == null)
        {
            Debug.LogWarning("WingsVisual: 未设置翅膀渲染器，翅膀将不会显示。");
        }
    }

    private void Start()
    {
        // 初始隐藏翅膀
        SetWingsVisible(false);
        
        // 订阅翅膀状态变化事件
        if (playerFlight != null)
        {
            playerFlight.OnWingStateChanged += OnWingStateChanged;
            
            // 初始化当前状态
            currentState = playerFlight.CurrentWingState;
            targetState = currentState;
            currentGrowthProgress = playerFlight.WingGrowthProgress;
            
            // 根据初始状态设置翅膀
            UpdateWingsImmediate();
        }
    }

    private void OnDestroy()
    {
        // 取消订阅事件
        if (playerFlight != null)
        {
            playerFlight.OnWingStateChanged -= OnWingStateChanged;
        }
    }

    private void Update()
    {
        // 处理状态过渡动画
        if (currentTransitionProgress < 1f)
        {
            currentTransitionProgress += Time.deltaTime / transitionDuration;
            currentTransitionProgress = Mathf.Min(currentTransitionProgress, 1f);

            // 应用过渡效果
            ApplyTransition();
        }

        // 在 Immature 状态时，根据成长进度更新翅膀
        if (currentState == WingState.Immature && playerFlight != null)
        {
            float newProgress = playerFlight.WingGrowthProgress;
            if (Mathf.Abs(newProgress - currentGrowthProgress) > 0.01f)
            {
                currentGrowthProgress = newProgress;
                UpdateImmatureWings();
            }
        }
    }

    #endregion

    #region 私有方法

    /// <summary>
    /// 翅膀状态变化事件处理
    /// </summary>
    private void OnWingStateChanged(WingState newState, float progress)
    {
        // 如果状态改变，开始过渡动画
        if (newState != currentState)
        {
            targetState = newState;
            currentTransitionProgress = 0f;
        }

        currentGrowthProgress = progress;
    }

    /// <summary>
    /// 应用过渡动画效果
    /// </summary>
    private void ApplyTransition()
    {
        if (leftWing == null || rightWing == null) return;

        // 计算过渡参数
        float easedProgress = EaseOutQuad(currentTransitionProgress);

        // 根据目标状态计算目标值
        CalculateTargetValues(targetState);

        // 应用缩放
        float currentScale = Mathf.Lerp(0f, targetScale, easedProgress);
        ApplyScale(currentScale);

        // 应用颜色
        Color currentColor = Color.Lerp(Color.clear, targetColor, easedProgress);
        ApplyColor(currentColor);

        // 过渡完成时更新当前状态
        if (currentTransitionProgress >= 1f)
        {
            currentState = targetState;
            
            // 显示/隐藏翅膀
            SetWingsVisible(currentState != WingState.None);
        }
    }

    /// <summary>
    /// 根据状态计算目标值
    /// </summary>
    private void CalculateTargetValues(WingState state)
    {
        switch (state)
        {
            case WingState.None:
                targetScale = 0f;
                targetColor = Color.clear;
                break;

            case WingState.Immature:
                // 未成熟翅膀：大小和透明度随进度增加
                float progressScale = immatureScale + (matureScale - immatureScale) * currentGrowthProgress * 0.5f;
                targetScale = progressScale;
                targetColor = immatureColor;
                break;

            case WingState.Mature:
                targetScale = matureScale;
                targetColor = matureColor;
                break;
        }
    }

    /// <summary>
    /// 更新未成熟翅膀（根据成长进度）
    /// </summary>
    private void UpdateImmatureWings()
    {
        if (leftWing == null || rightWing == null) return;
        if (currentState != WingState.Immature) return;

        CalculateTargetValues(WingState.Immature);
        ApplyScale(targetScale);
        
        // 根据进度调整透明度
        Color color = immatureColor;
        color.a = Mathf.Lerp(0.4f, 0.8f, currentGrowthProgress);
        ApplyColor(color);
    }

    /// <summary>
    /// 立即更新翅膀状态（无动画）
    /// </summary>
    private void UpdateWingsImmediate()
    {
        if (leftWing == null || rightWing == null) return;

        CalculateTargetValues(currentState);
        ApplyScale(targetScale);
        ApplyColor(targetColor);
        SetWingsVisible(currentState != WingState.None);
        currentTransitionProgress = 1f;
    }

    /// <summary>
    /// 应用缩放到两个翅膀
    /// </summary>
    private void ApplyScale(float scale)
    {
        if (leftWing != null)
        {
            leftWing.transform.localScale = Vector3.one * scale;
        }
        if (rightWing != null)
        {
            rightWing.transform.localScale = Vector3.one * scale;
        }
    }

    /// <summary>
    /// 应用颜色到两个翅膀
    /// </summary>
    private void ApplyColor(Color color)
    {
        if (leftWing != null)
        {
            leftWing.color = color;
        }
        if (rightWing != null)
        {
            rightWing.color = color;
        }
    }

    /// <summary>
    /// 设置翅膀可见性
    /// </summary>
    private void SetWingsVisible(bool visible)
    {
        if (leftWing != null)
        {
            leftWing.enabled = visible;
        }
        if (rightWing != null)
        {
            rightWing.enabled = visible;
        }
    }

    /// <summary>
    /// 缓动函数：easeOutQuad
    /// </summary>
    private float EaseOutQuad(float t)
    {
        return 1f - (1f - t) * (1f - t);
    }

    #endregion

    #region 公开方法

    /// <summary>
    /// 重置翅膀状态
    /// 用于玩家重生等场景
    /// </summary>
    public void ResetWings()
    {
        currentState = WingState.None;
        targetState = WingState.None;
        currentGrowthProgress = 0f;
        currentTransitionProgress = 1f;

        SetWingsVisible(false);
        ApplyScale(0f);
        ApplyColor(Color.clear);
    }

    #endregion
}