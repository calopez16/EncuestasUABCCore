using System.ComponentModel.DataAnnotations.Schema;

namespace EncuestasUABC.Models.Relaciones
{
    public class UsuarioEgresado
    {
        public string UsuarioId { get; set; }
        public int EgresadoId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public ApplicationUser Usuario { get; set; }
        [ForeignKey(nameof(EgresadoId))]
        public Egresado Egresado { get; set; }
    }
}
