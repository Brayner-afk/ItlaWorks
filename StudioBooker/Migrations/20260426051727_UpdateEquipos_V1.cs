using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioBooker.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEquipos_V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Equipos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Equipos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "Equipos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Modelo",
                table: "Equipos",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "Marca",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "Modelo",
                table: "Equipos");
        }
    }
}
