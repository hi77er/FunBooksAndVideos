using FunBooksAndVideos.DAL.Context;
using FunBooksAndVideos.Repository.Configuration;
using FunBooksAndVideos.WebApi.Behaviors;
using FunBooksAndVideos.WebApi.Mapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

const string connStrName = "DefaultConnection";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddJsonOptions(o =>
        o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FunDbContext>(
    options => options.UseSqlServer(
            builder.Configuration.GetConnectionString(connStrName)
        ));
builder.Services.AddAutoMapper(typeof(EntityDTOProfile));
builder.Services.ConfigureRepositories();
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
