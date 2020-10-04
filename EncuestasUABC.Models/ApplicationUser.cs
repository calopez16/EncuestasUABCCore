using EncuestasUABC.Models.Catalogos;
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
        public int RolId { get; set; }
        public bool Activo { get; set; } = true;

        public Rol RolIdNavigation { get; set; }
        public virtual Administrativo Administrativo { get; set; }
        public virtual Alumno Alumno { get; set; }
        public virtual Egresado Egresado { get; set; }
        public virtual ICollection<UsuarioPermiso> Permisos { get; set; }

    }
}
