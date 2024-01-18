using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseServer.Api.Migrations
{
    /// <inheritdoc />
    public partial class RenameIncomesColumnDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Incomes",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Incomes",
                newName: "DateTime");
        }
    }
}
