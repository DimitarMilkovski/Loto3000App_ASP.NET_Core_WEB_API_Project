using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loto3000_App.DataAcess.Migrations
{
    /// <inheritdoc />
    public partial class addedComputedOngoinSessionColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Ongoing",
                table: "Sessions",
                type: "bit",
                nullable: false,
                computedColumnSql: "CASE WHEN EndDate < GETDATE() THEN 0 ELSE 1 END",
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Ongoing",
                table: "Sessions",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComputedColumnSql: "CASE WHEN EndDate < GETDATE() THEN 0 ELSE 1 END");
        }
    }
}
