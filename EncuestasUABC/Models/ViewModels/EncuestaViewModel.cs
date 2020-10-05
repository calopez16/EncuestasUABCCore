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
        //public virtual ICollection<EncuestaSeccion> EncuestaSecciones { get; set; }
    }
}
