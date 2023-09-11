using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig_10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "aspnetusers",
                keyColumn: "id",
                keyValue: "94c328af-952d-42a5-ae86-4f0fe6d84d74",
                columns: new[] { "concurrencystamp", "passwordhash" },
                values: new object[] { "246bab49-f909-411b-8486-22c37813669a", "AQAAAAIAAYagAAAAELp1IrTg/NCcP1/UMulQs/u0NBuCpv7Q00NwQImek9V0vhPnGPOossc5+8BkjQKcfw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "aspnetusers",
                keyColumn: "id",
                keyValue: "94c328af-952d-42a5-ae86-4f0fe6d84d74",
                columns: new[] { "concurrencystamp", "passwordhash" },
                values: new object[] { "2d2f2831-f1ff-4a7b-9bd3-d2ed47628dcb", "AQAAAAIAAYagAAAAEFlvJwAx84Hz1NhWjAeOQOhLAtsdsQKzsrmHHLQAX8De4hV3OKLXVLWIvUHzy7HBUQ==" });
        }
    }
}
