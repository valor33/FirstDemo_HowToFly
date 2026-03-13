using UnityEngine;

/// <summary>
/// 死亡区域
/// 玩家进入后触发死亡，由 PlayerRespawn 处理重生
/// 通常放置在场景底部或危险区域
/// </summary>
public class DeathZone : MonoBehaviour
{
    #region 序列化字段（在 Inspector 中可配置）

    [Header("死亡参数")]
    [Tooltip("是否显示调试信息")]
    [SerializeField] private bool showDebug = true;

    [Tooltip("死亡特效预制体（可选）")]
    [SerializeField] private GameObject deathEffectPrefab;

    [Tooltip("死亡特效持续时间")]
    [SerializeField] private float effectDuration = 1f;

    #endregion

    #region 私有字段

    // 当前进入死亡区域的玩家
    private GameObject currentPlayer;

    #endregion

    #region Unity 生命周期方法

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查是否是玩家
        if (!other.CompareTag("Player")) return;

        // 获取玩家对象
        currentPlayer = other.gameObject;

        // 播放死亡特效
        PlayDeathEffect(other.transform.position);

        // 优先使用 PlayerHealth 系统
        PlayerHealth health = currentPlayer.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage();
            
            if (showDebug)
            {
                Debug.Log($"DeathZone: 玩家进入死亡区域，剩余生命: {health.CurrentHealth}");
            }
            return;
        }

        // 如果没有 PlayerHealth，使用 PlayerRespawn
        PlayerRespawn respawn = currentPlayer.GetComponent<PlayerRespawn>();
        if (respawn != null)
        {
            respawn.Die();
            
            if (showDebug)
            {
                Debug.Log($"DeathZone: 玩家进入死亡区域 {gameObject.name}");
            }
        }
        else
        {
            Debug.LogWarning("DeathZone: 玩家没有 PlayerRespawn 或 PlayerHealth 组件");
        }
    }

    #endregion

    #region 私有方法

    /// <summary>
    /// 播放死亡特效
    /// </summary>
    private void PlayDeathEffect(Vector3 position)
    {
        if (deathEffectPrefab == null) return;

        GameObject effect = Instantiate(deathEffectPrefab, position, Quaternion.identity);
        Destroy(effect, effectDuration);
    }

    #endregion

    #region 编辑器可视化

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // 绘制死亡区域范围
        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
        
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            Bounds bounds = collider.bounds;
            Gizmos.DrawCube(bounds.center, bounds.size);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            Bounds bounds = collider.bounds;
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }
    }
#endif

    #endregion
}