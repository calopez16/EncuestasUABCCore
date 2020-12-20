using System.Collections.Generic;

namespace EncuestasUABC.Models
{
    public class TipoPregunta
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }

        public virtual ICollection<EncuestaPregunta> EncuestaPreguntas { get; set; }

    }
}