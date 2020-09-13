﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EncuestasUABC.AccesoDatos.Migrations
{
    public partial class PrimeraMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 80, nullable: true),
                    ApellidoPaterno = table.Column<string>(maxLength: 80, nullable: true),
                    ApellidoMaterno = table.Column<string>(maxLength: 80, nullable: true),
                    Activo = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Campus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Egresados",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeriodoIngreso = table.Column<string>(nullable: true),
                    PeriodoEgreso = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    CorreoAlterno = table.Column<string>(nullable: true),
                    Facebook = table.Column<string>(nullable: true),
                    Otro = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Celular = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Egresados", x => x.Id);
                });

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
                name: "Maestros",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroEmpleado = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    CorreoAlterno = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maestros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: true),
                    Icono = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    Controller = table.Column<string>(nullable: true),
                    Menu = table.Column<bool>(nullable: false),
                    PermisoIdPadre = table.Column<int>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permisos_Permisos_PermisoIdPadre",
                        column: x => x.PermisoIdPadre,
                        principalTable: "Permisos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnidadesAcademicas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    CampusId = table.Column<int>(nullable: false),
                    Estatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadesAcademicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnidadesAcademicas_Campus_CampusId",
                        column: x => x.CampusId,
                        principalTable: "Campus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosEgresados",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(nullable: false),
                    EgresadoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosEgresados", x => new { x.EgresadoId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_UsuariosEgresados_Egresados_EgresadoId",
                        column: x => x.EgresadoId,
                        principalTable: "Egresados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosEgresados_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosMaestros",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(nullable: false),
                    MaestroId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosMaestros", x => new { x.MaestroId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_UsuariosMaestros_Maestros_MaestroId",
                        column: x => x.MaestroId,
                        principalTable: "Maestros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosMaestros_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosPermisos",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(nullable: false),
                    PermisoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosPermisos", x => new { x.PermisoId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_UsuariosPermisos_Permisos_PermisoId",
                        column: x => x.PermisoId,
                        principalTable: "Permisos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosPermisos_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carreras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    UnidadAcademicaId = table.Column<int>(nullable: false),
                    Estatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carreras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carreras_UnidadesAcademicas_UnidadAcademicaId",
                        column: x => x.UnidadAcademicaId,
                        principalTable: "UnidadesAcademicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alumnos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Matricula = table.Column<int>(nullable: false),
                    Semestre = table.Column<int>(nullable: false),
                    PeriodoIngreso = table.Column<string>(nullable: true),
                    CorreoAlterno = table.Column<string>(nullable: true),
                    Celular = table.Column<string>(nullable: true),
                    FechaNacimiento = table.Column<DateTime>(nullable: false),
                    CampusId = table.Column<int>(nullable: true),
                    UnidadAcademicaId = table.Column<int>(nullable: true),
                    CarreraId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumnos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alumnos_Campus_CampusId",
                        column: x => x.CampusId,
                        principalTable: "Campus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Alumnos_Carreras_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carreras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Alumnos_UnidadesAcademicas_UnidadAcademicaId",
                        column: x => x.UnidadAcademicaId,
                        principalTable: "UnidadesAcademicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Encuestas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosAlumnos",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(nullable: false),
                    AlumnoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosAlumnos", x => new { x.AlumnoId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_UsuariosAlumnos_Alumnos_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "Alumnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosAlumnos_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Eliminado = table.Column<bool>(nullable: false),
                    EncuestaIdPadre = table.Column<int>(nullable: true),
                    EncuestaSeccionIdPadre = table.Column<int>(nullable: true),
                    EncuestaPreguntaIdPadre = table.Column<int>(nullable: true)
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
                        name: "FK_EncuestaPreguntas_EncuestaSecciones_EncuestaSeccionId_EncuestaId",
                        columns: x => new { x.EncuestaSeccionId, x.EncuestaId },
                        principalTable: "EncuestaSecciones",
                        principalColumns: new[] { "Id", "EncuestaId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EncuestaPreguntas_EncuestaPreguntas_EncuestaPreguntaIdPadre_EncuestaIdPadre_EncuestaSeccionIdPadre",
                        columns: x => new { x.EncuestaPreguntaIdPadre, x.EncuestaIdPadre, x.EncuestaSeccionIdPadre },
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
                        name: "FK_EncuestaPreguntaOpciones_EncuestaPreguntas_EncuestaPreguntaId_EncuestaId_EncuestaSeccionId",
                        columns: x => new { x.EncuestaPreguntaId, x.EncuestaId, x.EncuestaSeccionId },
                        principalTable: "EncuestaPreguntas",
                        principalColumns: new[] { "Id", "EncuestaId", "EncuestaSeccionId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EstatusEncuesta",
                columns: new[] { "Id", "Descripcion", "Estatus" },
                values: new object[,]
                {
                    { 1, "Activa", true },
                    { 2, "Inactiva", true },
                    { 3, "Eliminada", true }
                });

            migrationBuilder.InsertData(
                table: "TiposPregunta",
                columns: new[] { "Id", "Descripcion", "Estatus" },
                values: new object[,]
                {
                    { 1, "Abierta", true },
                    { 2, "Multiple", true },
                    { 3, "Unica Opcion", true },
                    { 4, "Condicional", true },
                    { 5, "Matriz", true },
                    { 6, "SubPregunta", true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_CampusId",
                table: "Alumnos",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_CarreraId",
                table: "Alumnos",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_UnidadAcademicaId",
                table: "Alumnos",
                column: "UnidadAcademicaId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Carreras_UnidadAcademicaId",
                table: "Carreras",
                column: "UnidadAcademicaId");

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaPreguntaOpciones_EncuestaPreguntaId_EncuestaId_EncuestaSeccionId",
                table: "EncuestaPreguntaOpciones",
                columns: new[] { "EncuestaPreguntaId", "EncuestaId", "EncuestaSeccionId" });

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaPreguntas_TipoPreguntaId",
                table: "EncuestaPreguntas",
                column: "TipoPreguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaPreguntas_EncuestaSeccionId_EncuestaId",
                table: "EncuestaPreguntas",
                columns: new[] { "EncuestaSeccionId", "EncuestaId" });

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaPreguntas_EncuestaPreguntaIdPadre_EncuestaIdPadre_EncuestaSeccionIdPadre",
                table: "EncuestaPreguntas",
                columns: new[] { "EncuestaPreguntaIdPadre", "EncuestaIdPadre", "EncuestaSeccionIdPadre" });

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

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_PermisoIdPadre",
                table: "Permisos",
                column: "PermisoIdPadre");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadesAcademicas_CampusId",
                table: "UnidadesAcademicas",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosAlumnos_UsuarioId",
                table: "UsuariosAlumnos",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosEgresados_UsuarioId",
                table: "UsuariosEgresados",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosMaestros_UsuarioId",
                table: "UsuariosMaestros",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosPermisos_UsuarioId",
                table: "UsuariosPermisos",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EncuestaPreguntaOpciones");

            migrationBuilder.DropTable(
                name: "UsuariosAlumnos");

            migrationBuilder.DropTable(
                name: "UsuariosEgresados");

            migrationBuilder.DropTable(
                name: "UsuariosMaestros");

            migrationBuilder.DropTable(
                name: "UsuariosPermisos");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "EncuestaPreguntas");

            migrationBuilder.DropTable(
                name: "Alumnos");

            migrationBuilder.DropTable(
                name: "Egresados");

            migrationBuilder.DropTable(
                name: "Maestros");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "TiposPregunta");

            migrationBuilder.DropTable(
                name: "EncuestaSecciones");

            migrationBuilder.DropTable(
                name: "Encuestas");

            migrationBuilder.DropTable(
                name: "Carreras");

            migrationBuilder.DropTable(
                name: "EstatusEncuesta");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "UnidadesAcademicas");

            migrationBuilder.DropTable(
                name: "Campus");
        }
    }
}
