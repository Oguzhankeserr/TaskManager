using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Business.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isdone",
                table: "tasks");

            migrationBuilder.AddColumn<int>(
                name: "label",
                table: "tasks",
                type: "integer",
                nullable: false,
                defaultValue: -1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "label",
                table: "tasks");

            migrationBuilder.AddColumn<bool>(
                name: "isdone",
                table: "tasks",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
