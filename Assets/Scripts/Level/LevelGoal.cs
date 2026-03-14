using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 关卡终点
/// 玩家到达后触发胜利事件
/// 可以配置加载下一关卡或其他行为
/// </summary>
public class LevelGoal : MonoBehaviour
{
    #region 序列化字段（在 Inspector 中可配置）

    [Header("终点参数")]
    [Tooltip("是否需要飞行能力才能通关")]
    [SerializeField] private bool requireFlight = false;

    [Tooltip("通关时的 fly 值要求（-1 表示无要求）")]
    [SerializeField] private float requiredFlyValue = -1f;

    [Tooltip("是否只检测一次")]
    [SerializeField] private bool singleTrigger = true;

    [Header("关卡配置")]
    [Tooltip("下一关卡名称（留空则不切换场景）")]
    [SerializeField] private string nextLevelName = "";

    [Tooltip("通关后延迟切换场景的时间")]
    [SerializeField] private float levelTransitionDelay = 2f;

    [Header("视觉效果")]
    [Tooltip("终点颜色")]
    [SerializeField] private Color goalColor = new Color(1f, 0.8f, 0f, 1f);

    [Tooltip("通关特效预制体")]
    [SerializeField] private GameObject victoryEffect;

    [Header("组件引用")]
    [Tooltip("终点渲染器")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    #endregion

    #region 私有字段

    private bool hasTriggered = false;
    private bool levelCompleted = false;
    private GameObject currentPlayer;

    #endregion

    #region 公开事件

    /// <summary>
    /// 玩家到达终点事件
    /// </summary>
    public event System.Action OnGoalReached;

    /// <summary>
    /// 关卡完成事件
    /// </summary>
    public event System.Action OnLevelCompleted;

    #endregion

    #region 公开属性

    /// <summary>
    /// 是否已触发终点
    /// </summary>
    public bool HasTriggered => hasTriggered;

    /// <summary>
    /// 关卡是否已完成
    /// </summary>
    public bool LevelCompleted => levelCompleted;

    #endregion

    #region Unity 生命周期方法

    private void Awake()
    {
        // 如果未指定 SpriteRenderer，尝试获取
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // 设置终点颜色
        if (spriteRenderer != null)
        {
            spriteRenderer.color = goalColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查是否是玩家
        if (!other.CompareTag("Player")) return;

        // 检查是否已触发
        if (singleTrigger && hasTriggered) return;

        // 检查通关条件
        if (!CheckCompletionRequirements(other.gameObject)) return;

        // 触发终点
        TriggerGoal(other.gameObject);
    }

    #endregion

    #region 公开方法

    /// <summary>
    /// 触发终点
    /// </summary>
    /// <param name="player">玩家对象</param>
    public void TriggerGoal(GameObject player)
    {
        hasTriggered = true;
        currentPlayer = player;

        OnGoalReached?.Invoke();

        Debug.Log($"LevelGoal: 玩家到达终点 {gameObject.name}");

        PlayVictoryEffect();
        CompleteLevel();
    }

    /// <summary>
    /// 重置终点状态
    /// </summary>
    public void ResetGoal()
    {
        hasTriggered = false;
        levelCompleted = false;
    }

    #endregion

    #region 私有方法

    /// <summary>
    /// 检查通关条件
    /// </summary>
    private bool CheckCompletionRequirements(GameObject player)
    {
        PlayerFlight playerFlight = player.GetComponent<PlayerFlight>();

        // 检查飞行能力要求
        if (requireFlight)
        {
            if (playerFlight == null || !playerFlight.IsFlightUnlocked)
            {
                Debug.Log("LevelGoal: 需要解锁飞行能力才能通关");
                return false;
            }
        }

        // 检查 fly 值要求
        if (requiredFlyValue > 0f)
        {
            if (playerFlight == null || playerFlight.CurrentFlyValue < requiredFlyValue)
            {
                Debug.Log($"LevelGoal: 需要至少 {requiredFlyValue} fly 值才能通关");
                return false;
            }
        }

        return true;
    }

private void CompleteLevel()
    {
        levelCompleted = true;
        
        DisablePlayerControl(currentPlayer);
        
        OnLevelCompleted?.Invoke();
        
        Debug.Log("LevelGoal: 关卡完成！");

        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowVictory();
        }
        else if (!string.IsNullOrEmpty(nextLevelName))
        {
            Invoke(nameof(LoadNextLevel), levelTransitionDelay);
        }
    }

    /// <summary>
    /// 加载下一关卡
    /// </summary>
    private void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            Debug.Log($"LevelGoal: 加载下一关卡 {nextLevelName}");
            SceneManager.LoadScene(nextLevelName);
        }
    }

    private void DisablePlayerControl(GameObject player)
    {
        if (player == null) return;
        
        PlayerController controller = player.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.DisableControl();
        }
    }
    
    private void PlayVictoryEffect()
    {
        if (victoryEffect == null) return;

        GameObject effect = Instantiate(victoryEffect, transform.position, Quaternion.identity);
        Destroy(effect, 3f);
    }

    #endregion

    #region 编辑器可视化

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // 绘制终点区域
        Gizmos.color = new Color(1f, 0.8f, 0f, 0.3f);

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            Bounds bounds = collider.bounds;
            Gizmos.DrawCube(bounds.center, bounds.size);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

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