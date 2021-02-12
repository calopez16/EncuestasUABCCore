using EncuestasUABC.Models.ViewModels.Catalogos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EncuestasUABC.Models
{
    public class ApplicationUserViewModel
    {

        [Required(ErrorMessage = "El {0} es requerido")]
        [MaxLength(80)]
        public string Nombre { get; set; }

        [Display(Name = "Apellido Paterno")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [MaxLength(80)]
        public string ApellidoPaterno { get; set; }

        [Display(Name = "Apellido Materno")]
        [MaxLength(80)]
        public string ApellidoMaterno { get; set; }
        public string UserName { get; set; }

        [Remote(action: "ValidarCorreoUABC", controller: "Validador", AdditionalFields = nameof(UserName))]
        [Display(Name = "Correo")]
        [EmailAddress(ErrorMessage = "El campo {0} no es un correo válido")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public string Email { get; set; }
        public bool Estatus { get; set; } = true;

        [Required(ErrorMessage = "El {0} es requerido")]
        public string Rol { get; set; }


    }

    public class PermisoViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public bool Menu { get; set; }
        public int? IdPermisoPadre { get; set; }
        public bool Estatus { get; set; }
        public int Orden { get; set; }

        public PermisoViewModel IdPermisoPadreNavigation { get; set; }
        public ICollection<PermisoViewModel> PermisosHijos { get; set; }
    }
}
