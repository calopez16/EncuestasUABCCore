using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EncuestasUABC.Models
{
    public class EncuestaSeccion
    {
        public EncuestaSeccion()
        {
            EncuestaPreguntas= new HashSet<EncuestaPregunta>();
            Eliminado = false;
        }
        
        public int Id { get; set; }
        public int IdEncuesta { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
        public bool? Eliminado { get; set; }

        public Encuesta IdEncuestaNavigation { get; set; }
        public ICollection<EncuestaPregunta> EncuestaPreguntas { get; set; }
    }
}