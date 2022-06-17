using UserMicroService.Data;
using UserMicroService.Repositories;
using UserMicroService.Repositories.Interfaces;
using UserMicroService.Services;
using UserMicroService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;
using UserMicroService.Domain;
using System.Text;
using AutoMapper;
using UserMicroService.Models.DTO;
using UserMicroService.Models;
using MassTransit;
using UserMicroService.Messaging;
using Serilog;
using TwitterCloneBackend.Shared.Logging;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
            .CreateLogger();

Log.Information("Starting TwitterCloneBackend PostMicroService");

try
{
    Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}

void Run()
{
    IMapper SetupMapper()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserDTO>();
            cfg.CreateMap<UserDTO, User>();
            cfg.CreateMap<Subscription, SubscriptionDTO>();
            cfg.CreateMap<SubscriptionDTO, Subscription>();
        });

        return new Mapper(configuration);
    }


    var builder = WebApplication.CreateBuilder(args);

    builder.Host.AddTwitterCloneLogging();

    // Add services to the container.
    builder.Services.AddSingleton<IMapper>(x => SetupMapper());
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Dependency injection
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
    builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
    builder.Services.AddScoped<ISendMessageHandler, SendMessageHandler>();

    // Setup database
    builder.Services.AddDbContext<UserDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

    // Setup Messaging
    builder.Services.AddMassTransit(x =>
    {
        x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
        {
            config.Host(new Uri(builder.Configuration.GetSection("AppConfig")["RabbitMQBaseUrl"]));
        }));
    });
    builder.Services.AddMassTransitHostedService();

    var app = builder.Build();

    app.UseSerilogRequestLogging();

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
}