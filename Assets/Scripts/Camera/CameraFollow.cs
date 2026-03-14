using UnityEngine;

/// <summary>
/// 相机跟随控制器
/// 平滑跟随目标对象，支持边界限制
/// </summary>
public class CameraFollow : MonoBehaviour
{
    #region 序列化字段

    [Header("跟随目标")]
    [Tooltip("跟随的目标对象（玩家）")]
    [SerializeField] private Transform target;

    [Tooltip("相机偏移量")]
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);

    [Header("跟随参数")]
    [Tooltip("跟随速度")]
    [SerializeField] private float followSpeed = 5f;

    [Tooltip("是否跟随 X 轴")]
    [SerializeField] private bool followX = true;

    [Tooltip("是否跟随 Y 轴")]
    [SerializeField] private bool followY = true;

    [Header("边界限制")]
    [Tooltip("是否启用边界限制")]
    [SerializeField] private bool useBounds = true;

    [Tooltip("X 轴最小值")]
    [SerializeField] private float minX = -20f;

    [Tooltip("X 轴最大值")]
    [SerializeField] private float maxX = 20f;

    [Tooltip("Y 轴最小值")]
    [SerializeField] private float minY = -10f;

    [Tooltip("Y 轴最大值")]
    [SerializeField] private float maxY = 10f;

    #endregion

    #region 私有字段

    private Camera cam;
    private float halfWidth;
    private float halfHeight;

    #endregion

    #region Unity 生命周期方法

    private void Awake()
    {
        cam = GetComponent<Camera>();
        
        if (cam != null && cam.orthographic)
        {
            halfHeight = cam.orthographicSize;
            halfWidth = halfHeight * cam.aspect;
        }
    }

    private void Start()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
                Debug.Log("CameraFollow: 自动找到玩家目标");
            }
        }
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = CalculateTargetPosition();

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );
    }

    #endregion

    #region 公开方法

    /// <summary>
    /// 设置跟随目标
    /// </summary>
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    /// <summary>
    /// 设置边界范围
    /// </summary>
    public void SetBounds(float newMinX, float newMaxX, float newMinY, float newMaxY)
    {
        minX = newMinX;
        maxX = newMaxX;
        minY = newMinY;
        maxY = newMaxY;
        useBounds = true;
    }

    /// <summary>
    /// 立即移动到目标位置
    /// </summary>
    public void SnapToTarget()
    {
        if (target == null) return;

        transform.position = CalculateTargetPosition();
    }

    #endregion

    #region 私有方法

    /// <summary>
    /// 计算目标位置
    /// </summary>
    private Vector3 CalculateTargetPosition()
    {
        Vector3 targetPos = transform.position;

        if (followX)
        {
            targetPos.x = target.position.x + offset.x;
        }

        if (followY)
        {
            targetPos.y = target.position.y + offset.y;
        }

        targetPos.z = offset.z;

        if (useBounds)
        {
            targetPos.x = Mathf.Clamp(targetPos.x, minX + halfWidth, maxX - halfWidth);
            targetPos.y = Mathf.Clamp(targetPos.y, minY + halfHeight, maxY - halfHeight);
        }

        return targetPos;
    }

    #endregion

    #region 编辑器可视化

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (!useBounds) return;

        Gizmos.color = new Color(0f, 1f, 0f, 0.3f);

        float width = maxX - minX;
        float height = maxY - minY;
        Vector3 center = new Vector3((minX + maxX) / 2f, (minY + maxY) / 2f, 0f);

        Gizmos.DrawCube(center, new Vector3(width, height, 0.1f));

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, new Vector3(width, height, 0.1f));
    }
#endif

    #endregion
}