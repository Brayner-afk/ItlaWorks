using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioBooker.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels_V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Motivo",
                table: "Reservas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cualidades",
                table: "Cabinas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Cabinas",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Motivo",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "Cualidades",
                table: "Cabinas");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Cabinas");
        }
    }
}
