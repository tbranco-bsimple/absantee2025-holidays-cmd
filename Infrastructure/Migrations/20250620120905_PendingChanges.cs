using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PendingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collaborators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PeriodDateTime__initDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PeriodDateTime__finalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaborators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HolidayPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CollaboratorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HolidayPeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PeriodDate_InitDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PeriodDate_FinalDate = table.Column<DateOnly>(type: "date", nullable: false),
                    HolidayPlanDataModelId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HolidayPeriods_HolidayPlans_HolidayPlanDataModelId",
                        column: x => x.HolidayPlanDataModelId,
                        principalTable: "HolidayPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HolidayPeriods_HolidayPlanDataModelId",
                table: "HolidayPeriods",
                column: "HolidayPlanDataModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Collaborators");

            migrationBuilder.DropTable(
                name: "HolidayPeriods");

            migrationBuilder.DropTable(
                name: "HolidayPlans");
        }
    }
}
