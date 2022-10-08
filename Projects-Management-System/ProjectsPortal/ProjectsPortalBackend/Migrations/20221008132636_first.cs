using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace assetManagementBackend.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BudgetSources",
                columns: table => new
                {
                    BudgetSourceID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BudgetSourceName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetSources", x => x.BudgetSourceID);
                });

            migrationBuilder.CreateTable(
                name: "LogOperations",
                columns: table => new
                {
                    LogID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LogSituation = table.Column<bool>(type: "boolean", nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    Process = table.Column<string>(type: "text", nullable: true),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserIp = table.Column<string>(type: "text", nullable: true),
                    Statement = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogOperations", x => x.LogID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserFirstname = table.Column<string>(type: "text", nullable: true),
                    UserSecondname = table.Column<string>(type: "text", nullable: true),
                    UserEmail = table.Column<string>(type: "text", nullable: true),
                    UserRole = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    UserDepartment = table.Column<string>(type: "text", nullable: true),
                    UserPassword = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    DistrictID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DistrictName = table.Column<string>(type: "text", nullable: true),
                    BudgetSourceID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.DistrictID);
                    table.ForeignKey(
                        name: "FK_Districts_BudgetSources_BudgetSourceID",
                        column: x => x.BudgetSourceID,
                        principalTable: "BudgetSources",
                        principalColumn: "BudgetSourceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessUnits",
                columns: table => new
                {
                    BusinessUnitID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DistrictID = table.Column<int>(type: "integer", nullable: false),
                    BusinessUnitName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessUnits", x => x.BusinessUnitID);
                    table.ForeignKey(
                        name: "FK_BusinessUnits_Districts_DistrictID",
                        column: x => x.DistrictID,
                        principalTable: "Districts",
                        principalColumn: "DistrictID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    BusinessUnitID = table.Column<int>(type: "integer", nullable: false),
                    Budget = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_BusinessUnits_BusinessUnitID",
                        column: x => x.BusinessUnitID,
                        principalTable: "BusinessUnits",
                        principalColumn: "BusinessUnitID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessUnits_DistrictID",
                table: "BusinessUnits",
                column: "DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_BudgetSourceID",
                table: "Districts",
                column: "BudgetSourceID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_BusinessUnitID",
                table: "Projects",
                column: "BusinessUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserID",
                table: "Projects",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogOperations");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "BusinessUnits");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "BudgetSources");
        }
    }
}
