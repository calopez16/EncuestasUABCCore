using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EncuestasUABC.Models
{
    public class Academico
    {
        public int Id { get; set; }
        public DateTime FechaRegistro { get; set; }
        [MaxLength(80)]
        public string Nombre { get; set; }
        [MaxLength(80)]
        public string ApellidoPaterno { get; set; }
        [MaxLength(80)]
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Correo { get; set; }
        public string CorreoAlterno { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string Facebook { get; set; }
        public int IdTipoAcademico { get; set; }
        public string Otro { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdAdministrativo { get; set; }
        public bool? Estatus { get; set; }
        public TipoAcademico IdTipoAcademicoNavigation { get; set; }
        public Alumno IdAlumnoNavigation { get; set; }
        public Administrativo IdAdministrativoNavigation { get; set; }
    }
}
