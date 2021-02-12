
namespace EncuestasUABC.Models.Relaciones
{
    public class UsuarioPermiso
    {
        public string IdUsuario { get; set; }
        public int IdPermiso { get; set; }
        public ApplicationUser IdUsuarioNavigation { get; set; }
        public Permiso IdPermisoNavigation { get; set; }

    }
}