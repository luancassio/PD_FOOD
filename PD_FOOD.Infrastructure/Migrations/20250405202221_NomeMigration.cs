using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PD_FOOD.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NomeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Hour = table.Column<TimeOnly>(type: "TIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotifications", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UserNotifications",
                columns: new[] { "Id", "Email", "Hour", "IsActive", "Name" },
                values: new object[,]
                {
                    { new Guid("3c905bd1-e6ce-441b-9305-c2441bc6c85b"), "teste123@gmail.com", new TimeOnly(18, 0, 0), false, "Email Teste" },
                    { new Guid("4313f41e-055b-4ff7-9c2a-55360d90a5aa"), "josielesilva2802@gmail.com", new TimeOnly(18, 0, 0), true, "Josiele Gomes" },
                    { new Guid("8b4cfc53-2ad6-4e61-8d5e-4ff2d7a1c31b"), "luancassio2307@gmail.com", new TimeOnly(18, 0, 0), true, "Luan Cássio" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_Email",
                table: "UserNotifications",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserNotifications");
        }
    }
}
