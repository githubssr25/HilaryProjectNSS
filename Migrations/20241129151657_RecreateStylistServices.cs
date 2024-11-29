using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HilaryProject.Migrations
{
    public partial class RecreateStylistServices : Migration
    {
       protected override void Up(MigrationBuilder migrationBuilder)
{
    // Create StylistServices table
    migrationBuilder.CreateTable(
        name: "StylistServices",
        columns: table => new
        {
            StylistServiceId = table.Column<int>(nullable: false)
                .Annotation("Npgsql:ValueGenerationStrategy", Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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

    // Adjusted Seed Data with Proper Quoting
    migrationBuilder.Sql(@"
        INSERT INTO ""StylistServices"" (""StylistId"", ""ServiceId"") VALUES
        (1, 1),
        (1, 2),
        (1, 3),
        (2, 1),
        (2, 2),
        (2, 4),
        (2, 5),
        (3, 3),
        (3, 5),
        (4, 1),
        (4, 2),
        (4, 3),
        (4, 4),
        (4, 5),
        (5, 1),
        (5, 3),
        (5, 5),
        (6, 2),
        (6, 3),
        (6, 4),
        (7, 1),
        (7, 3),
        (7, 5),
        (8, 2),
        (8, 4);
    ");
}

protected override void Down(MigrationBuilder migrationBuilder)
{
    migrationBuilder.DropTable(
        name: "StylistServices");
}

    }

}