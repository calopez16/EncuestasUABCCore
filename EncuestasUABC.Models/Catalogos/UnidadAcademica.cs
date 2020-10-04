using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EncuestasUABC.Models.Catalogos
{
    public class UnidadAcademica
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int CampusId { get; set; }
        public bool Estatus { get; set; }
        public Campus CampusIdNavigation { get; set; }

        public virtual ICollection<Carrera> Carreras { get; set; }

    }
}