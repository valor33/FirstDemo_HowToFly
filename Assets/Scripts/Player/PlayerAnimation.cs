using UnityEngine;

/// <summary>
/// 玩家动画控制器
/// 负责根据玩家状态播放对应动画
/// </summary>
public class PlayerAnimation : MonoBehaviour
{
    #region 序列化字段
    
    [Header("动画组件")]
    [Tooltip("动画控制器组件")]
    [SerializeField] private Animator animator;
    
    [Tooltip("精灵渲染器组件")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [Tooltip("玩家控制器引用")]
    [SerializeField] private PlayerController playerController;
    
    #endregion
    
    #region 私有字段
    
    private bool isDashing = false;
    private bool hasBigWings = false;
    private bool wasGrounded = true;
    
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int IsDashingHash = Animator.StringToHash("IsDashing");
    private static readonly int HasBigWingsHash = Animator.StringToHash("HasBigWings");
    
    #endregion
    
    #region Unity 生命周期方法
    
    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        if (playerController == null)
        {
            playerController = GetComponent<PlayerController>();
        }
        
        if (animator == null)
        {
            Debug.LogError("PlayerAnimation: 缺少 Animator 组件！");
        }
        
        if (spriteRenderer == null)
        {
            Debug.LogError("PlayerAnimation: 缺少 SpriteRenderer 组件！");
        }
    }
    
    private void Update()
    {
        if (animator == null) return;
        
        float speed = 0f;
        if (playerController != null)
        {
            speed = Mathf.Abs(playerController.HorizontalInput);
            
            // 只有从空中落到地面时才重置冲刺状态
            if (playerController.IsGrounded && !wasGrounded)
            {
                isDashing = false;
            }
            wasGrounded = playerController.IsGrounded;
            
            if (playerController.HorizontalInput != 0)
            {
                spriteRenderer.flipX = playerController.HorizontalInput < 0;
            }
        }
        
        animator.SetFloat(SpeedHash, speed);
        animator.SetBool(IsDashingHash, isDashing);
        animator.SetBool(HasBigWingsHash, hasBigWings);
    }
    
    #endregion
    
    #region 公开方法
    
    /// <summary>
    /// 设置冲刺状态
    /// </summary>
    /// <param name="dashing">是否正在冲刺</param>
    public void SetDashing(bool dashing)
    {
        isDashing = dashing;
    }
    
    /// <summary>
    /// 当翅膀解锁时调用（一次性触发）
    /// </summary>
    public void OnWingsUnlocked()
    {
        hasBigWings = true;
        Debug.Log("PlayerAnimation: 大翅膀已解锁！");
    }
    
    /// <summary>
    /// 重置所有状态
    /// </summary>
    public void ResetState()
    {
        isDashing = false;
        wasGrounded = true;
    }
    
    #endregion
}