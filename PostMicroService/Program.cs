using PostMicroService.Data;
using PostMicroService.Repositories;
using PostMicroService.Repositories.Interfaces;
using PostMicroService.Services;
using PostMicroService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var MyCors = "_myCors";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyCors,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:8080")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(MyCors);

app.MapControllers();

app.Run();
