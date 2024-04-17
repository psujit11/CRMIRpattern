using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ir.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Assignuserrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Leads",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leads_ApplicationUserId",
                table: "Leads",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_AspNetUsers_ApplicationUserId",
                table: "Leads",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leads_AspNetUsers_ApplicationUserId",
                table: "Leads");

            migrationBuilder.DropIndex(
                name: "IX_Leads_ApplicationUserId",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Leads");
        }
    }
}
