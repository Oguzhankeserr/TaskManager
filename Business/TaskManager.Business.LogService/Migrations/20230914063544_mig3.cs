using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Business.LogService.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "projectid",
                table: "logs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "projectid",
                table: "logs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
