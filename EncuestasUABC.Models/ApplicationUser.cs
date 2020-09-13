using EncuestasUABC.Models.Relaciones;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EncuestasUABC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Permisos = new HashSet<UsuarioPermiso>();
        }
        [MaxLength(80)]
        public string Nombre { get; set; }
        [MaxLength(80)]
        public string ApellidoPaterno { get; set; }
        [MaxLength(80)]
        public string ApellidoMaterno { get; set; }
        public bool Activo { get; set; } = true;

        public virtual ICollection<UsuarioPermiso> Permisos { get; set; }
        public virtual UsuarioMaestro UsuarioMaestro { get; set; }
        public virtual UsuarioAlumno UsuarioAlumno { get; set; }
        public virtual UsuarioEgresado UsuarioEgresado { get; set; }

        #region NotMapped
        [NotMapped]
        public string Rol { get; set; }

        #endregion
    }
}
