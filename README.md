# The_Legends_of_Jetkeey

### 游戏分两个场景
	1. UIPanel
	2. Map01 
第一个场景。一共有4个drawCall。第1个drawcall是清除先前渲染的内容，第2个是游戏开始界面的图片，第3个是游戏开始的文字，第4个是Camera.imageEffects。
将相机的MSAA关闭后，第4个DrawCall就没了

第二个场景。一共有10个drawcall。第1个DrawCall是清空先前渲染的内容，第2个是场景的一张大背景图，独立的一张image。第3个至第6个是场景tilemap的不通sorting layer的渲染。第7个是怪物的显示，第8个是任务的显示，第9个是跳跃，攻击按钮底图的渲染，第10是跳跃和攻击文字的渲染。当游戏结束的时候，跳出的界面，会新增加2个图片和文字的DrawCall。
这个已经没有可以降低drawCall的地方了。

### 主角攻击和碰撞一些细节

角色攻击就是他的技能，冷却是1.5秒，冷却中点击界面的按钮不生效，之后冷却结束点击才有效。攻击到怪物时，怪物会播放被击中的动画。
角色的节点上挂载了一个weapon的子节点。挂载有圆形触发器(默认不激活的)、Attack的脚本。玩家攻击挥刀那一帧动画里，会将武器的触发器激活，然后如果刚好接触到怪物身上的碰撞器，在attack的脚本里OntriggerStay2D的函数被触发。怪物身上的Character脚本就执行受伤的逻辑，怪物播放受伤特效。

### 我的一些补充
	1. 人物可以跳跃(按空格)，2种移动状态，分别是跑步和慢走(按Shift)
	2. 怪物的警戒范围是5m。怪物也有2种移动状态，未发现敌人(移动速度3)和发现敌人(移动速度5)。如果玩家超出怪物检测的5m之外。怪物也就不再跟随。
	3. 玩家和怪物都有一小段被伤害后的无敌时间。玩家是2秒，怪物是1秒。
	4. 用Cinemachine做了跟随，并且设置了跟随的范围。
	5. 项目已经用webGL发布在itch上，直接打开链接就可以测试。链接：https://jetkeey.itch.io/the-legends-of-jetkeey 
	   源码开源：https://github.com/zhang12c/The_Legends_of_Jetkeey.git
