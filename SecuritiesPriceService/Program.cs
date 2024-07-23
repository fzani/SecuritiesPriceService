using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BNP.SecuritiesPriceService.Data;
using BNP.SecuritiesPriceService.Repositories;
using BNP.SecuritiesPriceService.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddSingleton<SecuritiesDbContext>(provider => new SecuritiesDbContext());

builder.Services.AddScoped<ISecurityRepository, SecurityRepository>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddHttpClient<ISecurityService, SecurityService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BPN Securities Price Service", Version = "v1" });

    // Inclui comentários XML na documentação do Swagger
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BPN Securities Price Service v1");
    });

}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
