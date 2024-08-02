using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// 註冊Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "菜雞 API",
        Description = "根據菜雞新訓記的範例建立的範例",
        // 服務條款
        TermsOfService = new Uri("https://igouist.github.io/"),
        // 聯絡
        Contact = new OpenApiContact
        {
            Name = "Albert",
            Email = string.Empty,
            Url = new Uri("https://igouist.github.io/post/2021/05/newbie-4-swagger/")
        },
        // 憑證
        License = new OpenApiLicense
        {
            Name = "TEST",
            Url = new Uri("https://igouist.github.io/post/2021/05/newbie-4-swagger/")
        }
    });

    // 讀取 XML 檔案產生 API 說明
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; // 用目前執行的程式集名稱組合成字串XML文件名稱
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile); // 取得應用程式基底目錄並與XML文件名稱組合成一個XML文件的完整路徑
    c.IncludeXmlComments(xmlPath); //告訴Swagger使用指定路徑的XML文件

    // Authorize
    // 設定好的驗證資訊(SecurityScheme)
    c.AddSecurityDefinition("Bearer",
    new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization"
    });

    // 抓前面設定好的驗證資訊
    c.AddSecurityRequirement(
    new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); // 也可以不指定 會有預設
        // c.RoutePrefix = string.Empty;  // 將空字串設為swagger的路徑
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
