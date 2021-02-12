
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EncuestasUABC.ViewModels
{
    public class ResetearContrasenaViewModel
    {
        public string UserName { get; set; }
        [Display(Name = "Nueva Contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Password)]
        [Remote(action: "ValidarContrasena", controller: "Validador")]
        public string Contrasena { get; set; }
        [Display(Name = "Confirmar Contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Compare(nameof(Contrasena), ErrorMessage = "Las contraseñas no coinciden")]
        [DataType(DataType.Password)]
        public string ConfirmarContrasena { get; set; }
    }
}
