using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EncuestasUABC.AccesoDatos.Repository.Interfaces;
using EncuestasUABC.Models;
using EncuestasUABC.Models.Catalogos;
using EncuestasUABC.Models.Paginacion;
using EncuestasUABC.Utilidades;
using EncuestasUABC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EncuestasUABC.Controllers
{
    [Authorize]
    public class UsuariosController : BaseController
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRepository _repository;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsuariosController(ILogger<UsuariosController> logger,
            SignInManager<ApplicationUser> signInManager,
            IUsuarioRepository usuarioRepository,
            IRepository repository,
            RoleManager<IdentityRole> roleManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _signInManager = signInManager;
            _usuarioRepository = usuarioRepository;
            _repository = repository;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
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
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        var user = await _usuarioRepository.Get(model.Email);
                        var permisos = await _usuarioRepository.PermisosUsuario(user.Id);
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
                var result = await _usuarioRepository.CambiarContrasena(user,contrasenaActual ,contrasena);
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
        [HttpPost]
        public async Task<IActionResult> CambiarContrasena(string email, string contrasena)
        {
            #region CambiarContrasena

            try
            {
                var user = await _usuarioRepository.Get(email);
                var result = await _usuarioRepository.CambiarContrasena(user, contrasena);
                if (result.Succeeded)
                {
                    ShowMessageSuccess(Constantes.Mensajes.USUARIOS_MSJ16);
                    return RedirectToAction(nameof(Edit), new { email });
                }
                else
                {
                    throw new MessageAlertException(Enumerador.MessageAlertType.WARNING, Constantes.Mensajes.USUARIOS_MSJ15);
                }
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

        #region Usuarios

        public async Task<IActionResult> Index()
        {
            #region Usuarios

            return View();

            #endregion
        }

        [HttpPost]
        public async Task<IActionResult> Paginado([FromBody]Paginacion paginacion)
        {
            #region Paginado
            try
            {
                var usuarios = await _usuarioRepository.GetAll();
                int totalRegistros = usuarios.Count();
                usuarios = await PaginacionApplicationUser(paginacion, usuarios);
                var result = new PaginacionResult<ApplicationUser>()
                {
                    Resultado = usuarios,
                    TotalRegistros = totalRegistros
                };
                var registrosFiltrados = result.TotalRegistros;
                var totalRegistrosFiltrados = result.Resultado.Count;
                var datosPaginados = result.Resultado;
                return Json(new { draw = paginacion.Draw, recordsFiltered = registrosFiltrados, recordsTotal = totalRegistrosFiltrados, data = datosPaginados });
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

            #endregion
        }

        public async Task<IActionResult> Create()
        {
            #region Create

            await Roles();
            await Campus();
            return View();

            #endregion
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUser model)
        {
            #region Create
            try
            {
                model.UserName = model.Email;
                model.Activo = true;
                model.EmailConfirmed = true;
                string rolAsignado = model.Rol;
                if ((await _usuarioRepository.Get(model.UserName)) != null)
                    throw new MessageAlertException(Enumerador.MessageAlertType.INFORMATION, string.Format(Constantes.Mensajes.USUARIOS_MSJ03, model.Email));

                var result = await _usuarioRepository.Create(model);
                if (result.Succeeded)
                {
                    var user = await _usuarioRepository.Get(model.Email);

                    var resultSetRol = await _usuarioRepository.SetRolToUser(user, rolAsignado);
                    if (!resultSetRol.Succeeded)
                        throw new MessageAlertException(Enumerador.MessageAlertType.WARNING, Constantes.Mensajes.USUARIOS_MSJ04);

                    var resultUpdate = await _usuarioRepository.Update(user);
                    //if(!resultUpdate.Succeeded)
                    //    throw new MessageAlertException(Enumerador.MessageAlertType.WARNING, string.Format(Constantes.Mensajes.USUARIOS_MSJ08, user.Email));


                }
                else
                    throw new MessageAlertException(Enumerador.MessageAlertType.DANGER, Constantes.Mensajes.USUARIOS_MSJ05);

                ShowMessageSuccess(string.Format(Constantes.Mensajes.USUARIOS_MSJ01, model.Email));
                return RedirectToAction(nameof(Index));
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
                await Roles();
            }
            return View(model);
            #endregion
        }
        public async Task<IActionResult> Edit(string email)
        {
            #region Edit

            try
            {
                var user = await _usuarioRepository.Get(email);
                return View(user);
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
                await Roles();
                await Campus();
            }
            return RedirectToAction(nameof(Index));

            #endregion
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser model)
        {
            #region Edit

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
                return RedirectToAction(nameof(Index));
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
                await Roles();
            }
            return View(model);

            #endregion
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string email)
        {
            #region Delete
            try
            {
                var user = await _usuarioRepository.Get(email);
                user.Activo = false;
                var resultUpdate = await _usuarioRepository.Update(user);
                if (!resultUpdate.Succeeded)
                    throw new MessageAlertException(Enumerador.MessageAlertType.WARNING, string.Format(Constantes.Mensajes.USUARIOS_MSJ11, user.Email));
                ShowMessageSuccess(string.Format(Constantes.Mensajes.USUARIOS_MSJ09, email));
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
            return RedirectToAction(nameof(Index));

            #endregion
        }

        [HttpPost]
        public async Task<IActionResult> Restore(string email)
        {
            #region Restore
            try
            {
                var user = await _usuarioRepository.Get(email);
                user.Activo = true;
                var resultUpdate = await _usuarioRepository.Update(user);
                if (!resultUpdate.Succeeded)
                    throw new MessageAlertException(Enumerador.MessageAlertType.WARNING, string.Format(Constantes.Mensajes.USUARIOS_MSJ12, user.Email));

                ShowMessageSuccess(string.Format(Constantes.Mensajes.USUARIOS_MSJ10, email));

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
            return RedirectToAction(nameof(Index));

            #endregion
        }
        #endregion

        #region AJAX
        [HttpPost]
        public async Task<IActionResult> UnidadesAcademicas(int campusId)
        {
            #region UnidadesAcademicas

            return Ok(_repository.FindBy<UnidadAcademica>(x => x.CampusId == campusId).Select(x => new { id = x.Id, text = x.Nombre }));

            #endregion
        }
        [HttpPost]
        public async Task<IActionResult> Carreras(int unidadAcademicaId)
        {
            #region Carreras

            return Ok(_repository.FindBy<Carrera>(x => x.UnidadAcademicaId == unidadAcademicaId).Select(x => new { id = x.Id, text = x.Nombre }));

            #endregion
        }
        #endregion


        #region ViewBags

        public async Task Roles()
        {
            #region Roles

            ViewBag.Roles = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");

            #endregion
        }

        public async Task Campus()
        {
            #region Campus

            ViewBag.Campus = new SelectList(_repository.GetAll<Campus>(), "Id", "Nombre");

            #endregion
        }

        public async Task UnidadesAcademicas()
        {
            #region UnidadesAcademicas

            ViewBag.UnidadesAcademicas = new SelectList(_repository.GetAll<UnidadAcademica>(), "Id", "Nombre");

            #endregion
        }

        public async Task Carreras()
        {
            #region Carreras

            ViewBag.Carreras = new SelectList(_repository.GetAll<Carrera>(), "Id", "Nombre");

            #endregion
        }

        #endregion

        private async Task<List<ApplicationUser>> PaginacionApplicationUser(Paginacion paginacion, List<ApplicationUser> usuarios)
        {
            #region PaginacionApplicationUser

            int columnaOrdenar = paginacion.Order[0].Column;
            string ordenacion = paginacion.Order[0].Dir;
            string busqueda = paginacion.Search.Value ?? "";

            switch (columnaOrdenar)
            {
                case 1:
                    if (ordenacion.Equals("asc")) usuarios = usuarios.OrderBy(x => x.UserName).ToList();
                    else usuarios = usuarios.OrderByDescending(x => x.UserName).ToList();
                    break;
                case 2:
                    if (ordenacion.Equals("asc")) usuarios = usuarios.OrderBy(x => x.Nombre).ToList();
                    else usuarios = usuarios.OrderByDescending(x => x.Nombre).ToList();
                    break;
                case 3:
                    if (ordenacion.Equals("asc")) usuarios = usuarios.OrderBy(x => x.ApellidoPaterno).ToList();
                    else usuarios = usuarios.OrderByDescending(x => x.ApellidoPaterno).ToList();
                    break;
                case 4:
                    if (ordenacion.Equals("asc")) usuarios = usuarios.OrderBy(x => x.ApellidoMaterno).ToList();
                    else usuarios = usuarios.OrderByDescending(x => x.ApellidoMaterno).ToList();
                    break;
                case 6:
                    if (ordenacion.Equals("asc")) usuarios = usuarios.OrderBy(x => x.Activo).ToList();
                    else usuarios = usuarios.OrderByDescending(x => x.Activo).ToList();
                    break;
                default:
                    usuarios = usuarios.OrderBy(x => x.UserName).ToList();
                    break;
            }

            if (!string.IsNullOrEmpty(paginacion.Search.Value))
            {
                busqueda = busqueda.ToLower();
                usuarios = usuarios.Where(x => x.Email.ToLower().Contains(busqueda)
                  || x.Nombre.ToLower().Contains(busqueda)
                  || x.ApellidoPaterno.ToLower().Contains(busqueda)
                  || x.ApellidoMaterno.ToLower().Contains(busqueda)).ToList();
            }

            usuarios = usuarios
                       .Skip(paginacion.Start)
                       .Take(paginacion.Length)
                       .ToList();

            foreach (var usuario in usuarios)
            {
                usuario.Rol = await _usuarioRepository.GetRolByUser(usuario);
            }
            return usuarios;
            #endregion
        }
    }
}