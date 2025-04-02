using Microsoft.EntityFrameworkCore;
using PD_FOOD.Domain.Entities;


namespace PD_FOOD.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<FinancialTransaction> FinancialTransactions => Set<FinancialTransaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            Seed(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FinancialTransaction>().HasData(
                new FinancialTransaction
                {
                    Id = Guid.Parse("a1c3f1b2-8f23-4d3e-9e71-f8d0b117f8e2"),
                    Type = TransactionType.Income,
                    Date = DateTime.SpecifyKind(DateTime.Parse("2023-01-10"), DateTimeKind.Utc),
                    Description = "Venda de Equipamentos",
                    Amount = 4800.00M
                },
                new FinancialTransaction
                {
                    Id = Guid.Parse("b2f7c3a1-9c44-4201-99a7-84c0f5bdee66"),
                    Type = TransactionType.Expense,
                    Date = DateTime.SpecifyKind(DateTime.Parse("2023-01-12"), DateTimeKind.Utc),
                    Description = "Compra de Materiais",
                    Amount = 1100.50M
                },
                new FinancialTransaction
                {
                    Id = Guid.Parse("c8a144b9-1105-4c41-b15c-31dd2e021e97"),
                    Type = TransactionType.Expense,
                    Date = DateTime.SpecifyKind(DateTime.Parse("2023-01-15"), DateTimeKind.Utc),
                    Description = "Aluguel do Escritório",
                    Amount = 2200.00M
                },
                new FinancialTransaction
                {
                    Id = Guid.Parse("d451f8de-8b99-4fc3-bb62-6b5e23850ad5"),
                    Type = TransactionType.Income,
                    Date = DateTime.SpecifyKind(DateTime.Parse("2023-01-20"), DateTimeKind.Utc),
                    Description = "Serviço de Consultoria",
                    Amount = 3200.00M
                },
                new FinancialTransaction
                {
                    Id = Guid.Parse("e3f2f2a1-7d41-4ae3-8e37-c020f0f4b3fc"),
                    Type = TransactionType.Expense,
                    Date = DateTime.SpecifyKind(DateTime.Parse("2023-01-22"), DateTimeKind.Utc),
                    Description = "Assinatura de Softwares",
                    Amount = 499.99M
                },
                new FinancialTransaction
                {
                    Id = Guid.Parse("fdb317aa-66a2-48c3-9d79-8ed1f6224b3d"),
                    Type = TransactionType.Expense,
                    Date = DateTime.SpecifyKind(DateTime.Parse("2023-01-28"), DateTimeKind.Utc),
                    Description = "Conta de Energia",
                    Amount = 832.75M
                },
                new FinancialTransaction
                {
                    Id = Guid.Parse("a6e778c3-09c4-44b7-a693-449ebf160b23"),
                    Type = TransactionType.Income,
                    Date = DateTime.SpecifyKind(DateTime.Parse("2023-02-01"), DateTimeKind.Utc),
                    Description = "Pagamento de Cliente ABC",
                    Amount = 5500.00M
                },
                new FinancialTransaction
                {
                    Id = Guid.Parse("7b99b6d1-d230-4d7f-94e5-6e8fcb1fd308"),
                    Type = TransactionType.Income,
                    Date = DateTime.SpecifyKind(DateTime.Parse("2023-02-03"), DateTimeKind.Utc),
                    Description = "Reembolso Viagem",
                    Amount = 650.00M
                },
                new FinancialTransaction
                {
                    Id = Guid.Parse("29a3a1d0-e764-4a6a-b9b2-72d33a3fcbf7"),
                    Type = TransactionType.Expense,
                    Date = DateTime.SpecifyKind(DateTime.Parse("2023-02-10"), DateTimeKind.Utc),
                    Description = "Internet Corporativa",
                    Amount = 239.90M
                }

            );
        }


    }

}
