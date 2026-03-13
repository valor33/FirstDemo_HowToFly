using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 胜利界面
/// 显示通关时间和重新开始按钮
/// </summary>
public class VictoryPanel : MonoBehaviour
{
    #region 序列化字段（在 Inspector 中可配置）

    [Header("UI 组件")]
    [Tooltip("时间显示文本")]
    [SerializeField] private TextMeshProUGUI timeText;

    [Tooltip("标题文本")]
    [SerializeField] private TextMeshProUGUI titleText;

    [Tooltip("重新开始按钮")]
    [SerializeField] private Button restartButton;

    [Header("设置")]
    [Tooltip("时间文本前缀")]
    [SerializeField] private string timePrefix = "Your Time: ";

    [Tooltip("默认标题")]
    [SerializeField] private string defaultTitle = "Victory!";

    #endregion

    #region Unity 生命周期方法

    private void Awake()
    {
        // 绑定按钮事件
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(OnRestartButtonClicked);
        }
    }

    private void OnEnable()
    {
        // 更新显示
        UpdateDisplay();
    }

    private void OnDestroy()
    {
        // 移除按钮事件
        if (restartButton != null)
        {
            restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        }
    }

    #endregion

    #region 私有方法

    /// <summary>
    /// 更新显示内容
    /// </summary>
    private void UpdateDisplay()
    {
        // 更新时间文本
        if (timeText != null && UIManager.Instance != null)
        {
            timeText.text = timePrefix + UIManager.Instance.GameTimeFormatted;
        }

        // 更新标题
        if (titleText != null)
        {
            titleText.text = defaultTitle;
        }
    }

    /// <summary>
    /// 重新开始按钮点击事件
    /// </summary>
    private void OnRestartButtonClicked()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.RestartGame();
        }
        else
        {
            Debug.LogWarning("VictoryPanel: UIManager 不存在");
        }
    }

    #endregion

    #region 公开方法

    /// <summary>
    /// 设置标题文本
    /// </summary>
    public void SetTitle(string title)
    {
        if (titleText != null)
        {
            titleText.text = title;
        }
        defaultTitle = title;
    }

    /// <summary>
    /// 设置时间文本前缀
    /// </summary>
    public void SetTimePrefix(string prefix)
    {
        timePrefix = prefix;
        UpdateDisplay();
    }

    #endregion
}