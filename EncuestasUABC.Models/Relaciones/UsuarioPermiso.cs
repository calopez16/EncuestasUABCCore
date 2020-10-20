using EncuestasUABC.Models.Catalogos;

namespace EncuestasUABC.Models.Relaciones
{
    public class UsuarioPermiso
    {
        public string UsuarioId { get; set; }
        public int PermisoId { get; set; }
        public ApplicationUser UsuarioIdNavigation { get; set; }
        public Permiso PermisoIdNavigation { get; set; }

    }
}