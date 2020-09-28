using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EncuestasUABC.AccesoDatos.Repository.Interfaces;
using EncuestasUABC.Models;
using EncuestasUABC.Models.Catalogos;
using EncuestasUABC.Models.Paginacion;
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
        public UsuariosController(ILogger<UsuariosController> logger,
            IUsuarioRepository usuarioRepository,
            IRepository repository,
            RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
            _repository = repository;
            _roleManager = roleManager;
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

        #endregion

        #region Edit
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
            return RedirectToAction(nameof(Edit), new { email });
            #endregion
        }
        #endregion

        #region AJAX
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

            try
            {
                ViewBag.Campus = new SelectList(await _repository.GetAll<Campus>(), "Id", "Nombre");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            #endregion
        }

        public async Task UnidadesAcademicas()
        {
            #region UnidadesAcademicas

            try
            {
                ViewBag.UnidadesAcademicas = new SelectList(await _repository.GetAll<UnidadAcademica>(), "Id", "Nombre");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            #endregion
        }

        public async Task Carreras()
        {
            #region Carreras

            try
            {
                ViewBag.UnidadesAcademicas = new SelectList(await _repository.GetAll<UnidadAcademica>(), "Id", "Nombre");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            #endregion
        }

        #endregion

        #region PrivateMethods
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
        #endregion

    }
}