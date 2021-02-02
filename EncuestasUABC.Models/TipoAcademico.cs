using System;
using System.Collections.Generic;
using System.Text;

namespace EncuestasUABC.Models
{
    public class TipoAcademico
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool? Estatus { get; set; }

        public virtual ICollection<Academico> Academicos { get; set; }
    }
}
