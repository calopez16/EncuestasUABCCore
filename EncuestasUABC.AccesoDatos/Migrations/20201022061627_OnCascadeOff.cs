using Microsoft.EntityFrameworkCore.Migrations;

namespace EncuestasUABC.AccesoDatos.Migrations
{
    public partial class OnCascadeOff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carreras_UnidadesAcademicas_UnidadAcademicaId",
                table: "Carreras");

            migrationBuilder.DropForeignKey(
                name: "FK_EncuestaPreguntaOpciones_EncuestaPreguntas_EncuestaPreguntaId_EncuestaId_EncuestaSeccionId",
                table: "EncuestaPreguntaOpciones");

            migrationBuilder.DropForeignKey(
                name: "FK_EncuestaPreguntas_TiposPregunta_TipoPreguntaId",
                table: "EncuestaPreguntas");

            migrationBuilder.DropForeignKey(
                name: "FK_EncuestaPreguntas_EncuestaSecciones_EncuestaSeccionId_EncuestaId",
                table: "EncuestaPreguntas");

            migrationBuilder.DropForeignKey(
                name: "FK_Encuestas_Carreras_CarreraId",
                table: "Encuestas");

            migrationBuilder.DropForeignKey(
                name: "FK_Encuestas_EstatusEncuesta_EstatusEncuestaId",
                table: "Encuestas");

            migrationBuilder.DropForeignKey(
                name: "FK_EncuestaSecciones_Encuestas_EncuestaId",
                table: "EncuestaSecciones");

            migrationBuilder.DropForeignKey(
                name: "FK_UnidadesAcademicas_Campus_CampusId",
                table: "UnidadesAcademicas");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosPermisos_Permisos_PermisoId",
                table: "UsuariosPermisos");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosPermisos_AspNetUsers_UsuarioId",
                table: "UsuariosPermisos");

            migrationBuilder.AddForeignKey(
                name: "FK_Carreras_UnidadesAcademicas_UnidadAcademicaId",
                table: "Carreras",
                column: "UnidadAcademicaId",
                principalTable: "UnidadesAcademicas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EncuestaPreguntaOpciones_EncuestaPreguntas_EncuestaPreguntaId_EncuestaId_EncuestaSeccionId",
                table: "EncuestaPreguntaOpciones",
                columns: new[] { "EncuestaPreguntaId", "EncuestaId", "EncuestaSeccionId" },
                principalTable: "EncuestaPreguntas",
                principalColumns: new[] { "Id", "EncuestaId", "EncuestaSeccionId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EncuestaPreguntas_TiposPregunta_TipoPreguntaId",
                table: "EncuestaPreguntas",
                column: "TipoPreguntaId",
                principalTable: "TiposPregunta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EncuestaPreguntas_EncuestaSecciones_EncuestaSeccionId_EncuestaId",
                table: "EncuestaPreguntas",
                columns: new[] { "EncuestaSeccionId", "EncuestaId" },
                principalTable: "EncuestaSecciones",
                principalColumns: new[] { "Id", "EncuestaId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Encuestas_Carreras_CarreraId",
                table: "Encuestas",
                column: "CarreraId",
                principalTable: "Carreras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Encuestas_EstatusEncuesta_EstatusEncuestaId",
                table: "Encuestas",
                column: "EstatusEncuestaId",
                principalTable: "EstatusEncuesta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EncuestaSecciones_Encuestas_EncuestaId",
                table: "EncuestaSecciones",
                column: "EncuestaId",
                principalTable: "Encuestas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnidadesAcademicas_Campus_CampusId",
                table: "UnidadesAcademicas",
                column: "CampusId",
                principalTable: "Campus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosPermisos_Permisos_PermisoId",
                table: "UsuariosPermisos",
                column: "PermisoId",
                principalTable: "Permisos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosPermisos_AspNetUsers_UsuarioId",
                table: "UsuariosPermisos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carreras_UnidadesAcademicas_UnidadAcademicaId",
                table: "Carreras");

            migrationBuilder.DropForeignKey(
                name: "FK_EncuestaPreguntaOpciones_EncuestaPreguntas_EncuestaPreguntaId_EncuestaId_EncuestaSeccionId",
                table: "EncuestaPreguntaOpciones");

            migrationBuilder.DropForeignKey(
                name: "FK_EncuestaPreguntas_TiposPregunta_TipoPreguntaId",
                table: "EncuestaPreguntas");

            migrationBuilder.DropForeignKey(
                name: "FK_EncuestaPreguntas_EncuestaSecciones_EncuestaSeccionId_EncuestaId",
                table: "EncuestaPreguntas");

            migrationBuilder.DropForeignKey(
                name: "FK_Encuestas_Carreras_CarreraId",
                table: "Encuestas");

            migrationBuilder.DropForeignKey(
                name: "FK_Encuestas_EstatusEncuesta_EstatusEncuestaId",
                table: "Encuestas");

            migrationBuilder.DropForeignKey(
                name: "FK_EncuestaSecciones_Encuestas_EncuestaId",
                table: "EncuestaSecciones");

            migrationBuilder.DropForeignKey(
                name: "FK_UnidadesAcademicas_Campus_CampusId",
                table: "UnidadesAcademicas");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosPermisos_Permisos_PermisoId",
                table: "UsuariosPermisos");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosPermisos_AspNetUsers_UsuarioId",
                table: "UsuariosPermisos");

            migrationBuilder.AddForeignKey(
                name: "FK_Carreras_UnidadesAcademicas_UnidadAcademicaId",
                table: "Carreras",
                column: "UnidadAcademicaId",
                principalTable: "UnidadesAcademicas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EncuestaPreguntaOpciones_EncuestaPreguntas_EncuestaPreguntaId_EncuestaId_EncuestaSeccionId",
                table: "EncuestaPreguntaOpciones",
                columns: new[] { "EncuestaPreguntaId", "EncuestaId", "EncuestaSeccionId" },
                principalTable: "EncuestaPreguntas",
                principalColumns: new[] { "Id", "EncuestaId", "EncuestaSeccionId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EncuestaPreguntas_TiposPregunta_TipoPreguntaId",
                table: "EncuestaPreguntas",
                column: "TipoPreguntaId",
                principalTable: "TiposPregunta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EncuestaPreguntas_EncuestaSecciones_EncuestaSeccionId_EncuestaId",
                table: "EncuestaPreguntas",
                columns: new[] { "EncuestaSeccionId", "EncuestaId" },
                principalTable: "EncuestaSecciones",
                principalColumns: new[] { "Id", "EncuestaId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Encuestas_Carreras_CarreraId",
                table: "Encuestas",
                column: "CarreraId",
                principalTable: "Carreras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Encuestas_EstatusEncuesta_EstatusEncuestaId",
                table: "Encuestas",
                column: "EstatusEncuestaId",
                principalTable: "EstatusEncuesta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EncuestaSecciones_Encuestas_EncuestaId",
                table: "EncuestaSecciones",
                column: "EncuestaId",
                principalTable: "Encuestas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnidadesAcademicas_Campus_CampusId",
                table: "UnidadesAcademicas",
                column: "CampusId",
                principalTable: "Campus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosPermisos_Permisos_PermisoId",
                table: "UsuariosPermisos",
                column: "PermisoId",
                principalTable: "Permisos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosPermisos_AspNetUsers_UsuarioId",
                table: "UsuariosPermisos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
