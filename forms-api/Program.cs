using forms_api.Config;
using forms_api.Models;
using forms_api.Repositories;
using forms_api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();

var databaseUrl = builder.Configuration.GetConnectionString("AtlasUri");
var databaseName = builder.Configuration.GetConnectionString("DatabaseName");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMongoDB(databaseUrl, databaseName);
});

builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<TenantService>();

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

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<TenantMiddleware>();

app.MapControllers();

app.Run();
