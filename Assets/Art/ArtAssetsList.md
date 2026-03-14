# 美术素材需求清单

## 项目信息

- **项目名称**: How To Fly
- **项目类型**: 2D 平台跳跃游戏
- **分辨率参考**: 1920x1080
- **美术风格**: 手绘卡通风格
- **素材规格建议**: 128x128 px（角色） / 256x64 px（平台）

---

## 素材优先级说明

| 优先级 | 说明 |
|--------|------|
| 🔴 高 | 核心游戏体验必需 |
| 🟡 中 | 增强视觉效果 |
| 🟢 低 | 锦上添花 |

---

## 1. 玩家角色

### 1.1 主角精灵

| 名称 | 说明 | 规格 | 优先级 |
|------|------|------|--------|
| Player_Idle | 待机状态 | 128x128 px | 🔴 高 |
| Player_Walk | 行走动画（可选4-8帧） | 128x128 px | 🟡 中 |
| Player_Jump | 跳跃状态 | 128x128 px | 🔴 高 |
| Player_Fly | 飞行状态 | 128x128 px | 🔴 高 |

### 1.2 翅膀

| 名称 | 说明 | 规格 | 优先级 |
|------|------|------|--------|
| Wing_Immature | 未成熟翅膀（35-49 fly值）<br>半透明、小尺寸、浅蓝色 | 64x64 px | 🔴 高 |
| Wing_Mature | 成熟翅膀（50+ fly值）<br>不透明、完整、亮色 | 128x128 px | 🔴 高 |

---

## 2. 环境/平台

### 2.1 地面

| 名称 | 说明 | 规格 | 优先级 |
|------|------|------|--------|
| Ground_Tile | 地面平台纹理<br>可平铺（9-slice） | 256x64 px | 🔴 高 |
| Ground_Corner_Left | 地面左转角 | 64x64 px | 🟡 中 |
| Ground_Corner_Right | 地面右转角 | 64x64 px | 🟡 中 |

### 2.2 洞穴

| 名称 | 说明 | 规格 | 优先级 |
|------|------|------|--------|
| Cave_Wall | 洞穴墙壁/平台 | 128x64 px | 🟡 中 |
| Cave_Floor | 洞穴底部 | 256x64 px | 🟡 中 |
| Cave_Entrance | 洞穴入口装饰 | 128x128 px | 🟢 低 |
| Cave_Exit | 洞穴出口装饰 | 128x64 px | 🟢 低 |

### 2.3 特殊平台

| 名称 | 说明 | 规格 | 优先级 |
|------|------|------|--------|
| BouncePlatform | 弹跳平台（荷叶）<br>绿色、有弹性感 | 128x32 px | 🔴 高 |
| BreakableWall | 可破坏墙壁<br>有裂纹/可破坏外观 | 64x128 px | 🟡 中 |
| BreakableWall_Cracked | 破坏中状态（可选） | 64x128 px | 🟢 低 |

---

## 3. 收集物

### 3.1 Fly 值道具

| 名称 | 说明 | 规格 | 优先级 |
|------|------|------|--------|
| FlyItem_Small | 小型道具（+5 fly值）<br>发光能量球/羽毛 | 64x64 px | 🔴 高 |
| FlyItem_Large | 大型道具（+50 fly值）<br>更亮、更大、特殊外观 | 96x96 px | 🔴 高 |

### 3.2 道具动画（可选）

| 名称 | 说明 | 规格 | 优先级 |
|------|------|------|--------|
| FlyItem_Anim | 浮动/旋转动画（2-4帧） | 64x64 px | 🟡 中 |

---

## 4. 关卡元素

### 4.1 检查点

| 名称 | 说明 | 规格 | 优先级 |
|------|------|------|--------|
| Checkpoint_Inactive | 未激活状态<br>灰色/暗淡 | 64x128 px | 🟡 中 |
| Checkpoint_Active | 激活状态<br>亮色/发光 | 64x128 px | 🟡 中 |

### 4.2 终点

| 名称 | 说明 | 规格 | 优先级 |
|------|------|------|--------|
| LevelGoal | 关卡终点<br>金色/发光标记 | 128x128 px | 🟡 中 |
| LevelGoal_Glow | 发光效果叠加（可选） | 128x128 px | 🟢 低 |

### 4.3 死亡区域

| 名称 | 说明 | 规格 | 优先级 |
|------|------|------|--------|
| DeathZone_Spike | 尖刺装饰 | 64x32 px | 🟢 低 |
| DeathZone_Warning | 危险警告标志 | 128x128 px | 🟢 低 |

---

## 5. UI 界面

### 5.1 游戏标题

| 名称 | 说明 | 规格 | 优先级 |
|------|------|------|--------|
| Logo_Title | 游戏标题 "How To Fly"<br>艺术字/Logo | 512x128 px | 🔴 高 |

### 5.2 按钮

| 名称 | 说明 | 规格 | 优先级 |
|------|------|------|--------|
| Button_Normal | 按钮正常状态 | 320x80 px | 🟡 中 |
| Button_Hover | 按钮悬停状态 | 320x80 px | 🟡 中 |
| Button_Pressed | 按钮按下状态 | 320x80 px | 🟡 中 |

### 5.3 生命值

| 名称 | 说明 | 规格 | 优先级 |
|------|------|------|--------|
| Heart_Full | 满生命值心形 | 64x64 px | 🟡 中 |
| Heart_Empty | 空生命值心形 | 64x64 px | 🟡 中 |

### 5.4 面板背景

| 名称 | 说明 | 规格 | 优先级 |
|------|------|------|--------|
| Panel_Background | 半透明面板背景<br>可9-slice | 512x512 px | 🟢 低 |

### 5.5 Fly 值显示（可选）

| 名称 | 说明 | 规格 | 优先级 |
|------|------|------|--------|
| FlyBar_Background | 进度条背景 | 256x32 px | 🟢 低 |
| FlyBar_Fill | 进度条填充 | 256x32 px | 🟢 低 |
| FlyIcon | Fly 值图标 | 48x48 px | 🟢 低 |

---

## 6. 特效

### 6.1 粒子/特效图

| 名称 | 说明 | 规格 | 优先级 |
|------|------|------|--------|
| Effect_Break | 墙壁破碎碎片 | 32x32 px | 🟡 中 |
| Effect_Dust | 灰尘粒子 | 16x16 px | 🟡 中 |
| Effect_Sparkle | 收集闪光 | 32x32 px | 🟡 中 |
| Effect_Unlock | 解锁飞行光芒 | 128x128 px | 🟡 中 |
| Effect_Death | 死亡消散效果 | 64x64 px | 🟢 低 |

---

## 7. 背景

### 7.1 主背景

| 名称 | 说明 | 规格 | 优先级 |
|------|------|------|--------|
| BG_Sky | 天空背景<br>可平铺或单张 | 1920x1080 px | 🟡 中 |

### 7.2 视差层（可选）

| 名称 | 说明 | 规格 | 优先级 |
|------|------|------|--------|
| BG_Far | 远景层（云朵/山脉） | 1920x480 px | 🟢 低 |
| BG_Near | 近景层（树木/岩石） | 1920x320 px | 🟢 低 |

---

## 素材数量统计

| 分类 | 高优先级 | 中优先级 | 低优先级 | 合计 |
|------|----------|----------|----------|------|
| 玩家角色 | 4 | 1 | 0 | 5 |
| 环境/平台 | 2 | 4 | 3 | 9 |
| 收集物 | 2 | 1 | 0 | 3 |
| 关卡元素 | 0 | 4 | 4 | 8 |
| UI 界面 | 1 | 4 | 5 | 10 |
| 特效 | 0 | 4 | 1 | 5 |
| 背景 | 0 | 1 | 2 | 3 |
| **合计** | **9** | **19** | **15** | **43** |

---

## 素材导入建议

### 通用设置

```
Texture Type: Sprite (2D and UI)
Pixels Per Unit: 100
Filter Mode: Bilinear (平滑风格，适合手绘卡通)
Compression: None (最佳质量)
```

### 9-Slice 精灵设置

对于需要拉伸的平台/面板：

```
Border 设置: 根据素材边缘像素调整
例: Ground_Tile Border = 8,8,8,8
```

### 动画精灵

```
Multiple Sprites 模式
按帧数切割
Animation FPS 建议: 8-12
```

---

## 文件夹结构建议

```
Assets/
├── Art/
│   ├── Characters/
│   │   ├── Player/
│   │   └── Wings/
│   ├── Environment/
│   │   ├── Ground/
│   │   ├── Cave/
│   │   └── Platforms/
│   ├── Collectibles/
│   ├── LevelElements/
│   ├── UI/
│   ├── Effects/
│   └── Backgrounds/
└── Sprites/ (备用)
```

---

## 配色建议

| 元素 | 建议色系 |
|------|----------|
| 玩家 | 温暖色（橙/黄/白） |
| 翅膀（未成熟） | 浅蓝 #80B4FF，透明度 60% |
| 翅膀（成熟） | 亮蓝 #4D99FF，透明度 100% |
| 地面 | 棕色/绿色系 |
| 弹跳平台 | 绿色系 #7FFF7F |
| Fly 道具 | 金色/青色发光 |
| 检查点 | 灰 → 绿色 |
| 终点 | 金色 #FFD700 |
| 背景 | 渐变蓝/紫天空 |

---

## 备注

1. 所有素材建议使用 **PNG 格式**，支持透明通道
2. 手绘卡通风格使用 **Bilinear Filter**，保持边缘平滑
3. 动画帧数建议 **4-8 帧**，保证流畅同时减少工作量
4. 背景可使用 **渐变或平铺**，减少素材尺寸
5. 保持线条粗细一致，建议 **2-4px 描边**

---

## AI 素材生成指南

### 推荐工具

| 工具 | 适用场景 | 特点 |
|------|----------|------|
| **Geminibanana** | 角色、背景、道具 | 免费，易上手，适合手绘风格 |
| **Midjourney** | 角色、背景、概念图 | 艺术感强，风格多样 |
| **DALL-E 3** | UI图标、道具、Logo | 理解准确，适合具体描述 |
| **Leonardo.AI** | 游戏素材专用 | 支持透明背景，游戏素材优化 |
| **Stable Diffusion** | 批量生成 | 开源免费，可本地运行 |

---

### 通用风格关键词

在所有提示词中添加以下关键词保持风格一致：

```
[手绘卡通风格 - 基础]
hand-drawn cartoon style, 2D game asset, flat design, 
clean lines, vibrant colors, simple shapes, 
transparent background PNG, vector art style

[风格变体]
cute cartoon style        # 可爱卡通风格
flat design               # 扁平化设计
minimalist cartoon        # 极简卡通
whimsical illustration    # 异想天开插画风格
kid-friendly art style    # 儿童友好风格
```

---

### 提示词模板

#### 1. 玩家角色

```
[通用模板]
hand-drawn cartoon [角色描述], [姿态], front view, 
game character, 128x128 pixels, transparent background PNG, 
simple design, vibrant colors, flat design, clean black outline

[示例 - 待机状态]
hand-drawn cartoon cute small bird character standing idle, 
front view, game character, 128x128 pixels, transparent background PNG, 
orange and white feathers, simple design, flat design, clean black outline

[示例 - 飞行状态]
hand-drawn cartoon cute small bird flying with spread wings, 
side view, game character, 128x128 pixels, transparent background PNG, 
dynamic pose, glowing blue wings, flat design, clean black outline
```

#### 2. 翅膀

```
[未成熟翅膀]
hand-drawn cartoon small translucent light blue wings, 
64x64 pixels, transparent background PNG, 
immature appearance, 60% opacity, simple feathers, flat design

[成熟翅膀]
hand-drawn cartoon large majestic bright blue wings, 
128x128 pixels, transparent background PNG, 
mature appearance, fully opaque, detailed feathers, glowing effect, flat design
```

#### 3. 平台/地面

```
[通用模板]
hand-drawn cartoon [平台描述], side view, 
seamless tileable, 256x64 pixels, transparent background PNG, 
game platform asset, simple texture, flat design

[示例 - 地面]
hand-drawn cartoon grass and dirt ground platform, side view, 
seamless tileable, 256x64 pixels, transparent background PNG, 
brown earth with green grass on top, game platform asset, flat design

[示例 - 弹跳平台（荷叶）]
hand-drawn cartoon green lily pad, top-down view, 
128x32 pixels, transparent background PNG, 
bouncy appearance, game platform asset, vibrant green, flat design
```

#### 4. 洞穴素材

```
[洞穴墙壁]
hand-drawn cartoon dark rocky cave wall, side view, 
128x64 pixels, transparent background PNG, 
mossy rocks, dark blue-gray tones, game environment, flat design

[洞穴平台]
hand-drawn cartoon stone platform inside cave, 
128x64 pixels, transparent background PNG, 
rough rocky texture, dark colors, game platform, flat design
```

#### 5. 收集物

```
[小型道具]
hand-drawn cartoon glowing golden energy orb, 
64x64 pixels, transparent background PNG, 
floating, magical aura, game collectible, bright colors, flat design

[大型道具]
hand-drawn cartoon large glowing cyan crystal, 
96x96 pixels, transparent background PNG, 
powerful appearance, magical effect, game collectible, flat design
```

#### 6. UI 素材

```
[按钮]
hand-drawn cartoon button for game UI, "Start Game" text area, 
320x80 pixels, rounded corners, blue gradient background, 
three states: normal, hover, pressed, simple design, flat design

[心形图标]
hand-drawn cartoon heart icon for health UI, 64x64 pixels, 
red heart shape, simple design, game UI element, flat design, 
two versions: full red and empty outline
```

#### 7. 背景

```
[天空背景]
hand-drawn cartoon sky background, gradient from light blue to purple, 
1920x1080 pixels, soft clouds, sunset atmosphere, 
game background, peaceful mood, no characters, flat design

[远景层]
hand-drawn cartoon parallax background layer, distant mountains and clouds, 
1920x480 pixels, pastel colors, game background, 
silhouette style, simple shapes, flat design
```

---

### 尺寸配置建议

| AI工具 | 推荐设置 |
|--------|----------|
| **Geminibanana** | 说明 "transparent background PNG" + 具体尺寸 + "hand-drawn cartoon style" |
| **Midjourney** | `--ar 1:1` (角色) / `--ar 4:1` (平台) + `--no background` |
| **DALL-E 3** | 明确说明 "transparent background PNG" + 尺寸 |
| **Leonardo.AI** | 选择 "Game Asset" 预设 + 透明背景选项 |

**具体尺寸配置**:

```
角色/道具: 512x512 → 后期裁剪到 128x128
平台: 512x256 → 后期裁剪到实际尺寸
背景: 1920x1080 或 1024x576
UI: 根据实际需求设置
```

---

### 风格一致性检查清单

生成每批素材后检查：

| 检查项 | 要求 |
|--------|------|
| ✓ 色调一致 | 所有素材使用相同色板 |
| ✓ 线条风格 | 统一的描边粗细（建议 2-4px） |
| ✓ 边缘处理 | 干净的黑色描边，无模糊 |
| ✓ 透视角度 | 统一的视角（侧视/俯视/正视） |
| ✓ 光源方向 | 所有素材光源来自同一方向 |
| ✓ 对比度 | 背景与前景有足够区分度 |
| ✓ 色彩饱和度 | 统一的鲜艳程度 |

---

### 后处理工作流

```
1. AI生成素材
     ↓
2. 裁剪到目标尺寸
     ↓
3. 去除/修复背景（如需要）
     ↓
4. 调整色彩饱和度/对比度
     ↓
5. 统一线条粗细和风格
     ↓
6. 导出 PNG（透明背景）
     ↓
7. 导入 Unity 并配置
```

### 推荐后处理工具

| 工具 | 用途 |
|------|------|
| **Photoshop/GIMP** | 高级编辑、批量处理、描边统一 |
| **Photopea** | 在线免费编辑，支持 PSD |
| **Figma** | 矢量风格编辑，适合 UI |
| **Pixlr** | 在线快速编辑 |
| **Aseprite** | 动画制作、帧编辑 |

---

### 批量生成提示

**一次生成多个素材**:

```
[Geminibanana 示例]
hand-drawn cartoon game assets sheet, includes: 
1. cute bird character idle pose, 
2. cute bird character jumping pose, 
3. cute bird character flying pose, 
4. small translucent blue wings, 
5. large bright blue wings, 
all on transparent background, 512x512 sprite sheet, flat design
```

**分批生成策略**:

| 批次 | 内容 | 原因 |
|------|------|------|
| 第1批 | 玩家+翅膀 | 风格统一最重要 |
| 第2批 | 地面+平台 | 环境基调 |
| 第3批 | 收集物+关卡元素 | 与角色风格匹配 |
| 第4批 | UI+特效 | 配合整体风格 |
| 第5批 | 背景 | 最后定调 |

---

### 常见问题解决

| 问题 | 解决方案 |
|------|----------|
| 背景不透明 | 使用 Photoshop/Photopea 去除背景，或使用 Leonardo.AI 透明背景功能 |
| 尺寸不合适 | 用 Photoshop 裁剪或缩放 |
| 风格不统一 | 固定使用同一组关键词，保持提示词结构一致 |
| 边缘模糊 | 使用锐化滤镜，或在编辑软件中重绘描边 |
| 颜色太暗/太亮 | 调整饱和度和对比度，统一色板 |
| 线条粗细不一 | 使用描边滤镜统一添加黑色描边 |
| 风格太复杂 | 添加 "simple design, minimal details" 关键词 |

---

### 色板参考

为保持风格一致，建议使用固定色板：

```
[角色色板]
#FFFFFF (白)    #FFD700 (金)    #FF8C00 (橙)    #FF4500 (红橙)
#4D99FF (亮蓝)  #80B4FF (浅蓝)  #00CED1 (青)

[环境色板]
#8B4513 (棕)    #228B22 (深绿)  #90EE90 (浅绿)  #696969 (灰)
#2F4F4F (深灰)  #D2691E (土黄)

[UI色板]
#FF0000 (红)    #00FF00 (绿)    #0000FF (蓝)    #FFFF00 (黄)
#000000 (黑)    #FFFFFF (白)
```

将色板保存为图片，在生成时作为参考图输入。