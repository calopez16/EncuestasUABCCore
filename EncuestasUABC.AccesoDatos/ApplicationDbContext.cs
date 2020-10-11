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
        public DbSet<Administrativo> Administrativos { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Rol> RolesSistema { get; set; }
        public DbSet<TipoPregunta> TiposPregunta { get; set; }
        public DbSet<UnidadAcademica> UnidadesAcademicas { get; set; }
        public DbSet<UsuarioPermiso> UsuariosPermisos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
          
            modelBuilder.Entity<UsuarioPermiso>().HasKey(x => new { x.PermisoId, x.UsuarioId });

            #region ApplicationUser
            modelBuilder.Entity<ApplicationUser>().HasOne(x => x.Alumno).WithOne(x => x.UsuarioIdNavigation).HasForeignKey<Alumno>(e => e.UsuarioId);
            modelBuilder.Entity<ApplicationUser>().HasOne(x => x.Egresado).WithOne(x => x.UsuarioIdNavigation).HasForeignKey<Egresado>(e => e.UsuarioId);
            modelBuilder.Entity<ApplicationUser>().HasOne(x => x.Administrativo).WithOne(x => x.UsuarioIdNavigation).HasForeignKey<Administrativo>(e => e.UsuarioId);
            modelBuilder.Entity<ApplicationUser>().HasOne(x => x.RolIdNavigation).WithMany(x => x.Usuarios).HasForeignKey(x => x.RolId);
            #endregion

            modelBuilder.Entity<Carrera>().HasMany(x => x.Alumnos).WithOne(x => x.CarreraIdNavigation).HasForeignKey(x => x.CarreraId);
            modelBuilder.Entity<UnidadAcademica>().HasMany(x => x.Carreras).WithOne(x => x.UnidadAcademicaIdNavigation).HasForeignKey(x => x.UnidadAcademicaId);
            modelBuilder.Entity<Campus>().HasMany(x => x.UnidadesAcademicas).WithOne(x => x.CampusIdNavigation).HasForeignKey(x => x.CampusId);

            modelBuilder.Entity<Encuesta>().HasOne(x => x.EstatusEncuestaIdNavigation).WithMany(x => x.Encuestas).HasForeignKey(x => x.EstatusEncuestaId);
            modelBuilder.Entity<Encuesta>().HasOne(x => x.CarreraIdNavigation).WithMany(x => x.Encuestas).HasForeignKey(x => x.CarreraId);
            modelBuilder.Entity<Encuesta>().HasOne(x => x.UsuarioIdNavigation).WithMany(x => x.EncuestasCreadas).HasForeignKey(x => x.UsuarioId);

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
                b.HasOne(x => x.EncuestaSeccionIdNavigation).WithMany(x => x.EncuestaPreguntas).HasForeignKey(x => new {x.EncuestaSeccionId, x.EncuestaId });
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
