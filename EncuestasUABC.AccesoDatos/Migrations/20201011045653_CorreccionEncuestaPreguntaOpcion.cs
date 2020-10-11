using Microsoft.EntityFrameworkCore.Migrations;

namespace EncuestasUABC.AccesoDatos.Migrations
{
    public partial class CorreccionEncuestaPreguntaOpcion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EncuestaPreguntaOpciones_EncuestaPreguntas_EncuestaId_EncuestaSeccionId_EncuestaPreguntaId",
                table: "EncuestaPreguntaOpciones");

            migrationBuilder.DropIndex(
                name: "IX_EncuestaPreguntaOpciones_EncuestaId_EncuestaSeccionId_EncuestaPreguntaId",
                table: "EncuestaPreguntaOpciones");

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaPreguntaOpciones_EncuestaPreguntaId_EncuestaId_EncuestaSeccionId",
                table: "EncuestaPreguntaOpciones",
                columns: new[] { "EncuestaPreguntaId", "EncuestaId", "EncuestaSeccionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EncuestaPreguntaOpciones_EncuestaPreguntas_EncuestaPreguntaId_EncuestaId_EncuestaSeccionId",
                table: "EncuestaPreguntaOpciones",
                columns: new[] { "EncuestaPreguntaId", "EncuestaId", "EncuestaSeccionId" },
                principalTable: "EncuestaPreguntas",
                principalColumns: new[] { "Id", "EncuestaId", "EncuestaSeccionId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EncuestaPreguntaOpciones_EncuestaPreguntas_EncuestaPreguntaId_EncuestaId_EncuestaSeccionId",
                table: "EncuestaPreguntaOpciones");

            migrationBuilder.DropIndex(
                name: "IX_EncuestaPreguntaOpciones_EncuestaPreguntaId_EncuestaId_EncuestaSeccionId",
                table: "EncuestaPreguntaOpciones");

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaPreguntaOpciones_EncuestaId_EncuestaSeccionId_EncuestaPreguntaId",
                table: "EncuestaPreguntaOpciones",
                columns: new[] { "EncuestaId", "EncuestaSeccionId", "EncuestaPreguntaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EncuestaPreguntaOpciones_EncuestaPreguntas_EncuestaId_EncuestaSeccionId_EncuestaPreguntaId",
                table: "EncuestaPreguntaOpciones",
                columns: new[] { "EncuestaId", "EncuestaSeccionId", "EncuestaPreguntaId" },
                principalTable: "EncuestaPreguntas",
                principalColumns: new[] { "Id", "EncuestaId", "EncuestaSeccionId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
