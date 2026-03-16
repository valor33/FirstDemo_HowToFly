using UnityEngine;

/// <summary>
/// 光晕状态枚举
/// </summary>
public enum AuraState
{
    None,   // 无光晕 (0 - auraThreshold1)
    Light,  // 浅色光晕 (auraThreshold1 - auraThreshold2)
    Deep    // 深色光晕 (auraThreshold2+)
}

/// <summary>
/// 光晕状态变化事件委托
/// </summary>
/// <param name="newState">新的光晕状态</param>
public delegate void AuraStateChangedHandler(AuraState newState);

/// <summary>
/// 玩家飞行控制器
/// 负责管理 fly 值系统和冲刺能力解锁
/// 玩家通过跳跃积累 fly 值，达到阈值后解锁冲刺能力
/// </summary>
public class PlayerFlight : MonoBehaviour
{
    #region 序列化字段
    
    [Header("Fly 值参数")]
    [Tooltip("fly 值的最大值")]
    [SerializeField] private float maxFlyValue = 100f;
    
    [Tooltip("解锁冲刺能力所需的 fly 值阈值")]
    [SerializeField] private float unlockThreshold = 50f;
    
    [Tooltip("浅色光晕阈值")]
    [SerializeField] private float auraThreshold1 = 10f;
    
    [Tooltip("深色光晕阈值")]
    [SerializeField] private float auraThreshold2 = 20f;
    
    [Header("作弊调试")]
    [Tooltip("作弊键每次增加的 fly 值")]
    [SerializeField] private float cheatAddValue = 25f;
    
    #endregion
    
    #region 私有字段
    
    private float currentFlyValue = 0f;
    private bool isFlightUnlocked = false;
    private bool hasUnlockedWings = false;
    
    private Rigidbody2D rb;
    private PlayerController playerController;
    private PlayerAnimation playerAnimation;
    
    private AuraState lastAuraState = AuraState.None;
    
    #endregion
    
    #region 公开事件
    
    /// <summary>
    /// 光晕状态变化事件
    /// </summary>
    public event AuraStateChangedHandler OnAuraStateChanged;
    
    /// <summary>
    /// 翅膀解锁事件（一次性触发）
    /// </summary>
    public event System.Action OnWingsUnlocked;
    
    #endregion
    
    #region 公开属性
    
    public float CurrentFlyValue => currentFlyValue;
    public float MaxFlyValue => maxFlyValue;
    public bool IsFlightUnlocked => isFlightUnlocked;
    public bool HasUnlockedWings => hasUnlockedWings;
    public float FlyValuePercent => maxFlyValue > 0f ? currentFlyValue / maxFlyValue : 0f;
    public float UnlockProgress => unlockThreshold > 0f ? currentFlyValue / unlockThreshold : 0f;
    
    /// <summary>
    /// 获取当前光晕状态
    /// </summary>
    public AuraState CurrentAuraState
    {
        get
        {
            if (currentFlyValue < auraThreshold1)
                return AuraState.None;
            else if (currentFlyValue < auraThreshold2)
                return AuraState.Light;
            else
                return AuraState.Deep;
        }
    }
    
    /// <summary>
    /// 获取可冲刺次数
    /// </summary>
    public int DashCount => Mathf.FloorToInt(currentFlyValue / 10f);
    
    #endregion
    
    #region Unity 生命周期方法
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        playerAnimation = GetComponent<PlayerAnimation>();
        
        if (rb == null)
        {
            Debug.LogError("PlayerFlight: 缺少 Rigidbody2D 组件！");
        }
    }
    
    private void Update()
    {
        if (playerController != null && !playerController.CanControl) return;
        
        // 作弊键
        if (Input.GetKeyDown(KeyCode.P))
        {
            AddFlyValue(cheatAddValue);
            Debug.Log($"PlayerFlight: 作弊增加 {cheatAddValue} fly 值");
        }
        
        // 飞行逻辑已禁用，由 PlayerJump 处理冲刺
    }
    
    private void FixedUpdate()
    {
        // 飞行逻辑已禁用
    }
    
    #endregion
    
    #region 公开方法
    
    /// <summary>
    /// 增加 fly 值
    /// </summary>
    public void AddFlyValue(float amount)
    {
        currentFlyValue = Mathf.Min(currentFlyValue + amount, maxFlyValue);
        
        Debug.Log($"PlayerFlight: fly 值增加 {amount}，当前: {currentFlyValue}/{maxFlyValue}");
        
        CheckUnlock();
        CheckAuraState();
    }
    
    /// <summary>
    /// 减少 fly 值
    /// </summary>
    public void ReduceFlyValue(float amount)
    {
        currentFlyValue = Mathf.Max(currentFlyValue - amount, 0f);
        
        CheckAuraState();
    }
    
    /// <summary>
    /// 设置 fly 值
    /// </summary>
    public void SetFlyValue(float value)
    {
        currentFlyValue = Mathf.Clamp(value, 0f, maxFlyValue);
        CheckUnlock();
        CheckAuraState();
    }
    
    /// <summary>
    /// 解锁冲刺能力
    /// </summary>
    public void UnlockFlight()
    {
        if (!isFlightUnlocked)
        {
            isFlightUnlocked = true;
            Debug.Log("PlayerFlight: 冲刺能力已解锁！");
            
            // 触发翅膀解锁事件（一次性）
            if (!hasUnlockedWings)
            {
                hasUnlockedWings = true;
                OnWingsUnlocked?.Invoke();
                Debug.Log("PlayerFlight: 翅膀解锁！");
                
                if (playerAnimation != null)
                {
                    playerAnimation.OnWingsUnlocked();
                }
            }
        }
    }
    
    #endregion
    
    #region 私有方法
    
    private void CheckUnlock()
    {
        if (isFlightUnlocked) return;
        
        if (currentFlyValue >= unlockThreshold)
        {
            UnlockFlight();
        }
    }
    
    private void CheckAuraState()
    {
        AuraState currentState = CurrentAuraState;
        
        if (currentState != lastAuraState)
        {
            lastAuraState = currentState;
            OnAuraStateChanged?.Invoke(currentState);
            Debug.Log($"PlayerFlight: 光晕状态变化 -> {currentState}");
        }
    }
    
    #endregion
}