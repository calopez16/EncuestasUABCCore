using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EncuestasUABC.AccesoDatos.Repository.Interfaces;
using EncuestasUABC.Constantes;
using EncuestasUABC.Enumerador;
using EncuestasUABC.Models;
using EncuestasUABC.Utilidades;
using EncuestasUABC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper _mapper;
        public AccountController(ILogger<UsuariosController> logger,
            SignInManager<ApplicationUser> signInManager,
            IUsuarioRepository usuarioRepository,
            IRepository repository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _logger = logger;
            _signInManager = signInManager;
            _usuarioRepository = usuarioRepository;
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        #region Login

        /// <summary>
        /// Devuelve la vista de Inicio de Sesión.
        /// </summary>
        /// <param name="returnUrl">Url al que se redireccionará al hacer Login. (No implementado aún)</param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            #region Login

            return View();

            #endregion
        }

        /// <summary>
        /// Llamada para iniciar sesión en la applicación.
        /// </summary>
        /// <param name="model">Contiene la información para el inicio de sesión.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            #region Login
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _repository.FirstOrDefault<ApplicationUser>(x => x.Email.Equals(model.Email));
                    if (user != null)
                    {
                        if ((await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false)).Succeeded)
                        {
                            List<PermisoViewModel> todosPermisos = new List<PermisoViewModel>();
                            if (user.Email.Equals(Defaults.AdminEmail))
                                todosPermisos = _mapper.Map<List<PermisoViewModel>>(await _repository.GetAll<Permiso>());
                            else
                                todosPermisos = _mapper.Map<List<PermisoViewModel>>(await _usuarioRepository.PermisosByUser(user.Id));

                            var permisosAcciones = todosPermisos.Where(x => !x.Menu).ToList();
                            var permisosMenu = todosPermisos.Where(x => x.Menu && !x.PermisoIdPadre.HasValue).OrderBy(x => x.Orden).ToList();
                            foreach (var menu in permisosMenu)
                            {
                                menu.PermisosHijos = todosPermisos.Where(x => x.PermisoIdPadre == menu.Id).OrderBy(x => x.Descripcion).ToList();
                            }
                            HttpContext.Session.SetString("UsuarioInfo", JsonConvert.SerializeObject(new UsuarioSessionInfoViewModel
                            {
                                Id = user.Id,
                                NombreCompleto = $"{user.Nombre} {user.ApellidoPaterno} {user.ApellidoMaterno}",
                                Rol = await _usuarioRepository.GetRolByUser(user),
                                Email = user.Email,
                                PermisosMenu = permisosMenu,
                                PermisosAcciones = permisosAcciones
                            }, new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            }));

                            _logger.LogInformation("User logged in.");
                            return RedirectToAction("Index", "Home");
                        }
                        else TempData["InvalidLogin"] = Mensajes.Login_MSJ01;
                    }
                    else TempData["InvalidLogin"] = Mensajes.Login_MSJ03;


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["InvalidLogin"] = Mensajes.Login_MSJ03;
            }
            return View(model);

            #endregion
        }
        #endregion

        #region Logout

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            #region LogOut

            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(Login));

            #endregion
        }

        #endregion

        #region Perfil
        /// <summary>
        /// Redirecciona a la vista del Perfil del usuario actual.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Perfil()
        {
            #region Perfil
            try
            {
                var usuarioInfo = _httpContextAccessor.GetUsuarioInfoViewModel();
                var user = await _repository.FirstOrDefault<ApplicationUser>(x => x.Id.Equals(usuarioInfo.Id));
                var userRol = await _usuarioRepository.GetRolByUser(user);
                var userResult = _mapper.Map<ApplicationUserViewModel>(user);
                userResult.Rol = userRol;
                return View(userResult);
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
            return RedirectToAction("Index", "Home");

            #endregion
        }

        /// <summary>
        /// Llamada para actualizar la información del usuario.
        /// </summary>
        /// <param name="model">Contiene la información del usuario.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Perfil(ApplicationUserViewModel model)
        {
            #region Perfil

            try
            {
                var user = await _repository.FirstOrDefault<ApplicationUser>(x => x.Email.Equals(model.Email));
                if (user == null)
                    throw new MessageAlertException(MessageAlertType.Warning, Mensajes.USUARIOS_MSJ07);
               
                user.Nombre = model.Nombre;
                user.ApellidoPaterno = model.ApellidoPaterno;
                user.ApellidoMaterno = model.ApellidoMaterno;

                if (!(await _usuarioRepository.Update(user)).Succeeded)
                    throw new MessageAlertException(MessageAlertType.Warning, string.Format(Mensajes.USUARIOS_MSJ08, user.Email));

                ShowMessageSuccess(string.Format(Mensajes.USUARIOS_MSJ06, user.Email));
                return RedirectToAction(nameof(Perfil));
            }
            catch (MessageAlertException ex)
            {
                _logger.LogError(ex.Message);
                GenerarAlerta(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ShowMessageException(ex.Message);
            }
            finally
            {
            }
            return View(model);

            #endregion
        }
        #endregion

        #region CambiarContrasena
        /// <summary>
        /// Llamada para cambiar la contraseña del usuario desde la vista de Perfil.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CambiarContrasena(CambiarContrasenaViewModel model)
        {
            #region CambiarContrasena

            try
            {
                var usuarioInfo = _httpContextAccessor.GetUsuarioInfoViewModel();
                var user = await _repository.FirstOrDefault<ApplicationUser>(x => x.Id.Equals(usuarioInfo.Id));
                var result = await _usuarioRepository.CambiarContrasena(user, model.OldPassword, model.Password);
                if (!result.Succeeded)
                {
                    if (result.Errors.Any(x => x.Code.Equals("PasswordMismatch")))
                        throw new MessageAlertException(MessageAlertType.Warning, Mensajes.Usuarios_Msj19);
                    throw new MessageAlertException(MessageAlertType.Warning, Mensajes.USUARIOS_MSJ15);
                }

                ShowMessageSuccess(Mensajes.USUARIOS_MSJ14);
            }
            catch (MessageAlertException ex)
            {
                _logger.LogError(ex.Message);
                GenerarAlerta(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ShowMessageException(ex.Message);
            }
            return RedirectToAction(nameof(Perfil));
            #endregion
        }

        #endregion
    }
}