using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EncuestasUABC.AccesoDatos.Repository.Interfaces;
using EncuestasUABC.Constantes;
using EncuestasUABC.Enumerador;
using EncuestasUABC.Models;
using EncuestasUABC.Models.Paginacion;
using EncuestasUABC.Models.Relaciones;
using EncuestasUABC.Models.Usuarios;
using EncuestasUABC.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EncuestasUABC.Controllers
{
    [Authorize]
    public class UsuariosController : BaseController
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRepository _repository;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UsuariosController(ILogger<UsuariosController> logger,
            IUsuarioRepository usuarioRepository,
            IRepository repository,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
            _repository = repository;
            _roleManager = roleManager;
            _mapper = mapper;
        }



        #region Index

        public async Task<IActionResult> Index()
        {
            #region Index

            await Roles();
            return View();

            #endregion
        }

        #endregion

        #region Create

        public async Task<IActionResult> Create()
        {
            #region Create

            await Roles();
            return View();

            #endregion
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUserViewModel model)
        {
            #region Create
            try
            {
                model.UserName = model.Email;
                model.Activo = true;

                if ((await _repository.FindBy<ApplicationUser>(x => x.Email.Equals(model.Email))).Any())
                    throw new MessageAlertException(MessageAlertType.Information, string.Format(Mensajes.USUARIOS_MSJ03, model.Email));
                if (model.Rol == RolesSistema.Administrador)
                {
                    model.Alumno = null;
                    model.Administrativo = null;
                    model.Egresado = null;
                }
                else if (model.Rol.Equals(RolesSistema.Alumno))
                {
                    model.Administrativo = null;
                    model.Egresado = null;
                }
                else if (model.Rol.Equals(RolesSistema.Egresado))
                {
                    model.Alumno = null;
                    model.Administrativo = null;
                }
                else
                {
                    model.Alumno = null;
                    model.Egresado = null;
                }
                var newApplicationUser = _mapper.Map<ApplicationUser>(model);
                var result = await _usuarioRepository.Create(newApplicationUser);
                if (!result.Succeeded)
                    throw new MessageAlertException(MessageAlertType.Danger, Mensajes.USUARIOS_MSJ05);

                await _usuarioRepository.SetRolToUser(newApplicationUser, model.Rol);

                ShowMessageSuccess(string.Format(Mensajes.USUARIOS_MSJ01, model.Email));
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

        #endregion

        #region Edit
        public async Task<IActionResult> Edit(string email)
        {
            #region Edit

            try
            {
                var user = await _repository.FirstOrDefault<ApplicationUser>(x => x.Email.Equals(email), x => x.Administrativo, x => x.Alumno, x => x.Egresado);
                if (user == null)
                    throw new MessageAlertException(MessageAlertType.Warning, Mensajes.USUARIOS_MSJ07);
                var userResult = _mapper.Map<ApplicationUserViewModel>(user);
                userResult.Rol = await _usuarioRepository.GetRolByUser(user);
                return View(userResult);
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
            return RedirectToAction(nameof(Index));

            #endregion
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUserViewModel model)
        {
            #region Edit

            try
            {
                var user = await _repository.FirstOrDefault<ApplicationUser>(x => x.Email.Equals(model.Email));
                if (user == null)
                    throw new MessageAlertException(MessageAlertType.Warning, Mensajes.USUARIOS_MSJ07);
                var userRol = await _usuarioRepository.GetRolByUser(user);

                if (!model.Email.Equals(user.Email))
                {
                    if ((await _repository.FindBy<ApplicationUser>(x => x.Email.Equals(model.Email))).Any())
                        throw new MessageAlertException(MessageAlertType.Information, string.Format(Mensajes.USUARIOS_MSJ03, model.Email));
                }

                if (model.Rol == RolesSistema.Administrador)
                {
                    model.Alumno = null;
                    model.Administrativo = null;
                    model.Egresado = null;
                }
                else if (model.Rol.Equals(RolesSistema.Alumno))
                {
                    model.Administrativo = null;
                    model.Egresado = null;
                    user.Alumno = _mapper.Map<Alumno>(model.Alumno);
                }
                else if (model.Rol.Equals(RolesSistema.Egresado))
                {
                    model.Alumno = null;
                    model.Administrativo = null;
                    user.Egresado = _mapper.Map<Egresado>(model.Egresado);
                }
                else
                {
                    model.Alumno = null;
                    model.Egresado = null;
                    user.Administrativo = _mapper.Map<Administrativo>(model.Administrativo);
                }
                user.Nombre = model.Nombre;
                user.ApellidoPaterno = model.ApellidoPaterno;
                user.ApellidoMaterno = model.ApellidoMaterno;
                user.Email = model.Email;
                user.UserName = model.Email;
                user.NormalizedEmail = model.Email.ToUpper();
                user.NormalizedUserName = model.Email.ToUpper();

                if (!(await _usuarioRepository.Update(user)).Succeeded)
                    throw new MessageAlertException(MessageAlertType.Warning, string.Format(Mensajes.USUARIOS_MSJ08, user.Email));

                if (!userRol.Equals(model.Rol))
                {
                    if ((await _usuarioRepository.RemoveRolOfUser(user, userRol)).Succeeded)
                    {
                        if (!(await _usuarioRepository.SetRolToUser(user, model.Rol)).Succeeded)
                            throw new MessageAlertException(MessageAlertType.Warning, Mensajes.USUARIOS_MSJ13);
                    }
                }

                ShowMessageSuccess(string.Format(Mensajes.USUARIOS_MSJ06, user.Email));
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
        #endregion

        #region Permisos
        public async Task<IActionResult> Permisos(string email)
        {
            #region Permisos

            try
            {
                var permisos = await _usuarioRepository.Permisos();
                var user = await _repository.FirstOrDefault<ApplicationUser>(x => x.Email.Equals(email));
                var permisosUsuario = await _usuarioRepository.PermisosByUser(user.Id);
                ViewBag.PermisosUsuario = _mapper.Map<List<PermisoViewModel>>(permisosUsuario);
                var permisosResult = _mapper.Map<List<PermisoViewModel>>(permisos);
                ViewBag.Email = email;
                return View(permisosResult);
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
        public async Task<IActionResult> Permisos(int[] idPermiso, string email)
        {
            #region Permisos

            try
            {
                var user = await _repository.FirstOrDefault<ApplicationUser>(x => x.Email.Equals(email));
                List<UsuarioPermiso> usuarioPermisos = new List<UsuarioPermiso>();
                foreach (var permisoid in idPermiso)
                {
                    usuarioPermisos.Add(new UsuarioPermiso
                    {
                        PermisoId = permisoid,
                        UsuarioId = user.Id
                    });
                }

                user.Permisos = usuarioPermisos;
                var result = await _usuarioRepository.Update(user);
                if (result.Succeeded)
                    ShowMessageSuccess(string.Format(Mensajes.Usuarios_Msj17, user.Email));
                else
                    throw new MessageAlertException(MessageAlertType.Warning, string.Format(Mensajes.Usuarios_Msj18, user.Email));

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
            return RedirectToAction(nameof(Permisos), new { email });

            #endregion
        }

        #endregion

        #region Delete & Restore
        [HttpPost]
        public async Task<IActionResult> Delete(string email)
        {
            #region Delete
            try
            {
                var user = await _repository.FirstOrDefault<ApplicationUser>(x => x.Email.Equals(email));
                user.Activo = false;
                var resultUpdate = await _usuarioRepository.Update(user);
                if (!resultUpdate.Succeeded)
                    throw new MessageAlertException(MessageAlertType.Warning, string.Format(Mensajes.USUARIOS_MSJ11, user.Email));
                ShowMessageSuccess(string.Format(Mensajes.USUARIOS_MSJ09, email));
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
                var user = await _repository.FirstOrDefault<ApplicationUser>(x => x.Email.Equals(email));
                user.Activo = true;
                var resultUpdate = await _usuarioRepository.Update(user);
                if (!resultUpdate.Succeeded)
                    throw new MessageAlertException(MessageAlertType.Warning, string.Format(Mensajes.USUARIOS_MSJ12, user.Email));

                ShowMessageSuccess(string.Format(Mensajes.USUARIOS_MSJ10, email));

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

        #region CambiarContrasena

        [HttpPost]
        public async Task<IActionResult> CambiarContrasena(CambiarContrasenaViewModel model)
        {
            #region CambiarContrasena

            try
            {
                var user = await _repository.FirstOrDefault<ApplicationUser>(x => x.Email.Equals(model.Email));
                var result = await _usuarioRepository.CambiarContrasena(user, model.Password);
                if (result.Succeeded)
                {
                    ShowMessageSuccess(Mensajes.USUARIOS_MSJ16);
                }
                else
                {
                    throw new MessageAlertException(MessageAlertType.Warning, Mensajes.USUARIOS_MSJ15);
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
            return RedirectToAction(nameof(Edit), new { model.Email });
            #endregion
        }
        #endregion

        #region PAGINADO
        [HttpPost]
        public async Task<IActionResult> Paginado([FromBody] Paginacion paginacion)
        {
            #region Paginado
            try
            {
                var correo = !string.IsNullOrEmpty(paginacion.OtrosFiltros[0].ToString()) ? paginacion.OtrosFiltros[0].ToString() : "";
                var nombre = !string.IsNullOrEmpty(paginacion.OtrosFiltros[1].ToString()) ? paginacion.OtrosFiltros[1].ToString() : "";
                var rolName = !string.IsNullOrEmpty(paginacion.OtrosFiltros[2].ToString()) ? paginacion.OtrosFiltros[2].ToString() : "";
                var eliminado = paginacion.OtrosFiltros[3] != null ? bool.Parse(paginacion.OtrosFiltros[3].ToString()) : false;

                var usuarios = await _usuarioRepository.GetAll(rolName,!eliminado,nombre,correo);
                int totalRegistros = usuarios.Count();
                var usuariosResult = _mapper.Map<List<ApplicationUserViewModel>>(usuarios);               

                usuariosResult = await PaginacionApplicationUser(paginacion, usuariosResult);
                var result = new PaginacionResult<ApplicationUserViewModel>()
                {
                    Resultado = usuariosResult,
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
        #endregion

        #region ViewBags

        public async Task Roles()
        {
            #region Roles

            ViewBag.Roles = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");

            #endregion
        }


        #endregion

        #region PrivateMethods
        private async Task<List<ApplicationUserViewModel>> PaginacionApplicationUser(Paginacion paginacion, List<ApplicationUserViewModel> usuarios)
        {
            #region PaginacionApplicationUser

            int columnaOrdenar = paginacion.Order[0].Column;
            string ordenacion = paginacion.Order[0].Dir;
            string busqueda = paginacion.Search.Value ?? "";

            switch (columnaOrdenar)
            {
                case 0:
                    if (ordenacion.Equals("asc")) usuarios = usuarios.OrderBy(x => x.Email).ToList();
                    else usuarios = usuarios.OrderByDescending(x => x.Email).ToList();
                    break;
                case 1:
                    if (ordenacion.Equals("asc")) usuarios = usuarios.OrderBy(x => x.Nombre).ToList();
                    else usuarios = usuarios.OrderByDescending(x => x.Nombre).ToList();
                    break;
                case 2:
                    if (ordenacion.Equals("asc")) usuarios = usuarios.OrderBy(x => x.ApellidoPaterno).ToList();
                    else usuarios = usuarios.OrderByDescending(x => x.ApellidoPaterno).ToList();
                    break;
                case 3:
                    if (ordenacion.Equals("asc")) usuarios = usuarios.OrderBy(x => x.ApellidoMaterno).ToList();
                    else usuarios = usuarios.OrderByDescending(x => x.ApellidoMaterno).ToList();
                    break;
                case 4:
                    if (ordenacion.Equals("asc")) usuarios = usuarios.OrderBy(x => x.Rol).ToList();
                    else usuarios = usuarios.OrderByDescending(x => x.Rol).ToList();
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

            //foreach (var usuario in usuarios)
            //{
            //    usuario.Rol = await _usuarioRepository.GetRolByUser(usuario);
            //}
            return usuarios;
            #endregion
        }
        #endregion

    }
}