using EncuestasUABC.Models.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EncuestasUABC.ViewModels
{
    public class UsuarioInfoViewModel
    {
        public UsuarioInfoViewModel()
        {
            Permisos = new List<Permiso>();
        }
        public string Id { get; set; }
        public string NombreCompleto { get; set; }
        public string UserName { get; set; }
        public string Rol { get; set; }
        public List<Permiso> Permisos{ get; set; }
    }
}
