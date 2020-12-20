using EncuestasUABC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EncuestasUABC.Models
{
    [Table("EncuestasAsignadas")]
    public class EncuestaAsignada
    {
        public int Id { get; set; }
        public int EncuestaId { get; set; }
        [StringLength(128)]
        public string UsuarioIdAsignado { get; set; }
        [StringLength(128)]
        public string UsuarioIdEncuestador { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
        public int EncuestaAsignadaEstatusId { get; set; }       

        //Relaciones
        [ForeignKey("EncuestaId")]
        public Encuesta Encuesta { get; set; }       
        //[ForeignKey("EncuestaAsignadaEstatusId")]
        //public EncuestaAsignadaEstatus EncuestaAsignadaEstatus { get; set; }




    }
}