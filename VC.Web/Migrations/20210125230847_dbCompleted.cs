using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VC.Web.Migrations
{
    public partial class dbCompleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VaccineRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccineRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaccineRequests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "vaccineRequestDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClinicId = table.Column<int>(nullable: true),
                    CalendarId = table.Column<DateTime>(nullable: true),
                    RequestStatusId = table.Column<int>(nullable: true),
                    VaccineRequestId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vaccineRequestDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vaccineRequestDetails_Calendars_CalendarId",
                        column: x => x.CalendarId,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_vaccineRequestDetails_Clinic_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_vaccineRequestDetails_RequestStatuses_RequestStatusId",
                        column: x => x.RequestStatusId,
                        principalTable: "RequestStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_vaccineRequestDetails_VaccineRequests_VaccineRequestId",
                        column: x => x.VaccineRequestId,
                        principalTable: "VaccineRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_vaccineRequestDetails_CalendarId",
                table: "vaccineRequestDetails",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_vaccineRequestDetails_ClinicId",
                table: "vaccineRequestDetails",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_vaccineRequestDetails_RequestStatusId",
                table: "vaccineRequestDetails",
                column: "RequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_vaccineRequestDetails_VaccineRequestId",
                table: "vaccineRequestDetails",
                column: "VaccineRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineRequests_UserId",
                table: "VaccineRequests",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vaccineRequestDetails");

            migrationBuilder.DropTable(
                name: "RequestStatuses");

            migrationBuilder.DropTable(
                name: "VaccineRequests");
        }
    }
}
