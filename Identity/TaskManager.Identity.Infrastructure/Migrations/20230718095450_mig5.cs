using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "336e1648-5384-4d2c-b886-0281db620ccb",
                column: "NormalizedName",
                value: "USER");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a2c4fe5-5b10-45b6-a1f6-7cfecc629d3f",
                column: "NormalizedName",
                value: "ADMIN");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "94c328af-952d-42a5-ae86-4f0fe6d84d74",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "63d1cda0-bf3e-455a-ba5e-71115b957847", "AQAAAAIAAYagAAAAEOKi/uxmyVzJVg3pJkPvW56v7p1YOy28pg+Y88DJWbwpKfK5uaMDtsSpeBBPW2qCDA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "336e1648-5384-4d2c-b886-0281db620ccb",
                column: "NormalizedName",
                value: "User");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a2c4fe5-5b10-45b6-a1f6-7cfecc629d3f",
                column: "NormalizedName",
                value: "Admin");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "94c328af-952d-42a5-ae86-4f0fe6d84d74",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e59508ae-186f-4c72-a907-41ae9514768d", "AQAAAAIAAYagAAAAEH3WgSNbnEELjy35/Hbf6oeylvCFtDuHYoWQg6o6fhj1wzWNX2H/D9RSw5B4RumdPQ==" });
        }
    }
}
