using EncuestasUABC.Models.Catalogos;
using System;

namespace EncuestasUABC.Models
{
    public class Alumno
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public string Matricula { get; set; }
        public int Semestre { get; set; }
        public string PeriodoIngreso { get; set; }
        public string CorreoAlterno { get; set; }
        public string Celular { get; set; }
        public DateTime FechaNacimiento { get; set; } 
        public int? CarreraId { get; set; }
        public Carrera CarreraIdNavigation { get; set; }
        public ApplicationUser UsuarioIdNavigation { get; set; }

    }
}