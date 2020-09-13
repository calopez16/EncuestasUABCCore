using System.ComponentModel.DataAnnotations;

namespace EncuestasUABC.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "¿Recordar mi contraseña?")]
        public bool RememberMe { get; set; }
    }
}
