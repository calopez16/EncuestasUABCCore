using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EncuestasUABC.AccesoDatos.Migrations
{
    public partial class Encuestas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstatusEncuesta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstatusEncuesta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposPregunta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposPregunta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Encuestas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    EstatusEncuestaId = table.Column<int>(nullable: false),
                    CarreraId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encuestas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Encuestas_Carreras_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carreras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Encuestas_EstatusEncuesta_EstatusEncuestaId",
                        column: x => x.EstatusEncuestaId,
                        principalTable: "EstatusEncuesta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Encuestas_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EncuestaSecciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EncuestaId = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Orden = table.Column<int>(nullable: false),
                    Eliminado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncuestaSecciones", x => new { x.Id, x.EncuestaId });
                    table.ForeignKey(
                        name: "FK_EncuestaSecciones_Encuestas_EncuestaId",
                        column: x => x.EncuestaId,
                        principalTable: "Encuestas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EncuestaPreguntas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EncuestaId = table.Column<int>(nullable: false),
                    EncuestaSeccionId = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    TipoPreguntaId = table.Column<int>(nullable: false),
                    Obligatoria = table.Column<bool>(nullable: false),
                    Orden = table.Column<int>(nullable: false),
                    EncuestaIdPadre = table.Column<int>(nullable: true),
                    EncuestaSeccionIdPadre = table.Column<int>(nullable: true),
                    EncuestaPreguntaIdPadre = table.Column<int>(nullable: true),
                    Eliminado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncuestaPreguntas", x => new { x.Id, x.EncuestaId, x.EncuestaSeccionId });
                    table.ForeignKey(
                        name: "FK_EncuestaPreguntas_TiposPregunta_TipoPreguntaId",
                        column: x => x.TipoPreguntaId,
                        principalTable: "TiposPregunta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EncuestaPreguntas_EncuestaSecciones_EncuestaId_EncuestaSeccionId",
                        columns: x => new { x.EncuestaId, x.EncuestaSeccionId },
                        principalTable: "EncuestaSecciones",
                        principalColumns: new[] { "Id", "EncuestaId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EncuestaPreguntas_EncuestaPreguntas_EncuestaIdPadre_EncuestaSeccionIdPadre_EncuestaPreguntaIdPadre",
                        columns: x => new { x.EncuestaIdPadre, x.EncuestaSeccionIdPadre, x.EncuestaPreguntaIdPadre },
                        principalTable: "EncuestaPreguntas",
                        principalColumns: new[] { "Id", "EncuestaId", "EncuestaSeccionId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EncuestaPreguntaOpciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EncuestaId = table.Column<int>(nullable: false),
                    EncuestaSeccionId = table.Column<int>(nullable: false),
                    EncuestaPreguntaId = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    Orden = table.Column<int>(nullable: false),
                    Eliminado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncuestaPreguntaOpciones", x => new { x.Id, x.EncuestaId, x.EncuestaSeccionId, x.EncuestaPreguntaId });
                    table.ForeignKey(
                        name: "FK_EncuestaPreguntaOpciones_EncuestaPreguntas_EncuestaId_EncuestaSeccionId_EncuestaPreguntaId",
                        columns: x => new { x.EncuestaId, x.EncuestaSeccionId, x.EncuestaPreguntaId },
                        principalTable: "EncuestaPreguntas",
                        principalColumns: new[] { "Id", "EncuestaId", "EncuestaSeccionId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaPreguntaOpciones_EncuestaId_EncuestaSeccionId_EncuestaPreguntaId",
                table: "EncuestaPreguntaOpciones",
                columns: new[] { "EncuestaId", "EncuestaSeccionId", "EncuestaPreguntaId" });

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaPreguntas_TipoPreguntaId",
                table: "EncuestaPreguntas",
                column: "TipoPreguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaPreguntas_EncuestaId_EncuestaSeccionId",
                table: "EncuestaPreguntas",
                columns: new[] { "EncuestaId", "EncuestaSeccionId" });

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaPreguntas_EncuestaIdPadre_EncuestaSeccionIdPadre_EncuestaPreguntaIdPadre",
                table: "EncuestaPreguntas",
                columns: new[] { "EncuestaIdPadre", "EncuestaSeccionIdPadre", "EncuestaPreguntaIdPadre" });

            migrationBuilder.CreateIndex(
                name: "IX_Encuestas_CarreraId",
                table: "Encuestas",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_Encuestas_EstatusEncuestaId",
                table: "Encuestas",
                column: "EstatusEncuestaId");

            migrationBuilder.CreateIndex(
                name: "IX_Encuestas_UsuarioId",
                table: "Encuestas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaSecciones_EncuestaId",
                table: "EncuestaSecciones",
                column: "EncuestaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EncuestaPreguntaOpciones");

            migrationBuilder.DropTable(
                name: "EncuestaPreguntas");

            migrationBuilder.DropTable(
                name: "TiposPregunta");

            migrationBuilder.DropTable(
                name: "EncuestaSecciones");

            migrationBuilder.DropTable(
                name: "Encuestas");

            migrationBuilder.DropTable(
                name: "EstatusEncuesta");
        }
    }
}
