using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EncuestasUABC.AccesoDatos.Repository.Interfaces;
using EncuestasUABC.Models;
using EncuestasUABC.Models.Catalogos;
using EncuestasUABC.Utilidades;
using EncuestasUABC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EncuestasUABC.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(ILogger<UsuariosController> logger,
          SignInManager<ApplicationUser> signInManager,
          IUsuarioRepository usuarioRepository,
          IRepository repository,
          IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _signInManager = signInManager;
            _usuarioRepository = usuarioRepository;
            _httpContextAccessor = httpContextAccessor;
            _repository = repository;
        }
        #region Login
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            #region Login

            return View();

            #endregion
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            #region Login
            try
            {
                if (ModelState.IsValid)
                {
                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                    var user = await _usuarioRepository.Get(model.Email);
                    if (user != null)
                    {
                        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                        if (result.Succeeded)
                        {
                            List<Permiso> permisos;
                            if (user.Rol.Equals(Constantes.RolesSistema.ADMINISTRADOR))
                            {
                                permisos = await _usuarioRepository.AllPermisosUsuario();
                            }
                            else
                            {
                                permisos = await _usuarioRepository.PermisosUsuario(user.Id);
                            }
                            HttpContext.Session.SetString("UsuarioInfo", JsonConvert.SerializeObject(new UsuarioInfoViewModel
                            {
                                Id = user.Id,
                                NombreCompleto = $"{user.Nombre} {user.ApellidoPaterno} {user.ApellidoMaterno}",
                                Rol = user.Rol,
                                Permisos = permisos
                            }, new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            }));

                            _logger.LogInformation("User logged in.");
                            return RedirectToAction("Index", "Home");
                        }
                    }


                    TempData["InvalidLogin"] = "El usuario y/o contraseña son incorrectos.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return View(model);

            #endregion
        }
        #endregion

        #region Logout

        #region Perfil
        public async Task<IActionResult> Perfil()
        {
            #region Perfil
            try
            {
                var usuarioInfo = JsonConvert.DeserializeObject<UsuarioInfoViewModel>(_httpContextAccessor.HttpContext.Session.GetString("UsuarioInfo"));
                var user = await _usuarioRepository.GetById(usuarioInfo.Id);
                return View(user);
            }
            catch (MessageAlertException ex)
            {
                _logger.LogInformation(ex.Message);
                GenerarAlerta(ex);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
            finally
            {
                await Campus();
            }
            return RedirectToAction(nameof(Index));

            #endregion
        }

        [HttpPost]
        public async Task<IActionResult> Perfil(ApplicationUser model)
        {
            #region Perfil

            try
            {
                var user = await _usuarioRepository.Get(model.UserName);
                if (!model.Email.Equals(user.Email))
                {
                    if ((await _usuarioRepository.Get(model.Email)) != null)
                        throw new MessageAlertException(Enumerador.MessageAlertType.INFORMATION, string.Format(Constantes.Mensajes.USUARIOS_MSJ03, model.Email));
                }

                if (!user.Rol.Equals(model.Rol))
                {
                    var resultRemoveRol = await _usuarioRepository.RemoveRolOfUser(user, user.Rol);
                    if (resultRemoveRol.Succeeded)
                    {
                        var resultSetRol = await _usuarioRepository.SetRolToUser(user, model.Rol);
                        if (resultSetRol.Succeeded)
                            user.Rol = model.Rol;
                        else
                            throw new MessageAlertException(Enumerador.MessageAlertType.WARNING, Constantes.Mensajes.USUARIOS_MSJ13);
                    }
                }
                user.UsuarioEgresado = model.UsuarioEgresado;
                user.UsuarioAlumno = model.UsuarioAlumno;
                user.UsuarioMaestro = model.UsuarioMaestro;
                user.Nombre = model.Nombre;
                user.ApellidoPaterno = model.ApellidoPaterno;
                user.ApellidoMaterno = model.ApellidoMaterno;
                user.Email = model.Email;
                user.UserName = model.Email;
                user.NormalizedEmail = model.Email.ToUpper();
                user.NormalizedUserName = model.Email.ToUpper();

                var resultUpdate = await _usuarioRepository.Update(user);
                if (!resultUpdate.Succeeded)
                    throw new MessageAlertException(Enumerador.MessageAlertType.WARNING, string.Format(Constantes.Mensajes.USUARIOS_MSJ08, user.Email));
                ShowMessageSuccess(string.Format(Constantes.Mensajes.USUARIOS_MSJ06, user.Email));
                return RedirectToAction(nameof(Perfil));
            }
            catch (MessageAlertException ex)
            {
                _logger.LogInformation(ex.Message);
                GenerarAlerta(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ShowMessageException(ex.Message);
            }
            finally
            {
                await Campus();
            }
            return View(model);

            #endregion
        }
        #endregion

        #region CambiarContrasena
        [HttpPost]
        public async Task<IActionResult> CambiarContrasenaPerfil(string contrasenaActual, string contrasena)
        {
            #region CambiarContrasena

            try
            {
                var usuarioInfo = JsonConvert.DeserializeObject<UsuarioInfoViewModel>(_httpContextAccessor.HttpContext.Session.GetString("UsuarioInfo"));
                var user = await _usuarioRepository.GetById(usuarioInfo.Id);
                var result = await _usuarioRepository.CambiarContrasena(user, contrasenaActual, contrasena);
                if (result.Succeeded)
                {
                    TempData["InfoLogin"] = Constantes.Mensajes.USUARIOS_MSJ14;
                    await LogOut();
                }
                else
                {
                    throw new MessageAlertException(Enumerador.MessageAlertType.WARNING, Constantes.Mensajes.USUARIOS_MSJ15);
                }
            }
            catch (MessageAlertException ex)
            {
                _logger.LogInformation(ex.Message);
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex);
            }
            return RedirectToAction(nameof(Perfil));
            #endregion
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            #region LogOut

            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return View(nameof(Login));

            #endregion
        }

        #endregion

        #region ViewBags

        public async Task Campus()
        {
            #region Campus

            try
            {
                ViewBag.Campus = new SelectList(_repository.GetAll<Campus>(), "Id", "Nombre");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            #endregion
        }


        #endregion
    }
}