using EncuestasUABC.Models;
using System;
using System.Collections.Generic;

namespace EncuestasUABC.Models
{
    public class Encuesta
    {
        public Encuesta()
        {
            EncuestaSecciones = new HashSet<EncuestaSeccion>();
        }
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string IdUsuarioRegistro { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdEstatusEncuesta { get; set; }
        public int IdCarrera { get; set; }

        public EstatusEncuesta IdEstatusEncuestaNavigation { get; set; }
        public Carrera IdCarreraNavigation { get; set; }
        public ApplicationUser IdUsuarioRegistroNavigation { get; set; }
        public virtual ICollection<EncuestaSeccion> EncuestaSecciones { get; set; }

    }
}