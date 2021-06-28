using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Template.AccessData.Migrations
{
    public partial class Migration001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstadoReserva",
                columns: table => new
                {
                    EstadoReservaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoReserva", x => x.EstadoReservaId);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    ReservaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    HabitacionId = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoReservaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.ReservaId);
                    table.ForeignKey(
                        name: "FK_Reserva_EstadoReserva_EstadoReservaId",
                        column: x => x.EstadoReservaId,
                        principalTable: "EstadoReserva",
                        principalColumn: "EstadoReservaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EstadoReserva",
                columns: new[] { "EstadoReservaId", "Descripcion" },
                values: new object[] { 1, "Disponible" });

            migrationBuilder.InsertData(
                table: "EstadoReserva",
                columns: new[] { "EstadoReservaId", "Descripcion" },
                values: new object[] { 2, "Reservado" });

            migrationBuilder.InsertData(
                table: "EstadoReserva",
                columns: new[] { "EstadoReservaId", "Descripcion" },
                values: new object[] { 3, "No ofrecido" });

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_EstadoReservaId",
                table: "Reserva",
                column: "EstadoReservaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "EstadoReserva");
        }
    }
}
