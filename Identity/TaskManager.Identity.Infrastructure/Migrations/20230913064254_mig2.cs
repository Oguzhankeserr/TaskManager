using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "aspnetusers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "aspnetusers",
                keyColumn: "id",
                keyValue: "94c328af-952d-42a5-ae86-4f0fe6d84d74",
                columns: new[] { "concurrencystamp", "passwordhash", "status" },
                values: new object[] { "e9d8bd6f-77c7-47d2-870f-a38de4fd11fa", "AQAAAAIAAYagAAAAEPlcr8zaZa1xgn3EmLR803keHFOvokwO3P/TgCUGINpXKcqLg5diZvysH698AiYcJA==", false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "aspnetusers");

            migrationBuilder.UpdateData(
                table: "aspnetusers",
                keyColumn: "id",
                keyValue: "94c328af-952d-42a5-ae86-4f0fe6d84d74",
                columns: new[] { "concurrencystamp", "passwordhash" },
                values: new object[] { "246bab49-f909-411b-8486-22c37813669a", "AQAAAAIAAYagAAAAELp1IrTg/NCcP1/UMulQs/u0NBuCpv7Q00NwQImek9V0vhPnGPOossc5+8BkjQKcfw==" });
        }
    }
}
