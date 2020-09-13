using EncuestasUABC.Models;
using EncuestasUABC.Models.Catalogos;
using EncuestasUABC.Models.Catalogos.Estatus;
using EncuestasUABC.Models.Catalogos.Tipos;
using EncuestasUABC.Models.Relaciones;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EncuestasUABC.AccesoDatos.Data

{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Campus> Campus { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<Egresado> Egresados { get; set; }
        public DbSet<Encuesta> Encuestas { get; set; }
        public DbSet<EncuestaPregunta> EncuestaPreguntas { get; set; }
        public DbSet<EncuestaPreguntaOpcion> EncuestaPreguntaOpciones { get; set; }
        public DbSet<EncuestaSeccion> EncuestaSecciones { get; set; }
        public DbSet<EstatusEncuesta> EstatusEncuesta { get; set; }
        public DbSet<Maestro> Maestros { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<TipoPregunta> TiposPregunta { get; set; }
        public DbSet<UnidadAcademica> UnidadesAcademicas { get; set; }
        public DbSet<UsuarioAlumno> UsuariosAlumnos { get; set; }
        public DbSet<UsuarioEgresado> UsuariosEgresados { get; set; }
        public DbSet<UsuarioMaestro> UsuariosMaestros { get; set; }
        public DbSet<UsuarioPermiso> UsuariosPermisos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Permiso>().HasData(
            //    new Permiso { Id = 1, Descripcion = "Encuestas", Icono = "fa fa-folder", Menu = true },
            //    new Permiso { Id = 2, Descripcion = "Creadas", Icono = "fa fa-folder", Action = "Creadas", Controller = "Encuestas", Menu = true, PermisoIdPadre = 1 },
            //    new Permiso { Id = 3, Descripcion = "Asignar encuestas", Icono = "fa fa-folder", Action = "Asignar", Controller = "Encuestas", Menu = false, PermisoIdPadre = 2 },
            //    new Permiso { Id = 4, Descripcion = "Pendientes", Icono = "fa fa-folder", Action = "Pendientes", Controller = "Encuestas", Menu = true, PermisoIdPadre = 1 },
            //    new Permiso { Id = 5, Descripcion = "Configuracion", Icono = "fa fa-folder", Menu = true },
            //    new Permiso { Id = 6, Descripcion = "Usuarios", Icono = "fa fa-folder", Action = "Index", Controller = "Usuarios", Menu = true, PermisoIdPadre = 5 },
            //    new Permiso { Id = 7, Descripcion = "Crear usuario", Icono = "fa fa-folder", Action = "Create", Controller = "Usuarios", Menu = true, PermisoIdPadre = 5 }
            //    );

            //modelBuilder.Entity<Campus>().HasData(
            //    new Campus { Id = 1, Nombre = "Mexicali", Estatus = true },
            //    new Campus { Id = 2, Nombre = "Ensenada", Estatus = true },
            //    new Campus { Id = 3, Nombre = "Tijuana", Estatus = true }
            //    );
            //modelBuilder.Entity<UnidadAcademica>().HasData(
            //    new UnidadAcademica { Id = 1, Nombre = "Facultad de ingeniería", CampusId = 1, Estatus = true }
            //    );
            //modelBuilder.Entity<Carrera>().HasData(
            //    new Carrera { Id = 1, Nombre = "Licenciado en sistemas computacionales", UnidadAcademicaId = 1, Estatus = true }
            //    );
            modelBuilder.Entity<EstatusEncuesta>().HasData(
                new EstatusEncuesta { Id = 1, Descripcion = "Activa", Estatus = true },
                new EstatusEncuesta { Id = 2, Descripcion = "Inactiva", Estatus = true },
                new EstatusEncuesta { Id = 3, Descripcion = "Eliminada", Estatus = true }
                );
            modelBuilder.Entity<TipoPregunta>().HasData(
                new TipoPregunta { Id = 1, Descripcion = "Abierta", Estatus = true },
                new TipoPregunta { Id = 2, Descripcion = "Multiple", Estatus = true },
                new TipoPregunta { Id = 3, Descripcion = "Unica Opcion", Estatus = true },
                new TipoPregunta { Id = 4, Descripcion = "Condicional", Estatus = true },
                new TipoPregunta { Id = 5, Descripcion = "Matriz", Estatus = true },
                new TipoPregunta { Id = 6, Descripcion = "SubPregunta", Estatus = true }
                );
            modelBuilder.Entity<UsuarioPermiso>().HasKey(x => new { x.PermisoId, x.UsuarioId });
            modelBuilder.Entity<UsuarioAlumno>().HasKey(x => new { x.AlumnoId, x.UsuarioId });
            modelBuilder.Entity<UsuarioEgresado>().HasKey(x => new { x.EgresadoId, x.UsuarioId });
            modelBuilder.Entity<UsuarioMaestro>().HasKey(x => new { x.MaestroId, x.UsuarioId });
            
            modelBuilder.Entity<EncuestaSeccion>(
                 b =>{
                        b.HasKey(x => new { x.Id, x.EncuestaId });
                        b.Property(e => e.Id).ValueGeneratedOnAdd();
                    }
                 );
            modelBuilder.Entity<EncuestaPregunta>(
                b =>{
                    b.HasKey(x => new { x.Id, x.EncuestaId, x.EncuestaSeccionId });
                    b.Property(e => e.Id).ValueGeneratedOnAdd();
                }
               );
            modelBuilder.Entity<EncuestaPreguntaOpcion>(
               b =>{
                   b.HasKey(x => new { x.Id, x.EncuestaId, x.EncuestaSeccionId, x.EncuestaPreguntaId });
                   b.Property(e => e.Id).ValueGeneratedOnAdd();
               }
              );
        }
    }
}
