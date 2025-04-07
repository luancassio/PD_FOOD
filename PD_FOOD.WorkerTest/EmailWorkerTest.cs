using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using PD_FOOD.Domain.Dtos;
using PD_FOOD.Domain.Entities;
using PD_FOOD.Domain.Interfaces;
using PD_FOOD.Infrastructure;


namespace PD_FOOD.WorkerTest
{
    public class EmailWorkerTests
    {
        private readonly Mock<ILogger<EmailWorker>> _loggerMock;
        public EmailWorkerTests()
        {
            _loggerMock = new Mock<ILogger<EmailWorker>>();
        }

        [Fact]
        public async Task Should_SendEmail_When_BalanceIsNegative_AndUsersExist()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("EmailWorkerTestDb1"));

            var emailMock = new Mock<IEmailSender>();
            services.AddSingleton(emailMock.Object);

            var provider = services.BuildServiceProvider();

            using (var scope = provider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Simula transações com saldo negativo
                db.FinancialTransactions.AddRange(new List<FinancialTransaction>
                {
                    new FinancialTransaction
                    {
                        Id = Guid.NewGuid(),
                        Type = TransactionType.Income,
                        Date = DateTime.Today,
                        Description = "Entrada",
                        Amount = 100
                    },
                    new FinancialTransaction
                    {
                        Id = Guid.NewGuid(),
                        Type = TransactionType.Expense,
                        Date = DateTime.Today,
                        Description = "Saída",
                        Amount = 200
                    }
                });

                // Simula usuários ativos para envio de e-mail
                db.UserNotifications.Add(new UserNotification
                {
                    Id = Guid.NewGuid(),
                    Email = "test@example.com",
                    Name = "User Test",
                    IsActive = true,
                    Hour = new TimeOnly(18, 0)
                });

                await db.SaveChangesAsync();
            }

            var loggerMock = new Mock<ILogger<EmailWorker>>();
            var worker = new EmailWorker(provider, loggerMock.Object);

            await worker.StartAsync(CancellationToken.None);

            emailMock.Verify(s => s.SendEmailAsync(It.IsAny<List<SendEmailDto>>()), Times.Once);
        }

        [Fact]
        public async Task Should_NotSendEmail_When_BalanceIsNegative_AndNoUsersExist()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("EmailWorkerTestDb2"));

            var emailMock = new Mock<IEmailSender>();
            services.AddSingleton(emailMock.Object);

            var provider = services.BuildServiceProvider();

            using (var scope = provider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Simula transações com saldo negativo
                db.FinancialTransactions.AddRange(new List<FinancialTransaction>
                {
                    new FinancialTransaction
                    {
                        Id = Guid.NewGuid(),
                        Type = TransactionType.Income,
                        Date = DateTime.Today,
                        Description = "Entrada",
                        Amount = 100
                    },
                    new FinancialTransaction
                    {
                        Id = Guid.NewGuid(),
                        Type = TransactionType.Expense,
                        Date = DateTime.Today,
                        Description = "Saída",
                        Amount = 200
                    }
                });

                // NÃO adiciona usuários
                await db.SaveChangesAsync();
            }

            var loggerMock = new Mock<ILogger<EmailWorker>>();
            var worker = new EmailWorker(provider, loggerMock.Object);

            await worker.StartAsync(CancellationToken.None);

            // Verifica que o método NUNCA foi chamado
            emailMock.Verify(s => s.SendEmailAsync(It.IsAny<List<SendEmailDto>>()), Times.Never);
        }

    }

}
