using BrowerBookmariks.Model;
using BrowerBookmariks.Model.Entitys;
using BrowerBookmariks.Model.Services;
using BrowerBookmariks.Services.BookTop;
using BrowserBookmarks.Filters;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);


//ע��ͳһ��ʽ����ֵ������
var mvcBuilder = builder.Services.AddControllersWithViews(
    options => { options.Filters.Add<ResponseWrapperFilter>(); }
);
//���ݿ�����
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
//���ú�˶˿�
builder.WebHost.UseUrls("http://*:9031");
//����
//��ӿ������
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", 
        opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("http://localhost:8080/"));
});
//�����ע��
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
//��������
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
