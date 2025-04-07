using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PD_FOOD.Domain.Dtos;
using PD_FOOD.Domain.Entities;
using PD_FOOD.Domain.Interfaces;
using PD_FOOD.Infrastructure;

public class EmailWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<EmailWorker> _logger;

    public EmailWorker(IServiceProvider serviceProvider, ILogger<EmailWorker> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("EmailWorker started at {Time}", DateTime.Now);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                var today = DateTime.UtcNow.Date;
                var transactions = await db.FinancialTransactions
                    .AsNoTracking()
                    .Where(t => t.Date.Date == today)
                    .ToListAsync();

                var income = transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
                var expense = transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);

                var listUserNotifications = await db.UserNotifications.AsNoTracking().Where(u => u.IsActive).ToListAsync();

                if (expense > income)
                {
                    _logger.LogWarning("Negative balance detected for today ({Date})", today);
                    List<SendEmailDto> emails = new List<SendEmailDto>();
                    if (listUserNotifications.Count > 0)
                    {
                        emails = listUserNotifications.Select(x => new SendEmailDto
                        {
                            Email = x.Email,
                            UserName = x.Name
                        }).ToList();

                        await emailSender.SendEmailAsync(emails);
                        _logger.LogInformation("Alert email sent.");
                    }
                } else
                {
                    _logger.LogInformation("No negative balance. No email sent.");
                }
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in EmailWorker.");
            }
            
            await Task.Delay(TimeSpan.FromSeconds(20), stoppingToken);
        }
        
    }
}
