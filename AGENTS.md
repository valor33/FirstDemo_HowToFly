# AGENTS.md - FirstDemo_HowToFly Unity 项目指南

## 项目概述

Unity 2D 平台跳跃游戏 Demo，玩家通过跳跃积累 fly 值，解锁飞行能力。

- **引擎**: Unity 2022.x | **语言**: C# | **渲染管线**: Built-in Render Pipeline

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
│   └── WingsVisual.cs        # 翅膀视觉效果
├── Level/
│   ├── PlayerRespawn.cs      # 重生逻辑
│   ├── DeathZone.cs          # 死亡区域触发
│   ├── LevelGoal.cs          # 关卡终点胜利
│   ├── Checkpoint.cs         # 检查点重生
│   └── BreakableWall.cs      # 可破坏墙壁
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
using System.Collections;       // 仅在使用协程时
```

### 注释

中文注释，使用 XML 文档注释类和公开方法：

```csharp
/// <summary>
/// 玩家主控制器
/// </summary>
public class PlayerController : MonoBehaviour { }
```

### 调试可视化

```csharp
private void OnDrawGizmosSelected()
{
    Gizmos.color = Color.green;
    Gizmos.DrawWireCube(checkPosition, size);
}
```

---

## 常见问题

| 问题 | 解决方案 |
|------|----------|
| 编译错误 | 检查 Unity Console 窗口 |
| 组件引用为空 | 确保 `GetComponent` 在 `Awake` 中调用 |
| 地面检测失效 | 检查 Ground Layer 设置和地面对象 Layer |

---

## 待开发功能

- [ ] 移动平台
- [ ] 挑战任务系统
- [ ] 区域触发解锁
- [ ] 音效系统
- [ ] 存档系统