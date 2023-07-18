using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManager.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "336e1648-5384-4d2c-b886-0281db620ccb", "2", "User", "User" },
                    { "6a2c4fe5-5b10-45b6-a1f6-7cfecc629d3f", "1", "Admin", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "94c328af-952d-42a5-ae86-4f0fe6d84d74", 0, "85af28c2-a9b8-4f79-addf-4a5ff52cd452", "admin@gmail.com", false, false, null, "First", null, null, null, null, false, null, "Admin", false, "firstAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "6a2c4fe5-5b10-45b6-a1f6-7cfecc629d3f", "94c328af-952d-42a5-ae86-4f0fe6d84d74" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "336e1648-5384-4d2c-b886-0281db620ccb");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6a2c4fe5-5b10-45b6-a1f6-7cfecc629d3f", "94c328af-952d-42a5-ae86-4f0fe6d84d74" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a2c4fe5-5b10-45b6-a1f6-7cfecc629d3f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "94c328af-952d-42a5-ae86-4f0fe6d84d74");
        }
    }
}
