using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 玩家生命系统
/// 管理玩家生命值，生命归零时触发游戏结束
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    #region 序列化字段（在 Inspector 中可配置）

    [Header("生命参数")]
    [Tooltip("最大生命值")]
    [SerializeField] private int maxHealth = 3;

    [Tooltip("受伤后的无敌时间")]
    [SerializeField] private float invincibleTime = 1.5f;

    [Header("视觉效果")]
    [Tooltip("受伤时是否闪烁")]
    [SerializeField] private bool flashOnHurt = true;

    [Tooltip("闪烁次数")]
    [SerializeField] private int flashCount = 3;

    #endregion

    #region 私有字段

    // 当前生命值
    private int currentHealth;

    // 是否处于无敌状态
    private bool isInvincible = false;

    // 无敌计时器
    private float invincibleTimer = 0f;

    // 玩家渲染器
    private SpriteRenderer spriteRenderer;

    // 玩家重生管理器
    private PlayerRespawn playerRespawn;

    // 是否已死亡
    private bool isDead = false;

    #endregion

    #region 公开属性

    /// <summary>
    /// 当前生命值
    /// </summary>
    public int CurrentHealth => currentHealth;

    /// <summary>
    /// 最大生命值
    /// </summary>
    public int MaxHealth => maxHealth;

    /// <summary>
    /// 是否处于无敌状态
    /// </summary>
    public bool IsInvincible => isInvincible;

    /// <summary>
    /// 是否已死亡
    /// </summary>
    public bool IsDead => isDead;

    /// <summary>
    /// 生命值百分比 (0-1)
    /// </summary>
    public float HealthPercent => maxHealth > 0 ? (float)currentHealth / maxHealth : 0f;

    #endregion

    #region 公开事件

    /// <summary>
    /// 生命值变化事件
    /// </summary>
    public UnityEvent<int, int> OnHealthChanged;

    /// <summary>
    /// 受伤事件
    /// </summary>
    public UnityEvent OnHurt;

    /// <summary>
    /// 死亡事件
    /// </summary>
    public UnityEvent OnDeath;

    #endregion

    #region Unity 生命周期方法

    private void Awake()
    {
        // 初始化生命值
        currentHealth = maxHealth;

        // 获取组件
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerRespawn = GetComponent<PlayerRespawn>();
    }

    private void Update()
    {
        // 更新无敌计时器
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0f)
            {
                EndInvincibility();
            }
        }
    }

    #endregion

    #region 公开方法

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="damage">伤害值</param>
    public void TakeDamage(int damage = 1)
    {
        // 检查是否可以受伤
        if (isInvincible || isDead) return;

        // 减少生命值
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        // 触发事件
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        OnHurt?.Invoke();

        Debug.Log($"PlayerHealth: 受到伤害，当前生命: {currentHealth}/{maxHealth}");

        // 检查是否死亡
        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        // 开始无敌时间
        StartInvincibility();

        // 触发重生（保留剩余生命）
        if (playerRespawn != null)
        {
            playerRespawn.Die();
        }
    }

    /// <summary>
    /// 恢复生命值
    /// </summary>
    /// <param name="amount">恢复量</param>
    public void Heal(int amount = 1)
    {
        if (isDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        Debug.Log($"PlayerHealth: 恢复生命，当前生命: {currentHealth}/{maxHealth}");
    }

    /// <summary>
    /// 重置生命值（重新开始游戏）
    /// </summary>
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        isDead = false;
        isInvincible = false;
        invincibleTimer = 0f;

        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        // 恢复渲染器可见
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = 1f;
            spriteRenderer.color = color;
        }

        Debug.Log("PlayerHealth: 生命值已重置");
    }

    /// <summary>
    /// 立即死亡
    /// </summary>
    public void Die()
    {
        if (isDead) return;

        isDead = true;

        Debug.Log("PlayerHealth: 玩家死亡");

        // 触发死亡事件
        OnDeath?.Invoke();

        // 显示游戏结束界面
        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowGameOver();
        }
    }

    #endregion

    #region 私有方法

    /// <summary>
    /// 开始无敌时间
    /// </summary>
    private void StartInvincibility()
    {
        isInvincible = true;
        invincibleTimer = invincibleTime;

        // 闪烁效果
        if (flashOnHurt && spriteRenderer != null)
        {
            StartCoroutine(FlashEffect());
        }
    }

    /// <summary>
    /// 结束无敌状态
    /// </summary>
    private void EndInvincibility()
    {
        isInvincible = false;

        // 恢复渲染器可见
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = 1f;
            spriteRenderer.color = color;
        }
    }

    /// <summary>
    /// 闪烁效果
    /// </summary>
    private System.Collections.IEnumerator FlashEffect()
    {
        if (spriteRenderer == null) yield break;

        float flashInterval = invincibleTime / (flashCount * 2);

        for (int i = 0; i < flashCount; i++)
        {
            // 隐藏
            SetSpriteAlpha(0.3f);
            yield return new WaitForSeconds(flashInterval);

            // 显示
            SetSpriteAlpha(1f);
            yield return new WaitForSeconds(flashInterval);
        }
    }

    /// <summary>
    /// 设置精灵透明度
    /// </summary>
    private void SetSpriteAlpha(float alpha)
    {
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }
    }

    #endregion
}