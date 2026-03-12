using UnityEngine;

/// <summary>
/// 玩家动画控制器
/// 负责根据玩家状态改变精灵颜色
/// 使用颜色变化代替动画，适合快速原型开发
/// </summary>
public class PlayerAnimation : MonoBehaviour
{
    #region 序列化字段（在 Inspector 中可配置）
    
    [Header("颜色配置")]
    [Tooltip("静止状态的颜色")]
    [SerializeField] private Color idleColor = Color.white;
    
    [Tooltip("移动状态的颜色")]
    [SerializeField] private Color walkColor = new Color(0.7f, 0.8f, 1f);
    
    [Tooltip("跳跃状态的颜色")]
    [SerializeField] private Color jumpColor = Color.yellow;
    
    [Tooltip("飞行状态的颜色")]
    [SerializeField] private Color flyColor = Color.cyan;
    
    [Header("组件引用")]
    [Tooltip("精灵渲染器组件")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [Tooltip("玩家控制器引用")]
    [SerializeField] private PlayerController playerController;
    
    #endregion
    
    #region 私有字段
    
    // 当前状态
    private bool isJumping = false;
    private bool isFlying = false;
    private bool flightUnlocked = false;
    
    #endregion
    
    #region Unity 生命周期方法
    
    private void Awake()
    {
        // 如果未指定 SpriteRenderer，尝试自动获取
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        // 如果未指定 PlayerController，尝试自动获取
        if (playerController == null)
        {
            playerController = GetComponent<PlayerController>();
        }
        
        // 检查必要组件
        if (spriteRenderer == null)
        {
            Debug.LogError("PlayerAnimation: 缺少 SpriteRenderer 组件！");
        }
    }
    
    private void Update()
    {
        // 如果飞行能力已解锁且正在飞行，优先显示飞行颜色
        if (flightUnlocked && isFlying)
        {
            SetColor(flyColor);
            return;
        }
        
        // 如果正在跳跃，显示跳跃颜色
        if (isJumping)
        {
            SetColor(jumpColor);
            return;
        }
        
        // 根据移动状态决定颜色
        if (playerController != null)
        {
            // 如果有水平输入，表示在移动
            if (Mathf.Abs(playerController.HorizontalInput) > 0.01f)
            {
                SetColor(walkColor);
            }
            else
            {
                SetColor(idleColor);
            }
        }
    }
    
    #endregion
    
    #region 公开方法
    
    /// <summary>
    /// 设置跳跃状态
    /// 由 PlayerJump 调用
    /// </summary>
    /// <param name="jumping">是否正在跳跃</param>
    public void SetJumping(bool jumping)
    {
        isJumping = jumping;
    }
    
    /// <summary>
    /// 设置飞行状态
    /// 由 PlayerFlight 调用
    /// </summary>
    /// <param name="flying">是否正在飞行</param>
    public void SetFlying(bool flying)
    {
        isFlying = flying;
    }
    
    /// <summary>
    /// 当飞行能力解锁时调用
    /// 由 PlayerFlight 调用
    /// </summary>
    public void OnFlightUnlocked()
    {
        flightUnlocked = true;
        
        // 可以在这里添加解锁特效
        Debug.Log("PlayerAnimation: 飞行能力已解锁！");
    }
    
    /// <summary>
    /// 重置所有状态
    /// 用于玩家重生等场景
    /// </summary>
    public void ResetState()
    {
        isJumping = false;
        isFlying = false;
        flightUnlocked = false;
        SetColor(idleColor);
    }
    
    #endregion
    
    #region 私有方法
    
    /// <summary>
    /// 设置精灵颜色
    /// </summary>
    /// <param name="color">目标颜色</param>
    private void SetColor(Color color)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }
    }
    
    #endregion
}