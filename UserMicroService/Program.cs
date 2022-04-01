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

IMapper SetupMapper()
{
    var configuration = new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<UserDTO, User>();
        cfg.CreateMap<User, UserDTO>();
    });

    return new Mapper(configuration);
}


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IMapper>(x => SetupMapper());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency injection
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Setup database
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<UserDbContext>();
    context.Database.EnsureCreated();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/json";

        var error = context.Features.Get<IExceptionHandlerFeature>();
        if (error != null)
        {
            var ex = error.Error;

            await context.Response.WriteAsync(new IError()
            {
                StatusCode = 400,
                Message = ex.Message
            }.ToString(), Encoding.UTF8);
        }
    });
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();