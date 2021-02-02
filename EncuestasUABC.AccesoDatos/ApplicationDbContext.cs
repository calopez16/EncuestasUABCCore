using EncuestasUABC.Models;
using EncuestasUABC.Models.Relaciones;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EncuestasUABC.AccesoDatos.Data

{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Academico> Academicos { get; set; }
        public DbSet<Administrativo> Administrativos { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Campus> Campus { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<Encuesta> Encuestas { get; set; }
        public DbSet<EncuestaPregunta> EncuestaPreguntas { get; set; }
        public DbSet<EncuestaPreguntaOpcion> EncuestaPreguntaOpciones { get; set; }
        public DbSet<EncuestaSeccion> EncuestaSecciones { get; set; }
        public DbSet<EstatusEncuesta> EstatusEncuesta { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<TipoAcademico> TiposAcademico { get; set; }
        public DbSet<TipoAdministrativo> TiposAdministrativo { get; set; }
        public DbSet<TipoPregunta> TiposPregunta { get; set; }
        public DbSet<UnidadAcademica> UnidadesAcademicas { get; set; }
        public DbSet<UsuarioPermiso> UsuariosPermisos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);

            #region ApplicationUser
            modelBuilder.Entity<ApplicationUser>().HasOne(x => x.IdAdministrativoNavigation).WithOne(x => x.ApplicationUser).HasForeignKey<ApplicationUser>(e => e.IdAdministrativo);

            modelBuilder.Entity<ApplicationUser>(p =>
            {
                p.Property(x => x.Nombre).HasMaxLength(80);
                p.Property(x => x.ApellidoPaterno).HasMaxLength(80);
                p.Property(x => x.ApellidoMaterno).HasMaxLength(80);
            });

            modelBuilder.Entity<Permiso>(b =>
            {
                b.HasKey(x => new { x.Id });
                b.HasOne(x => x.PermisoIdPadreNavigation).WithMany(x => x.PermisosHijos).HasForeignKey(x => x.PermisoIdPadre);
            });
            modelBuilder.Entity<UsuarioPermiso>(b =>
            {
                b.HasKey(x => new { x.PermisoId, x.UsuarioId });
                b.HasOne(x => x.UsuarioIdNavigation).WithMany(x => x.Permisos).HasForeignKey(x => x.UsuarioId);
                b.HasOne(x => x.PermisoIdNavigation).WithMany(x => x.Usuarios).HasForeignKey(x => x.PermisoId);
            });
            #endregion

            modelBuilder.Entity<Academico>().HasOne(x => x.IdAlumnoNavigation).WithOne(x => x.AcademicoNavigation).HasForeignKey<Alumno>(e => e.Id);
            modelBuilder.Entity<Academico>().HasOne(x => x.IdAdministrativoNavigation).WithOne(x => x.AcademicoNavigation).HasForeignKey<Administrativo>(e => e.Id);
            
            modelBuilder.Entity<Carrera>().HasMany(x => x.Alumnos).WithOne(x => x.IdCarreraNavigation).HasForeignKey(x => x.IdCarrera);
            modelBuilder.Entity<UnidadAcademica>().HasMany(x => x.Carreras).WithOne(x => x.IdUnidadAcademicaNavigation).HasForeignKey(x => x.IdUnidadAcademica);
            modelBuilder.Entity<Campus>().HasMany(x => x.UnidadesAcademicas).WithOne(x => x.CampusIdNavigation).HasForeignKey(x => x.CampusId);
            modelBuilder.Entity<TipoAcademico>().HasMany(x => x.Academicos).WithOne(x => x.IdTipoAcademicoNavigation).HasForeignKey(x => x.IdTipoAcademico);
            modelBuilder.Entity<TipoAdministrativo>().HasMany(x => x.Adminstrativos).WithOne(x => x.IdTipoAdministrativoNavigation).HasForeignKey(x => x.IdTipoAdministrativo);

            modelBuilder.Entity<Encuesta>().HasOne(x => x.IdEstatusEncuestaNavigation).WithMany(x => x.Encuestas).HasForeignKey(x => x.IdEstatusEncuesta);
            modelBuilder.Entity<Encuesta>().HasOne(x => x.IdCarreraNavigation).WithMany(x => x.Encuestas).HasForeignKey(x => x.IdCarrera);
            modelBuilder.Entity<Encuesta>().HasOne(x => x.IdUsuarioRegistroNavigation).WithMany(x => x.EncuestasCreadas).HasForeignKey(x => x.IdUsuarioRegistro);

            modelBuilder.Entity<EncuestaSeccion>(b =>
            {
                b.HasKey(x => new { x.Id, x.EncuestaId });
                b.Property(e => e.Id).ValueGeneratedOnAdd();
                b.HasOne(x => x.EncuestaIdNavigation).WithMany(x => x.EncuestaSecciones).HasForeignKey(x => x.EncuestaId);
            });
            modelBuilder.Entity<EncuestaPregunta>(b =>
            {
                b.HasKey(x => new { x.Id, x.EncuestaId, x.EncuestaSeccionId });
                b.Property(e => e.Id).ValueGeneratedOnAdd();
                b.HasOne(x => x.EncuestaSeccionIdNavigation).WithMany(x => x.EncuestaPreguntas).HasForeignKey(x => new { x.EncuestaSeccionId, x.EncuestaId });
                b.HasOne(x => x.TipoPreguntaIdNavigation).WithMany(x => x.EncuestaPreguntas).HasForeignKey(x => x.TipoPreguntaId);
                b.HasMany(x => x.SubPreguntas).WithOne(x => x.EncuestaPreguntaIdPadreNavigation).HasForeignKey(x => new { x.EncuestaSeccionIdPadre, x.EncuestaIdPadre, x.EncuestaPreguntaIdPadre });
            });
            modelBuilder.Entity<EncuestaPreguntaOpcion>(b =>
            {
                b.HasKey(x => new { x.Id, x.EncuestaId, x.EncuestaSeccionId, x.EncuestaPreguntaId });
                b.Property(e => e.Id).ValueGeneratedOnAdd();
                b.HasOne(x => x.EncuestaPreguntaIdNavigation).WithMany(x => x.Opciones).HasForeignKey(x => new { x.EncuestaPreguntaId, x.EncuestaId, x.EncuestaSeccionId });
            });
        }
    }
}
