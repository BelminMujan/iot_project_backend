using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Microsoft.EntityFrameworkCore;
using IOT_Backend.Models;
using IOT_Backend.Services.RoomService;
using IOT_Backend.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(opt => opt.UseMySQL(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IRoomService, RoomService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

