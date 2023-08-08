using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TaskManager.Business.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "columns",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    projectid = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: false),
                    createdbyuser = table.Column<Guid>(type: "uuid", nullable: false),
                    updatedbyuser = table.Column<Guid>(type: "uuid", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updateddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_columns", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "files",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    filename = table.Column<string>(type: "text", nullable: false),
                    path = table.Column<string>(type: "text", nullable: false),
                    storage = table.Column<string>(type: "text", nullable: false),
                    discriminator = table.Column<string>(type: "text", nullable: false),
                    projectid = table.Column<int>(type: "integer", nullable: true),
                    taskid = table.Column<int>(type: "integer", nullable: true),
                    userid = table.Column<int>(type: "integer", nullable: true),
                    createdbyuser = table.Column<Guid>(type: "uuid", nullable: false),
                    updatedbyuser = table.Column<Guid>(type: "uuid", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updateddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_files", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: false),
                    createdbyuser = table.Column<Guid>(type: "uuid", nullable: false),
                    updatedbyuser = table.Column<Guid>(type: "uuid", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updateddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_projects", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "projectusers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    projectid = table.Column<int>(type: "integer", nullable: false),
                    userid = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_projectusers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    projectid = table.Column<int>(type: "integer", nullable: false),
                    columnid = table.Column<int>(type: "integer", nullable: false),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    userupdateddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    enddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: false),
                    assigneeid = table.Column<string>(type: "text", nullable: false),
                    reporterid = table.Column<string>(type: "text", nullable: false),
                    isdone = table.Column<bool>(type: "boolean", nullable: false),
                    createdbyuser = table.Column<Guid>(type: "uuid", nullable: false),
                    updatedbyuser = table.Column<Guid>(type: "uuid", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updateddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tasks", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "columns");

            migrationBuilder.DropTable(
                name: "files");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "projectusers");

            migrationBuilder.DropTable(
                name: "tasks");
        }
    }
}
