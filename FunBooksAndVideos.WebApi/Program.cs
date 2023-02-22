using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.Repository.Configuration;
using FunBooksAndVideos.WebApi.Behaviors;
using MediatR;
using Microsoft.EntityFrameworkCore;

const string connStrName = "DefaultConnection";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FunDbContext>(
    options => options.UseSqlServer(
            builder.Configuration.GetConnectionString(connStrName)
        ));
builder.Services.ConfigureRepositories();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddMediatR((config) =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

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
