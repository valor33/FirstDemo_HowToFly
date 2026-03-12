using UnityEngine;

/// <summary>
/// 玩家主控制器
/// 负责处理玩家的移动、翻转、地面检测等核心功能
/// 作为其他玩家组件的协调中心
/// </summary>
public class PlayerController : MonoBehaviour
{
    #region 序列化字段（在 Inspector 中可配置）
    
    [Header("移动参数")]
    [Tooltip("玩家水平移动速度")]
    [SerializeField] private float moveSpeed = 5f;
    
    [Header("地面检测参数")]
    [Tooltip("地面层级，用于射线检测")]
    [SerializeField] private LayerMask groundLayer;
    
    [Tooltip("地面检测盒子的尺寸")]
    [SerializeField] private Vector2 groundCheckBoxSize = new Vector2(0.9f, 0.1f);
    
    [Tooltip("地面检测点的偏移量（相对于玩家底部）")]
    [SerializeField] private float groundCheckOffset = 0.5f;
    
    #endregion
    
    #region 私有字段
    
    // 刚体组件引用，用于控制物理移动
    private Rigidbody2D rb;
    
    // 玩家渲染器，用于翻转显示
    private SpriteRenderer spriteRenderer;
    
    // 当前水平输入值（-1 到 1）
    private float horizontalInput;
    
    // 当前是否在地面上的状态
    private bool isGrounded;
    
    #endregion
    
    #region 公开属性（供其他脚本访问）
    
    /// <summary>
    /// 获取当前玩家是否在地面
    /// </summary>
    public bool IsGrounded => isGrounded;
    
    /// <summary>
    /// 获取当前水平移动方向（-1: 左, 0: 静止, 1: 右）
    /// </summary>
    public float HorizontalInput => horizontalInput;
    
    /// <summary>
    /// 获取玩家刚体组件
    /// </summary>
    public Rigidbody2D Rb => rb;
    
    #endregion
    
    #region Unity 生命周期方法
    
    /// <summary>
    /// 初始化时调用，获取组件引用
    /// </summary>
    private void Awake()
    {
        // 获取必要的组件
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // 检查组件是否存在
        if (rb == null)
        {
            Debug.LogError("PlayerController: 缺少 Rigidbody2D 组件！");
        }
        
        if (spriteRenderer == null)
        {
            Debug.LogWarning("PlayerController: 缺少 SpriteRenderer 组件，翻转功能将不可用。");
        }
    }
    
    /// <summary>
    /// 每帧更新，处理输入
    /// 注意：输入处理放在 Update 中，物理操作放在 FixedUpdate 中
    /// </summary>
    private void Update()
    {
        // 获取水平输入（A/D 或 左/右 方向键）
        horizontalInput = Input.GetAxis("Horizontal");
        
        // 处理角色翻转
        HandleFlip();
    }
    
    /// <summary>
    /// 固定时间间隔更新，处理物理相关操作
    /// </summary>
    private void FixedUpdate()
    {
        // 处理水平移动
        HandleMovement();
        
        // 检测是否在地面
        CheckGrounded();
    }
    
    #endregion
    
    #region 私有方法
    
    /// <summary>
    /// 处理玩家水平移动
    /// 使用刚体的速度控制，保持原有的 Y 轴速度不变
    /// </summary>
    private void HandleMovement()
    {
        // 计算新的水平速度，保持原有的垂直速度
        // horizontalInput * moveSpeed 计算目标水平速度
        // rb.velocity.y 保持当前的垂直速度（如跳跃、下落）
        float targetVelocityX = horizontalInput * moveSpeed;
        rb.velocity = new Vector2(targetVelocityX, rb.velocity.y);
    }
    
    /// <summary>
    /// 处理角色朝向翻转
    /// 根据移动方向翻转精灵的朝向
    /// </summary>
    private void HandleFlip()
    {
        // 如果没有 SpriteRenderer，跳过翻转逻辑
        if (spriteRenderer == null) return;
        
        // 向右移动时，面朝右（localScale.x 为正）
        if (horizontalInput > 0.01f)
        {
            spriteRenderer.flipX = false;
        }
        // 向左移动时，面朝左（翻转精灵）
        else if (horizontalInput < -0.01f)
        {
            spriteRenderer.flipX = true;
        }
        // 当 horizontalInput 接近 0 时不做任何改变，保持当前朝向
    }
    
    /// <summary>
    /// 检测玩家是否站在地面上
    /// 使用 BoxCast（盒状射线）检测玩家底部是否有地面
    /// </summary>
    private void CheckGrounded()
    {
        // 计算检测起始位置：玩家底部中心点
        // transform.position 是玩家中心点，向下偏移得到底部位置
        Vector2 checkPosition = (Vector2)transform.position - new Vector2(0f, groundCheckOffset);
        
        // 使用 BoxCast 进行盒状射线检测
        // 参数说明：
        // - origin: 射线起始位置
        // - size: 检测盒子的尺寸
        // - angle: 旋转角度（0表示不旋转）
        // - direction: 射线方向（向下）
        // - distance: 检测距离
        // - layerMask: 检测的图层
        isGrounded = Physics2D.BoxCast(
            checkPosition,           // 检测位置（玩家底部）
            groundCheckBoxSize,       // 检测盒子大小
            0f,                       // 不旋转
            Vector2.down,             // 向下检测
            0.1f,                     // 检测距离
            groundLayer               // 只检测地面层
        );
    }
    
    /// <summary>
    /// 在编辑器中绘制地面检测范围（仅在 Scene 视图中可见）
    /// 方便调试和调整参数
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        // 设置绘制颜色为绿色
        Gizmos.color = Color.green;
        
        // 计算检测位置
        Vector2 checkPosition = (Vector2)transform.position - new Vector2(0f, groundCheckOffset);
        
        // 绘制地面检测盒子的范围
        Gizmos.DrawWireCube(checkPosition, groundCheckBoxSize);
    }
    
    #endregion
}