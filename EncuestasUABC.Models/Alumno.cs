using EncuestasUABC.Models.Catalogos;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EncuestasUABC.Models
{
    public class Alumno
    {
        public int Id { get; set; }
        public int Matricula { get; set; }
        public int Semestre { get; set; }
        public string PeriodoIngreso { get; set; }
        public string CorreoAlterno { get; set; }
        public string Celular { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int? CampusId { get; set; }
        public int? UnidadAcademicaId { get; set; }
        public int? CarreraId { get; set; }

        [ForeignKey(nameof(CampusId))]
        public Campus Campus { get; set; }
        [ForeignKey(nameof(UnidadAcademicaId))]
        public UnidadAcademica UnidadAcademica { get; set; }
        [ForeignKey(nameof(CarreraId))]
        public Carrera Carrera { get; set; }





    }
}