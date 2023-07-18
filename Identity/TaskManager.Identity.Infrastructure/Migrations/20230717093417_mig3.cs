using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "94c328af-952d-42a5-ae86-4f0fe6d84d74",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c925ada8-0db8-44a7-b2be-1f209da39069", "AQAAAAIAAYagAAAAEE1IedHyPA9v9OZiFkt66idUnW5UBBoNS21uQamq4IVgNuZNTWLGIARER4SfglVTUQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "94c328af-952d-42a5-ae86-4f0fe6d84d74",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "85af28c2-a9b8-4f79-addf-4a5ff52cd452", null });
        }
    }
}
