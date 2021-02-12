using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EncuestasUABC.Models
{
    public class EncuestaPreguntaOpcion
    {
        public int Id { get; set; }
        public int IdEncuesta { get; set; }
        public int IdEncuestaSeccion { get; set; }
        public int IdEncuestaPregunta { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
        public bool Eliminado { get; set; } = false;
        
        public EncuestaPregunta IdEncuestaPreguntaNavigation { get; set; }
    }
}