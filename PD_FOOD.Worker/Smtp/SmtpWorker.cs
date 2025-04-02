using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PD_FOOD.Domain.Entities;
using PD_FOOD.Domain.Intercface;
using PD_FOOD.Infrastructure;
using System.Net.Mail;


namespace PD_FOOD.Worker.Smtp
{
    public class SmtpWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ISmtpSender _smtpSender;
        private readonly ILogger<SmtpWorker> _logger;

        public SmtpWorker(IServiceProvider serviceProvider, ISmtpSender smtpSender, ILogger<SmtpWorker> logger)
        {
            _serviceProvider = serviceProvider;
            _smtpSender = smtpSender;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("SmtpWorker started at {Time}", DateTime.Now);

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var today = DateTime.Today;
                var transactions = await db.FinancialTransactions
                    .Where(t => t.Date.Date == today)
                    .ToListAsync();

                var income = transactions
                    .Where(t => t.Type == TransactionType.Income)
                    .Sum(t => t.Amount);

                var expense = transactions
                    .Where(t => t.Type == TransactionType.Expense)
                    .Sum(t => t.Amount);

                _logger.LogInformation("Transactions found: {Count}, Income: {Income}, Expense: {Expense}", transactions.Count, income, expense);

                if (expense > income)
                {
                    _logger.LogWarning("Negative balance detected for today ({Date})", today);

                    var message = new MailMessage("noreply@yourapp.com", "user@yourdomain.com", "Negative Balance Alert", "Your balance is negative today.");
                    await _smtpSender.SendMailAsync(message);

                    _logger.LogInformation("Alert email sent to user.");
                } else
                {
                    _logger.LogInformation("No negative balance. No email sent.");
                }
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing the SmtpWorker.");
            }
        }
    }

}
