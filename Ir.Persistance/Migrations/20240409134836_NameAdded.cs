using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ir.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class NameAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OpportunityName",
                table: "Opportunities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LeadName",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpportunityName",
                table: "Opportunities");

            migrationBuilder.DropColumn(
                name: "LeadName",
                table: "Leads");
        }
    }
}
