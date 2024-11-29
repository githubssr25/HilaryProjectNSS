using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HilaryProject.Migrations
{
    public partial class DropStylistServicesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the StylistServices table if it exists
            migrationBuilder.Sql(@"DROP TABLE IF EXISTS ""StylistServices""");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Recreate the StylistServices table in the Down method
            migrationBuilder.CreateTable(
                name: "StylistServices",
                columns: table => new
                {
                    StylistServiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"), // Use this for SQL Server
                    StylistId = table.Column<int>(nullable: false),
                    ServiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StylistServices", x => x.StylistServiceId);
                    table.ForeignKey(
                        name: "FK_StylistServices_Stylists_StylistId",
                        column: x => x.StylistId,
                        principalTable: "Stylists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StylistServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
