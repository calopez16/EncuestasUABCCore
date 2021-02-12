using EncuestasUABC.Models.ViewModels.Catalogos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public int IdEstatusEncuesta { get; set; }
        [Display(Name = "Carrera")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdCarrera { get; set; }

        //public EstatusEncuesta EstatusEncuestaIdNavigation { get; set; }
        public CarreraViewModel IdCarreraNavigation { get; set; }
        public ApplicationUserViewModel IdUsuarioNavigation { get; set; }
        public virtual ICollection<EncuestaSeccionViewModel> EncuestaSecciones { get; set; }
    }
    public class EncuestaSeccionViewModel
    {

        public int Id { get; set; }
        public int IdEncuesta { get; set; }
        [Required(ErrorMessage ="El campo {0} es requerido")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
        public bool Eliminado { get; set; } = false;

        public EncuestaViewModel IdEncuestaNavigation { get; set; }
        public ICollection<EncuestaPreguntaViewModel> EncuestaPreguntas { get; set; }
    }

    public class EncuestaPreguntaViewModel
    {       
        public int Id { get; set; }
        public int IdEncuesta { get; set; }
        public int IdEncuestaSeccion { get; set; }
        [Display(Name = "Descripción")]
        [Required(ErrorMessage ="El campo {0} es requerido")]
        public string Descripcion { get; set; }
        public int IdTipoPregunta { get; set; }
        public bool Obligatoria { get; set; }
        public int Orden { get; set; }
        public int? IdEncuestaPadre { get; set; }
        public int? IdEncuestaSeccionPadre { get; set; }
        public int? IdEncuestaPreguntaPadre { get; set; }
        public bool Eliminado { get; set; } = false;

        public EncuestaSeccionViewModel IdEncuestaSeccionNavigation { get; set; }
        public TipoPreguntaViewModel IdTipoPreguntaNavigation { get; set; }
        public EncuestaPreguntaViewModel IdEncuestaPreguntaPadreNavigation { get; set; }
        public virtual ICollection<EncuestaPreguntaViewModel> SubPreguntas { get; set; }
        public virtual ICollection<EncuestaPreguntaOpcionViewModel> Opciones { get; set; }

    }

    public class EncuestaPreguntaOpcionViewModel
    {
        public int Id { get; set; }
        public int IdEncuesta { get; set; }
        public int IdEncuestaSeccion { get; set; }
        public int IdEncuestaPregunta { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
        public bool Eliminado { get; set; } = false;

        public EncuestaPreguntaViewModel IdEncuestaPreguntaNavigation { get; set; }
    }

    public class TipoPreguntaViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }

        public virtual ICollection<EncuestaPreguntaViewModel> EncuestaPreguntas { get; set; }

    }

}
