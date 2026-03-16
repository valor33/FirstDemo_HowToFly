# AGENTS.md - FirstDemo_HowToFly Unity 项目指南

## 项目概述

Unity 2D 平台跳跃游戏 Demo，玩家通过跳跃积累 fly 值，解锁飞行能力。

- **引擎**: Unity 2022.3.62f3
- **语言**: C# 9.0
- **目标框架**: .NET Standard 2.1
- **渲染管线**: Built-in Render Pipeline

---

## 构建与测试

本项目使用 Unity 编辑器进行构建，无命令行构建脚本。

**代码验证**: Unity 保存 .cs 文件后自动编译，检查 Console 窗口查看错误。

**手动测试流程**:
1. 打开 `Assets/Scenes/SampleScene.unity`
2. 进入 Play 模式
3. A/D 或 ←/→ 移动 | Space 跳跃/飞行 | ESC 暂停

---

## 项目结构

```
Assets/Scripts/
├── Player/
│   ├── PlayerController.cs   # 移动、翻转、地面检测
│   ├── PlayerJump.cs         # 跳跃逻辑、fly 值积累
│   ├── PlayerFlight.cs       # 飞行能力管理
│   ├── PlayerHealth.cs       # 生命值、受伤、无敌
│   ├── PlayerAnimation.cs    # 颜色状态变化
│   └── AuraVisual.cs         # 光晕视觉效果
├── Level/
│   ├── PlayerRespawn.cs      # 重生逻辑
│   ├── DeathZone.cs          # 死亡区域触发
│   ├── LevelGoal.cs          # 关卡终点胜利
│   ├── Checkpoint.cs         # 检查点重生
│   ├── BreakableWall.cs      # 可破坏墙壁
│   └── CaveExit.cs           # 洞穴出口
├── Platforms/
│   └── BouncePlatform.cs     # 弹跳平台
├── Collectibles/
│   ├── ICollectible.cs       # 收集物接口
│   └── FlyValueItem.cs       # fly 值道具
├── Camera/
│   └── CameraFollow.cs       # 相机跟随
└── UI/
    ├── UIManager.cs          # UI 管理器（单例）
    ├── GameUI.cs             # 游戏中 UI
    ├── StartPanel.cs         # 开始界面
    ├── VictoryPanel.cs       # 胜利界面
    └── GameOverPanel.cs      # 结束界面
```

---

## 代码风格规范

### 命名约定

| 类型 | 规范 | 示例 |
|------|------|------|
| 类名/公开成员 | PascalCase | `PlayerController`, `IsGrounded` |
| 私有字段 | camelCase | `isGrounded`, `currentHealth` |
| 序列化字段 | camelCase + `[SerializeField]` | `[SerializeField] private float moveSpeed;` |
| 常量 | PascalCase | `MaxFlyValue` |
| 事件 | PascalCase | `OnGameStart`, `OnHealthChanged` |

### 代码组织

使用 `#region` 分块，顺序：序列化字段 → 私有字段 → 公开属性 → 公开事件 → Unity 生命周期 → 公开方法 → 私有方法。

```csharp
public class Example : MonoBehaviour
{
    #region 序列化字段
    [Header("分组标题")]
    [Tooltip("参数说明")]
    [SerializeField] private float paramName = 5f;
    #endregion

    #region 私有字段
    private bool isSomething;
    #endregion

    #region 公开属性
    public bool IsSomething => isSomething;
    #endregion

    #region 公开事件
    public event System.Action OnSomethingChanged;
    #endregion

    #region Unity 生命周期方法
    private void Awake() { }
    private void Update() { }
    private void FixedUpdate() { }
    #endregion

    #region 公开方法
    public void DoSomething() { }
    #endregion

    #region 私有方法
    private void HandleSomething() { }
    #endregion
}
```

### Unity 组件引用

```csharp
private Rigidbody2D rb;

private void Awake()
{
    rb = GetComponent<Rigidbody2D>();
    if (rb == null)
    {
        Debug.LogError("ClassName: 缺少 Rigidbody2D 组件！");
    }
}
```

### 输入处理

- **Update**: 输入检测、状态更新
- **FixedUpdate**: 物理操作、刚体移动

### using 导入

仅导入必要的命名空间：

```csharp
using UnityEngine;
using UnityEngine.UI;           // 仅在使用 UI 时
using UnityEngine.Events;       // 仅在使用 UnityEvent 时
using System.Collections;       // 仅在使用协程时
using TMPro;                    // 仅在使用 TextMesh Pro 时
```

### 注释规范

- 使用 XML 文档注释类和公开方法
- 不添加行内注释，代码应自解释
- 注释使用中文

```csharp
/// <summary>
/// 玩家主控制器
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 禁用玩家控制
    /// </summary>
    public void DisableControl() { }
}
```

### 调试与日志

```csharp
Debug.Log($"ClassName: 操作描述，相关值: {value}");     // 信息
Debug.LogWarning("ClassName: 警告信息");               // 警告
Debug.LogError("ClassName: 错误信息");                  // 错误
```

---

## 设计模式

### 事件与委托

**C# 事件**（用于代码内部通信）:

```csharp
public event System.Action OnGameStart;
public event System.Action<int, int> OnHealthChanged;

// 触发
OnGameStart?.Invoke();
OnHealthChanged?.Invoke(currentHealth, maxHealth);
```

**UnityEvent**（可在 Inspector 中配置）:

```csharp
public UnityEvent OnHurt;
public UnityEvent<int, int> OnHealthChanged;

// 触发
OnHurt?.Invoke();
OnHealthChanged?.Invoke(currentHealth, maxHealth);
```

**自定义委托**:

```csharp
public delegate void AuraStateChangedHandler(AuraState newState);
public event AuraStateChangedHandler OnAuraStateChanged;
```

### 单例模式

```csharp
public class UIManager : MonoBehaviour
{
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
}
```

### 协程

```csharp
public void ShowTip(string message, float duration)
{
    StartCoroutine(ShowTipCoroutine(message, duration));
}

private IEnumerator ShowTipCoroutine(string message, float duration)
{
    tipPanel.SetActive(true);
    yield return new WaitForSeconds(duration);
    tipPanel.SetActive(false);
}
```

### 编辑器专用代码

使用条件编译包裹编辑器可视化代码：

```csharp
#if UNITY_EDITOR
private void OnDrawGizmosSelected()
{
    Gizmos.color = Color.green;
    Gizmos.DrawWireCube(checkPosition, size);
}
#endif
```

---

## 常见问题

| 问题 | 解决方案 |
|------|----------|
| 编译错误 | 检查 Unity Console 窗口 |
| 组件引用为空 | 确保 `GetComponent` 在 `Awake` 中调用 |
| 地面检测失效 | 检查 Ground Layer 设置和地面对象 Layer |
| 事件未触发 | 确保使用 `?.Invoke()` 空检查 |

---

## 关键 Layers 和 Tags

- **Player Tag**: 用于标识玩家对象
- **Ground Layer**: 用于地面检测（LayerMask）