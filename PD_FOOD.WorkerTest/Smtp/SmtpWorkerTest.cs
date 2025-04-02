using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using PD_FOOD.Domain.Entities;
using PD_FOOD.Domain.Intercface;
using PD_FOOD.Infrastructure;
using PD_FOOD.Worker.Smtp;
using System.Net.Mail;


namespace PD_FOOD.WorkerTest.Smtp
{
    public class SmtpWorkerTests
    {
        private readonly Mock<ILogger<SmtpWorker>> _loggerMock;

        public SmtpWorkerTests()
        {
            _loggerMock = new Mock<ILogger<SmtpWorker>>();
        }

        [Fact]
        public async Task Should_SendEmail_When_BalanceIsNegative()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("SmtpWorkerTestDb"));

            var smtpMock = new Mock<ISmtpSender>();
            services.AddSingleton(smtpMock.Object);

            var provider = services.BuildServiceProvider();

            using (var scope = provider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.FinancialTransactions.AddRange(new List<FinancialTransaction>
            {
                new FinancialTransaction
                {
                    Id = Guid.NewGuid(),
                    Type = TransactionType.Income,
                    Date = DateTime.Today,
                    Description = "Entrada baixa",
                    Amount = 100
                },
                new FinancialTransaction
                {
                    Id = Guid.NewGuid(),
                    Type = TransactionType.Expense,
                    Date = DateTime.Today,
                    Description = "Despesa alta",
                    Amount = 1000
                }
            });
                db.SaveChanges();
            }

            var worker = new SmtpWorker(provider, smtpMock.Object, _loggerMock.Object);
            await worker.StartAsync(CancellationToken.None);

            smtpMock.Verify(s => s.SendMailAsync(It.IsAny<MailMessage>()), Times.Once);
        }
    }

}
