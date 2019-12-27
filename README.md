# SUSTech Mahjong
## 介绍
- 基于unity3D的麻将游戏前端
- 用作南方科技大学2019秋OOAD课程学生项目

## 运行环境
- 输出了Windows的exe文件
- 输出了Mac的dmg文件

## 开发环境：
- unity 2018.3.9f1 Pro
- JetBrains Rider 2019.2.3
- MacOS Catalina 10.15.2

## 运行
- 克隆源码后使用unity打开
- 打开游戏前需要启动服务器，服务器github地址：[后端]（https://github.com/DiogerChen/OOAD_Project）

## 代码说明
- Assets/resources 下放置了所有的素材，包括麻将桌，麻将子，手的模型，UI等
- Assets/Scenes 下放置了场景文件。需要注意，因为没有固定的服务器，所以IP场景用来输入服务器IP，务必输入正确游戏才能正常启动
- Assets/scrips 下分类别放置了每个场景需要使用的脚本代码。xxxxController.cs类脚本用于控制组件的行为。Network文件夹中的代码是用于网络连接部分

## 致谢
-感谢小组其他成员贡献的代码
