using System;
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
                name: "Campus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    Estatus = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstatusEncuesta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: true),
                    Estatus = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstatusEncuesta", x => x.Id);
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
                    IdPermisoPadre = table.Column<int>(nullable: true),
                    Estatus = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permisos_Permisos_IdPermisoPadre",
                        column: x => x.IdPermisoPadre,
                        principalTable: "Permisos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TiposAcademico",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: true),
                    Estatus = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposAcademico", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposAdministrativo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<int>(nullable: false),
                    Estatus = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposAdministrativo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposPregunta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: true),
                    Estatus = table.Column<bool>(nullable: true)
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
                name: "UnidadesAcademicas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    IdCampus = table.Column<int>(nullable: false),
                    Estatus = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadesAcademicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnidadesAcademicas_Campus_IdCampus",
                        column: x => x.IdCampus,
                        principalTable: "Campus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Academicos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaRegistro = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 80, nullable: true),
                    ApellidoPaterno = table.Column<string>(maxLength: 80, nullable: true),
                    ApellidoMaterno = table.Column<string>(maxLength: 80, nullable: true),
                    FechaNacimiento = table.Column<DateTime>(nullable: false),
                    Correo = table.Column<string>(nullable: true),
                    CorreoAlterno = table.Column<string>(nullable: true),
                    Celular = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Facebook = table.Column<string>(nullable: true),
                    IdTipoAcademico = table.Column<int>(nullable: false),
                    Otro = table.Column<string>(nullable: true),
                    IdAlumno = table.Column<int>(nullable: true),
                    IdAdministrativo = table.Column<int>(nullable: true),
                    Estatus = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Academicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Academicos_TiposAcademico_IdTipoAcademico",
                        column: x => x.IdTipoAcademico,
                        principalTable: "TiposAcademico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Carreras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    IdUnidadAcademica = table.Column<int>(nullable: false),
                    Estatus = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carreras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carreras_UnidadesAcademicas_IdUnidadAcademica",
                        column: x => x.IdUnidadAcademica,
                        principalTable: "UnidadesAcademicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Administrativos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    FechaRegistro = table.Column<DateTime>(nullable: false),
                    NumeroEmpleado = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    IdTipoAdministrativo = table.Column<int>(nullable: false),
                    Estatus = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrativos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Administrativos_Academicos_Id",
                        column: x => x.Id,
                        principalTable: "Academicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Administrativos_TiposAdministrativo_IdTipoAdministrativo",
                        column: x => x.IdTipoAdministrativo,
                        principalTable: "TiposAdministrativo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Alumnos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    FechaRegistro = table.Column<DateTime>(nullable: false),
                    Matricula = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    Semestre = table.Column<int>(nullable: true),
                    PeriodoIngreso = table.Column<string>(nullable: true),
                    PeriodoEgreso = table.Column<string>(nullable: true),
                    IdCarrera = table.Column<int>(nullable: true),
                    Egresado = table.Column<bool>(nullable: true),
                    Estatus = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumnos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alumnos_Academicos_Id",
                        column: x => x.Id,
                        principalTable: "Academicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Alumnos_Carreras_IdCarrera",
                        column: x => x.IdCarrera,
                        principalTable: "Carreras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    FechaRegistro = table.Column<DateTime>(nullable: true),
                    Estatus = table.Column<bool>(nullable: true),
                    IdAdministrativo = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Administrativos_IdAdministrativo",
                        column: x => x.IdAdministrativo,
                        principalTable: "Administrativos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "Encuestas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(nullable: false),
                    IdUsuarioRegistro = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    IdEstatusEncuesta = table.Column<int>(nullable: false),
                    IdCarrera = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encuestas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Encuestas_Carreras_IdCarrera",
                        column: x => x.IdCarrera,
                        principalTable: "Carreras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Encuestas_EstatusEncuesta_IdEstatusEncuesta",
                        column: x => x.IdEstatusEncuesta,
                        principalTable: "EstatusEncuesta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Encuestas_AspNetUsers_IdUsuarioRegistro",
                        column: x => x.IdUsuarioRegistro,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosPermisos",
                columns: table => new
                {
                    IdUsuario = table.Column<string>(nullable: false),
                    IdPermiso = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosPermisos", x => new { x.IdPermiso, x.IdUsuario });
                    table.ForeignKey(
                        name: "FK_UsuariosPermisos_Permisos_IdPermiso",
                        column: x => x.IdPermiso,
                        principalTable: "Permisos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuariosPermisos_AspNetUsers_IdUsuario",
                        column: x => x.IdUsuario,
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
                    IdEncuesta = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Orden = table.Column<int>(nullable: false),
                    Eliminado = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncuestaSecciones", x => new { x.Id, x.IdEncuesta });
                    table.ForeignKey(
                        name: "FK_EncuestaSecciones_Encuestas_IdEncuesta",
                        column: x => x.IdEncuesta,
                        principalTable: "Encuestas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EncuestaPreguntas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEncuesta = table.Column<int>(nullable: false),
                    IdEncuestaSeccion = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    IdTipoPregunta = table.Column<int>(nullable: false),
                    Obligatoria = table.Column<bool>(nullable: false),
                    Orden = table.Column<int>(nullable: false),
                    IdEncuestaPadre = table.Column<int>(nullable: true),
                    IdEncuestaSeccionPadre = table.Column<int>(nullable: true),
                    IdEncuestaPreguntaPadre = table.Column<int>(nullable: true),
                    Eliminado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncuestaPreguntas", x => new { x.Id, x.IdEncuesta, x.IdEncuestaSeccion });
                    table.ForeignKey(
                        name: "FK_EncuestaPreguntas_TiposPregunta_IdTipoPregunta",
                        column: x => x.IdTipoPregunta,
                        principalTable: "TiposPregunta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EncuestaPreguntas_EncuestaSecciones_IdEncuestaSeccion_IdEncuesta",
                        columns: x => new { x.IdEncuestaSeccion, x.IdEncuesta },
                        principalTable: "EncuestaSecciones",
                        principalColumns: new[] { "Id", "IdEncuesta" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EncuestaPreguntas_EncuestaPreguntas_IdEncuestaSeccionPadre_IdEncuestaPadre_IdEncuestaPreguntaPadre",
                        columns: x => new { x.IdEncuestaSeccionPadre, x.IdEncuestaPadre, x.IdEncuestaPreguntaPadre },
                        principalTable: "EncuestaPreguntas",
                        principalColumns: new[] { "Id", "IdEncuesta", "IdEncuestaSeccion" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EncuestaPreguntaOpciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEncuesta = table.Column<int>(nullable: false),
                    IdEncuestaSeccion = table.Column<int>(nullable: false),
                    IdEncuestaPregunta = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    Orden = table.Column<int>(nullable: false),
                    Eliminado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncuestaPreguntaOpciones", x => new { x.Id, x.IdEncuesta, x.IdEncuestaSeccion, x.IdEncuestaPregunta });
                    table.ForeignKey(
                        name: "FK_EncuestaPreguntaOpciones_EncuestaPreguntas_IdEncuestaPregunta_IdEncuesta_IdEncuestaSeccion",
                        columns: x => new { x.IdEncuestaPregunta, x.IdEncuesta, x.IdEncuestaSeccion },
                        principalTable: "EncuestaPreguntas",
                        principalColumns: new[] { "Id", "IdEncuesta", "IdEncuestaSeccion" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Academicos_IdTipoAcademico",
                table: "Academicos",
                column: "IdTipoAcademico");

            migrationBuilder.CreateIndex(
                name: "IX_Administrativos_IdTipoAdministrativo",
                table: "Administrativos",
                column: "IdTipoAdministrativo");

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_IdCarrera",
                table: "Alumnos",
                column: "IdCarrera");

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
                name: "IX_AspNetUsers_IdAdministrativo",
                table: "AspNetUsers",
                column: "IdAdministrativo",
                unique: true,
                filter: "[IdAdministrativo] IS NOT NULL");

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
                name: "IX_Carreras_IdUnidadAcademica",
                table: "Carreras",
                column: "IdUnidadAcademica");

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaPreguntaOpciones_IdEncuestaPregunta_IdEncuesta_IdEncuestaSeccion",
                table: "EncuestaPreguntaOpciones",
                columns: new[] { "IdEncuestaPregunta", "IdEncuesta", "IdEncuestaSeccion" });

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaPreguntas_IdTipoPregunta",
                table: "EncuestaPreguntas",
                column: "IdTipoPregunta");

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaPreguntas_IdEncuestaSeccion_IdEncuesta",
                table: "EncuestaPreguntas",
                columns: new[] { "IdEncuestaSeccion", "IdEncuesta" });

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaPreguntas_IdEncuestaSeccionPadre_IdEncuestaPadre_IdEncuestaPreguntaPadre",
                table: "EncuestaPreguntas",
                columns: new[] { "IdEncuestaSeccionPadre", "IdEncuestaPadre", "IdEncuestaPreguntaPadre" });

            migrationBuilder.CreateIndex(
                name: "IX_Encuestas_IdCarrera",
                table: "Encuestas",
                column: "IdCarrera");

            migrationBuilder.CreateIndex(
                name: "IX_Encuestas_IdEstatusEncuesta",
                table: "Encuestas",
                column: "IdEstatusEncuesta");

            migrationBuilder.CreateIndex(
                name: "IX_Encuestas_IdUsuarioRegistro",
                table: "Encuestas",
                column: "IdUsuarioRegistro");

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaSecciones_IdEncuesta",
                table: "EncuestaSecciones",
                column: "IdEncuesta");

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_IdPermisoPadre",
                table: "Permisos",
                column: "IdPermisoPadre");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadesAcademicas_IdCampus",
                table: "UnidadesAcademicas",
                column: "IdCampus");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosPermisos_IdUsuario",
                table: "UsuariosPermisos",
                column: "IdUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alumnos");

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
                name: "UsuariosPermisos");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "EncuestaPreguntas");

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
                name: "Administrativos");

            migrationBuilder.DropTable(
                name: "Campus");

            migrationBuilder.DropTable(
                name: "Academicos");

            migrationBuilder.DropTable(
                name: "TiposAdministrativo");

            migrationBuilder.DropTable(
                name: "TiposAcademico");
        }
    }
}
