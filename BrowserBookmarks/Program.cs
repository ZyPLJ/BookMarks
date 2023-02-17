using BrowerBookmariks.Model;
using BrowerBookmariks.Model.Entitys;
using BrowerBookmariks.Model.Services;
using BrowerBookmariks.Services.BookTop;
using BrowserBookmarks.Filters;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);


//注册统一格式返回值过滤器
var mvcBuilder = builder.Services.AddControllersWithViews(
    options => { options.Filters.Add<ResponseWrapperFilter>(); }
);
//数据库连接
builder.Services.AddDbContext<MyDbContext>(opt =>
{
    //string connStr = builder.Configuration.GetSection("ConnStr").Value;
    string connStr = "";
    opt.UseMySql(connStr, new MySqlServerVersion(new Version(5, 7, 40)));
});
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
//配置后端端口
builder.WebHost.UseUrls("http://*:9031");
//跨域
//添加跨域策略
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", 
        opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("http://localhost:8080/"));
});
//服务层注入
builder.Services.AddTransient<IBookmarks, Bookmarks>();
builder.Services.AddTransient<IBookTopService, BookTopService>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//开启跨域
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
