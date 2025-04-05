using FlightManagementApi;
using Microsoft.EntityFrameworkCore;
using UserAlertManagement.Data;
using UserAlertManagement.Data.Context;
using UserAlertManagement.Data.Exceptions;
using UserAlertManagement.Data.Interfaces;
using UserAlertManagement.Data.Mappers;
using UserAlertManagement.Services;
using UserAlertManagement.Services.Interfaces;
using UserAlertManagement.Services.Mappers;

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine("Connection string: " + builder.Configuration.GetConnectionString("DefaultConnection"));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseLazyLoadingProxies());
builder.Services.AddAutoMapper(typeof(AlertMapper));
builder.Services.AddAutoMapper(typeof(UserMapper));
builder.Services.AddAutoMapper(typeof(UserModelMapper));

builder.Services.AddHostedService<FlightDealConsumer>();
builder.Services.AddScoped<IAlertRepository, AlertRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserAlertService, UserAlertService>();
builder.Services.AddSingleton<IMessageQueue, MessageQueue>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
