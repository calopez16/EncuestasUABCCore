using System.ComponentModel.DataAnnotations.Schema;

namespace EncuestasUABC.Models.Relaciones
{
    public class UsuarioMaestro
    {
        public string UsuarioId { get; set; }
        public int MaestroId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public ApplicationUser Usuario { get; set; }
        [ForeignKey(nameof(MaestroId))]
        public Maestro Maestro { get; set; }
    }
}
