using Microsoft.EntityFrameworkCore.Migrations;

namespace VC.Web.Migrations
{
    public partial class documentUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Document",
                table: "AspNetUsers",
                column: "Document",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Document",
                table: "AspNetUsers");
        }
    }
}
