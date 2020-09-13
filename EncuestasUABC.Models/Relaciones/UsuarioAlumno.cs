using System.ComponentModel.DataAnnotations.Schema;

namespace EncuestasUABC.Models.Relaciones
{
    public class UsuarioAlumno
    {
        public string UsuarioId { get; set; }
        public int AlumnoId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public ApplicationUser Usuario { get; set; }
        [ForeignKey(nameof(AlumnoId))]
        public Alumno Alumno { get; set; }
    }
}
