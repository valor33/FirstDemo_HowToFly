using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

/// <summary>
/// UI 管理器
/// 管理所有 UI 面板的显示和隐藏
/// 控制游戏状态（开始、暂停、结束）
/// </summary>
public class UIManager : MonoBehaviour
{
    #region 单例模式

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region 序列化字段（在 Inspector 中可配置）

    [Header("UI 面板")]
    [Tooltip("开始界面")]
    [SerializeField] private GameObject startPanel;

    [Tooltip("游戏中 UI")]
    [SerializeField] private GameObject gameUI;

    [Tooltip("胜利界面")]
    [SerializeField] private GameObject victoryPanel;

    [Tooltip("游戏结束界面")]
    [SerializeField] private GameObject gameOverPanel;

    [Tooltip("提示面板")]
    [SerializeField] private GameObject tipPanel;

    [Header("游戏状态")]
    [Tooltip("游戏开始时是否自动显示游戏 UI")]
    [SerializeField] private bool autoShowGameUI = true;

    #endregion

    #region 私有字段

    // 游戏是否已开始
    private bool gameStarted = false;

    // 游戏是否暂停
    private bool gamePaused = false;

    // 游戏计时
    private float gameTime = 0f;

    // 是否正在计时
    private bool isTiming = false;

    #endregion

    #region 公开属性

    /// <summary>
    /// 游戏是否已开始
    /// </summary>
    public bool GameStarted => gameStarted;

    /// <summary>
    /// 游戏是否暂停
    /// </summary>
    public bool GamePaused => gamePaused;

    /// <summary>
    /// 当前游戏时间
    /// </summary>
    public float GameTime => gameTime;

    /// <summary>
    /// 格式化的游戏时间 (分:秒)
    /// </summary>
    public string GameTimeFormatted
    {
        get
        {
            int minutes = Mathf.FloorToInt(gameTime / 60f);
            int seconds = Mathf.FloorToInt(gameTime % 60f);
            return $"{minutes:00}:{seconds:00}";
        }
    }

    #endregion

    #region 公开事件

    public event System.Action OnGameStart;
    public event System.Action OnGamePause;
    public event System.Action OnGameResume;
    public event System.Action OnGameEnd;

    #endregion

    #region Unity 生命周期方法

    private void Start()
    {
        // 初始隐藏所有面板
        HideAllPanels();

        // 显示开始界面
        ShowStartPanel();

        // 初始化时间
        gameTime = 0f;
    }

    private void Update()
    {
        // 更新游戏计时
        if (isTiming && !gamePaused)
        {
            gameTime += Time.deltaTime;
        }

        // 暂停键（ESC）
        if (gameStarted && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    #endregion

    #region 公开方法

    /// <summary>
    /// 开始游戏
    /// </summary>
    public void StartGame()
    {
        if (gameStarted) return;

        gameStarted = true;
        gameTime = 0f;
        isTiming = true;

        // 隐藏开始界面，显示游戏 UI
        HideStartPanel();
        if (autoShowGameUI)
        {
            ShowGameUI();
        }

        // 触发事件
        OnGameStart?.Invoke();

        Debug.Log("UIManager: 游戏开始");
    }

    /// <summary>
    /// 切换暂停状态
    /// </summary>
    public void TogglePause()
    {
        if (gamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    /// <summary>
    /// 暂停游戏
    /// </summary>
    public void PauseGame()
    {
        if (!gameStarted || gamePaused) return;

        gamePaused = true;
        Time.timeScale = 0f;

        OnGamePause?.Invoke();

        Debug.Log("UIManager: 游戏暂停");
    }

    /// <summary>
    /// 继续游戏
    /// </summary>
    public void ResumeGame()
    {
        if (!gamePaused) return;

        gamePaused = false;
        Time.timeScale = 1f;

        OnGameResume?.Invoke();

        Debug.Log("UIManager: 游戏继续");
    }

    /// <summary>
    /// 游戏胜利
    /// </summary>
    public void ShowVictory()
    {
        isTiming = false;

        HideGameUI();
        ShowVictoryPanel();

        OnGameEnd?.Invoke();

        Debug.Log($"UIManager: 游戏胜利，用时: {GameTimeFormatted}");
    }

    /// <summary>
    /// 游戏结束（死亡）
    /// </summary>
    public void ShowGameOver()
    {
        isTiming = false;

        HideGameUI();
        ShowGameOverPanel();

        OnGameEnd?.Invoke();

        Debug.Log($"UIManager: 游戏结束，用时: {GameTimeFormatted}");
    }

    /// <summary>
    /// 重新开始游戏
    /// </summary>
    public void RestartGame()
    {
        // 恢复时间
        Time.timeScale = 1f;

        // 重置状态
        gameStarted = false;
        gamePaused = false;
        gameTime = 0f;
        isTiming = false;

        // 重新加载当前场景
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    /// <summary>
    /// 返回主菜单
    /// </summary>
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        gameStarted = false;
        gamePaused = false;
        gameTime = 0f;
        isTiming = false;

        HideAllPanels();
        ShowStartPanel();
    }

    /// <summary>
    /// 显示提示
    /// </summary>
    /// <param name="message">提示内容</param>
    /// <param name="duration">显示时间</param>
    public void ShowTip(string message, float duration = 3f)
    {
        StartCoroutine(ShowTipCoroutine(message, duration));
    }

    private System.Collections.IEnumerator ShowTipCoroutine(string message, float duration)
    {
        if (tipPanel == null)
        {
            Debug.LogWarning("UIManager: Tip Panel 未设置！");
            yield break;
        }

        TextMeshProUGUI tipText = tipPanel.GetComponentInChildren<TextMeshProUGUI>();
        if (tipText != null)
        {
            tipText.text = message;
        }

        tipPanel.SetActive(true);

        yield return new WaitForSeconds(duration);

        tipPanel.SetActive(false);
    }

    #endregion

    #region 私有方法 - 面板控制

    private void HideAllPanels()
    {
        if (startPanel != null) startPanel.SetActive(false);
        if (gameUI != null) gameUI.SetActive(false);
        if (victoryPanel != null) victoryPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    private void ShowStartPanel()
    {
        if (startPanel != null) startPanel.SetActive(true);
    }

    private void HideStartPanel()
    {
        if (startPanel != null) startPanel.SetActive(false);
    }

    private void ShowGameUI()
    {
        if (gameUI != null) gameUI.SetActive(true);
    }

    private void HideGameUI()
    {
        if (gameUI != null) gameUI.SetActive(false);
    }

    private void ShowVictoryPanel()
    {
        if (victoryPanel != null) victoryPanel.SetActive(true);
    }

    private void ShowGameOverPanel()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    #endregion
}