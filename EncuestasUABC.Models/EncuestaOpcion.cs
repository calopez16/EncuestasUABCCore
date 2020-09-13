using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EncuestasUABC.Models
{
    [Table("EncuestasOpciones")]
    public class EncuestaOpcion
    {
        public int Id { get; set; }
        public int EncuestaId { get; set; }
        public int EncuestaSeccionId { get; set; }
        public int EncuestaPreguntaId { get; set; }
        [Required(ErrorMessage ="Ingresa una descripción"),MaxLength(150)]
        public string Descripcion { get; set; }
        [Required(ErrorMessage ="Ingresa un orden")]
        public int Orden { get; set; }
        public bool Eliminado { get; set; } = false;


        //Relaciones
        [ForeignKey("EncuestaId")]
        public Encuesta Encuesta { get; set; }
        [ForeignKey("EncuestaSeccionId")]
        public EncuestaSeccion EncuestaSeccion { get; set; }
        [ForeignKey("EncuestaPreguntaId")]
        public EncuestaPregunta EncuestaPregunta{ get; set; }

    }
}