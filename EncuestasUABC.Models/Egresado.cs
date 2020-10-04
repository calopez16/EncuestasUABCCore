
using System.ComponentModel.DataAnnotations.Schema;

namespace EncuestasUABC.Models
{
    public class Egresado
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public string PeriodoIngreso { get; set; }
        public string PeriodoEgreso { get; set; }
        public string Correo { get; set; }
        public string CorreoAlterno { get; set; }
        public string Facebook { get; set; }
        public string Otro { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public ApplicationUser UsuarioIdNavigation { get; set; }
    }
}
