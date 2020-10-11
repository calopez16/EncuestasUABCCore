using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EncuestasUABC.Models.ViewModels
{
    public class EncuestaViewModel
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string UsuarioId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int EstatusEncuestaId { get; set; }
        [Display(Name = "Carrera")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int CarreraId { get; set; }

        //public EstatusEncuesta EstatusEncuestaIdNavigation { get; set; }
        //public Carrera CarreraIdNavigation { get; set; }
        public ApplicationUserViewModel UsuarioIdNavigation { get; set; }
        public virtual ICollection<EncuestaSeccionViewModel> EncuestaSecciones { get; set; }
    }
    public class EncuestaSeccionViewModel
    {

        public int Id { get; set; }
        public int EncuestaId { get; set; }
        [Required(ErrorMessage ="El campo {0} es requerido")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
        public bool Eliminado { get; set; } = false;

        public EncuestaViewModel EncuestaIdNavigation { get; set; }
        public ICollection<EncuestaPreguntaViewModel> EncuestaPreguntas { get; set; }
    }

    public class EncuestaPreguntaViewModel
    {       
        public int Id { get; set; }
        public int EncuestaId { get; set; }
        public int EncuestaSeccionId { get; set; }
        public string Descripcion { get; set; }
        public int TipoPreguntaId { get; set; }
        public bool Obligatoria { get; set; }
        public int Orden { get; set; }
        public int? EncuestaIdPadre { get; set; }
        public int? EncuestaSeccionIdPadre { get; set; }
        public int? EncuestaPreguntaIdPadre { get; set; }
        public bool Eliminado { get; set; } = false;

        public EncuestaSeccionViewModel EncuestaSeccionIdNavigation { get; set; }
        //public TipoPregunta TipoPreguntaIdNavigation { get; set; }
        public EncuestaPreguntaViewModel EncuestaPreguntaIdPadreNavigation { get; set; }
        public virtual ICollection<EncuestaPreguntaViewModel> SubPreguntas { get; set; }
        //public virtual ICollection<EncuestaPreguntaOpcion> Opciones { get; set; }

    }
}
