using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Business.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "tasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "tasks",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
