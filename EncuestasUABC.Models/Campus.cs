using System.Collections.Generic;

namespace EncuestasUABC.Models
{
    public class Campus
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estatus { get; set; }
        public virtual ICollection<UnidadAcademica> UnidadesAcademicas { get; set; }

    }
}