using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BNP.SecuritiesPriceService.Data;
using BNP.SecuritiesPriceService.Repositories;
using BNP.SecuritiesPriceService.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddSingleton<SecuritiesDbContext>(provider => new SecuritiesDbContext());

builder.Services.AddScoped<ISecurityRepository, SecurityRepository>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddHttpClient<ISecurityService, SecurityService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
