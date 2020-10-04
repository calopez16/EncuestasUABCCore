using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EncuestasUABC.Models.Catalogos
{
    public class Carrera
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int UnidadAcademicaId { get; set; }
        public bool Estatus { get; set; }
        public UnidadAcademica UnidadAcademicaIdNavigation { get; set; }

        public virtual ICollection<Alumno> Alumnos { get; set; }

    }
}