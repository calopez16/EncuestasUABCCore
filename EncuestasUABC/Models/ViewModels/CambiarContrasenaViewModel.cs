using System.ComponentModel.DataAnnotations;

namespace EncuestasUABC.ViewModels
{
    public class CambiarContrasenaViewModel
    {
        public string UserName { get; set; }
        [Display(Name = "Contraseña Actual")]
        [DataType(DataType.Password)]
        public string ContrasenaActual { get; set; }
        [Display(Name = "Nueva Contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Password)]
        public string NuevaContrasena { get; set; }

        [Display(Name = "Confirmar Contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Compare(nameof(NuevaContrasena), ErrorMessage = "Las contraseñas no coinciden")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
