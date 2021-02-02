using EncuestasUABC.Models;
using System;

namespace EncuestasUABC.Models
{
    public class Alumno
    {
        public Alumno()
        {
            FechaRegistro = DateTime.Now;
        }
        public int Id { get; set; }
        public DateTime FechaRegistro{ get; set; }
        public string Matricula { get; set; }
        public string Correo { get; set; }
        public int? Semestre { get; set; }
        public string PeriodoIngreso { get; set; }
        public string PeriodoEgreso { get; set; }
        public int? IdCarrera { get; set; }
        public bool? Egresado { get; set; }
        public bool? Estatus { get; set; }
        public Carrera IdCarreraNavigation { get; set; }
        public virtual Academico AcademicoNavigation { get; set; }

    }
}