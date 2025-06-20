using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UptimeMonitor.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceSystems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ServiceSystemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Components_ServiceSystems_ServiceSystemId",
                        column: x => x.ServiceSystemId,
                        principalTable: "ServiceSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UptimeChecks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceSystemId = table.Column<int>(type: "int", nullable: false),
                    ComponentId = table.Column<int>(type: "int", nullable: false),
                    CheckUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckInterval = table.Column<int>(type: "int", nullable: false),
                    CheckTimeout = table.Column<int>(type: "int", nullable: false),
                    RequestHeaders = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DownAlertDelay = table.Column<int>(type: "int", nullable: false),
                    DownAlertResend = table.Column<int>(type: "int", nullable: false),
                    DownAlertMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlertEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UptimeChecks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UptimeChecks_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UptimeChecks_ServiceSystems_ServiceSystemId",
                        column: x => x.ServiceSystemId,
                        principalTable: "ServiceSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Components_ServiceSystemId",
                table: "Components",
                column: "ServiceSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_UptimeChecks_ComponentId",
                table: "UptimeChecks",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_UptimeChecks_ServiceSystemId",
                table: "UptimeChecks",
                column: "ServiceSystemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UptimeChecks");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "ServiceSystems");
        }
    }
}
