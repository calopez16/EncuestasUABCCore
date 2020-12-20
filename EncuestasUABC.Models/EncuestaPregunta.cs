using EncuestasUABC.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EncuestasUABC.Models
{
    public class EncuestaPregunta
    {
        public EncuestaPregunta()
        {
            SubPreguntas= new HashSet<EncuestaPregunta>();
            Opciones= new HashSet<EncuestaPreguntaOpcion>();
        }
        public int Id { get; set; }
        public int EncuestaId { get; set; }
        public int EncuestaSeccionId { get; set; }
        public string Descripcion { get; set; }
        public int TipoPreguntaId { get; set; }
        public bool Obligatoria { get; set; }
        public int Orden { get; set; }
        public int? EncuestaIdPadre { get; set; }
        public int? EncuestaSeccionIdPadre { get; set; }
        public int? EncuestaPreguntaIdPadre { get; set; }

        public bool Eliminado { get; set; } = false;

        public EncuestaSeccion EncuestaSeccionIdNavigation { get; set; }
        public TipoPregunta TipoPreguntaIdNavigation { get; set; }
        public EncuestaPregunta EncuestaPreguntaIdPadreNavigation { get; set; }
        public virtual ICollection<EncuestaPregunta> SubPreguntas { get; set; }
        public virtual ICollection<EncuestaPreguntaOpcion> Opciones { get; set; }
      
    }
}