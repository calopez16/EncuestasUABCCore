using System.Collections.Generic;

namespace EncuestasUABC.Models
{
    public class UnidadAcademica
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdCampus { get; set; }
        public bool? Estatus { get; set; }
        public Campus IdCampusNavigation { get; set; }
        public virtual ICollection<Carrera> Carreras { get; set; }

    }
}