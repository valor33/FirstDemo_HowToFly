using UnityEngine;

/// <summary>
/// 翅膀状态枚举
/// </summary>
public enum WingState
{
    None,       // 无翅膀 (0 - firstThreshold)
    Immature,   // 未成熟翅膀 (firstThreshold - unlockThreshold)
    Mature      // 成熟翅膀 (unlockThreshold+)
}

/// <summary>
/// 翅膀状态变化事件委托
/// </summary>
/// <param name="newState">新的翅膀状态</param>
/// <param name="progress">翅膀成长进度 (0-1)</param>
public delegate void WingStateChangedHandler(WingState newState, float progress);

/// <summary>
/// 玩家飞行控制器
/// 负责管理 fly 值系统和飞行能力
/// 玩家通过跳跃积累 fly 值，达到阈值后解锁飞行能力
/// </summary>
public class PlayerFlight : MonoBehaviour
{
    #region 序列化字段（在 Inspector 中可配置）
    
    [Header("Fly 值参数")]
    [Tooltip("fly 值的最大值")]
    [SerializeField] private float maxFlyValue = 100f;
    
    [Tooltip("解锁飞行能力所需的 fly 值阈值")]
    [SerializeField] private float unlockThreshold = 50f;
    
    [Tooltip("翅膀出现的第一阶段阈值")]
    [SerializeField] private float firstThreshold = 35f;
    
    [Header("飞行参数")]
    [Tooltip("飞行时的水平移动速度")]
    [SerializeField] private float flySpeed = 8f;
    
    [Tooltip("每秒飞行消耗的 fly 值")]
    [SerializeField] private float flyCostPerSecond = 5f;
    
    [Tooltip("飞行时的向上速度")]
    [SerializeField] private float flyUpSpeed = 6f;
    
    [Tooltip("正常重力值")]
    [SerializeField] private float normalGravityScale = 3f;
    
    [Header("作弊调试")]
    [Tooltip("作弊键每次增加的 fly 值")]
    [SerializeField] private float cheatAddValue = 25f;
    
    #endregion
    
    #region 私有字段
    
    // 当前的 fly 值
    private float currentFlyValue = 0f;
    
    // 飞行能力是否已解锁
    private bool isFlightUnlocked = false;
    
    // 当前是否正在飞行
    private bool isFlying = false;
    
    // 用于防止跳跃后立即触发飞行
    // 跳跃后需要松开空格再按住才能飞行
    private bool waitingForSpaceRelease = false;
    
    // 刚体组件引用
    private Rigidbody2D rb;
    
    // PlayerController 引用
    private PlayerController playerController;
    
    // PlayerAnimation 引用
    private PlayerAnimation playerAnimation;
    
    // 上一次的翅膀状态（用于检测状态变化）
    private WingState lastWingState = WingState.None;
    
    #endregion
    
    #region 公开事件
    
    /// <summary>
    /// 翅膀状态变化事件
    /// 当翅膀状态改变时触发
    /// </summary>
    public event WingStateChangedHandler OnWingStateChanged;
    
    #endregion
    
    #region 公开属性
    
    /// <summary>
    /// 获取当前 fly 值
    /// </summary>
    public float CurrentFlyValue => currentFlyValue;
    
    /// <summary>
    /// 获取最大 fly 值
    /// </summary>
    public float MaxFlyValue => maxFlyValue;
    
    /// <summary>
    /// 飞行能力是否已解锁
    /// </summary>
    public bool IsFlightUnlocked => isFlightUnlocked;
    
    /// <summary>
    /// 当前是否正在飞行
    /// </summary>
    public bool IsFlying => isFlying;
    
    /// <summary>
    /// 获取 fly 值的百分比（0-1）
    /// </summary>
    public float FlyValuePercent => maxFlyValue > 0f ? currentFlyValue / maxFlyValue : 0f;
    
    /// <summary>
    /// 获取解锁进度百分比（0-1）
    /// </summary>
    public float UnlockProgress => unlockThreshold > 0f ? currentFlyValue / unlockThreshold : 0f;
    
    /// <summary>
    /// 获取第一阶段阈值
    /// </summary>
    public float FirstThreshold => firstThreshold;
    
    /// <summary>
    /// 获取当前翅膀状态
    /// </summary>
    public WingState CurrentWingState
    {
        get
        {
            if (currentFlyValue < firstThreshold)
                return WingState.None;
            else if (currentFlyValue < unlockThreshold)
                return WingState.Immature;
            else
                return WingState.Mature;
        }
    }
    
    /// <summary>
    /// 获取翅膀成长进度（用于 WingsVisual 计算视觉效果）
    /// - None 阶段：0 - firstThreshold 的进度 (0-1)
    /// - Immature 阶段：firstThreshold - unlockThreshold 的进度 (0-1)
    /// - Mature 阶段：固定返回 1
    /// </summary>
    public float WingGrowthProgress
    {
        get
        {
            if (currentFlyValue < firstThreshold)
            {
                return 0f;
            }
            else if (currentFlyValue < unlockThreshold)
            {
                float range = unlockThreshold - firstThreshold;
                return range > 0f ? (currentFlyValue - firstThreshold) / range : 0f;
            }
            else
            {
                return 1f;
            }
        }
    }
    
    #endregion
    
    #region Unity 生命周期方法
    
    private void Awake()
    {
        // 获取组件引用
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
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            AddFlyValue(cheatAddValue);
            Debug.Log($"PlayerFlight: 作弊增加 {cheatAddValue} fly 值");
        }
        
        if (!isFlightUnlocked) return;
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            waitingForSpaceRelease = false;
        }
        
        HandleFlightInput();
    }
    
    private void FixedUpdate()
    {
        if (playerController != null && !playerController.CanControl)
        {
            if (isFlying) StopFlying();
            return;
        }
        
        if (isFlying)
        {
            HandleFlightMovement();
        }
    }
    
    #endregion
    
    #region 公开方法
    
    /// <summary>
    /// 增加 fly 值
    /// 由 PlayerJump 在每次跳跃时调用
    /// </summary>
    /// <param name="amount">增加的数量</param>
    public void AddFlyValue(float amount)
    {
        // 增加 fly 值，但不能超过最大值
        currentFlyValue = Mathf.Min(currentFlyValue + amount, maxFlyValue);
        
        // 输出调试信息
        Debug.Log($"PlayerFlight: fly 值增加 {amount}，当前: {currentFlyValue}/{maxFlyValue}");
        
        // 检查是否达到解锁阈值
        CheckUnlock();
        
        // 检查翅膀状态变化
        CheckWingState();
    }
    
    /// <summary>
    /// 减少 fly 值
    /// 飞行时消耗 fly 值
    /// </summary>
    /// <param name="amount">减少的数量</param>
    public void ReduceFlyValue(float amount)
    {
        // 减少 fly 值，但不能小于 0
        currentFlyValue = Mathf.Max(currentFlyValue - amount, 0f);
        
        // 如果 fly 值耗尽，停止飞行
        if (currentFlyValue <= 0f && isFlying)
        {
            StopFlying();
        }
        
        // 检查翅膀状态变化
        CheckWingState();
    }
    
    /// <summary>
    /// 设置 fly 值（用于测试或特殊道具）
    /// </summary>
    /// <param name="value">目标值</param>
    public void SetFlyValue(float value)
    {
        currentFlyValue = Mathf.Clamp(value, 0f, maxFlyValue);
        CheckUnlock();
    }
    
    /// <summary>
    /// 手动解锁飞行能力（用于特殊解锁方式）
    /// </summary>
    public void UnlockFlight()
    {
        if (!isFlightUnlocked)
        {
            isFlightUnlocked = true;
            Debug.Log("PlayerFlight: 飞行能力已解锁！");
            
            // 通知动画组件
            if (playerAnimation != null)
            {
                playerAnimation.OnFlightUnlocked();
            }
        }
    }
    
    #endregion
    
    #region 私有方法
    
    /// <summary>
    /// 检查是否达到解锁飞行的条件
    /// </summary>
    private void CheckUnlock()
    {
        // 如果已经解锁，不再重复检查
        if (isFlightUnlocked)
        {
            return;
        }
        
        // 检查是否达到解锁阈值
        if (currentFlyValue >= unlockThreshold)
        {
            UnlockFlight();
        }
    }
    
    /// <summary>
    /// 检查翅膀状态是否发生变化
    /// 如果状态改变，触发 OnWingStateChanged 事件
    /// </summary>
    private void CheckWingState()
    {
        WingState currentState = CurrentWingState;
        
        // 如果状态发生变化
        if (currentState != lastWingState)
        {
            lastWingState = currentState;
            
            // 触发事件
            OnWingStateChanged?.Invoke(currentState, WingGrowthProgress);
            
            Debug.Log($"PlayerFlight: 翅膀状态变化 -> {currentState}，进度: {WingGrowthProgress:P0}");
        }
    }
    
    /// <summary>
    /// 处理飞行输入检测
    /// </summary>
    private void HandleFlightInput()
    {
        // 地面时，不触发飞行，等待跳跃
        // 并且标记需要等待空格松开后才能飞行
        if (IsPlayerGrounded())
        {
            // 在地面时，如果正在飞行则停止
            if (isFlying)
            {
                StopFlying();
            }
            // 标记等待空格松开（用于防止跳跃后立即飞行）
            if (Input.GetKey(KeyCode.Space))
            {
                waitingForSpaceRelease = true;
            }
            return;
        }
        
        // 空中时，检测是否可以飞行
        // 条件：飞行已解锁 + 按住空格 + 不在等待松开状态 + 有 fly 值
        if (Input.GetKey(KeyCode.Space) && !waitingForSpaceRelease && currentFlyValue > 0f)
        {
            if (!isFlying)
            {
                StartFlying();
            }
        }
        else
        {
            // 松开空格或 fly 值耗尽，停止飞行
            if (isFlying)
            {
                StopFlying();
            }
        }
    }
    
    /// <summary>
    /// 开始飞行
    /// </summary>
    private void StartFlying()
    {
        isFlying = true;
        
        // 通知动画组件
        if (playerAnimation != null)
        {
            playerAnimation.SetFlying(true);
        }
        
        Debug.Log("PlayerFlight: 开始飞行");
    }
    
    /// <summary>
    /// 停止飞行
    /// </summary>
    private void StopFlying()
    {
        isFlying = false;
        
        // 恢复正常重力
        if (rb != null)
        {
            rb.gravityScale = normalGravityScale;
        }
        
        // 通知动画组件
        if (playerAnimation != null)
        {
            playerAnimation.SetFlying(false);
        }
        
        Debug.Log("PlayerFlight: 停止飞行");
    }
    
    /// <summary>
    /// 处理飞行时的移动
    /// </summary>
    private void HandleFlightMovement()
    {
        // 消耗 fly 值
        float cost = flyCostPerSecond * Time.fixedDeltaTime;
        ReduceFlyValue(cost);
        
        // 如果 fly 值耗尽，停止飞行
        if (currentFlyValue <= 0f)
        {
            return;
        }
        
        // 获取水平输入
        float horizontalInput = Input.GetAxis("Horizontal");
        
        // 计算飞行速度
        // 水平方向：根据输入移动
        // 垂直方向：按住空格向上飞
        float velocityX = horizontalInput * flySpeed;
        float velocityY = flyUpSpeed;
        
        // 设置刚体速度
        rb.velocity = new Vector2(velocityX, velocityY);
        
        // 设置较小的重力，使飞行更加平稳
        rb.gravityScale = 0.5f;
    }
    
    /// <summary>
    /// 检查玩家是否在地面
    /// </summary>
    private bool IsPlayerGrounded()
    {
        if (playerController != null)
        {
            return playerController.IsGrounded;
        }
        return false;
    }
    
    #endregion
}