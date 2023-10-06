using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loto3000_App.DataAcess.Migrations
{
    /// <inheritdoc />
    public partial class autoIncrementTicketNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "TicketNumber",
                startValue: 1000L);

            migrationBuilder.AlterColumn<int>(
                name: "TicketNumber",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR TicketNumber",
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "TicketNumber");

            migrationBuilder.AlterColumn<int>(
                name: "TicketNumber",
                table: "Tickets",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "NEXT VALUE FOR TicketNumber");
        }
    }
}
