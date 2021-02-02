using EncuestasUABC.Models.Relaciones;
using System.Collections.Generic;

namespace EncuestasUABC.Models
{
    public class Permiso
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public bool Menu { get; set; }
        public int? PermisoIdPadre { get; set; }
        public bool? Estatus { get; set; }

        public Permiso PermisoIdPadreNavigation { get; set; }
        public virtual ICollection<Permiso> PermisosHijos { get; set; }
        public virtual ICollection<UsuarioPermiso> Usuarios { get; set; }

    }
}