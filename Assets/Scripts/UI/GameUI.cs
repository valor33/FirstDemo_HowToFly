using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 游戏中 UI
/// 显示计时器和生命值
/// </summary>
public class GameUI : MonoBehaviour
{
    #region 序列化字段（在 Inspector 中可配置）

    [Header("计时器")]
    [Tooltip("计时器文本")]
    [SerializeField] private TextMeshProUGUI timerText;

    [Tooltip("计时器前缀")]
    [SerializeField] private string timerPrefix = "Time: ";

    [Header("生命值")]
    [Tooltip("生命值容器")]
    [SerializeField] private Transform healthContainer;

    [Tooltip("生命值图标预制体")]
    [SerializeField] private GameObject healthIconPrefab;

    [Tooltip("生命值图标列表（如果不使用预制体）")]
    [SerializeField] private Image[] healthIcons;

    [Header("图标设置")]
    [Tooltip("满生命图标")]
    [SerializeField] private Sprite fullHealthSprite;

    [Tooltip("空生命图标")]
    [SerializeField] private Sprite emptyHealthSprite;

    #endregion

    #region 私有字段

    // 玩家生命系统
    private PlayerHealth playerHealth;

    // 当前显示的生命图标
    private Image[] currentHealthIcons;

    #endregion

    #region Unity 生命周期方法

    private void Awake()
    {
        // 查找玩家生命系统
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    private void OnEnable()
    {
        // 订阅生命变化事件
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged.AddListener(UpdateHealthDisplay);
            playerHealth.OnDeath.AddListener(OnPlayerDeath);
        }

        // 初始化生命显示
        InitializeHealthDisplay();
    }

    private void OnDisable()
    {
        // 取消订阅事件
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged.RemoveListener(UpdateHealthDisplay);
            playerHealth.OnDeath.RemoveListener(OnPlayerDeath);
        }
    }

    private void Update()
    {
        // 更新计时器
        UpdateTimer();
    }

    #endregion

    #region 私有方法

    /// <summary>
    /// 更新计时器显示
    /// </summary>
    private void UpdateTimer()
    {
        if (timerText == null) return;

        if (UIManager.Instance != null)
        {
            timerText.text = timerPrefix + UIManager.Instance.GameTimeFormatted;
        }
    }

    /// <summary>
    /// 初始化生命显示
    /// </summary>
    private void InitializeHealthDisplay()
    {
        if (playerHealth == null) return;

        // 如果使用预制体
        if (healthContainer != null && healthIconPrefab != null)
        {
            // 清除现有图标
            foreach (Transform child in healthContainer)
            {
                Destroy(child.gameObject);
            }

            // 创建新图标
            currentHealthIcons = new Image[playerHealth.MaxHealth];
            for (int i = 0; i < playerHealth.MaxHealth; i++)
            {
                GameObject icon = Instantiate(healthIconPrefab, healthContainer);
                currentHealthIcons[i] = icon.GetComponent<Image>();
            }
        }
        else if (healthIcons != null && healthIcons.Length > 0)
        {
            // 使用预设的图标数组
            currentHealthIcons = healthIcons;
        }

        // 更新显示
        UpdateHealthDisplay(playerHealth.CurrentHealth, playerHealth.MaxHealth);
    }

    /// <summary>
    /// 更新生命值显示
    /// </summary>
    private void UpdateHealthDisplay(int currentHealth, int maxHealth)
    {
        if (currentHealthIcons == null) return;

        for (int i = 0; i < currentHealthIcons.Length; i++)
        {
            if (currentHealthIcons[i] == null) continue;

            // 根据索引设置图标状态
            if (i < currentHealth)
            {
                // 满生命
                if (fullHealthSprite != null)
                {
                    currentHealthIcons[i].sprite = fullHealthSprite;
                }
                currentHealthIcons[i].color = Color.white;
            }
            else
            {
                // 空生命
                if (emptyHealthSprite != null)
                {
                    currentHealthIcons[i].sprite = emptyHealthSprite;
                }
                currentHealthIcons[i].color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            }
        }
    }

    /// <summary>
    /// 玩家死亡事件处理
    /// </summary>
    private void OnPlayerDeath()
    {
        // 显示游戏结束（可以扩展）
        Debug.Log("GameUI: 玩家死亡，游戏结束");
    }

    #endregion

    #region 公开方法

    /// <summary>
    /// 设置计时器前缀
    /// </summary>
    public void SetTimerPrefix(string prefix)
    {
        timerPrefix = prefix;
    }

    #endregion
}