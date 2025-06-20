using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UptimeMonitor.API.Migrations
{
    /// <inheritdoc />
    public partial class AddUptimeEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UptimeEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UptimeCheckId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsUp = table.Column<bool>(type: "bit", nullable: false),
                    IsFalsePositive = table.Column<bool>(type: "bit", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JiraTicket = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaintenanceType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UptimeEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UptimeEvents_UptimeChecks_UptimeCheckId",
                        column: x => x.UptimeCheckId,
                        principalTable: "UptimeChecks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UptimeEvents_UptimeCheckId",
                table: "UptimeEvents",
                column: "UptimeCheckId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UptimeEvents");
        }
    }
}
