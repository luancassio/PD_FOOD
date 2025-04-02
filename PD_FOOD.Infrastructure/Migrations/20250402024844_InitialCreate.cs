using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PD_FOOD.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinancialTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialTransactions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "FinancialTransactions",
                columns: new[] { "Id", "Amount", "Date", "Description", "Type" },
                values: new object[,]
                {
                    { new Guid("29a3a1d0-e764-4a6a-b9b2-72d33a3fcbf7"), 239.90m, new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Internet Corporativa", 2 },
                    { new Guid("7b99b6d1-d230-4d7f-94e5-6e8fcb1fd308"), 650.00m, new DateTime(2023, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Reembolso Viagem", 1 },
                    { new Guid("a1c3f1b2-8f23-4d3e-9e71-f8d0b117f8e2"), 4800.00m, new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Venda de Equipamentos", 1 },
                    { new Guid("a6e778c3-09c4-44b7-a693-449ebf160b23"), 5500.00m, new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Pagamento de Cliente ABC", 1 },
                    { new Guid("b2f7c3a1-9c44-4201-99a7-84c0f5bdee66"), 1100.50m, new DateTime(2023, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Compra de Materiais", 2 },
                    { new Guid("c8a144b9-1105-4c41-b15c-31dd2e021e97"), 2200.00m, new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Aluguel do Escritório", 2 },
                    { new Guid("d451f8de-8b99-4fc3-bb62-6b5e23850ad5"), 3200.00m, new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Serviço de Consultoria", 1 },
                    { new Guid("e3f2f2a1-7d41-4ae3-8e37-c020f0f4b3fc"), 499.99m, new DateTime(2023, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Assinatura de Softwares", 2 },
                    { new Guid("fdb317aa-66a2-48c3-9d79-8ed1f6224b3d"), 832.75m, new DateTime(2023, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Conta de Energia", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialTransactions");
        }
    }
}
