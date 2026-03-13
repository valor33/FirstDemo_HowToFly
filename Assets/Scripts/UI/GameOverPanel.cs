using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 游戏结束界面
/// 玩家死亡时显示
/// </summary>
public class GameOverPanel : MonoBehaviour
{
    #region 序列化字段（在 Inspector 中可配置）

    [Header("UI 组件")]
    [Tooltip("标题文本")]
    [SerializeField] private TextMeshProUGUI titleText;

    [Tooltip("重新开始按钮")]
    [SerializeField] private Button restartButton;

    [Header("设置")]
    [Tooltip("默认标题")]
    [SerializeField] private string defaultTitle = "Game Over";

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
        // 更新标题
        if (titleText != null)
        {
            titleText.text = defaultTitle;
        }
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
            Debug.LogWarning("GameOverPanel: UIManager 不存在");
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

    #endregion
}