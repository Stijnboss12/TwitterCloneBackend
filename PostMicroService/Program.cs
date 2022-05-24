using PostMicroService.Data;
using PostMicroService.Repositories;
using PostMicroService.Repositories.Interfaces;
using PostMicroService.Services;
using PostMicroService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;
using PostMicroService.Domain;
using System.Text;
using AutoMapper;
using PostMicroService.Models.DTO;
using PostMicroService.Models;
using MassTransit;
using PostMicroService.Messaging;
using Serilog;
using TwitterCloneBackend.Shared.Logging;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
            .CreateLogger();

Log.Information("Starting ACI.Reservations microservice");

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
            cfg.CreateMap<PostDTO, Post>();
            cfg.CreateMap<Post, PostDTO>();
        });

        return new Mapper(configuration);
    }

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.AddAciLogging();

    // Add services to the container.
    builder.Services.AddSingleton<IMapper>(x => SetupMapper());
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Dependency injection
    builder.Services.AddScoped<IPostRepository, PostRepository>();
    builder.Services.AddScoped<IPostService, PostService>();

    // Setup database
    builder.Services.AddDbContext<PostDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

    // Setup message broker
    builder.Services.AddMassTransit(x =>
    {
        x.AddConsumer<ConsumeMessageHandler>();
        x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.Host(new Uri(builder.Configuration.GetSection("AppConfig")["RabbitMQBaseUrl"]));
            cfg.ReceiveEndpoint("MessageQueue", ep =>
            {
                ep.PrefetchCount = 16;
                ep.ConfigureConsumer<ConsumeMessageHandler>(provider);
            });
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
    else
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
