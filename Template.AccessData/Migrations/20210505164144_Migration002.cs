using Microsoft.EntityFrameworkCore.Migrations;

namespace Template.AccessData.Migrations
{
    public partial class Migration002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EstadoReserva",
                columns: new[] { "EstadoReservaId", "Descripcion" },
                values: new object[] { 4, "Cancelado" });

            migrationBuilder.InsertData(
                table: "EstadoReserva",
                columns: new[] { "EstadoReservaId", "Descripcion" },
                values: new object[] { 5, "CanceladoAdmin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EstadoReserva",
                keyColumn: "EstadoReservaId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EstadoReserva",
                keyColumn: "EstadoReservaId",
                keyValue: 5);
        }
    }
}
