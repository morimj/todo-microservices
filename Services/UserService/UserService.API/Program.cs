using Microsoft.EntityFrameworkCore;
using UserService.Application.Mappings;
using UserService.Application.Services.UserServices;
using UserService.Domain.Interfaces;
using UserService.Infrastructure.Messaging.Consumers;
using UserService.Infrastructure.Persistence.DbContext;
using UserService.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.AddHostedService<TaskCreatedConsumer>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService.Application.Services.UserServices.UserService>();
builder.Services.AddAutoMapper(typeof(UserMappingProfile));

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