using EncuestasUABC.Models.Relaciones;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EncuestasUABC.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(80)]
        public string Nombre { get; set; }
        [MaxLength(80)]
        public string ApellidoPaterno { get; set; }
        [MaxLength(80)]
        public string ApellidoMaterno { get; set; }
        public DateTime FechaRegistro{ get; set; }
        public bool? Estatus { get; set; } = true;
        public int? IdAdministrativo { get; set; }
        public Administrativo IdAdministrativoNavigation { get; set; }
        public virtual ICollection<UsuarioPermiso> Permisos { get; set; }
        public virtual ICollection<Encuesta> EncuestasCreadas { get; set; }

        [NotMapped]
        public string Rol { get; set; }
    }
}
