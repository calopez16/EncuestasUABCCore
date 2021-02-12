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
        public int IdEncuesta { get; set; }
        public int IdEncuestaSeccion { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoPregunta { get; set; }
        public bool Obligatoria { get; set; }
        public int Orden { get; set; }
        public int? IdEncuestaPadre { get; set; }
        public int? IdEncuestaSeccionPadre { get; set; }
        public int? IdEncuestaPreguntaPadre { get; set; }
        public bool Eliminado { get; set; } = false;

        public EncuestaSeccion IdEncuestaSeccionNavigation { get; set; }
        public TipoPregunta IdTipoPreguntaNavigation { get; set; }
        public EncuestaPregunta IdEncuestaPreguntaPadreNavigation { get; set; }
        public virtual ICollection<EncuestaPregunta> SubPreguntas { get; set; }
        public virtual ICollection<EncuestaPreguntaOpcion> Opciones { get; set; }
      
    }
}