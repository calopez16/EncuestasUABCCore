using System;

namespace EncuestasUABC.Models
{
    public class EncuestaAsignada
    {
        public int Id { get; set; }
        public int IdEncuesta { get; set; }
        public int IdAcademicoAsignado { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public DateTime? FechaConclusion { get; set; }
        //Relaciones
        public Encuesta IdEncuestaNavigation { get; set; }
        public Academico IdAcademicoNavigation { get; set; }
    }
}