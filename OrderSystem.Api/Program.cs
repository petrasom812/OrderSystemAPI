using Microsoft.EntityFrameworkCore;
using OrderSystem.Api.Data;
using OrderSystem.Api.Interface;
using OrderSystem.Api.Interfaces;
using OrderSystem.Api.Interfaces.Product;
using OrderSystem.Api.Services;
using OrderSystem.Api.Services.Inventory;
using OrderSystem.Api.Services.Product;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IServiceOrder, ServiceOrder>();
builder.Services.AddScoped<IServiceProduct, ServiceProduct>();
builder.Services.AddScoped<IServiceInventory, ServiceInventory>();
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlite("Data Source=order.db"));
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.MapControllers();
app.Run();
