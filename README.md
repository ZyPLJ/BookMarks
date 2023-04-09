# vu3＋.net6 webApi 书签管理项目

## 前言

作为一个bug程序员，保存了很多书签，直接用浏览器的每次都还要找，很麻烦，比如我自己的Google浏览器就200多个书签。所以做了个简单的项目去管理他们，同样该项目可以作为vue3、.net6 webApi入门项目，很容易上手。

部署项目需要用到.net6 SDK,百度去官网下载即可。

## 使用到的技术


- 前端：vue3
- 后端：.net core webApi，EF Core
- 数据库：mysql
- 插件：VueUse-useDark（切换主题），一为API（通过url获取网站图标），x-pageList（分页）


## 前端地址
([ZyPLJ/BookMarksVue: 书签项目前端 (github.com)](https://github.com/ZyPLJ/BookMarksVue))

## 博客园地址

[博客园](https://www.cnblogs.com/ZYPLJ/p/17133550.html)

## 接口文档
 **如何用控制台运行项目** :[点击跳转](https://www.cnblogs.com/ZYPLJ/p/17138996.html)

.net6 webapi自带Swagger接口文档，运行项目后，根据运行的url地址和端口号可以访问Swagger，项目运行文章查看如图所示：
![](https://gitee.com/zyplj/book-marks/raw/master/images/接口文档.png)

## 项目截图

新增原神首页
可以跳转最近很火的AI chatgpt

![](https://gitee.com/zyplj/book-marks/raw/master/images/首页.png)

新增百度搜索功能

![image](https://gitee.com/zyplj/book-marks/raw/master/images/百度搜索2.png)

新增筛选功能

![image](https://gitee.com/zyplj/book-marks/raw/master/images/筛选.png)

新增自定义菜单功能

![image](https://gitee.com/zyplj/book-marks/raw/master/images/添加菜单.png)

![](https://gitee.com/zyplj/book-marks/raw/master/images/自定义书签.png)

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


![](https://gitee.com/zyplj/book-marks/raw/master/images/数据库字符串.png)

### 开始部署

打包项目，Visual Studio 2022去官网下免费的，然后步骤在博客项目中可以看到。

如果没有Visual Studio 2022如何打包呢，可以使用命名行，进入项目BrowserBookmarks目录（bin文件的那一层），输入dotnet publish即可
然后在BrowserBookmarks\bin\Debug\net6.0\publish 中可以看到打包的项目，打包后目录如图所示：

![](https://gitee.com/zyplj/book-marks/raw/master/images/%E9%A1%B9%E7%9B%AE%E6%88%AA%E5%9B%BE.png)

如果没有Dockerfile文件，可以使用本项目中的参考的文件copy进去

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

然后修改初始化书签组件中上传文件的url路径

![](https://gitee.com/zyplj/book-marks/raw/master/images/文件上传.png)

可以去看一下我的博客园文章，步骤一样。

https://www.cnblogs.com/ZYPLJ/p/17103691.html

## 跨域问题
需要修改Program.cs中文件代码，根据自己去修改，如果是本地则只需要关注端口号，如图所示：

![](https://gitee.com/zyplj/book-marks/raw/master/images/跨域问题.png)

## 项目使用介绍

怎么使用呢，非常简单，只需要找到你使用的浏览器的`Boolmarks`文件目录即可，然后上传文件。

```
//这是我的Google浏览器目录
C:\Users\Lenovo\AppData\Local\Google\Chrome\User Data\Default\Bookmarks
```

![](https://gitee.com/zyplj/book-marks/raw/master/images/文件路径.png)

# 更新日记

## 2023/3/5

新增书签筛选，一开始准备用vue前端filter去解决，但是分页只展示12个书签。所以还是用后端去解决了，新增Classid参数。修改了书签的排列顺序，最新添加的书签将放在最前面。

优化了linq查询，将join联表换成了导航属性查询。

## 2023/3/14

新增百度搜索，一开始用axios去请求接口，但是发现部署之后会出现跨域问题，然后百度了很久，采用了script的方式做请求，也不是很明白，不过能用。

## 2023/3/21

新增菜单功能，可以自定义添加图标链接，可跳转/删除。现版本推荐使用sql文件创建数据库。

## 2023/3/31 bug修复

修复`Bookmarks`书签文件上传时，出现文件夹嵌套文件夹引起程序报错，如图所示

![](https://gitee.com/zyplj/book-marks/raw/master/images/文件嵌套.png)

原因是因为一开始只实现了文件夹套书签的json格式解析，没有考虑到文件夹套文件夹套书签。现在使用了递归去判断文件夹下面是否还有文件夹。目前测试文件夹->文件夹->文件夹->书签，3层文件夹没有问题。

优化了性能，使用了yield关键字返回实体。

## 2023/4/9

修改前端自定义书签的UI

将前端修改成了Vue3 + Vite项目

![](https://gitee.com/zyplj/book-marks/raw/master/images/vite.png)

# 遇到问题

![](https://gitee.com/zyplj/book-marks/raw/master/images/微信图片.jpg)