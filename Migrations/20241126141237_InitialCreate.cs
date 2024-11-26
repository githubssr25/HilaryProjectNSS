using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HilaryProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    DurationMinutes = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "Stylists",
                columns: table => new
                {
                    StylistId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stylists", x => x.StylistId);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    StylistId = table.Column<int>(type: "integer", nullable: false),
                    TimeOf = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsCancelled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Stylists_StylistId",
                        column: x => x.StylistId,
                        principalTable: "Stylists",
                        principalColumn: "StylistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StylistServices",
                columns: table => new
                {
                    StylistServiceId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StylistId = table.Column<int>(type: "integer", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StylistServices", x => x.StylistServiceId);
                    table.ForeignKey(
                        name: "FK_StylistServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StylistServices_Stylists_StylistId",
                        column: x => x.StylistId,
                        principalTable: "Stylists",
                        principalColumn: "StylistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentServices",
                columns: table => new
                {
                    AppointmentServiceId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppointmentId = table.Column<int>(type: "integer", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric", nullable: false)
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

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Name" },
                values: new object[,]
                {
                    { 1, "Alice Johnson" },
                    { 2, "Bob Smith" },
                    { 3, "Charlie Brown" },
                    { 4, "Diane Evans" },
                    { 5, "Ethan Parker" },
                    { 6, "Fiona Grey" },
                    { 7, "Grace White" },
                    { 8, "Henry Green" },
                    { 9, "Isla Blue" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "DurationMinutes", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 60, "Haircut", 20.00m },
                    { 2, 120, "Hair Coloring", 50.00m },
                    { 3, 30, "Beard Trim", 15.00m },
                    { 4, 150, "Perm", 70.00m },
                    { 5, 90, "Hair Treatment", 30.00m }
                });

            migrationBuilder.InsertData(
                table: "Stylists",
                columns: new[] { "StylistId", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, true, "Sophia Carter" },
                    { 2, true, "Oliver Davis" },
                    { 3, false, "Liam Taylor" },
                    { 4, true, "Emma Wilson" },
                    { 5, true, "Ava Moore" },
                    { 6, false, "Mason Harris" },
                    { 7, true, "Lucas Walker" },
                    { 8, true, "Mia Hall" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentId", "CustomerId", "IsCancelled", "StylistId", "TimeOf" },
                values: new object[,]
                {
                    { 1, 1, false, 1, new DateTime(2024, 12, 1, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, false, 2, new DateTime(2024, 12, 1, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, true, 4, new DateTime(2024, 12, 1, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 4, true, 5, new DateTime(2024, 12, 2, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 5, true, 6, new DateTime(2024, 12, 2, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 6, false, 7, new DateTime(2024, 12, 2, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 7, false, 8, new DateTime(2024, 12, 3, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 8, false, 1, new DateTime(2024, 12, 3, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 9, false, 2, new DateTime(2024, 12, 3, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 1, false, 3, new DateTime(2024, 12, 4, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 2, false, 4, new DateTime(2024, 12, 4, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 3, false, 5, new DateTime(2024, 12, 4, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, 4, false, 7, new DateTime(2024, 12, 5, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, 5, false, 8, new DateTime(2024, 12, 5, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, 6, false, 1, new DateTime(2024, 12, 5, 12, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CustomerId",
                table: "Appointments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_StylistId",
                table: "Appointments",
                column: "StylistId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentServices_AppointmentId",
                table: "AppointmentServices",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentServices_ServiceId",
                table: "AppointmentServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_StylistServices_ServiceId",
                table: "StylistServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_StylistServices_StylistId",
                table: "StylistServices",
                column: "StylistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentServices");

            migrationBuilder.DropTable(
                name: "StylistServices");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Stylists");
        }
    }
}
