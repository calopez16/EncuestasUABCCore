using System;
using System.Collections.Generic;
using System.Text;

namespace EncuestasUABC.Models.Catalogos
{
    public class Rol
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }

        public ICollection<ApplicationUser> Usuarios { get; set; }
    }
}
