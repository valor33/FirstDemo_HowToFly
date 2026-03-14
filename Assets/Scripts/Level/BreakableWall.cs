using UnityEngine;

/// <summary>
/// 可破坏墙壁
/// 玩家碰撞后触发破坏，显示隐藏区域或道具
/// </summary>
public class BreakableWall : MonoBehaviour
{
    #region 序列化字段

    [Header("破坏参数")]
    [Tooltip("破坏所需时间（秒），0表示立即破坏")]
    [SerializeField] private float destroyTime = 0f;

    [Tooltip("是否只能破坏一次")]
    [SerializeField] private bool singleUse = true;

    [Tooltip("破坏后是否禁用碰撞器")]
    [SerializeField] private bool disableCollider = true;

    [Header("视觉效果")]
    [Tooltip("破坏特效预制体")]
    [SerializeField] private GameObject breakEffect;

    [Tooltip("特效持续时间")]
    [SerializeField] private float effectDuration = 1f;

    [Tooltip("破坏后的颜色")]
    [SerializeField] private Color brokenColor = new Color(0.3f, 0.3f, 0.3f, 0.5f);

    [Header("隐藏内容")]
    [Tooltip("破坏后显示的对象")]
    [SerializeField] private GameObject hiddenContent;

    [Tooltip("是否在Start时隐藏内容")]
    [SerializeField] private bool hideContentOnStart = true;

    [Header("组件引用")]
    [Tooltip("墙壁渲染器")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Tooltip("墙壁碰撞器")]
    [SerializeField] private Collider2D wallCollider;

    #endregion

    #region 私有字段

    private bool isDestroyed = false;
    private float destroyTimer = 0f;
    private bool isDestroying = false;
    private Color originalColor;

    #endregion

    #region 公开属性

    public bool IsDestroyed => isDestroyed;

    #endregion

    #region Unity 生命周期方法

    private void Awake()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (wallCollider == null)
        {
            wallCollider = GetComponent<Collider2D>();
        }

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    private void Start()
    {
        if (hiddenContent != null && hideContentOnStart)
        {
            hiddenContent.SetActive(false);
        }
    }

    private void Update()
    {
        if (isDestroying && destroyTime > 0f)
        {
            destroyTimer += Time.deltaTime;
            
            float progress = destroyTimer / destroyTime;
            
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.Lerp(originalColor, brokenColor, progress);
            }
            
            if (destroyTimer >= destroyTime)
            {
                CompleteDestruction();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDestroyed || isDestroying) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            StartDestruction();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDestroyed || isDestroying) return;

        if (other.CompareTag("Player"))
        {
            StartDestruction();
        }
    }

    #endregion

    #region 公开方法

    /// <summary>
    /// 开始破坏过程
    /// </summary>
    public void StartDestruction()
    {
        if (isDestroyed || isDestroying) return;

        isDestroying = true;

        if (destroyTime <= 0f)
        {
            CompleteDestruction();
        }
        else
        {
            Debug.Log($"BreakableWall: 开始破坏 {gameObject.name}");
        }
    }

    /// <summary>
    /// 立即破坏
    /// </summary>
    public void BreakImmediately()
    {
        destroyTime = 0f;
        CompleteDestruction();
    }

    /// <summary>
    /// 重置墙壁状态
    /// </summary>
    public void ResetWall()
    {
        isDestroyed = false;
        isDestroying = false;
        destroyTimer = 0f;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }

        if (wallCollider != null)
        {
            wallCollider.enabled = true;
        }

        if (hiddenContent != null && hideContentOnStart)
        {
            hiddenContent.SetActive(false);
        }

        gameObject.SetActive(true);
    }

    #endregion

    #region 私有方法

    /// <summary>
    /// 完成破坏
    /// </summary>
    private void CompleteDestruction()
    {
        if (isDestroyed) return;

        isDestroyed = true;
        isDestroying = false;

        Debug.Log($"BreakableWall: 墙壁破坏 {gameObject.name}");

        PlayBreakEffect();

        if (disableCollider && wallCollider != null)
        {
            wallCollider.enabled = false;
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.color = brokenColor;
        }

        if (hiddenContent != null)
        {
            hiddenContent.SetActive(true);
        }

        if (singleUse)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }
        }
    }

    /// <summary>
    /// 播放破坏特效
    /// </summary>
    private void PlayBreakEffect()
    {
        if (breakEffect == null) return;

        GameObject effect = Instantiate(breakEffect, transform.position, Quaternion.identity);
        Destroy(effect, effectDuration);
    }

    #endregion

    #region 编辑器可视化

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.5f, 0f, 0.5f);
        
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            Bounds bounds = collider.bounds;
            Gizmos.DrawCube(bounds.center, bounds.size);
        }
    }
#endif

    #endregion
}