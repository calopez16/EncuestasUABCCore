using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EncuestasUABC.AccesoDatos.Repository.Interfaces;
using EncuestasUABC.Enumerador;
using EncuestasUABC.Models;
using EncuestasUABC.Models.Catalogos;
using EncuestasUABC.Models.Paginacion;
using EncuestasUABC.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

                if ((await _usuarioRepository.Get(model.UserName)) != null)
                    throw new MessageAlertException(MessageAlertType.Information, string.Format(Constantes.Mensajes.USUARIOS_MSJ03, model.Email));
                if (model.RolId == (int)RolId.Administrador)
                {
                    model.Alumno = null;
                    model.Administrativo = null;
                    model.Egresado = null;
                }
                else if(model.RolId == (int)RolId.Alumno)
                {
                    model.Administrativo = null;
                    model.Egresado = null;
                }
                else if (model.RolId == (int)RolId.Egresado)
                {
                    model.Alumno = null;
                    model.Administrativo = null;
                }
                else
                {
                    model.Administrativo = null;
                }
                var newApplicationUser = _mapper.Map<ApplicationUser>(model);

                var result = await _usuarioRepository.Create(newApplicationUser);
                if (!result.Succeeded)
                    throw new MessageAlertException(MessageAlertType.Danger, Constantes.Mensajes.USUARIOS_MSJ05);

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

        #endregion

        #region Edit
        public async Task<IActionResult> Edit(string email)
        {
            #region Edit

            try
            {
                var user = await _usuarioRepository.Get(email);
                var userResult = _mapper.Map<ApplicationUserViewModel>(user);
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
                var user = await _usuarioRepository.Get(model.UserName);
                if (!model.Email.Equals(user.Email))
                {
                    if ((await _usuarioRepository.Get(model.Email)) != null)
                        throw new MessageAlertException(MessageAlertType.Information, string.Format(Constantes.Mensajes.USUARIOS_MSJ03, model.Email));
                }

                user.Nombre = model.Nombre;
                user.ApellidoPaterno = model.ApellidoPaterno;
                user.ApellidoMaterno = model.ApellidoMaterno;
                user.Email = model.Email;
                user.UserName = model.Email;
                user.NormalizedEmail = model.Email.ToUpper();
                user.NormalizedUserName = model.Email.ToUpper();

                var resultUpdate = await _usuarioRepository.Update(user);
                if (!resultUpdate.Succeeded)
                    throw new MessageAlertException(MessageAlertType.Warning, string.Format(Constantes.Mensajes.USUARIOS_MSJ08, user.Email));
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
        #endregion

        #region Delete & Restore
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
                    throw new MessageAlertException(MessageAlertType.Warning, string.Format(Constantes.Mensajes.USUARIOS_MSJ11, user.Email));
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
                    throw new MessageAlertException(MessageAlertType.Warning, string.Format(Constantes.Mensajes.USUARIOS_MSJ12, user.Email));

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

        #region CambiarContrasena

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
                }
                else
                {
                    throw new MessageAlertException(Enumerador.MessageAlertType.Warning, Constantes.Mensajes.USUARIOS_MSJ15);
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
            return RedirectToAction(nameof(Edit), new { email });
            #endregion
        }
        #endregion

        #region AJAX
        [HttpPost]
        public async Task<IActionResult> Paginado([FromBody] Paginacion paginacion)
        {
            #region Paginado
            try
            {
                var usuarios = await _usuarioRepository.GetAll();
                int totalRegistros = usuarios.Count();
                var usuariosResult= _mapper.Map<List<ApplicationUserViewModel>>(usuarios);
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

            ViewBag.Roles = new SelectList(await _repository.GetAll<Rol>(), "Id", "Descripcion");

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