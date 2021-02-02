using System.Collections.Generic;

namespace EncuestasUABC.Models
{
    public class Carrera
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdUnidadAcademica { get; set; }
        public bool? Estatus { get; set; }
        public UnidadAcademica IdUnidadAcademicaNavigation { get; set; }
        public virtual ICollection<Alumno> Alumnos { get; set; }
        public virtual ICollection<Encuesta> Encuestas { get; set; }

    }
}