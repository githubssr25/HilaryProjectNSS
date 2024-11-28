using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HilaryProject.Migrations
{
    /// <inheritdoc />
    public partial class RenameStylistIdToId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StylistId",
                table: "Stylists",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Stylists",
                newName: "StylistId");
        }
    }
}
