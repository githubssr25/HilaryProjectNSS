using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HilaryProject.Migrations
{
    public partial class UpdatingAppointmentServiceJoin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add missing services to AppointmentServiceJoin table
            migrationBuilder.Sql(@"
                INSERT INTO ""AppointmentServices"" (""AppointmentId"", ""ServiceId"", ""Cost"") VALUES
                (3, 1, 20.0), -- AppointmentId 3, StylistId 4
                (4, 3, 15.0), -- AppointmentId 4, StylistId 5
                (5, 2, 50.0), -- AppointmentId 5, StylistId 6
                (7, 1, 20.0), -- AppointmentId 7, StylistId 7
                (10, 3, 15.0), -- AppointmentId 10, StylistId 3
                (11, 4, 70.0), -- AppointmentId 11, StylistId 4
                (13, 3, 15.0), -- AppointmentId 13, StylistId 7
                (19, 1, 20.0), -- AppointmentId 19, StylistId 5
                (20, 3, 15.0), -- AppointmentId 20, StylistId 5
                (21, 5, 30.0), -- AppointmentId 21, StylistId 5
                (22, 2, 50.0), -- AppointmentId 22, StylistId 5
                (23, 1, 20.0), -- AppointmentId 23, StylistId 5
                (24, 3, 15.0), -- AppointmentId 24, StylistId 3
                (25, 2, 50.0); -- AppointmentId 25, StylistId 4
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove inserted rows in case of rollback
            migrationBuilder.Sql(@"
                DELETE FROM ""AppointmentServices"" 
                WHERE ""AppointmentId"" IN (3, 4, 5, 7, 10, 11, 13, 19, 20, 21, 22, 23, 24, 25);
            ");
        }
    }
}
