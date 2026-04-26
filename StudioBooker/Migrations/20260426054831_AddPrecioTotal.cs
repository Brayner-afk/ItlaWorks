using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioBooker.Migrations
{
    /// <inheritdoc />
    public partial class AddPrecioTotal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PrecioTotal",
                table: "Reservas",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioTotal",
                table: "Reservas");
        }
    }
}
