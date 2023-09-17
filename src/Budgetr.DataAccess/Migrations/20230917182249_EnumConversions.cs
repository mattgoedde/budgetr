using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budgetr.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EnumConversions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoanType",
                table: "AmortizedLoans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoanType",
                table: "AmortizedLoans");
        }
    }
}
