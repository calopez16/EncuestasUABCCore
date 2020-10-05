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
        public int EncuestaId { get; set; }
        public int EncuestaSeccionId { get; set; }
        public int EncuestaPreguntaId { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
        public bool Eliminado { get; set; } = false;

        
        public EncuestaPregunta EncuestaPreguntaIdNavigation { get; set; }

    }
}