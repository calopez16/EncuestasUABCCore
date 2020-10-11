using Microsoft.EntityFrameworkCore.Migrations;

namespace EncuestasUABC.AccesoDatos.Migrations
{
    public partial class CorreccionEncuestaPregunta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EncuestaPreguntas_EncuestaSecciones_EncuestaId_EncuestaSeccionId",
                table: "EncuestaPreguntas");

            migrationBuilder.DropForeignKey(
                name: "FK_EncuestaPreguntas_EncuestaPreguntas_EncuestaIdPadre_EncuestaSeccionIdPadre_EncuestaPreguntaIdPadre",
                table: "EncuestaPreguntas");

            migrationBuilder.DropIndex(
                name: "IX_EncuestaPreguntas_EncuestaId_EncuestaSeccionId",
                table: "EncuestaPreguntas");

            migrationBuilder.DropIndex(
                name: "IX_EncuestaPreguntas_EncuestaIdPadre_EncuestaSeccionIdPadre_EncuestaPreguntaIdPadre",
                table: "EncuestaPreguntas");

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaPreguntas_EncuestaSeccionId_EncuestaId",
                table: "EncuestaPreguntas",
                columns: new[] { "EncuestaSeccionId", "EncuestaId" });

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaPreguntas_EncuestaSeccionIdPadre_EncuestaIdPadre_EncuestaPreguntaIdPadre",
                table: "EncuestaPreguntas",
                columns: new[] { "EncuestaSeccionIdPadre", "EncuestaIdPadre", "EncuestaPreguntaIdPadre" });

            migrationBuilder.AddForeignKey(
                name: "FK_EncuestaPreguntas_EncuestaSecciones_EncuestaSeccionId_EncuestaId",
                table: "EncuestaPreguntas",
                columns: new[] { "EncuestaSeccionId", "EncuestaId" },
                principalTable: "EncuestaSecciones",
                principalColumns: new[] { "Id", "EncuestaId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EncuestaPreguntas_EncuestaPreguntas_EncuestaSeccionIdPadre_EncuestaIdPadre_EncuestaPreguntaIdPadre",
                table: "EncuestaPreguntas",
                columns: new[] { "EncuestaSeccionIdPadre", "EncuestaIdPadre", "EncuestaPreguntaIdPadre" },
                principalTable: "EncuestaPreguntas",
                principalColumns: new[] { "Id", "EncuestaId", "EncuestaSeccionId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EncuestaPreguntas_EncuestaSecciones_EncuestaSeccionId_EncuestaId",
                table: "EncuestaPreguntas");

            migrationBuilder.DropForeignKey(
                name: "FK_EncuestaPreguntas_EncuestaPreguntas_EncuestaSeccionIdPadre_EncuestaIdPadre_EncuestaPreguntaIdPadre",
                table: "EncuestaPreguntas");

            migrationBuilder.DropIndex(
                name: "IX_EncuestaPreguntas_EncuestaSeccionId_EncuestaId",
                table: "EncuestaPreguntas");

            migrationBuilder.DropIndex(
                name: "IX_EncuestaPreguntas_EncuestaSeccionIdPadre_EncuestaIdPadre_EncuestaPreguntaIdPadre",
                table: "EncuestaPreguntas");

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaPreguntas_EncuestaId_EncuestaSeccionId",
                table: "EncuestaPreguntas",
                columns: new[] { "EncuestaId", "EncuestaSeccionId" });

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaPreguntas_EncuestaIdPadre_EncuestaSeccionIdPadre_EncuestaPreguntaIdPadre",
                table: "EncuestaPreguntas",
                columns: new[] { "EncuestaIdPadre", "EncuestaSeccionIdPadre", "EncuestaPreguntaIdPadre" });

            migrationBuilder.AddForeignKey(
                name: "FK_EncuestaPreguntas_EncuestaSecciones_EncuestaId_EncuestaSeccionId",
                table: "EncuestaPreguntas",
                columns: new[] { "EncuestaId", "EncuestaSeccionId" },
                principalTable: "EncuestaSecciones",
                principalColumns: new[] { "Id", "EncuestaId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EncuestaPreguntas_EncuestaPreguntas_EncuestaIdPadre_EncuestaSeccionIdPadre_EncuestaPreguntaIdPadre",
                table: "EncuestaPreguntas",
                columns: new[] { "EncuestaIdPadre", "EncuestaSeccionIdPadre", "EncuestaPreguntaIdPadre" },
                principalTable: "EncuestaPreguntas",
                principalColumns: new[] { "Id", "EncuestaId", "EncuestaSeccionId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
