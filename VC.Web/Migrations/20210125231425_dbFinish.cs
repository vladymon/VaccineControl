using Microsoft.EntityFrameworkCore.Migrations;

namespace VC.Web.Migrations
{
    public partial class dbFinish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VaccineId",
                table: "VaccineRequests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VaccineRequests_VaccineId",
                table: "VaccineRequests",
                column: "VaccineId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineRequests_Vaccines_VaccineId",
                table: "VaccineRequests",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VaccineRequests_Vaccines_VaccineId",
                table: "VaccineRequests");

            migrationBuilder.DropIndex(
                name: "IX_VaccineRequests_VaccineId",
                table: "VaccineRequests");

            migrationBuilder.DropColumn(
                name: "VaccineId",
                table: "VaccineRequests");
        }
    }
}
