using UnityEngine;

/// <summary>
/// 洞穴出口
/// 玩家只能从下方通过，无法从上方返回
/// 第一次通过时触发提示
/// </summary>
public class CaveExit : MonoBehaviour
{
    #region 序列化字段

    [Header("提示设置")]
    [Tooltip("提示文本")]
    [SerializeField] private string tipMessage = "您已解锁作弊键 P";

    [Tooltip("提示显示时间（秒）")]
    [SerializeField] private float tipDuration = 3f;

    #endregion

    #region 私有字段

    private bool hasTriggered = false;
    private Collider2D exitCollider;
    private float playerPreviousY;

    #endregion

    #region Unity 生命周期方法

    private void Awake()
    {
        exitCollider = GetComponent<Collider2D>();
        if (exitCollider == null)
        {
            Debug.LogError("CaveExit: 缺少 Collider2D 组件！");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            
            if (playerRb != null && playerRb.velocity.y > 0.5f)
            {
                hasTriggered = true;
                ShowTip();
                Debug.Log("CaveExit: 玩家通过出口");
            }
        }
    }

    #endregion

    #region 私有方法

    private void ShowTip()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowTip(tipMessage, tipDuration);
        }
    }

    #endregion
}