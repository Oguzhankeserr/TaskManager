using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "94c328af-952d-42a5-ae86-4f0fe6d84d74",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "e59508ae-186f-4c72-a907-41ae9514768d", "ADMIN@GMAIL.COM", "FIRSTADMIN", "AQAAAAIAAYagAAAAEH3WgSNbnEELjy35/Hbf6oeylvCFtDuHYoWQg6o6fhj1wzWNX2H/D9RSw5B4RumdPQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "94c328af-952d-42a5-ae86-4f0fe6d84d74",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "c925ada8-0db8-44a7-b2be-1f209da39069", null, null, "AQAAAAIAAYagAAAAEE1IedHyPA9v9OZiFkt66idUnW5UBBoNS21uQamq4IVgNuZNTWLGIARER4SfglVTUQ==" });
        }
    }
}
