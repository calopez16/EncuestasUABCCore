using System.Collections.Generic;

namespace EncuestasUABC.Models.Catalogos.Estatus
{
    public class EstatusEncuesta
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }

        public virtual ICollection<Encuesta> Encuestas { get; set; }
    }
}