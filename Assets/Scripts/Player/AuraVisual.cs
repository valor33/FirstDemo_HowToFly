using UnityEngine;

/// <summary>
/// 光晕视觉效果控制器
/// 根据 fly 值显示光晕效果
/// - 0-10: 无光晕（不显示）
/// - 10-20: 浅色光晕
/// - 20+: 深色光晕
/// </summary>
public class AuraVisual : MonoBehaviour
{
    #region 序列化字段

    [Header("组件引用")]
    [Tooltip("PlayerFlight 组件引用")]
    [SerializeField] private PlayerFlight playerFlight;

    [Tooltip("光晕渲染器（放在角色脚下的圆形光晕）")]
    [SerializeField] private SpriteRenderer auraRenderer;

    [Header("颜色配置")]
    [Tooltip("浅色光晕颜色")]
    [SerializeField] private Color lightAuraColor = new Color(0.5f, 0.7f, 1f, 0.4f);

    [Tooltip("深色光晕颜色")]
    [SerializeField] private Color deepAuraColor = new Color(0.3f, 0.5f, 1f, 0.6f);

    [Header("动画配置")]
    [Tooltip("状态切换动画时间")]
    [SerializeField] private float transitionDuration = 0.3f;

    [Tooltip("光晕呼吸动画速度")]
    [SerializeField] private float breatheSpeed = 2f;

    [Tooltip("光晕呼吸幅度")]
    [SerializeField] private float breatheAmplitude = 0.1f;

    #endregion

    #region 私有字段

    private AuraState currentAuraState = AuraState.None;
    private float currentTransitionProgress = 1f;
    private Color targetColor = Color.clear;
    private Color currentColor = Color.clear;

    #endregion

    #region Unity 生命周期方法

    private void Awake()
    {
        if (playerFlight == null)
        {
            playerFlight = GetComponentInParent<PlayerFlight>();
        }

        if (playerFlight == null)
        {
            Debug.LogError("AuraVisual: 缺少 PlayerFlight 组件引用！");
        }

        if (auraRenderer == null)
        {
            Debug.LogWarning("AuraVisual: 未设置光晕渲染器，光晕将不会显示。");
        }
    }

    private void Start()
    {
        SetAuraVisible(false);

        if (playerFlight != null)
        {
            playerFlight.OnAuraStateChanged += OnAuraStateChanged;
            currentAuraState = playerFlight.CurrentAuraState;
            UpdateAuraImmediate();
        }
    }

    private void OnDestroy()
    {
        if (playerFlight != null)
        {
            playerFlight.OnAuraStateChanged -= OnAuraStateChanged;
        }
    }

    private void Update()
    {
        if (auraRenderer == null) return;

        HandleTransition();

        HandleBreathing();
    }

    #endregion

    #region 私有方法

    private void OnAuraStateChanged(AuraState newState)
    {
        if (newState != currentAuraState)
        {
            currentAuraState = newState;
            currentTransitionProgress = 0f;
            CalculateTargetColor();
        }
    }

    private void HandleTransition()
    {
        if (currentTransitionProgress < 1f)
        {
            currentTransitionProgress += Time.deltaTime / transitionDuration;
            currentTransitionProgress = Mathf.Min(currentTransitionProgress, 1f);

            float easedProgress = EaseOutQuad(currentTransitionProgress);
            currentColor = Color.Lerp(Color.clear, targetColor, easedProgress);
            auraRenderer.color = currentColor;

            if (currentTransitionProgress >= 1f)
            {
                SetAuraVisible(currentAuraState != AuraState.None);
            }
        }
    }

    private void HandleBreathing()
    {
        if (currentAuraState != AuraState.None && currentTransitionProgress >= 1f)
        {
            float breathe = 1f + Mathf.Sin(Time.time * breatheSpeed) * breatheAmplitude;
            float baseAlpha = currentAuraState == AuraState.Light ? lightAuraColor.a : deepAuraColor.a;
            
            Color breathColor = currentColor;
            breathColor.a = baseAlpha * breathe;
            auraRenderer.color = breathColor;
        }
    }

    private void CalculateTargetColor()
    {
        switch (currentAuraState)
        {
            case AuraState.None:
                targetColor = Color.clear;
                break;
            case AuraState.Light:
                targetColor = lightAuraColor;
                break;
            case AuraState.Deep:
                targetColor = deepAuraColor;
                break;
        }
    }

    private void UpdateAuraImmediate()
    {
        if (auraRenderer == null) return;

        CalculateTargetColor();
        currentColor = targetColor;
        auraRenderer.color = currentColor;
        SetAuraVisible(currentAuraState != AuraState.None);
        currentTransitionProgress = 1f;
    }

    private void SetAuraVisible(bool visible)
    {
        if (auraRenderer != null)
        {
            auraRenderer.enabled = visible;
        }
    }

    private float EaseOutQuad(float t)
    {
        return 1f - (1f - t) * (1f - t);
    }

    #endregion

    #region 公开方法

    public void ResetAura()
    {
        currentAuraState = AuraState.None;
        currentTransitionProgress = 1f;
        currentColor = Color.clear;
        targetColor = Color.clear;

        SetAuraVisible(false);
        if (auraRenderer != null)
        {
            auraRenderer.color = Color.clear;
        }
    }

    public void SyncWithPlayerFlight(PlayerFlight flight)
    {
        if (flight == null || auraRenderer == null) return;

        AuraState targetState = flight.CurrentAuraState;
        
        if (targetState != currentAuraState)
        {
            currentAuraState = targetState;
            CalculateTargetColor();
            currentColor = targetColor;
            auraRenderer.color = currentColor;
            SetAuraVisible(targetState != AuraState.None);
            currentTransitionProgress = 1f;
        }
    }

    #endregion
}