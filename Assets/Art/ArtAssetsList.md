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

---

## 定制化提示词方案（Geminibanana）

> **风格定义**：背景采用极乐迪斯科风格（油画质感、复古、有光影深度），人物采用抽象派风格（几何化、简约、象征性）
>
> **红色内容**为可自定义替换的部分

---

### 风格关键词速查

```
[极乐迪斯科风格 - 用于环境/背景]
Disco Elysium style, oil painting texture, atmospheric lighting, 
warm muted colors, painterly brushstrokes, vintage aesthetic

[抽象派风格 - 用于角色/UI]
abstract minimalist, geometric shapes, simple silhouette, 
simplified form, expressive but minimal details
```

---

### 1. 玩家角色（抽象派）

#### 1.1 主角概念图（确定造型）

**英文提示词**：
```
abstract minimalist <font color="red">bird-like creature</font> concept design, 
geometric shapes, simple silhouette, <font color="red">warm orange and white</font> color palette, 
indie game character, front view, solid background for reference, 
oil painting texture influence, expressive but simplified form
```

**中文翻译**：
抽象极简风格的 <font color="red">鸟形生物</font> 概念设计，几何形状，简约剪影，<font color="red">温暖的橙色和白色</font> 色调，独立游戏角色，正视图，纯色背景作为参考，油画质感影响，富有表现力但简化的形态。

**可自定义内容**：
| 原内容 | 可替换为 |
|--------|----------|
| <font color="red">bird-like creature</font> | spirit creature, ghost character, winged blob, feathered being 等 |
| <font color="red">warm orange and white</font> | cool blue and white, golden and cream, red and black 等 |

---

#### 1.2 Player_Idle（待机状态）

**英文提示词**：
```
abstract minimalist <font color="red">bird creature</font> standing idle, 
geometric shapes, simple silhouette, <font color="red">orange white cream</font> color, 
128x128 pixel sprite, transparent background PNG, side view, 
clean edges, indie game asset, no background
```

**中文翻译**：
抽象极简风格的 <font color="red">鸟形生物</font> 静止站立，几何形状，简约剪影，<font color="red">橙白奶油色</font>，128x128像素精灵，透明背景PNG，侧视图，干净边缘，独立游戏素材，无背景。

---

#### 1.3 Player_Jump（跳跃状态）

**英文提示词**：
```
abstract minimalist <font color="red">bird creature</font> jumping pose, 
geometric shapes, dynamic silhouette, <font color="red">orange white cream</font> color, 
128x128 pixel sprite, transparent background PNG, side view, 
upward motion, stretched form, indie game asset, no background
```

**中文翻译**：
抽象极简风格的 <font color="red">鸟形生物</font> 跳跃姿态，几何形状，动态剪影，<font color="red">橙白奶油色</font>，128x128像素精灵，透明背景PNG，侧视图，向上运动，拉伸形态，独立游戏素材，无背景。

---

#### 1.4 Player_Fly（飞行状态）

**英文提示词**：
```
abstract minimalist <font color="red">bird creature</font> flying, 
geometric shapes, spread wings silhouette, <font color="red">orange white cream</font> color, 
128x128 pixel sprite, transparent background PNG, side view, 
<font color="red">glowing effect</font> around body, indie game asset, no background
```

**中文翻译**：
抽象极简风格的 <font color="red">鸟形生物</font> 飞行中，几何形状，展开翅膀剪影，<font color="red">橙白奶油色</font>，128x128像素精灵，透明背景PNG，侧视图，身体周围<font color="red">发光效果</font>，独立游戏素材，无背景。

**可自定义内容**：
| 原内容 | 可替换为 |
|--------|----------|
| <font color="red">glowing effect</font> | aura effect, sparkles, energy trail, light particles 等 |

---

#### 1.5 Player_Walk（行走动画4帧）

**英文提示词**：
```
abstract minimalist <font color="red">bird creature</font> walking animation, 
<font color="red">4</font> frame sprite sheet, geometric shapes, <font color="red">orange white cream</font> color, 
each frame 128x128 pixel, transparent background PNG, side view, 
walking cycle animation, indie game asset, no background, horizontal sprite strip
```

**中文翻译**：
抽象极简风格的 <font color="red">鸟形生物</font> 行走动画，<font color="red">4</font>帧精灵图，几何形状，<font color="red">橙白奶油色</font>，每帧128x128像素，透明背景PNG，侧视图，行走循环动画，独立游戏素材，无背景，水平精灵条。

**可自定义内容**：
| 原内容 | 可替换为 |
|--------|----------|
| <font color="red">4</font> frame | 6 frame, 8 frame（帧数越多动画越流畅） |

---

### 2. 翅膀（抽象派）

#### 2.1 Wing_Immature（未成熟翅膀，35-49 fly值）

**英文提示词**：
```
abstract minimalist <font color="red">small translucent wings</font>, 
geometric <font color="red">feather</font> shapes, <font color="red">light blue #80B4FF</font>, <font color="red">60%</font> opacity, 
64x64 pixel sprite, transparent background PNG, 
glowing ethereal effect, simple design, indie game asset, no background
```

**中文翻译**：
抽象极简风格的 <font color="red">小型半透明翅膀</font>，几何化<font color="red">羽毛</font>形状，<font color="red">浅蓝色 #80B4FF</font>，<font color="red">60%</font>透明度，64x64像素精灵，透明背景PNG，发光空灵效果，简约设计，独立游戏素材，无背景。

**可自定义内容**：
| 原内容 | 可替换为 |
|--------|----------|
| <font color="red">small translucent wings</font> | tiny ethereal wings, mini spirit wings, ghostly wings 等 |
| <font color="red">feather</font> | crystal, energy, light, geometric 等 |
| <font color="red">light blue #80B4FF</font> | cyan #00CED1, pale gold #FFD700, lavender #E6E6FA 等 |
| <font color="red">60%</font> opacity | 40% opacity, 50% opacity 等 |

---

#### 2.2 Wing_Mature（成熟翅膀，50+ fly值）

**英文提示词**：
```
abstract minimalist <font color="red">majestic wings</font>, 
geometric <font color="red">feather</font> shapes, <font color="red">bright blue #4D99FF</font>, fully opaque, 
128x128 pixel sprite, transparent background PNG, 
glowing radiant effect, detailed but simplified, indie game asset, no background
```

**中文翻译**：
抽象极简风格的 <font color="red">宏伟翅膀</font>，几何化<font color="red">羽毛</font>形状，<font color="red">亮蓝色 #4D99FF</font>，完全不透明，128x128像素精灵，透明背景PNG，发光辐射效果，细节但简化，独立游戏素材，无背景。

**可自定义内容**：
| 原内容 | 可替换为 |
|--------|----------|
| <font color="red">majestic wings</font> | powerful wings, large angelic wings, grand spirit wings 等 |
| <font color="red">bright blue #4D99FF</font> | golden #FFD700, cyan #00CED1, purple #9370DB 等 |

---

### 3. 环境/平台（极乐迪斯科风格）

#### 3.1 Ground_Tile（地面平台）

**英文提示词**：
```
Disco Elysium style game platform, <font color="red">grass and dirt ground</font>, 
oil painting texture, <font color="red">warm earth tones brown and green</font>, 
seamless tileable, 256x64 pixel, transparent background PNG, 
side view, atmospheric lighting, indie game asset, no background
```

**中文翻译**：
极乐迪斯科风格游戏平台，<font color="red">草地和泥土地面</font>，油画质感，<font color="red">温暖的土褐色和绿色</font>，无缝平铺，256x64像素，透明背景PNG，侧视图，大气光影，独立游戏素材，无背景。

**可自定义内容**：
| 原内容 | 可替换为 |
|--------|----------|
| <font color="red">grass and dirt ground</font> | stone floor, wooden planks, mossy rocks, sandy desert 等 |
| <font color="red">warm earth tones brown and green</font> | cool blue-gray tones, dark purple tones, golden autumn tones 等 |

---

#### 3.2 Ground_Corner（地面转角）

**英文提示词**：
```
Disco Elysium style ground corner piece, <font color="red">grass and dirt</font>, 
oil painting texture, <font color="red">warm earth tones</font>, 
64x64 pixel, transparent background PNG, 
side view, indie game asset, no background
```

**中文翻译**：
极乐迪斯科风格地面转角块，<font color="red">草地和泥土</font>，油画质感，<font color="red">温暖土色调</font>，64x64像素，透明背景PNG，侧视图，独立游戏素材，无背景。

---

#### 3.3 BouncePlatform（弹跳平台/荷叶）

**英文提示词**：
```
Disco Elysium style <font color="red">lily pad</font> platform, bouncy appearance, 
oil painting texture, <font color="red">vibrant green with warm highlights</font>, 
128x32 pixel, transparent background PNG, 
top-down view, indie game asset, no background
```

**中文翻译**：
极乐迪斯科风格 <font color="red">荷叶</font> 平台，有弹性的外观，油画质感，<font color="red">鲜艳绿色配温暖高光</font>，128x32像素，透明背景PNG，俯视图，独立游戏素材，无背景。

**可自定义内容**：
| 原内容 | 可替换为 |
|--------|----------|
| <font color="red">lily pad</font> | mushroom, trampoline, cloud, crystal pad 等 |
| <font color="red">vibrant green with warm highlights</font> | blue with golden glow, purple with pink tips 等 |

---

#### 3.4 Cave_Wall（洞穴墙壁）

**英文提示词**：
```
Disco Elysium style cave wall, <font color="red">dark rocky texture</font>, 
oil painting texture, <font color="red">blue-gray mossy tones</font>, atmospheric shadows, 
128x64 pixel, transparent background PNG, 
side view, indie game asset, no background
```

**中文翻译**：
极乐迪斯科风格洞穴墙壁，<font color="red">深色岩石纹理</font>，油画质感，<font color="red">蓝灰色苔藓色调</font>，大气阴影，128x64像素，透明背景PNG，侧视图，独立游戏素材，无背景。

**可自定义内容**：
| 原内容 | 可替换为 |
|--------|----------|
| <font color="red">dark rocky texture</font> | crystal cave, ice cave, ancient stone, brick wall 等 |
| <font color="red">blue-gray mossy tones</font> | warm brown tones, purple crystal tones, deep red tones 等 |

---

#### 3.5 Cave_Floor（洞穴地面）

**英文提示词**：
```
Disco Elysium style cave floor platform, <font color="red">dark stone</font>, 
oil painting texture, <font color="red">blue-gray tones</font>, atmospheric lighting, 
256x64 pixel, transparent background PNG, 
side view, indie game asset, no background
```

**中文翻译**：
极乐迪斯科风格洞穴地面平台，<font color="red">深色石头</font>，油画质感，<font color="red">蓝灰色调</font>，大气光影，256x64像素，透明背景PNG，侧视图，独立游戏素材，无背景。

---

#### 3.6 BreakableWall（可破坏墙壁）

**英文提示词**：
```
Disco Elysium style cracked <font color="red">stone wall</font>, breakable appearance, 
oil painting texture, visible cracks and fissures, <font color="red">brown gray tones</font>, 
64x128 pixel, transparent background PNG, 
side view, indie game asset, no background
```

**中文翻译**：
极乐迪斯科风格破裂的 <font color="red">石墙</font>，可破坏外观，油画质感，可见裂缝和裂隙，<font color="red">褐灰色调</font>，64x128像素，透明背景PNG，侧视图，独立游戏素材，无背景。

**可自定义内容**：
| 原内容 | 可替换为 |
|--------|----------|
| <font color="red">stone wall</font> | brick wall, wooden barrier, ice wall, crystal wall 等 |
| <font color="red">brown gray tones</font> | dark purple tones, blue tones, red tones 等 |

---

### 4. 收集物（抽象派 + 发光效果）

#### 4.1 FlyItem_Small（小型道具，+5 fly值）

**英文提示词**：
```
abstract minimalist <font color="red">glowing energy orb</font>, small collectible, 
geometric shape, <font color="red">golden #FFD700</font> color, magical aura, 
64x64 pixel, transparent background PNG, 
floating effect, glowing particles, indie game asset, no background
```

**中文翻译**：
抽象极简风格的 <font color="red">发光能量球</font>，小型收集物，几何形状，<font color="red">金色 #FFD700</font>，魔法光环，64x64像素，透明背景PNG，漂浮效果，发光粒子，独立游戏素材，无背景。

**可自定义内容**：
| 原内容 | 可替换为 |
|--------|----------|
| <font color="red">glowing energy orb</font> | floating feather, spirit light, crystal shard, star fragment 等 |
| <font color="red">golden #FFD700</font> | cyan #00CED1, purple #9370DB, green #00FF7F 等 |

---

#### 4.2 FlyItem_Large（大型道具，+50 fly值）

**英文提示词**：
```
abstract minimalist <font color="red">large glowing crystal</font>, power-up collectible, 
geometric faceted shape, <font color="red">cyan #00CED1</font> color, strong magical aura, 
96x96 pixel, transparent background PNG, 
radiant effect, sparkles, indie game asset, no background
```

**中文翻译**：
抽象极简风格的 <font color="red">大型发光水晶</font>，强化收集物，几何切面形状，<font color="red">青色 #00CED1</font>，强烈魔法光环，96x96像素，透明背景PNG，辐射效果，闪光，独立游戏素材，无背景。

**可自定义内容**：
| 原内容 | 可替换为 |
|--------|----------|
| <font color="red">large glowing crystal</font> | spirit essence, power core, ancient artifact, magic gem 等 |
| <font color="red">cyan #00CED1</font> | gold #FFD700, purple #9370DB, pink #FF69B4 等 |

---

#### 4.3 FlyItem_Anim（浮动动画4帧）

**英文提示词**：
```
abstract minimalist <font color="red">glowing energy orb</font> animation, <font color="red">4</font> frame sprite sheet, 
floating bobbing motion, geometric shape, <font color="red">golden</font> color, 
each frame 64x64 pixel, transparent background PNG, 
vertical sprite strip, indie game asset, no background
```

**中文翻译**：
抽象极简风格的 <font color="red">发光能量球</font> 动画，<font color="red">4</font>帧精灵图，漂浮上下运动，几何形状，<font color="red">金色</font>，每帧64x64像素，透明背景PNG，垂直精灵条，独立游戏素材，无背景。

---

### 5. UI界面

#### 5.1 Logo_Title（游戏标题）

**英文提示词**：
```
Disco Elysium style game title logo "<font color="red">How To Fly</font>", 
oil painting texture, hand-painted lettering, 
<font color="red">warm gold and blue</font> colors, atmospheric glow, 
512x128 pixel, transparent background PNG, 
artistic typography, indie game, no background
```

**中文翻译**：
极乐迪斯科风格游戏标题Logo "<font color="red">How To Fly</font>"，油画质感，手绘字体，<font color="red">温暖金色和蓝色</font>，大气发光，512x128像素，透明背景PNG，艺术字体，独立游戏，无背景。

**可自定义内容**：
| 原内容 | 可替换为 |
|--------|----------|
| <font color="red">How To Fly</font> | 你的游戏标题 |
| <font color="red">warm gold and blue</font> | red and gold, purple and cyan, green and gold 等 |

---

#### 5.2 Button_Normal/Hover/Pressed（按钮三态）

**英文提示词**：
```
Disco Elysium style game UI button, rounded rectangle, 
oil painting texture, <font color="red">blue gradient with warm highlights</font>, 
320x80 pixel, transparent background PNG, 
text area in center, <font color="red">three states: normal hover pressed</font>, 
indie game UI, no background
```

**中文翻译**：
极乐迪斯科风格游戏UI按钮，圆角矩形，油画质感，<font color="red">蓝色渐变配温暖高光</font>，320x80像素，透明背景PNG，中央文字区域，<font color="red">三种状态：正常、悬停、按下</font>，独立游戏UI，无背景。

**可自定义内容**：
| 原内容 | 可替换为 |
|--------|----------|
| <font color="red">blue gradient with warm highlights</font> | green gradient, purple gradient, golden gradient 等 |
| <font color="red">three states: normal hover pressed</font> | 可分别生成三种状态，用不同亮度区分 |

---

#### 5.3 Heart_Full/Empty（心形生命值）

**英文提示词**：
```
abstract minimalist <font color="red">heart</font> icon, health UI element, 
geometric simplified shape, 
64x64 pixel, transparent background PNG, 
<font color="red">full red version and empty outline version</font>, 
indie game UI, no background
```

**中文翻译**：
抽象极简风格的 <font color="red">心形</font> 图标，生命值UI元素，几何简化形状，64x64像素，透明背景PNG，<font color="red">实心红色版本和空心轮廓版本</font>，独立游戏UI，无背景。

**可自定义内容**：
| 原内容 | 可替换为 |
|--------|----------|
| <font color="red">heart</font> | star, diamond, orb, feather 等 |
| <font color="red">full red version and empty outline version</font> | 可分别生成两个版本 |

---

#### 5.4 Panel_Background（面板背景）

**英文提示词**：
```
Disco Elysium style UI panel background, 
oil painting texture, <font color="red">semi-transparent dark blue</font>, 
9-slice compatible edges, 
512x512 pixel, transparent background PNG, 
atmospheric vignette, indie game UI, no background
```

**中文翻译**：
极乐迪斯科风格UI面板背景，油画质感，<font color="red">半透明深蓝色</font>，兼容9-slice边缘，512x512像素，透明背景PNG，大气暗角，独立游戏UI，无背景。

**可自定义内容**：
| 原内容 | 可替换为 |
|--------|----------|
| <font color="red">semi-transparent dark blue</font> | dark purple, dark green, warm brown 等 |

---

#### 5.5 FlyBar_Background/Fill（Fly值进度条）

**英文提示词**：
```
abstract minimalist progress bar, fly value indicator, 
geometric shape, clean design, 
256x32 pixel, transparent background PNG, 
background frame and <font color="red">glowing cyan</font> fill bar, 
indie game UI, no background
```

**中文翻译**：
抽象极简风格进度条，Fly值指示器，几何形状，干净设计，256x32像素，透明背景PNG，背景框架和<font color="red">发光青色</font>填充条，独立游戏UI，无背景。

**可自定义内容**：
| 原内容 | 可替换为 |
|--------|----------|
| <font color="red">glowing cyan</font> | glowing gold, glowing purple, glowing green 等 |

---

### 6. 关卡元素（补充）

#### 6.1 Checkpoint_Inactive（检查点-未激活）

**英文提示词**：
```
abstract minimalist <font color="red">checkpoint marker</font>, inactive state, 
geometric <font color="red">pole with flag</font>, gray dim color, 
64x128 pixel, transparent background PNG, 
simple design, indie game asset, no background
```

**中文翻译**：
抽象极简风格的 <font color="red">检查点标记</font>，未激活状态，几何化<font color="red">旗杆</font>，灰色暗淡色，64x128像素，透明背景PNG，简约设计，独立游戏素材，无背景。

**可自定义内容**：
| 原内容 | 可替换为 |
|--------|----------|
| <font color="red">checkpoint marker</font> | save point, respawn beacon, spirit totem 等 |
| <font color="red">pole with flag</font> | crystal pillar, light beacon, floating orb 等 |

---

#### 6.2 Checkpoint_Active（检查点-激活）

**英文提示词**：
```
abstract minimalist <font color="red">checkpoint marker</font>, active state, 
geometric <font color="red">pole with flag</font>, <font color="red">bright green glowing</font>, 
64x128 pixel, transparent background PNG, 
magical aura effect, indie game asset, no background
```

**中文翻译**：
抽象极简风格的 <font color="red">检查点标记</font>，激活状态，几何化<font color="red">旗杆</font>，<font color="red">亮绿色发光</font>，64x128像素，透明背景PNG，魔法光环效果，独立游戏素材，无背景。

**可自定义内容**：
| 原内容 | 可替换为 |
|--------|----------|
| <font color="red">bright green glowing</font> | golden glowing, cyan glowing, purple glowing 等 |

---

#### 6.3 LevelGoal（关卡终点）

**英文提示词**：
```
abstract minimalist <font color="red">level goal marker</font>, 
geometric <font color="red">portal or gate</font> shape, <font color="red">golden #FFD700</font> glow, 
128x128 pixel, transparent background PNG, 
radiant effect, destination marker, indie game asset, no background
```

**中文翻译**：
抽象极简风格的 <font color="red">关卡终点标记</font>，几何化<font color="red">传送门或门</font>形状，<font color="red">金色 #FFD700</font>发光，128x128像素，透明背景PNG，辐射效果，终点标记，独立游戏素材，无背景。

**可自定义内容**：
| 原内容 | 可替换为 |
|--------|----------|
| <font color="red">level goal marker</font> | exit portal, finish line, destination beacon 等 |
| <font color="red">portal or gate</font> | light beam, floating crystal, spirit gateway 等 |
| <font color="red">golden #FFD700</font> | cyan #00CED1, purple #9370DB, silver #C0C0C0 等 |

---

### 7. 背景（极乐迪斯科风格）

#### 7.1 BG_Sky（天空背景）

**英文提示词**：
```
Disco Elysium style <font color="red">sky background</font>, 
gradient from <font color="red">light blue to purple</font>, 
oil painting texture, soft <font color="red">clouds</font>, <font color="red">sunset</font> atmosphere, 
1920x1080 pixel, game background, 
peaceful mood, no characters, atmospheric lighting
```

**中文翻译**：
极乐迪斯科风格 <font color="red">天空背景</font>，从 <font color="red">浅蓝渐变到紫色</font>，油画质感，柔和的<font color="red">云朵</font>，<font color="red">日落</font>氛围，1920x1080像素，游戏背景，宁静情绪，无角色，大气光影。

**可自定义内容**：
| 原内容 | 可替换为 |
|--------|----------|
| <font color="red">sky background</font> | forest background, cave background, mountain background 等 |
| <font color="red">light blue to purple</font> | orange to pink, cyan to deep blue, golden to red 等 |
| <font color="red">clouds</font> | birds, stars, floating islands 等 |
| <font color="red">sunset</font> | sunrise, night, dawn, dusk 等 |

---

#### 7.2 BG_Far（远景层-视差）

**英文提示词**：
```
Disco Elysium style parallax background layer, distant <font color="red">mountains and clouds</font>, 
oil painting texture, <font color="red">pastel colors</font>, 
1920x480 pixel, transparent background PNG, 
silhouette style, simple shapes, indie game asset, no background
```

**中文翻译**：
极乐迪斯科风格视差背景层，远处的<font color="red">山脉和云朵</font>，油画质感，<font color="red">柔和色调</font>，1920x480像素，透明背景PNG，剪影风格，简约形状，独立游戏素材，无背景。

**可自定义内容**：
| 原内容 | 可替换为 |
|--------|----------|
| <font color="red">mountains and clouds</font> | trees and hills, buildings and towers, crystals and rocks 等 |
| <font color="red">pastel colors</font> | dark muted tones, warm sunset tones, cool night tones 等 |

---

### 8. 特效（补充）

#### 8.1 Effect_Break（破碎特效）

**英文提示词**：
```
abstract minimalist <font color="red">stone debris</font> particles, 
<font color="red">4-6</font> pieces, geometric shapes, 
32x32 pixel each, transparent background PNG, 
breaking effect, game VFX, indie game asset, no background
```

**中文翻译**：
抽象极简风格的 <font color="red">石头碎片</font> 粒子，<font color="red">4-6</font>块，几何形状，每块32x32像素，透明背景PNG，破碎效果，游戏特效，独立游戏素材，无背景。

**可自定义内容**：
| 原内容 | 可替换为 |
|--------|----------|
| <font color="red">stone debris</font> | crystal shards, ice pieces, wood splinters 等 |
| <font color="red">4-6</font> pieces | 8-10 pieces, 12 pieces 等 |

---

#### 8.2 Effect_Dust（灰尘粒子）

**英文提示词**：
```
abstract minimalist <font color="red">dust cloud</font> particles, 
<font color="red">gray brown</font> color, soft edges, 
16x16 pixel, transparent background PNG, 
floating dust effect, game VFX, indie game asset, no background
```

**中文翻译**：
抽象极简风格的 <font color="red">灰尘云</font> 粒子，<font color="red">灰褐色</font>，柔和边缘，16x16像素，透明背景PNG，漂浮灰尘效果，游戏特效，独立游戏素材，无背景。

**可自定义内容**：
| 原内容 | 可替换为 |
|--------|----------|
| <font color="red">dust cloud</font> | smoke puff, sand particles, magic sparkles 等 |
| <font color="red">gray brown</font> | white, light blue, golden 等 |

---

#### 8.3 Effect_Sparkle（收集闪光）

**英文提示词**：
```
abstract minimalist <font color="red">sparkle</font> particles, 
<font color="red">golden yellow</font> color, geometric star shapes, 
32x32 pixel, transparent background PNG, 
magical collect effect, game VFX, indie game asset, no background
```

**中文翻译**：
抽象极简风格的 <font color="red">闪光</font> 粒子，<font color="red">金黄色</font>，几何星形，32x32像素，透明背景PNG，魔法收集效果，游戏特效，独立游戏素材，无背景。

**可自定义内容**：
| 原内容 | 可替换为 |
|--------|----------|
| <font color="red">sparkle</font> | glitter, star burst, energy flash 等 |
| <font color="red">golden yellow</font> | cyan, purple, white 等 |

---

#### 8.4 Effect_Unlock（解锁飞行光芒）

**英文提示词**：
```
abstract minimalist <font color="red">unlock flight</font> effect, 
<font color="red">bright blue</font> glowing rays, geometric burst pattern, 
128x128 pixel, transparent background PNG, 
magical transformation effect, game VFX, indie game asset, no background
```

**中文翻译**：
抽象极简风格的 <font color="red">解锁飞行</font> 特效，<font color="red">亮蓝色</font>发光射线，几何爆发图案，128x128像素，透明背景PNG，魔法变身效果，游戏特效，独立游戏素材，无背景。

**可自定义内容**：
| 原内容 | 可替换为 |
|--------|----------|
| <font color="red">unlock flight</font> | power up, level up, ability unlock 等 |
| <font color="red">bright blue</font> | golden, purple, white 等 |

---

## 快速参考表

### 素材生成优先级顺序

| 顺序 | 素材名称 | 英文提示词关键词 | 尺寸 |
|------|----------|------------------|------|
| 1 | 主角概念图 | abstract minimalist bird creature concept | 512x512 |
| 2 | Player_Idle | abstract minimalist bird creature idle | 128x128 |
| 3 | Player_Jump | abstract minimalist bird creature jumping | 128x128 |
| 4 | Player_Fly | abstract minimalist bird creature flying | 128x128 |
| 5 | Wing_Immature | abstract minimalist small translucent wings | 64x64 |
| 6 | Wing_Mature | abstract minimalist majestic wings | 128x128 |
| 7 | Ground_Tile | Disco Elysium style grass dirt platform | 256x64 |
| 8 | BouncePlatform | Disco Elysium style lily pad | 128x32 |
| 9 | FlyItem_Small | abstract minimalist glowing energy orb | 64x64 |
| 10 | FlyItem_Large | abstract minimalist large glowing crystal | 96x96 |
| 11 | Logo_Title | Disco Elysium style title logo | 512x128 |
| 12 | Heart_Full/Empty | abstract minimalist heart icon | 64x64 |

---

### 色板速查

```
[角色主色]
橙白奶油色: #FF8C00 (橙) #FFD700 (金) #FFFFFF (白)

[翅膀色]
未成熟: #80B4FF (浅蓝, 60%透明)
成熟: #4D99FF (亮蓝, 100%不透明)

[收集物色]
小型: #FFD700 (金色)
大型: #00CED1 (青色)

[UI色]
按钮: 蓝色渐变
生命值: #FF0000 (红)
终点: #FFD700 (金)
检查点激活: #00FF00 (绿)

[环境色]
地面: 棕色+绿色
洞穴: 蓝灰色
天空: 浅蓝→紫色渐变
```

---

## 使用提示

1. **先生成主角概念图**：确定角色造型后再生成动画帧，保持一致性
2. **分批生成**：按优先级表格顺序，每批3-5个素材
3. **风格一致性**：每次生成都包含 `abstract minimalist` 或 `Disco Elysium style` 关键词
4. **后处理**：生成后检查边缘是否干净，必要时用 Photoshop/Photopea 去除杂边
5. **尺寸调整**：AI生成尺寸可能不是精确值，需手动裁剪到目标尺寸