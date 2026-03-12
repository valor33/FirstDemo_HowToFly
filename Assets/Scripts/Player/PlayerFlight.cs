using UnityEngine;

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
    
    [Header("飞行参数")]
    [Tooltip("飞行时的移动速度")]
    [SerializeField] private float flySpeed = 8f;
    
    [Tooltip("每秒飞行消耗的 fly 值")]
    [SerializeField] private float flyCostPerSecond = 5f;
    
    [Tooltip("飞行时的垂直移动速度")]
    [SerializeField] private float flyVerticalSpeed = 6f;
    
    #endregion
    
    #region 私有字段
    
    // 当前的 fly 值
    private float currentFlyValue = 0f;
    
    // 飞行能力是否已解锁
    private bool isFlightUnlocked = false;
    
    // 当前是否正在飞行
    private bool isFlying = false;
    
    // 刚体组件引用
    private Rigidbody2D rb;
    
    // PlayerController 引用
    private PlayerController playerController;
    
    // PlayerAnimation 引用
    private PlayerAnimation playerAnimation;
    
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
        // 飞行能力未解锁时不处理飞行逻辑
        if (!isFlightUnlocked)
        {
            return;
        }
        
        // 检测飞行输入
        HandleFlightInput();
    }
    
    private void FixedUpdate()
    {
        // 如果正在飞行，处理飞行移动
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
    /// 处理飞行输入检测
    /// </summary>
    private void HandleFlightInput()
    {
        // 按住空格键在空中时进入飞行状态
        // 注意：需要确保不是在地面（地面上应该触发跳跃）
        if (Input.GetKey(KeyCode.Space) && !IsPlayerGrounded())
        {
            if (!isFlying && currentFlyValue > 0f)
            {
                StartFlying();
            }
        }
        else
        {
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
        
        // 获取垂直输入（上下移动）
        float verticalInput = Input.GetAxis("Vertical");
        
        // 计算飞行速度
        float velocityX = horizontalInput * flySpeed;
        float velocityY = verticalInput * flyVerticalSpeed;
        
        // 设置刚体速度
        rb.velocity = new Vector2(velocityX, velocityY);
        
        // 减小重力影响，使飞行更加平稳
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