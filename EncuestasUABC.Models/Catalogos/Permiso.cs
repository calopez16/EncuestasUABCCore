using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EncuestasUABC.Models.Catalogos
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
        public bool Estatus { get; set; }


        [ForeignKey(nameof(PermisoIdPadre))]
        public Permiso PermisoPadre { get; set; }
        public virtual ICollection<Permiso> PermisosHijos { get; set; }
    }
}