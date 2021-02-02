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

            modelBuilder.Entity("EncuestasUABC.Models.Academico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApellidoMaterno")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<string>("ApellidoPaterno")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<string>("Celular")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CorreoAlterno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Estatus")
                        .HasColumnType("bit");

                    b.Property<string>("Facebook")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IdAdministrativo")
                        .HasColumnType("int");

                    b.Property<int?>("IdAlumno")
                        .HasColumnType("int");

                    b.Property<int>("IdTipoAcademico")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<string>("Otro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdTipoAcademico");

                    b.ToTable("Academicos");
                });

            modelBuilder.Entity("EncuestasUABC.Models.Administrativo", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Estatus")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdTipoAdministrativo")
                        .HasColumnType("int");

                    b.Property<string>("NumeroEmpleado")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdTipoAdministrativo");

                    b.ToTable("Administrativos");
                });

            modelBuilder.Entity("EncuestasUABC.Models.Alumno", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Egresado")
                        .HasColumnType("bit");

                    b.Property<bool?>("Estatus")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IdCarrera")
                        .HasColumnType("int");

                    b.Property<string>("Matricula")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PeriodoEgreso")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PeriodoIngreso")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Semestre")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdCarrera");

                    b.ToTable("Alumnos");
                });

            modelBuilder.Entity("EncuestasUABC.Models.Campus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Estatus")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Campus");
                });

            modelBuilder.Entity("EncuestasUABC.Models.Carrera", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Estatus")
                        .HasColumnType("bit");

                    b.Property<int>("IdUnidadAcademica")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdUnidadAcademica");

                    b.ToTable("Carreras");
                });

            modelBuilder.Entity("EncuestasUABC.Models.Encuesta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCarrera")
                        .HasColumnType("int");

                    b.Property<int>("IdEstatusEncuesta")
                        .HasColumnType("int");

                    b.Property<string>("IdUsuarioRegistro")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdCarrera");

                    b.HasIndex("IdEstatusEncuesta");

                    b.HasIndex("IdUsuarioRegistro");

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

                    b.HasIndex("EncuestaSeccionIdPadre", "EncuestaIdPadre", "EncuestaPreguntaIdPadre");

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

                    b.Property<bool?>("Eliminado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Orden")
                        .HasColumnType("int");

                    b.HasKey("Id", "EncuestaId");

                    b.HasIndex("EncuestaId");

                    b.ToTable("EncuestaSecciones");
                });

            modelBuilder.Entity("EncuestasUABC.Models.EstatusEncuesta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Estatus")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("EstatusEncuesta");
                });

            modelBuilder.Entity("EncuestasUABC.Models.Permiso", b =>
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

                    b.Property<bool?>("Estatus")
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

            modelBuilder.Entity("EncuestasUABC.Models.TipoAcademico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Estatus")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("TiposAcademico");
                });

            modelBuilder.Entity("EncuestasUABC.Models.TipoAdministrativo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Descripcion")
                        .HasColumnType("int");

                    b.Property<bool?>("Estatus")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("TiposAdministrativo");
                });

            modelBuilder.Entity("EncuestasUABC.Models.TipoPregunta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Estatus")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("TiposPregunta");
                });

            modelBuilder.Entity("EncuestasUABC.Models.UnidadAcademica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CampusId")
                        .HasColumnType("int");

                    b.Property<bool?>("Estatus")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CampusId");

                    b.ToTable("UnidadesAcademicas");
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

                    b.Property<string>("ApellidoMaterno")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<string>("ApellidoPaterno")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<bool?>("Estatus")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IdAdministrativo")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.HasIndex("IdAdministrativo")
                        .IsUnique()
                        .HasFilter("[IdAdministrativo] IS NOT NULL");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("EncuestasUABC.Models.Academico", b =>
                {
                    b.HasOne("EncuestasUABC.Models.TipoAcademico", "IdTipoAcademicoNavigation")
                        .WithMany("Academicos")
                        .HasForeignKey("IdTipoAcademico")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("EncuestasUABC.Models.Administrativo", b =>
                {
                    b.HasOne("EncuestasUABC.Models.Academico", "AcademicoNavigation")
                        .WithOne("IdAdministrativoNavigation")
                        .HasForeignKey("EncuestasUABC.Models.Administrativo", "Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EncuestasUABC.Models.TipoAdministrativo", "IdTipoAdministrativoNavigation")
                        .WithMany("Adminstrativos")
                        .HasForeignKey("IdTipoAdministrativo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("EncuestasUABC.Models.Alumno", b =>
                {
                    b.HasOne("EncuestasUABC.Models.Academico", "AcademicoNavigation")
                        .WithOne("IdAlumnoNavigation")
                        .HasForeignKey("EncuestasUABC.Models.Alumno", "Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EncuestasUABC.Models.Carrera", "IdCarreraNavigation")
                        .WithMany("Alumnos")
                        .HasForeignKey("IdCarrera")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EncuestasUABC.Models.Carrera", b =>
                {
                    b.HasOne("EncuestasUABC.Models.UnidadAcademica", "IdUnidadAcademicaNavigation")
                        .WithMany("Carreras")
                        .HasForeignKey("IdUnidadAcademica")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("EncuestasUABC.Models.Encuesta", b =>
                {
                    b.HasOne("EncuestasUABC.Models.Carrera", "IdCarreraNavigation")
                        .WithMany("Encuestas")
                        .HasForeignKey("IdCarrera")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EncuestasUABC.Models.EstatusEncuesta", "IdEstatusEncuestaNavigation")
                        .WithMany("Encuestas")
                        .HasForeignKey("IdEstatusEncuesta")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EncuestasUABC.Models.ApplicationUser", "IdUsuarioRegistroNavigation")
                        .WithMany("EncuestasCreadas")
                        .HasForeignKey("IdUsuarioRegistro")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EncuestasUABC.Models.EncuestaPregunta", b =>
                {
                    b.HasOne("EncuestasUABC.Models.TipoPregunta", "TipoPreguntaIdNavigation")
                        .WithMany("EncuestaPreguntas")
                        .HasForeignKey("TipoPreguntaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EncuestasUABC.Models.EncuestaSeccion", "EncuestaSeccionIdNavigation")
                        .WithMany("EncuestaPreguntas")
                        .HasForeignKey("EncuestaSeccionId", "EncuestaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EncuestasUABC.Models.EncuestaPregunta", "EncuestaPreguntaIdPadreNavigation")
                        .WithMany("SubPreguntas")
                        .HasForeignKey("EncuestaSeccionIdPadre", "EncuestaIdPadre", "EncuestaPreguntaIdPadre")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EncuestasUABC.Models.EncuestaPreguntaOpcion", b =>
                {
                    b.HasOne("EncuestasUABC.Models.EncuestaPregunta", "EncuestaPreguntaIdNavigation")
                        .WithMany("Opciones")
                        .HasForeignKey("EncuestaPreguntaId", "EncuestaId", "EncuestaSeccionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("EncuestasUABC.Models.EncuestaSeccion", b =>
                {
                    b.HasOne("EncuestasUABC.Models.Encuesta", "EncuestaIdNavigation")
                        .WithMany("EncuestaSecciones")
                        .HasForeignKey("EncuestaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("EncuestasUABC.Models.Permiso", b =>
                {
                    b.HasOne("EncuestasUABC.Models.Permiso", "PermisoIdPadreNavigation")
                        .WithMany("PermisosHijos")
                        .HasForeignKey("PermisoIdPadre")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EncuestasUABC.Models.Relaciones.UsuarioPermiso", b =>
                {
                    b.HasOne("EncuestasUABC.Models.Permiso", "PermisoIdNavigation")
                        .WithMany("Usuarios")
                        .HasForeignKey("PermisoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EncuestasUABC.Models.ApplicationUser", "UsuarioIdNavigation")
                        .WithMany("Permisos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("EncuestasUABC.Models.UnidadAcademica", b =>
                {
                    b.HasOne("EncuestasUABC.Models.Campus", "CampusIdNavigation")
                        .WithMany("UnidadesAcademicas")
                        .HasForeignKey("CampusId")
                        .OnDelete(DeleteBehavior.Restrict)
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

            modelBuilder.Entity("EncuestasUABC.Models.ApplicationUser", b =>
                {
                    b.HasOne("EncuestasUABC.Models.Administrativo", "IdAdministrativoNavigation")
                        .WithOne("ApplicationUser")
                        .HasForeignKey("EncuestasUABC.Models.ApplicationUser", "IdAdministrativo")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
