using System.ComponentModel.DataAnnotations;

namespace EncuestasUABC.ViewModels
{
    public class CambiarContrasenaViewModel
    {
        public string Email { get; set; }
        [Display(Name = "Contraseña Actual")]
        public string OldPassword { get; set; }
        [Display(Name = "Nueva Contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Password { get; set; }
        [Display(Name = "Confirmar Contraseña")]
        [Required(ErrorMessage ="El campo {0} es requerido")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
