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

    var context = services.GetRequiredService<PostDbContext>();
    context.Database.EnsureCreated();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
