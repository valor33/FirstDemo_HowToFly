# AGENTS.md - FirstDemo_HowToFly Unity 项目指南

## 项目概述

这是一个 Unity 2D 平台跳跃游戏 Demo，玩家通过跳跃积累 fly 值，解锁飞行能力。

- **引擎**: Unity 2022.x
- **语言**: C#
- **渲染管线**: Built-in Render Pipeline
- **目标平台**: Desktop (Windows/Mac/Linux)

---

## 构建与测试命令

### Unity 编辑器操作

本项目使用 Unity 编辑器进行构建，无命令行构建脚本。

**常用操作**：
- 打开场景: `Assets/Scenes/SampleScene.unity`
- 运行游戏: Unity 编辑器中的 Play 按钮
- 构建: `File > Build Settings > Build`

### 代码编译

Unity 自动编译 C# 脚本，无需手动编译。

**代码验证方式**：
1. Unity 编辑器 Console 窗口查看编译错误
2. 等待 Unity 自动重新编译（保存 .cs 文件后）

### 测试

当前项目无自动化测试框架。

**手动测试流程**：
1. 在 Unity 编辑器中打开场景
2. 进入 Play 模式
3. 按 A/D 或 ←/→ 移动角色
4. 按 Space 跳跃
5. 积累 fly 值后按住 Space 飞行

---

## 项目结构

```
Assets/
├── Scripts/
│   └── Player/
│       ├── PlayerController.cs   # 主控制器：移动、翻转、地面检测
│       ├── PlayerJump.cs         # 跳跃逻辑、fly 值积累
│       ├── PlayerFlight.cs       # 飞行能力管理
│       └── PlayerAnimation.cs    # 颜色状态变化
├── Scenes/
│   └── SampleScene.unity         # 主场景
└── c#/
    └── NewBehaviourScript.cs     # (可删除) 默认模板
```

---

## 代码风格规范

### 1. 命名约定

| 类型 | 规范 | 示例 |
|------|------|------|
| 类名 | PascalCase | `PlayerController` |
| 公开属性/方法 | PascalCase | `IsGrounded`, `AddFlyValue()` |
| 私有字段 | camelCase | `isGrounded`, `currentFlyValue` |
| 序列化字段 | camelCase | `moveSpeed`, `jumpForce` |
| 常量 | PascalCase 或 UPPER_CASE | `MaxFlyValue` |
| 参数 | camelCase | `amount`, `force` |

### 2. Unity 组件引用

```csharp
// 使用 GetComponent 在 Awake 中获取引用
private Rigidbody2D rb;

private void Awake()
{
    rb = GetComponent<Rigidbody2D>();
}

// 序列化字段用于 Inspector 配置或外部引用
[SerializeField] private PlayerController playerController;
```

### 3. 代码组织

使用 `#region` 组织代码块：

```csharp
public class Example : MonoBehaviour
{
    #region 序列化字段（在 Inspector 中可配置）
    
    [Header("参数分组")]
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
    private void Start() { }
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

### 4. 注释规范

**中文注释**，面向新手开发者：

```csharp
/// <summary>
/// 玩家主控制器
/// 负责处理玩家的移动、翻转、地面检测等核心功能
/// </summary>
public class PlayerController : MonoBehaviour
{
    // 获取水平输入（A/D 或 左/右 方向键）
    float input = Input.GetAxis("Horizontal");
    
    // 只有在地面上才能跳跃
    if (!isGrounded) return;
}
```

### 5. Inspector 属性

```csharp
[Header("参数分组标题")]           // 创建分组标题
[Tooltip("参数说明文字")]          // 鼠标悬停显示说明
[Range(0f, 100f)]                 // 创建滑动条
[SerializeField] private float value;
```

### 6. 错误处理

```csharp
private void Awake()
{
    rb = GetComponent<Rigidbody2D>();
    
    // 必要组件检查
    if (rb == null)
    {
        Debug.LogError("PlayerController: 缺少 Rigidbody2D 组件！");
    }
    
    // 可选组件检查
    if (spriteRenderer == null)
    {
        Debug.LogWarning("PlayerController: 缺少 SpriteRenderer 组件。");
    }
}
```

### 7. 输入处理

使用旧 Input Manager：

```csharp
// 输入检测在 Update 中
private void Update()
{
    horizontalInput = Input.GetAxis("Horizontal");
    if (Input.GetKeyDown(KeyCode.Space)) { }
}

// 物理操作在 FixedUpdate 中
private void FixedUpdate()
{
    rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
}
```

### 8. using 语句

仅导入必要的命名空间：

```csharp
using UnityEngine;
using UnityEngine.UI;        // 仅在使用 UI 时添加
using System.Collections;     // 仅在使用协程时添加
```

---

## MonoBehaviour 开发规范

### 生命周期顺序

```
Awake()     → 组件引用初始化
OnEnable()  → 对象激活时
Start()     → 其他初始化（依赖其他组件时）
Update()    → 输入检测、状态更新
FixedUpdate() → 物理操作、刚体移动
LateUpdate() → 相机跟随、后期处理
OnDisable() → 对象禁用时
OnDestroy() → 对象销毁时
```

### 地面检测

使用 BoxCast 射线检测：

```csharp
private void CheckGrounded()
{
    Vector2 checkPosition = (Vector2)transform.position - new Vector2(0f, groundCheckOffset);
    isGrounded = Physics2D.BoxCast(
        checkPosition,
        groundCheckBoxSize,
        0f,
        Vector2.down,
        0.1f,
        groundLayer
    );
}
```

### 调试可视化

```csharp
private void OnDrawGizmosSelected()
{
    Gizmos.color = Color.green;
    Gizmos.DrawWireCube(checkPosition, groundCheckBoxSize);
}
```

---

## 常见问题

### 编译错误

Unity 保存文件后自动编译，检查 Console 窗口。

### 组件引用为空

确保 `GetComponent` 在 `Awake` 中调用，或使用 `[SerializeField]` 在 Inspector 中赋值。

### 地面检测失效

1. 检查 Ground Layer 是否正确设置
2. 确认地面对象的 Layer 已设为 Ground
3. 查看 Scene 视图中的 Gizmos 调试线

---

## 待开发功能

- [ ] 平台交互模块（移动平台、弹跳平台）
- [ ] 收集系统（道具增加 fly 值）
- [ ] 挑战任务系统
- [ ] 区域触发解锁
- [ ] UI 系统（fly 值显示）
- [ ] 关卡原型（死亡重生、终点检测）