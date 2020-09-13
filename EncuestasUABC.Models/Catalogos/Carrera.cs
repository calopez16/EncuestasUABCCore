using System.ComponentModel.DataAnnotations.Schema;

namespace EncuestasUABC.Models.Catalogos
{
    public class Carrera
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int UnidadAcademicaId { get; set; }
        public bool Estatus { get; set; }

        [ForeignKey(nameof(UnidadAcademicaId))]
        public UnidadAcademica UnidadAcademica{ get; set; }

    }
}