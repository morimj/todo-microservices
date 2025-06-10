using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using TaskService.Application.Mappings;
using TaskService.Application.Services.TaskServices;
using TaskService.Domain.Interfaces;
using TaskService.Application.Publishers;
using TaskService.Infrastructure.Messaging.Publishers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskCreatedPublisher, RabbitMqTaskCreatedPublisher>();
builder.Services.AddScoped<ITaskService, TaskService.Application.Services.TaskServices.TaskService>();
builder.Services.AddAutoMapper(typeof(TaskMappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();

    // Redirect root "/" to "/swagger"
    app.MapGet("/", () => Results.Redirect("/swagger"));
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();