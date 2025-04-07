using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PD_FOOD.Application;
using PD_FOOD.Domain.Interfaces;
using PD_FOOD.Infrastructure;
using PD_FOOD.Infrastructure.Configurations;
using PD_FOOD.Infrastructure.Email;
using PD_FOOD.Infrastructure.Repositories;
using PD_FOOD.Middlewares;
using Resend;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly));

builder.Services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
builder.Services.AddFluentValidationAutoValidation();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Configurações
builder.Services.Configure<EmailSettingsConfiguration>(builder.Configuration.GetSection("SendEmailSetting"));

builder.Services.AddHttpClient<ResendClient>();

// Repositórios
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IUserNotificationRepository, UserNotificationRepository>();


// Email (injeção de HttpClient + opções no ResendEmailSender)
builder.Services.AddHttpClient<IEmailSender, ResendEmailSender>();

// Worker
builder.Services.AddHostedService<EmailWorker>();


var app = builder.Build();
// Add services to the container.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/openapi/v1.json", "PD_FOOD.API"));
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseMiddleware<ExceptionMiddleware>();
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
