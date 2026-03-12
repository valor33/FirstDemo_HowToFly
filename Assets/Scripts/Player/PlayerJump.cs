using UnityEngine;

/// <summary>
/// 玩家跳跃控制器
/// 负责处理跳跃逻辑和 fly 值的积累
/// 跳跃是解锁飞行能力的主要途径
/// </summary>
public class PlayerJump : MonoBehaviour
{
    #region 序列化字段（在 Inspector 中可配置）
    
    [Header("跳跃参数")]
    [Tooltip("跳跃时的向上力度")]
    [SerializeField] private float jumpForce = 10f;
    
    [Tooltip("每次跳跃获得的 fly 值")]
    [SerializeField] private float flyValuePerJump = 1f;
    
    [Header("组件引用")]
    [Tooltip("玩家控制器引用，用于获取地面状态")]
    [SerializeField] private PlayerController playerController;
    
    #endregion
    
    #region 私有字段
    
    // 玩家刚体组件引用
    private Rigidbody2D rb;
    
    // PlayerFlight 组件引用，用于增加 fly 值
    private PlayerFlight playerFlight;
    
    // PlayerAnimation 组件引用，用于播放跳跃动画
    private PlayerAnimation playerAnimation;
    
    #endregion
    
    #region 公开属性
    
    /// <summary>
    /// 获取当前的跳跃力度
    /// </summary>
    public float JumpForce => jumpForce;
    
    #endregion
    
    #region Unity 生命周期方法
    
    /// <summary>
    /// 初始化时获取组件引用
    /// </summary>
    private void Awake()
    {
        // 获取刚体组件
        rb = GetComponent<Rigidbody2D>();
        
        // 尝试从同一对象获取 PlayerFlight 组件
        // 如果不存在，会在 Start 中继续查找
        playerFlight = GetComponent<PlayerFlight>();
        
        // 尝试获取 PlayerAnimation 组件
        playerAnimation = GetComponent<PlayerAnimation>();
        
        // 如果 PlayerController 未在 Inspector 中赋值，尝试自动获取
        if (playerController == null)
        {
            playerController = GetComponent<PlayerController>();
        }
        
        // 检查必要组件
        if (rb == null)
        {
            Debug.LogError("PlayerJump: 缺少 Rigidbody2D 组件！");
        }
        
        if (playerController == null)
        {
            Debug.LogError("PlayerJump: 缺少 PlayerController 组件引用！");
        }
    }
    
    /// <summary>
    /// 在 Start 中再次尝试获取 PlayerFlight
    /// 因为 PlayerFlight 可能是另一个脚本，初始化顺序可能不同
    /// </summary>
    private void Start()
    {
        // 如果 Awake 中没有找到 PlayerFlight，再次尝试获取
        if (playerFlight == null)
        {
            playerFlight = GetComponent<PlayerFlight>();
        }
    }
    
    /// <summary>
    /// 每帧更新，检测跳跃输入
    /// </summary>
    private void Update()
    {
        // 检测跳跃输入（空格键按下瞬间）
        // 使用 GetKeyDown 而不是 GetKey，确保每次按键只触发一次跳跃
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 尝试执行跳跃
            TryJump();
        }
    }
    
    #endregion
    
    #region 私有方法
    
    /// <summary>
    /// 尝试执行跳跃
    /// 只有在地面时才能跳跃
    /// </summary>
    private void TryJump()
    {
        // 检查玩家控制器是否存在
        if (playerController == null)
        {
            Debug.LogWarning("PlayerJump: 无法执行跳跃，缺少 PlayerController 引用。");
            return;
        }
        
        // 只有在地面上才能跳跃
        // isGrounded 由 PlayerController 通过射线检测维护
        if (!playerController.IsGrounded)
        {
            // 不在地面上，无法跳跃
            // 可以在这里添加额外逻辑，比如二段跳
            return;
        }
        
        // 执行跳跃
        PerformJump();
    }
    
    /// <summary>
    /// 执行跳跃动作
    /// 设置垂直速度为跳跃力度，并触发相关事件
    /// </summary>
    private void PerformJump()
    {
        // 设置刚体的垂直速度为跳跃力度
        // 保持水平速度不变，只改变垂直速度
        // 这样可以保持跳跃时的水平移动
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        
        // 增加 fly 值
        // 每次跳跃都会积累 fly 值，是解锁飞行的主要途径
        AddFlyValue();
        
        // 触发跳跃动画效果
        TriggerJumpAnimation();
        
        // 输出调试信息
        Debug.Log($"PlayerJump: 执行跳跃，获得 {flyValuePerJump} fly 值。");
    }
    
    /// <summary>
    /// 增加玩家的 fly 值
    /// 通过跳跃积累 fly 值，达到阈值后可解锁飞行能力
    /// </summary>
    private void AddFlyValue()
    {
        // 检查 PlayerFlight 组件是否存在
        if (playerFlight == null)
        {
            Debug.LogWarning("PlayerJump: PlayerFlight 组件不存在，无法增加 fly 值。");
            return;
        }
        
        // 调用 PlayerFlight 的增加 fly 值方法
        playerFlight.AddFlyValue(flyValuePerJump);
    }
    
    /// <summary>
    /// 触发跳跃动画效果
    /// 通知 PlayerAnimation 组件播放跳跃状态
    /// </summary>
    private void TriggerJumpAnimation()
    {
        // 检查 PlayerAnimation 组件是否存在
        if (playerAnimation == null)
        {
            // 如果没有动画组件，跳过动画逻辑
            // 这是正常的，可能游戏中暂时没有动画系统
            return;
        }
        
        // 通知动画组件进入跳跃状态
        playerAnimation.SetJumping(true);
    }
    
    #endregion
    
    #region 公开方法
    
    /// <summary>
    /// 外部调用的跳跃方法
    /// 可由其他脚本或输入系统调用
    /// </summary>
    /// <returns>如果成功跳跃返回 true，否则返回 false</returns>
    public bool Jump()
    {
        // 只有在地面时才能跳跃
        if (playerController != null && playerController.IsGrounded)
        {
            PerformJump();
            return true;
        }
        return false;
    }
    
    /// <summary>
    /// 强制跳跃（忽略地面检测）
    /// 用于特殊场景，如二段跳、弹跳平台等
    /// 注意：强制跳跃不会增加 fly 值
    /// </summary>
    /// <param name="force">可选的自定义跳跃力度，默认使用 jumpForce</param>
    public void ForceJump(float force = -1f)
    {
        // 如果未指定力度，使用默认跳跃力度
        float jumpPower = (force < 0f) ? jumpForce : force;
        
        // 设置垂直速度
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        
        // 触发跳跃动画
        TriggerJumpAnimation();
        
        Debug.Log($"PlayerJump: 执行强制跳跃，力度: {jumpPower}");
    }
    
    #endregion
}