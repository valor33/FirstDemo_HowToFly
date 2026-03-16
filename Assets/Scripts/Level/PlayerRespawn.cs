using UnityEngine;

/// <summary>
/// 玩家重生管理器
/// 管理重生点位置、处理玩家死亡和重生逻辑
/// </summary>
public class PlayerRespawn : MonoBehaviour
{
    #region 序列化字段（在 Inspector 中可配置）

    [Header("重生参数")]
    [Tooltip("重生时的无敌时间（秒）")]
    [SerializeField] private float invincibleTime = 1f;

    [Tooltip("重生后的初始 fly 值（-1 表示保持当前值）")]
    [SerializeField] private float respawnFlyValue = -1f;

    [Tooltip("是否在重生时重置 fly 值")]
    [SerializeField] private bool resetFlyValueOnRespawn = false;

    [Header("视觉效果")]
    [Tooltip("重生时是否闪烁提示")]
    [SerializeField] private bool flashOnRespawn = true;

    [Tooltip("闪烁次数")]
    [SerializeField] private int flashCount = 3;

    [Tooltip("闪烁间隔")]
    [SerializeField] private float flashInterval = 0.1f;

    #endregion

    #region 私有字段

    // 当前重生点位置
    private Vector3 currentRespawnPosition;

    // 是否有检查点记录
    private bool hasCheckpoint = false;

    // 初始位置（游戏开始时的位置）
    private Vector3 initialPosition;

    // 玩家组件引用
    private PlayerController playerController;
    private PlayerFlight playerFlight;
    private PlayerAnimation playerAnimation;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    // 是否处于无敌状态
    private bool isInvincible = false;

    #endregion

    #region 公开属性

    /// <summary>
    /// 当前是否处于无敌状态
    /// </summary>
    public bool IsInvincible => isInvincible;

    /// <summary>
    /// 当前重生点位置
    /// </summary>
    public Vector3 RespawnPosition => hasCheckpoint ? currentRespawnPosition : initialPosition;

    #endregion

    #region Unity 生命周期方法

    private void Awake()
    {
        // 记录初始位置
        initialPosition = transform.position;
        currentRespawnPosition = initialPosition;

        // 获取组件引用
        playerController = GetComponent<PlayerController>();
        playerFlight = GetComponent<PlayerFlight>();
        playerAnimation = GetComponent<PlayerAnimation>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    #endregion

    #region 公开方法

    /// <summary>
    /// 设置新的重生点
    /// 由 Checkpoint 调用
    /// </summary>
    /// <param name="position">新的重生点位置</param>
    public void SetRespawnPoint(Vector3 position)
    {
        currentRespawnPosition = position;
        hasCheckpoint = true;
        Debug.Log($"PlayerRespawn: 设置新重生点 {position}");
    }

    /// <summary>
    /// 玩家死亡，执行重生
    /// 由 DeathZone 调用
    /// </summary>
    public void Die()
    {
        // 检查是否处于无敌状态
        if (isInvincible)
        {
            Debug.Log("PlayerRespawn: 处于无敌状态，忽略死亡");
            return;
        }

        Debug.Log("PlayerRespawn: 玩家死亡，准备重生");
        Respawn();
    }

    /// <summary>
    /// 立即重生
    /// </summary>
    public void Respawn()
    {
        // 获取重生位置
        Vector3 respawnPos = RespawnPosition;

        // 重置位置
        transform.position = respawnPos;

        // 重置刚体速度
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // 重置玩家状态
        ResetPlayerState();

        // 开始无敌时间
        StartInvincibility();

        Debug.Log($"PlayerRespawn: 玩家在 {respawnPos} 重生");
    }

    /// <summary>
    /// 重置到初始状态（重新开始游戏）
    /// </summary>
    public void ResetToStart()
    {
        // 清除检查点
        hasCheckpoint = false;
        currentRespawnPosition = initialPosition;

        // 重置位置
        transform.position = initialPosition;

        // 重置刚体速度
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // 重置玩家状态
        ResetPlayerState();

        // 完全重置 fly 值
        if (playerFlight != null)
        {
            playerFlight.SetFlyValue(0f);
        }

        // 重置光晕视觉
        AuraVisual auraVisual = GetComponentInChildren<AuraVisual>();
        if (auraVisual != null)
        {
            auraVisual.ResetAura();
        }

        Debug.Log("PlayerRespawn: 重置到游戏初始状态");
    }

    #endregion

    #region 私有方法

    /// <summary>
    /// 重置玩家状态
    /// </summary>
    private void ResetPlayerState()
    {
        if (resetFlyValueOnRespawn && playerFlight != null)
        {
            if (respawnFlyValue >= 0f)
            {
                playerFlight.SetFlyValue(respawnFlyValue);
            }
            else
            {
                playerFlight.SetFlyValue(0f);
            }
        }

        if (playerAnimation != null)
        {
            playerAnimation.ResetState();
            
            if (playerFlight != null && playerFlight.HasUnlockedWings)
            {
                playerAnimation.OnWingsUnlocked();
            }
        }

        AuraVisual auraVisual = GetComponentInChildren<AuraVisual>();
        if (auraVisual != null && playerFlight != null)
        {
            auraVisual.SyncWithPlayerFlight(playerFlight);
        }
    }

    /// <summary>
    /// 开始无敌时间
    /// </summary>
    private void StartInvincibility()
    {
        if (invincibleTime > 0f)
        {
            isInvincible = true;

            // 闪烁效果
            if (flashOnRespawn && spriteRenderer != null)
            {
                StartCoroutine(FlashEffect());
            }

            // 延迟结束无敌
            Invoke(nameof(EndInvincibility), invincibleTime);
        }
    }

    /// <summary>
    /// 结束无敌状态
    /// </summary>
    private void EndInvincibility()
    {
        isInvincible = false;

        // 确保渲染器可见
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = 1f;
            spriteRenderer.color = color;
        }

        Debug.Log("PlayerRespawn: 无敌时间结束");
    }

    /// <summary>
    /// 闪烁效果协程
    /// </summary>
    private System.Collections.IEnumerator FlashEffect()
    {
        if (spriteRenderer == null) yield break;

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