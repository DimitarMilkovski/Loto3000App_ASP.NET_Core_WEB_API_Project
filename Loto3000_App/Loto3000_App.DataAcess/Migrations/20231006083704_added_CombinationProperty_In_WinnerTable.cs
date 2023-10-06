using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loto3000_App.DataAcess.Migrations
{
    /// <inheritdoc />
    public partial class added_CombinationProperty_In_WinnerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WinningCombinationId",
                table: "Winners",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Winners_WinningCombinationId",
                table: "Winners",
                column: "WinningCombinationId",
                unique: true,
                filter: "[WinningCombinationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Winners_Combinations_WinningCombinationId",
                table: "Winners",
                column: "WinningCombinationId",
                principalTable: "Combinations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Winners_Combinations_WinningCombinationId",
                table: "Winners");

            migrationBuilder.DropIndex(
                name: "IX_Winners_WinningCombinationId",
                table: "Winners");

            migrationBuilder.DropColumn(
                name: "WinningCombinationId",
                table: "Winners");
        }
    }
}
