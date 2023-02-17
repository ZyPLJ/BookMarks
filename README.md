# vu3＋.net6 webApi 书签管理项目

## 前言

作为一个bug程序员，保存了很多书签，直接用浏览器的每次都还要找，很麻烦，比如我自己的Google浏览器就200多个书签。所以做了个简单的项目去管理他们，同样该项目可以作为vue3、.net6 webApi入门项目，很容易上手。

## 使用到的技术


- 前端：vue3
- 后端：.net core webApi，EF Core
- 数据库：mysql
- 插件：VueUse-useDark（切换主题），一为API（通过url获取网站图标），x-pageList（分页）


## 前端地址
[书签前端地址](https://gitee.com/zyplj/book-marks-vue/tree/master)

## 项目截图

初始化书签界面

![初始化书签](https://gitee.com/zyplj/book-marks/raw/master/images/初始化书签.png)

查看所有书签-可以模糊查询，分页。

![](https://gitee.com/zyplj/book-marks/raw/master/images/所有书签.png)

![](https://gitee.com/zyplj/book-marks/raw/master/images/模糊查询.png)

书签置顶栏-只能置顶12个书签，刚好一页。

![](https://gitee.com/zyplj/book-marks/raw/master/images/书签置顶栏.png)

点击跳转
![](https://gitee.com/zyplj/book-marks/raw/master/images/跳转.png)

主题切换

![](https://gitee.com/zyplj/book-marks/raw/master/images/主题切换.png)

## 项目部署

建议采用Docker部署，方便快捷，还可以部署在自己电脑的本地，非常的nice

Docker下载链接：https://docs.docker.com/

### 后端部署

首先确保有数据库，如果没有则根据图片去创建，或者采用codefirst模式，或者执行sql文件，都行，3选1。

codefirst模式就不做演示了，需要的话可以去看我的另一个博客项目中有写。
博客项目地址：https://gitee.com/zyplj/personalblog

数据库名 BookMark

数据库表结构：

![](https://gitee.com/zyplj/book-marks/raw/master/images/数据库1.png)

bookmarks表：

主键不需要自增

![](https://gitee.com/zyplj/book-marks/raw/master/images/数据库2.png)

bookTops表：

主键需自增

![](https://gitee.com/zyplj/book-marks/raw/master/images/数据库3.png)

class表：

主键需自增

![](https://gitee.com/zyplj/book-marks/raw/master/images/数据库4.png)

数据库创建完成后就要修改项目的连接字符串了，如果不采用codefirst模式生成数据库，则只需要修改 `Program.cs`中的connStr即可，**注意后面5,7,40是mysql数据库的版本号！**

```
Server=数据库地址;Port=端口;Database=BookMark; User=root;Password=123456;

Server=localhost;Port=3306;Database=BookMark; User=root;Password=123456;
```

Server=101.43.25.210;Port=3306;Database=BookMark; User=root;Password=123456;

![](https://gitee.com/zyplj/book-marks/raw/master/images/数据库字符串.png)

**开始部署**

打包项目，Visual Studio 2022去官网下免费的，然后步骤在博客项目中可以看到。

其他方式也行，自行百度。

进入书签项目部署的目录，打开控制台，输入`docker build -t 名称 .`，如图：可以和我一样的名称

![](https://gitee.com/zyplj/book-marks/raw/master/images/docker部署1.png)

然后等待镜像下载完成，继续输入`docker run -d --restart=always -p 9031:9031 --name 名称 名称`，--restart=always让容器开机自动启动。如图：

![](https://gitee.com/zyplj/book-marks/raw/master/images/docker部署2.png)

出现一串字符就代表docker部署成功了。

<font color='red'>注意端口号本项目默认9031</font>，如果需要更改则要更改后端Dockerfile文件和Program.cs文件，如图：

![](https://gitee.com/zyplj/book-marks/raw/master/images/后端1.png)

![](https://gitee.com/zyplj/book-marks/raw/master/images/后端2.png)

### 前端部署

首先需要修改项目目录http中index.ts中httApi的值，它取决于你的后端部署url

![](https://gitee.com/zyplj/book-marks/raw/master/images/vue1.png)

可以去看一下我的博客园文章，步骤一样。

https://www.cnblogs.com/ZYPLJ/p/17103691.html

## 项目使用介绍

怎么使用呢，非常简单，只需要找到你使用的浏览器的`Boolmarks`文件目录即可，不要引号。

```
//这是我的Google浏览器目录
C:\Users\Lenovo\AppData\Local\Google\Chrome\User Data\Default\Bookmarks
```

![](https://gitee.com/zyplj/book-marks/raw/master/images/文件路径.png)

![](https://gitee.com/zyplj/book-marks/raw/master/images/初始化书签操作.png)

# 遇到问题

![](https://gitee.com/zyplj/book-marks/raw/master/images/5192045913af4a31a7988ed7077a1e0.jpg)
