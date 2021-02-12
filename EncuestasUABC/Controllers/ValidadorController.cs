using EncuestasUABC.AccesoDatos.Repository.Interfaces;
using EncuestasUABC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EncuestasUABC.Controllers
{
    [AllowAnonymous]
    public class ValidadorController : BaseController
    {
        private readonly ILogger<ValidadorController> _logger;
        private readonly IRepository _repository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ValidadorController(ILogger<ValidadorController> logger,
            IRepository repository,
            IUsuarioRepository usuarioRepository)
        {
            _logger = logger;
            _repository = repository;
            _usuarioRepository = usuarioRepository;
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> ValidarCorreoUABC(string Email, string UserName)
        {
            if (!Email.EndsWith("@uabc.edu.mx"))
                return Json($"El correo debe contener el dominio UABC(@uabc.edu.mx).");

            if ((await _repository.FindBy<ApplicationUser>(x => x.Email.Equals(Email) && !x.UserName.Equals(UserName))).Any())
                return Json($"El correo ya se encuentra registrado en el sistema.");
            return Json(true);
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> ValidarContrasena(string Contrasena)
        {
            var isContrasenaValida = await _usuarioRepository.ValidarContrasena(Contrasena);
            string errores = "";
            if (!isContrasenaValida.Succeeded)
            {
                foreach (var error in isContrasenaValida.Errors)
                {
                    switch (error.Code)
                    {
                        case nameof(IdentityErrorDescriber.PasswordTooShort):
                            errores += $"<li>{Constantes.Mensajes.Usuarios_Msj20}</li>";
                            break;
                        case nameof(IdentityErrorDescriber.PasswordRequiresDigit):
                            errores += $"<li>{Constantes.Mensajes.Usuarios_Msj21}</li>";
                            break;
                        case nameof(IdentityErrorDescriber.PasswordRequiresUpper):
                            errores += $"<li>{Constantes.Mensajes.Usuarios_Msj22}</li>";
                            break;
                        case nameof(IdentityErrorDescriber.PasswordRequiresLower):
                            errores += $"<li>{Constantes.Mensajes.Usuarios_Msj23}</li>";
                            break;
                        case nameof(IdentityErrorDescriber.PasswordRequiresNonAlphanumeric):
                            errores += $"<li>{Constantes.Mensajes.Usuarios_Msj24}</li>";
                            break;
                        default:
                            errores += $"<li>{Constantes.Mensajes.Usuarios_Msj25}</li>";
                            break;
                    }
                }
                return Json($"<ul>{errores}</ul>");

            }

            return Json(true);
        }
    }
}
