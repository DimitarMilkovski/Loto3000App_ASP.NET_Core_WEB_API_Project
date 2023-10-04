using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loto3000_App.DataAcess.Migrations
{
    /// <inheritdoc />
    public partial class addedPropertyOngoingInSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ongoing",
                table: "Sessions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ongoing",
                table: "Sessions");
        }
    }
}
