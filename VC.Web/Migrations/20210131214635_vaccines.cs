using Microsoft.EntityFrameworkCore.Migrations;

namespace VC.Web.Migrations
{
    public partial class vaccines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DosesNumber",
                table: "Vaccines",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DosesNumber",
                table: "Vaccines");
        }
    }
}
