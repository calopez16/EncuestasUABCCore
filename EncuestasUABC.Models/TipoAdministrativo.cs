using System;
using System.Collections.Generic;
using System.Text;

namespace EncuestasUABC.Models
{
    public class TipoAdministrativo
    {
        public int Id { get; set; }
        public int Descripcion { get; set; }
        public bool? Estatus { get; set; }
        public virtual ICollection<Administrativo> Adminstrativos { get; set; }
    }
}
