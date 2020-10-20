using EncuestasUABC.Models;
using System.Collections.Generic;

namespace EncuestasUABC.ViewModels
{
    public class UsuarioSessionInfoViewModel
    {
        public UsuarioSessionInfoViewModel()
        {
            PermisosMenu = new List<PermisoViewModel>();
            PermisosAcciones = new List<PermisoViewModel>();
        }
        public string Id { get; set; }
        public string NombreCompleto { get; set; }
        public string UserName { get; set; }
        public string Rol { get; set; }
        public string Email { get; set; }
        public List<PermisoViewModel> PermisosMenu { get; set; }
        public List<PermisoViewModel> PermisosAcciones { get; set; }
    }
}
