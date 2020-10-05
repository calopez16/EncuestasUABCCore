using EncuestasUABC.Models.Catalogos;
using EncuestasUABC.Models.Catalogos.Estatus;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int EstatusEncuestaId { get; set; }
        public int CarreraId { get; set; }

        public EstatusEncuesta EstatusEncuestaIdNavigation { get; set; }
        public Carrera CarreraIdNavigation { get; set; }
        public ApplicationUser UsuarioIdNavigation { get; set; }
        public virtual ICollection<EncuestaSeccion> EncuestaSecciones { get; set; }

    }
}