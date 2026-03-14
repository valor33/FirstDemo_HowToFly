using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 开始界面
/// 显示游戏标题和开始按钮
/// </summary>
public class StartPanel : MonoBehaviour
{
    #region 序列化字段（在 Inspector 中可配置）

    [Header("UI 组件")]
    [Tooltip("开始按钮")]
    [SerializeField] private Button startButton;

    [Tooltip("游戏标题文本")]
    [SerializeField] private TextMeshProUGUI titleText;

    [Tooltip("提示文本")]
    [SerializeField] private TextMeshProUGUI hintText;

    [Header("设置")]
    [Tooltip("是否支持按任意键开始")]
    [SerializeField] private bool anyKeyToStart = false;

    #endregion

    #region Unity 生命周期方法

    private void Awake()
    {
        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonClicked);
        }
        
        if (hintText != null)
        {
            hintText.gameObject.SetActive(anyKeyToStart);
        }
    }

    private void Update()
    {
        // 按任意键开始
        if (anyKeyToStart && Input.anyKeyDown)
        {
            OnStartButtonClicked();
        }
    }

    private void OnDestroy()
    {
        // 移除按钮事件
        if (startButton != null)
        {
            startButton.onClick.RemoveListener(OnStartButtonClicked);
        }
    }

    #endregion

    #region 私有方法

    /// <summary>
    /// 开始按钮点击事件
    /// </summary>
    private void OnStartButtonClicked()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.StartGame();
        }
        else
        {
            Debug.LogWarning("StartPanel: UIManager 不存在");
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
    }

    /// <summary>
    /// 设置提示文本
    /// </summary>
    public void SetHint(string hint)
    {
        if (hintText != null)
        {
            hintText.text = hint;
        }
    }

    #endregion
}