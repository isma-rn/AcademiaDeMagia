using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academia.MapeoDatos.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Afinidad",
                columns: table => new
                {
                    AfinidadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Afinidad", x => x.AfinidadId);
                });

            migrationBuilder.CreateTable(
                name: "Grimorio",
                columns: table => new
                {
                    GrimorioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NumeroHojas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grimorio", x => x.GrimorioId);
                });

            migrationBuilder.CreateTable(
                name: "Estudiante",
                columns: table => new
                {
                    EstudianteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Identificacion = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Edad = table.Column<byte>(type: "tinyint", nullable: false),
                    AfinidadId = table.Column<int>(type: "int", nullable: false),
                    GrimorioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiante", x => x.EstudianteId);
                    table.ForeignKey(
                        name: "FK_Estudiante_Afinidad_AfinidadId",
                        column: x => x.AfinidadId,
                        principalTable: "Afinidad",
                        principalColumn: "AfinidadId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Estudiante_Grimorio_GrimorioId",
                        column: x => x.GrimorioId,
                        principalTable: "Grimorio",
                        principalColumn: "GrimorioId");
                });

            migrationBuilder.CreateTable(
                name: "Solicitud",
                columns: table => new
                {
                    SolicitudId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estatus = table.Column<byte>(type: "tinyint", nullable: false),
                    Creacion = table.Column<int>(type: "int", nullable: false),
                    UltimaModificacion = table.Column<int>(type: "int", nullable: true),
                    EstudianteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitud", x => x.SolicitudId);
                    table.ForeignKey(
                        name: "FK_Solicitud_Estudiante_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiante",
                        principalColumn: "EstudianteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estudiante_AfinidadId",
                table: "Estudiante",
                column: "AfinidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiante_GrimorioId",
                table: "Estudiante",
                column: "GrimorioId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitud_EstudianteId",
                table: "Solicitud",
                column: "EstudianteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Solicitud");

            migrationBuilder.DropTable(
                name: "Estudiante");

            migrationBuilder.DropTable(
                name: "Afinidad");

            migrationBuilder.DropTable(
                name: "Grimorio");
        }
    }
}
