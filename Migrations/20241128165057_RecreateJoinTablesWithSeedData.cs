using Microsoft.EntityFrameworkCore.Migrations;

using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
 

#nullable disable

namespace HilaryProject.Migrations
{
    /// <inheritdoc />
   public partial class RecreateJoinTablesWithSeedData : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Recreate AppointmentServices table
        migrationBuilder.CreateTable(
            name: "AppointmentServices",
            columns: table => new
            {
                AppointmentServiceId = table.Column<int>(nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                AppointmentId = table.Column<int>(nullable: false),
                ServiceId = table.Column<int>(nullable: false),
                Cost = table.Column<decimal>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AppointmentServices", x => x.AppointmentServiceId);
                table.ForeignKey(
                    name: "FK_AppointmentServices_Appointments_AppointmentId",
                    column: x => x.AppointmentId,
                    principalTable: "Appointments",
                    principalColumn: "AppointmentId",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_AppointmentServices_Services_ServiceId",
                    column: x => x.ServiceId,
                    principalTable: "Services",
                    principalColumn: "ServiceId",
                    onDelete: ReferentialAction.Cascade);
            });

        // Recreate StylistServices table
        migrationBuilder.CreateTable(
            name: "StylistServices",
            columns: table => new
            {
                StylistServiceId = table.Column<int>(nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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

        // Seed data for AppointmentServices
        migrationBuilder.InsertData(
            table: "AppointmentServices",
            columns: new[] { "AppointmentServiceId", "AppointmentId", "ServiceId", "Cost" },
            values: new object[,]
            {
                { 1, 1, 1, 20.00m }, // Haircut for Appointment 1
                { 2, 1, 2, 50.00m }, // Hair Coloring for Appointment 1
                { 3, 2, 3, 15.00m }, // Beard Trim for Appointment 2
                { 4, 2, 4, 70.00m }, // Perm for Appointment 2
                { 5, 6, 5, 30.00m }  // Hair Treatment for Appointment 6
            });

        // Seed data for StylistServices
        migrationBuilder.InsertData(
            table: "StylistServices",
            columns: new[] { "StylistServiceId", "StylistId", "ServiceId" },
            values: new object[,]
            {
                { 1, 1, 1 }, // Sophia Carter offers Haircuts
                { 2, 1, 2 }, // Sophia Carter offers Hair Coloring
                { 3, 2, 3 }, // Oliver Davis offers Beard Trims
                { 4, 2, 4 }, // Oliver Davis offers Perms
                { 5, 4, 5 }  // Emma Wilson offers Hair Treatments
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // Drop the recreated tables
        migrationBuilder.DropTable(
            name: "AppointmentServices");

        migrationBuilder.DropTable(
            name: "StylistServices");
    }
}
}
