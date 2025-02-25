using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class seventh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Budgets",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Goals",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Goals");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Budgets",
                newName: "Status");
        }
    }
}
