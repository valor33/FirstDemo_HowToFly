using UnityEngine;

/// <summary>
/// 玩家主控制器
/// 负责处理玩家的移动、翻转、地面检测等核心功能
/// 作为其他玩家组件的协调中心
/// </summary>
public class PlayerController : MonoBehaviour
{
    #region 序列化字段
    
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
    
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float horizontalInput;
    private bool isGrounded;
    private bool canControl = true;
    
    #endregion
    
    #region 公开属性
    
    public bool IsGrounded => isGrounded;
    public float HorizontalInput => horizontalInput;
    public Rigidbody2D Rb => rb;
    public bool CanControl => canControl;
    
    #endregion
    
    #region Unity 生命周期方法
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (rb == null)
        {
            Debug.LogError("PlayerController: 缺少 Rigidbody2D 组件！");
        }
        
        if (spriteRenderer == null)
        {
            Debug.LogWarning("PlayerController: 缺少 SpriteRenderer 组件，翻转功能将不可用。");
        }
    }
    
    private void Update()
    {
        if (!canControl) return;
        
        horizontalInput = Input.GetAxis("Horizontal");
        HandleFlip();
    }
    
    private void FixedUpdate()
    {
        if (!canControl) return;
        
        HandleMovement();
        CheckGrounded();
    }
    
    #endregion
    
    #region 公开方法
    
    /// <summary>
    /// 禁用玩家控制，使角色静止
    /// </summary>
    public void DisableControl()
    {
        canControl = false;
        
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
        
        horizontalInput = 0f;
    }
    
    /// <summary>
    /// 启用玩家控制
    /// </summary>
    public void EnableControl()
    {
        canControl = true;
    }
    
    #endregion
    
    #region 私有方法
    
    private void HandleMovement()
    {
        float targetVelocityX = horizontalInput * moveSpeed;
        rb.velocity = new Vector2(targetVelocityX, rb.velocity.y);
    }
    
    private void HandleFlip()
    {
        if (spriteRenderer == null) return;
        
        if (horizontalInput > 0.01f)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < -0.01f)
        {
            spriteRenderer.flipX = true;
        }
    }
    
    private void CheckGrounded()
    {
        Vector2 checkPosition = (Vector2)transform.position - new Vector2(0f, groundCheckOffset);
        
        isGrounded = Physics2D.BoxCast(
            checkPosition,
            groundCheckBoxSize,
            0f,
            Vector2.down,
            0.1f,
            groundLayer
        );
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector2 checkPosition = (Vector2)transform.position - new Vector2(0f, groundCheckOffset);
        Gizmos.DrawWireCube(checkPosition, groundCheckBoxSize);
    }
    
    #endregion
}