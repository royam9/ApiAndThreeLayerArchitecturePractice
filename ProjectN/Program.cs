using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// ���USwagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "���� API",
        Description = "�ھڵ����s�V�O���d�ҫإߪ��d��",
        // �A�ȱ���
        TermsOfService = new Uri("https://igouist.github.io/"),
        // �p��
        Contact = new OpenApiContact
        {
            Name = "Albert",
            Email = string.Empty,
            Url = new Uri("https://igouist.github.io/post/2021/05/newbie-4-swagger/")
        },
        // ����
        License = new OpenApiLicense
        {
            Name = "TEST",
            Url = new Uri("https://igouist.github.io/post/2021/05/newbie-4-swagger/")
        }
    });

    // Ū�� XML �ɮײ��� API ����
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; // �Υثe���檺�{�����W�ٲզX���r��XML���W��
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile); // ���o���ε{���򩳥ؿ��ûPXML���W�ٲզX���@��XML��󪺧�����|
    c.IncludeXmlComments(xmlPath); //�i�DSwagger�ϥΫ��w���|��XML���

    // Authorize
    // �]�w�n�����Ҹ�T(SecurityScheme)
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

    // ��e���]�w�n�����Ҹ�T
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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); // �]�i�H�����w �|���w�]
        // c.RoutePrefix = string.Empty;  // �N�Ŧr��]��swagger�����|
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
