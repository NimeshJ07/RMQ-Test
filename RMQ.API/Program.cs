using Microsoft.EntityFrameworkCore;
using RMQ.Data;
using RMQ.IRepository;
using RMQ.IServices;
using RMQ.RabbitMQ;
using RMQ.Repository;
using RMQ.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var config = builder.Configuration;
var connStr = config.GetConnectionString("DeafaultConnection");

builder.Services.AddDbContext<RMQDbContext>(opt => opt.UseSqlServer(connStr, b => b.MigrationsAssembly("RMQ.API")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IRabitMQProducer, RabbitMQProducer>();
builder.Services.AddScoped<RabbitMQConsumer>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
