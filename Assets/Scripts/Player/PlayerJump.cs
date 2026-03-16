using UnityEngine;

/// <summary>
/// 玩家跳跃控制器
/// 负责处理跳跃逻辑、fly 值的积累和空中冲刺
/// 跳跃是解锁飞行能力的主要途径
/// </summary>
public class PlayerJump : MonoBehaviour
{
    #region 序列化字段
    
    [Header("跳跃参数")]
    [Tooltip("跳跃时的向上力度")]
    [SerializeField] private float jumpForce = 10f;
    
    [Tooltip("每次跳跃获得的 fly 值")]
    [SerializeField] private float flyValuePerJump = 1f;
    
    [Header("冲刺参数")]
    [Tooltip("冲刺速度")]
    [SerializeField] private float dashSpeed = 15f;
    
    [Tooltip("冲刺持续时间")]
    [SerializeField] private float dashDuration = 0.2f;
    
    [Tooltip("冲刺消耗的 fly 值")]
    [SerializeField] private float dashFlyCost = 10f;
    
    [Header("组件引用")]
    [Tooltip("玩家控制器引用，用于获取地面状态")]
    [SerializeField] private PlayerController playerController;
    
    #endregion
    
    #region 私有字段
    
    private Rigidbody2D rb;
    private PlayerFlight playerFlight;
    private PlayerAnimation playerAnimation;
    
    private bool isDashing = false;
    private float dashTimer = 0f;
    private Vector2 dashDirection;
    private bool hasAirJumped = false;
    private bool waitingForSpaceRelease = false;
    private float originalGravityScale = 3f;
    
    #endregion
    
    #region 公开属性
    
    public float JumpForce => jumpForce;
    public bool IsDashing => isDashing;
    
    #endregion
    
    #region Unity 生命周期方法
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerFlight = GetComponent<PlayerFlight>();
        playerAnimation = GetComponent<PlayerAnimation>();
        
        if (playerController == null)
        {
            playerController = GetComponent<PlayerController>();
        }
        
        if (rb == null)
        {
            Debug.LogError("PlayerJump: 缺少 Rigidbody2D 组件！");
        }
        
        if (playerController == null)
        {
            Debug.LogError("PlayerJump: 缺少 PlayerController 组件引用！");
        }
    }
    
    private void Start()
    {
        if (playerFlight == null)
        {
            playerFlight = GetComponent<PlayerFlight>();
        }
        
        if (rb != null)
        {
            originalGravityScale = rb.gravityScale;
        }
    }
    
    private void Update()
    {
        if (playerController != null && !playerController.CanControl) return;
        
        // 检测空格松开
        if (Input.GetKeyUp(KeyCode.Space))
        {
            waitingForSpaceRelease = false;
        }
        
        // 落地时重置空中跳跃状态
        if (playerController != null && playerController.IsGrounded)
        {
            hasAirJumped = false;
            waitingForSpaceRelease = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryJump();
        }
    }
    
    private void FixedUpdate()
    {
        if (isDashing)
        {
            UpdateDash();
        }
    }
    
    #endregion
    
    #region 私有方法
    
    private void TryJump()
    {
        if (playerController == null) return;
        
        // 在地面：执行正常跳跃
        if (playerController.IsGrounded)
        {
            PerformJump();
            waitingForSpaceRelease = true;
            return;
        }
        
        // 在空中：需要等待空格松开后才能触发冲刺
        if (waitingForSpaceRelease) return;
        
        // 尝试冲刺
        TryDash();
    }
    
    private void PerformJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        AddFlyValue();
        
        Debug.Log($"PlayerJump: 执行跳跃，获得 {flyValuePerJump} fly 值。");
    }
    
    private void TryDash()
    {
        // 检查是否可以冲刺
        if (hasAirJumped) return;
        
        if (playerFlight == null || !playerFlight.IsFlightUnlocked) return;
        
        if (playerFlight.CurrentFlyValue < dashFlyCost) return;
        
        // 执行冲刺
        StartDash();
    }
    
    private void StartDash()
    {
        // 计算冲刺方向
        float h = playerController.HorizontalInput;
        float v = playerController.VerticalInput;
        
        if (h == 0 && v == 0)
        {
            dashDirection = Vector2.up;
        }
        else
        {
            dashDirection = new Vector2(h, v).normalized;
        }
        
        isDashing = true;
        dashTimer = dashDuration;
        hasAirJumped = true;
        waitingForSpaceRelease = true;
        
        // 消耗 fly 值
        playerFlight.ReduceFlyValue(dashFlyCost);
        
        // 通知动画
        if (playerAnimation != null)
        {
            playerAnimation.SetDashing(true);
        }
        
        // 禁用重力
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
        
        Debug.Log($"PlayerJump: 执行冲刺，方向: {dashDirection}，消耗 {dashFlyCost} fly 值。");
    }
    
    private void UpdateDash()
    {
        dashTimer -= Time.fixedDeltaTime;
        
        // 冲刺移动
        rb.velocity = dashDirection * dashSpeed;
        
        if (dashTimer <= 0f)
        {
            EndDash();
        }
    }
    
    private void EndDash()
    {
        isDashing = false;
        
        // 确保必须松开空格后才能再次操作
        waitingForSpaceRelease = true;
        
        // 恢复重力
        rb.gravityScale = originalGravityScale;
        
        // 通知动画
        if (playerAnimation != null)
        {
            playerAnimation.SetDashing(false);
        }
        
        Debug.Log("PlayerJump: 冲刺结束");
    }
    
    private void AddFlyValue()
    {
        if (playerFlight == null)
        {
            Debug.LogWarning("PlayerJump: PlayerFlight 组件不存在，无法增加 fly 值。");
            return;
        }
        
        playerFlight.AddFlyValue(flyValuePerJump);
    }
    
    #endregion
    
    #region 公开方法
    
    public bool Jump()
    {
        if (playerController != null && playerController.IsGrounded)
        {
            PerformJump();
            return true;
        }
        return false;
    }
    
    public void ForceJump(float force = -1f)
    {
        float jumpPower = (force < 0f) ? jumpForce : force;
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        Debug.Log($"PlayerJump: 执行强制跳跃，力度: {jumpPower}");
    }
    
    #endregion
}