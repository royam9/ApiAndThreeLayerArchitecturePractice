using Microsoft.OpenApi.Models;
using ProjectN.Common.Implement;
using ProjectN.Common.Interface;
using ProjectN.Repository.Implement;
using ProjectN.Repository.Interface;
using ProjectN.Service.Implement;
using ProjectN.Service.Interface;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Log the environment
Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");

// Add services to the container.
// Repository
builder.Services.AddScoped<ICardRepository>(sp =>
{
    var dbConnectionString = builder.Configuration.GetConnectionString("NewbieSQL");
    Console.WriteLine($"Connection String: {dbConnectionString}");
    return new CardRepository(dbConnectionString);
});

//builder.Services.AddScoped<ICardRepository, CardRepository>();

#region Swagger
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2",
        Title = "¤À¼h¬[ºcapi",
        Description = "¤À¼h¬[ºcapi",
        TermsOfService = new Uri("https://google.com/"),
        Contact = new OpenApiContact
        {
            Name = "HaHa",
            Email = string.Empty,
            Url = new Uri("https://igouist.github.io/post/2021/05/newbie-4-swagger/")
        },
        License = new OpenApiLicense
        {
            Name = "TEST",
            Url = new Uri("https://igouist.github.io/post/2021/05/newbie-4-swagger/")
        }
    });

    var xmlfileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var filePath = Path.Combine(AppContext.BaseDirectory, xmlfileName);
    swagger.IncludeXmlComments(filePath);
});
#endregion

// Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Common
builder.Services.AddSingleton<IMapperService, MapperService>();

// Service
builder.Services.AddScoped<ICardService, CardService>();

builder.Services.AddRazorPages();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Log configuration values
Console.WriteLine("Configuration Values:");
foreach (var kvp in builder.Configuration.AsEnumerable())
{
    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Error");
    //// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
    
}

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2"); });

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapRazorPages();

app.Run();
