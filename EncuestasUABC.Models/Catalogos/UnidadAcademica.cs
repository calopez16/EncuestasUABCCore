using System.ComponentModel.DataAnnotations.Schema;

namespace EncuestasUABC.Models.Catalogos
{
    public class UnidadAcademica
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int CampusId { get; set; }
        public bool Estatus { get; set; }

        [ForeignKey(nameof(CampusId))]
        public Campus Campus { get; set; }

    }
}