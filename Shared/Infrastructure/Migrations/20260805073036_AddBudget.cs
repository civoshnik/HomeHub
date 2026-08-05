using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBudget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HouseholdBudgets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HouseholdId = table.Column<Guid>(type: "uuid", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Rent = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Utilities = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseholdBudgets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HouseholdBudgetCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HouseholdBudgetId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseholdBudgetCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HouseholdBudgetCategories_HouseholdBudgets_HouseholdBudgetId",
                        column: x => x.HouseholdBudgetId,
                        principalTable: "HouseholdBudgets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HouseholdBudgetCategories_HouseholdBudgetId",
                table: "HouseholdBudgetCategories",
                column: "HouseholdBudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseholdBudgets_HouseholdId_Year_Month",
                table: "HouseholdBudgets",
                columns: new[] { "HouseholdId", "Year", "Month" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HouseholdBudgetCategories");

            migrationBuilder.DropTable(
                name: "HouseholdBudgets");
        }
    }
}
