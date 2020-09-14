﻿// <auto-generated />
using System;
using EncuestasUABC.AccesoDatos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EncuestasUABC.AccesoDatos.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EncuestasUABC.Models.Alumno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CampusId")
                        .HasColumnType("int");

                    b.Property<int?>("CarreraId")
                        .HasColumnType("int");

                    b.Property<string>("Celular")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CorreoAlterno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<int>("Matricula")
                        .HasColumnType("int");

                    b.Property<string>("PeriodoIngreso")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Semestre")
                        .HasColumnType("int");

                    b.Property<int?>("UnidadAcademicaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CampusId");

                    b.HasIndex("CarreraId");

                    b.HasIndex("UnidadAcademicaId");

                    b.ToTable("Alumnos");
                });

            modelBuilder.Entity("EncuestasUABC.Models.Catalogos.Campus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Estatus")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Campus");
                });

            modelBuilder.Entity("EncuestasUABC.Models.Catalogos.Carrera", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Estatus")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UnidadAcademicaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UnidadAcademicaId");

                    b.ToTable("Carreras");
                });

            modelBuilder.Entity("EncuestasUABC.Models.Catalogos.Estatus.EstatusEncuesta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estatus")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("EstatusEncuesta");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descripcion = "Activa",
                            Estatus = true
                        },
                        new
                        {
                            Id = 2,
                            Descripcion = "Inactiva",
                            Estatus = true
                        },
                        new
                        {
                            Id = 3,
                            Descripcion = "Eliminada",
                            Estatus = true
                        });
                });

            modelBuilder.Entity("EncuestasUABC.Models.Catalogos.Permiso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Action")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Controller")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estatus")
                        .HasColumnType("bit");

                    b.Property<string>("Icono")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Menu")
                        .HasColumnType("bit");

                    b.Property<int?>("PermisoIdPadre")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PermisoIdPadre");

                    b.ToTable("Permisos");
                });

            modelBuilder.Entity("EncuestasUABC.Models.Catalogos.Tipos.TipoPregunta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estatus")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("TiposPregunta");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descripcion = "Abierta",
                            Estatus = true
                        },
                        new
                        {
                            Id = 2,
                            Descripcion = "Multiple",
                            Estatus = true
                        },
                        new
                        {
                            Id = 3,
                            Descripcion = "Unica Opcion",
                            Estatus = true
                        },
                        new
                        {
                            Id = 4,
                            Descripcion = "Condicional",
                            Estatus = true
                        },
                        new
                        {
                            Id = 5,
                            Descripcion = "Matriz",
                            Estatus = true
                        },
                        new
                        {
                            Id = 6,
                            Descripcion = "SubPregunta",
                            Estatus = true
                        });
                });

            modelBuilder.Entity("EncuestasUABC.Models.Catalogos.UnidadAcademica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CampusId")
                        .HasColumnType("int");

                    b.Property<bool>("Estatus")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CampusId");

                    b.ToTable("UnidadesAcademicas");
                });

            modelBuilder.Entity("EncuestasUABC.Models.Egresado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Celular")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CorreoAlterno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Facebook")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Otro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PeriodoEgreso")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PeriodoIngreso")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Egresados");
                });

            modelBuilder.Entity("EncuestasUABC.Models.Encuesta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CarreraId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EstatusEncuestaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CarreraId");

                    b.HasIndex("EstatusEncuestaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Encuestas");
                });

            modelBuilder.Entity("EncuestasUABC.Models.EncuestaPregunta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EncuestaId")
                        .HasColumnType("int");

                    b.Property<int>("EncuestaSeccionId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("bit");

                    b.Property<int?>("EncuestaIdPadre")
                        .HasColumnType("int");

                    b.Property<int?>("EncuestaPreguntaIdPadre")
                        .HasColumnType("int");

                    b.Property<int?>("EncuestaSeccionIdPadre")
                        .HasColumnType("int");

                    b.Property<bool>("Obligatoria")
                        .HasColumnType("bit");

                    b.Property<int>("Orden")
                        .HasColumnType("int");

                    b.Property<int>("TipoPreguntaId")
                        .HasColumnType("int");

                    b.HasKey("Id", "EncuestaId", "EncuestaSeccionId");

                    b.HasIndex("TipoPreguntaId");

                    b.HasIndex("EncuestaSeccionId", "EncuestaId");

                    b.HasIndex("EncuestaPreguntaIdPadre", "EncuestaIdPadre", "EncuestaSeccionIdPadre");

                    b.ToTable("EncuestaPreguntas");
                });

            modelBuilder.Entity("EncuestasUABC.Models.EncuestaPreguntaOpcion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EncuestaId")
                        .HasColumnType("int");

                    b.Property<int>("EncuestaSeccionId")
                        .HasColumnType("int");

                    b.Property<int>("EncuestaPreguntaId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("bit");

                    b.Property<int>("Orden")
                        .HasColumnType("int");

                    b.HasKey("Id", "EncuestaId", "EncuestaSeccionId", "EncuestaPreguntaId");

                    b.HasIndex("EncuestaPreguntaId", "EncuestaId", "EncuestaSeccionId");

                    b.ToTable("EncuestaPreguntaOpciones");
                });

            modelBuilder.Entity("EncuestasUABC.Models.EncuestaSeccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EncuestaId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Orden")
                        .HasColumnType("int");

                    b.HasKey("Id", "EncuestaId");

                    b.HasIndex("EncuestaId");

                    b.ToTable("EncuestaSecciones");
                });

            modelBuilder.Entity("EncuestasUABC.Models.Maestro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CorreoAlterno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroEmpleado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Maestros");
                });

            modelBuilder.Entity("EncuestasUABC.Models.Relaciones.UsuarioAlumno", b =>
                {
                    b.Property<int>("AlumnoId")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AlumnoId", "UsuarioId");

                    b.HasIndex("UsuarioId")
                        .IsUnique();

                    b.ToTable("UsuariosAlumnos");
                });

            modelBuilder.Entity("EncuestasUABC.Models.Relaciones.UsuarioEgresado", b =>
                {
                    b.Property<int>("EgresadoId")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("EgresadoId", "UsuarioId");

                    b.HasIndex("UsuarioId")
                        .IsUnique();

                    b.ToTable("UsuariosEgresados");
                });

            modelBuilder.Entity("EncuestasUABC.Models.Relaciones.UsuarioMaestro", b =>
                {
                    b.Property<int>("MaestroId")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MaestroId", "UsuarioId");

                    b.HasIndex("UsuarioId")
                        .IsUnique();

                    b.ToTable("UsuariosMaestros");
                });

            modelBuilder.Entity("EncuestasUABC.Models.Relaciones.UsuarioPermiso", b =>
                {
                    b.Property<int>("PermisoId")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PermisoId", "UsuarioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("UsuariosPermisos");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("EncuestasUABC.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("ApellidoMaterno")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<string>("ApellidoPaterno")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("EncuestasUABC.Models.Alumno", b =>
                {
                    b.HasOne("EncuestasUABC.Models.Catalogos.Campus", "Campus")
                        .WithMany()
                        .HasForeignKey("CampusId");

                    b.HasOne("EncuestasUABC.Models.Catalogos.Carrera", "Carrera")
                        .WithMany()
                        .HasForeignKey("CarreraId");

                    b.HasOne("EncuestasUABC.Models.Catalogos.UnidadAcademica", "UnidadAcademica")
                        .WithMany()
                        .HasForeignKey("UnidadAcademicaId");
                });

            modelBuilder.Entity("EncuestasUABC.Models.Catalogos.Carrera", b =>
                {
                    b.HasOne("EncuestasUABC.Models.Catalogos.UnidadAcademica", "UnidadAcademica")
                        .WithMany()
                        .HasForeignKey("UnidadAcademicaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EncuestasUABC.Models.Catalogos.Permiso", b =>
                {
                    b.HasOne("EncuestasUABC.Models.Catalogos.Permiso", "PermisoPadre")
                        .WithMany("PermisosHijos")
                        .HasForeignKey("PermisoIdPadre");
                });

            modelBuilder.Entity("EncuestasUABC.Models.Catalogos.UnidadAcademica", b =>
                {
                    b.HasOne("EncuestasUABC.Models.Catalogos.Campus", "Campus")
                        .WithMany()
                        .HasForeignKey("CampusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EncuestasUABC.Models.Encuesta", b =>
                {
                    b.HasOne("EncuestasUABC.Models.Catalogos.Carrera", "Carrera")
                        .WithMany()
                        .HasForeignKey("CarreraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EncuestasUABC.Models.Catalogos.Estatus.EstatusEncuesta", "EstatusEncuesta")
                        .WithMany()
                        .HasForeignKey("EstatusEncuestaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EncuestasUABC.Models.ApplicationUser", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EncuestasUABC.Models.EncuestaPregunta", b =>
                {
                    b.HasOne("EncuestasUABC.Models.Catalogos.Tipos.TipoPregunta", "TipoPregunta")
                        .WithMany()
                        .HasForeignKey("TipoPreguntaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EncuestasUABC.Models.EncuestaSeccion", "EncuestaSeccion")
                        .WithMany("EncuestaPreguntas")
                        .HasForeignKey("EncuestaSeccionId", "EncuestaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EncuestasUABC.Models.EncuestaPregunta", "EncuestaPreguntaPadre")
                        .WithMany("SubPreguntas")
                        .HasForeignKey("EncuestaPreguntaIdPadre", "EncuestaIdPadre", "EncuestaSeccionIdPadre");
                });

            modelBuilder.Entity("EncuestasUABC.Models.EncuestaPreguntaOpcion", b =>
                {
                    b.HasOne("EncuestasUABC.Models.EncuestaPregunta", "EncuestaPregunta")
                        .WithMany("Opciones")
                        .HasForeignKey("EncuestaPreguntaId", "EncuestaId", "EncuestaSeccionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EncuestasUABC.Models.EncuestaSeccion", b =>
                {
                    b.HasOne("EncuestasUABC.Models.Encuesta", "Encuesta")
                        .WithMany("EncuestaSecciones")
                        .HasForeignKey("EncuestaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EncuestasUABC.Models.Relaciones.UsuarioAlumno", b =>
                {
                    b.HasOne("EncuestasUABC.Models.Alumno", "Alumno")
                        .WithMany()
                        .HasForeignKey("AlumnoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EncuestasUABC.Models.ApplicationUser", "Usuario")
                        .WithOne("UsuarioAlumno")
                        .HasForeignKey("EncuestasUABC.Models.Relaciones.UsuarioAlumno", "UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EncuestasUABC.Models.Relaciones.UsuarioEgresado", b =>
                {
                    b.HasOne("EncuestasUABC.Models.Egresado", "Egresado")
                        .WithMany()
                        .HasForeignKey("EgresadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EncuestasUABC.Models.ApplicationUser", "Usuario")
                        .WithOne("UsuarioEgresado")
                        .HasForeignKey("EncuestasUABC.Models.Relaciones.UsuarioEgresado", "UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EncuestasUABC.Models.Relaciones.UsuarioMaestro", b =>
                {
                    b.HasOne("EncuestasUABC.Models.Maestro", "Maestro")
                        .WithMany()
                        .HasForeignKey("MaestroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EncuestasUABC.Models.ApplicationUser", "Usuario")
                        .WithOne("UsuarioMaestro")
                        .HasForeignKey("EncuestasUABC.Models.Relaciones.UsuarioMaestro", "UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EncuestasUABC.Models.Relaciones.UsuarioPermiso", b =>
                {
                    b.HasOne("EncuestasUABC.Models.Catalogos.Permiso", "Permiso")
                        .WithMany()
                        .HasForeignKey("PermisoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EncuestasUABC.Models.ApplicationUser", "Usuario")
                        .WithMany("Permisos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
