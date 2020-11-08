using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EncuestasUABC.AccesoDatos.Repository.Interfaces;
using EncuestasUABC.Models.Catalogos;
using EncuestasUABC.Models;
using EncuestasUABC.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using EncuestasUABC.Models.Paginacion;
using EncuestasUABC.Enumerador;
using EncuestasUABC.Models.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using EncuestasUABC.Models.SelectViewModel;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace EncuestasUABC.Controllers
{
    public class EncuestasController : BaseController
    {
        private readonly ILogger<EncuestasController> _logger;
        private readonly IEncuestasRepository _encuestasRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public EncuestasController(ILogger<EncuestasController> logger,
            IEncuestasRepository encuestasRepository,
            IUsuarioRepository usuarioRepository,
            IHttpContextAccessor httpContextAccessor,
            IRepository repository,
            IMapper mapper)
        {
            _encuestasRepository = encuestasRepository;
            _usuarioRepository = usuarioRepository;
            _logger = logger;
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;

        }

        public IActionResult Index()
        {
            return View();
        }

        #region CREADAS

        #region INDEX
        public async Task<IActionResult> Creadas()
        {
            #region Creadas

            return View();

            #endregion
        }

        #endregion

        #region EDITAR
        public async Task<IActionResult> Editar(int id)
        {
            #region Edit
            try
            {
                var encuesta = await _encuestasRepository.GetById(id);
                var encuestaViewModel = _mapper.Map<EncuestaViewModel>(encuesta);
                return View(encuestaViewModel);
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

        public async Task<IActionResult> EditarSeccion(int id, int encuestaId)
        {
            #region EditarSeccion
            try
            {
                var seccion = await _encuestasRepository.GetSeccionById(id, encuestaId);
                var seccionViewModel = _mapper.Map<EncuestaSeccionViewModel>(seccion);
                return View(seccionViewModel);
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
            return RedirectToAction(nameof(Editar), new { id = encuestaId });

            #endregion
        }
        #endregion

        #region CREAR
        [HttpPost]
        public async Task<IActionResult> Crear(EncuestaViewModel model)
        {
            #region Crear
            try
            {
                model.UsuarioId = _httpContextAccessor.GetUsuarioId();
                model.EstatusEncuestaId = (int)EstatusEncuestaId.Inactiva;
                var encuesta = _mapper.Map<Encuesta>(model);
                var result = await _repository.Add<Encuesta>(encuesta);
                ShowMessageSuccess(Constantes.Mensajes.ENCUESTAS_MSJ01);
                return RedirectToAction(nameof(Editar), new { id = result.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ShowMessageException(ex.Message);
            }
            return View(nameof(Creadas));

            #endregion
        }
        #endregion

        #region VISTA PREVIA

        public async Task<IActionResult> VistaPrevia(int id)
        {
            #region VistaPrevia
            try
            {
                var encuesta = await _encuestasRepository.GetById(id);
                var encuestaViewModel = _mapper.Map<EncuestaViewModel>(encuesta);
                return View(encuestaViewModel);
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

        #region Paginado
        /// <summary>
        /// Se obtienen los registro de las Encuestas Creadas de la base de datos
        /// </summary>
        /// <param name="paginacion">Contiene los valores para la paginación</param>
        /// <returns>Retorna una estructura Json que permite a la herramienta Datatables.js crear la paginación en el HTML.</returns>
        [HttpPost]
        public async Task<IActionResult> CreadasPaginado([FromBody] Paginacion paginacion)
        {
            #region CreadasPaginado
            try
            {
                var user = _httpContextAccessor.GetUsuarioInfoViewModel();
                List<Encuesta> encuestas;
                if (user.Rol.Equals(Constantes.RolesSistema.Administrador))
                {
                    encuestas = (await _repository.FindBy<Encuesta>(null, x => x.OrderByDescending(x => x.Fecha), x => x.CarreraIdNavigation, x => x.EstatusEncuestaIdNavigation)).ToList();
                }
                else
                {
                    encuestas = (await _repository.FindBy<Encuesta>(x => x.UsuarioId == user.Id, x => x.OrderByDescending(x => x.Fecha), x => x.CarreraIdNavigation, x => x.EstatusEncuestaIdNavigation)).ToList();
                }

                int totalRegistros = encuestas.Count();
                int column = paginacion.Order[0].Column;
                var order = paginacion.Order[0].Dir;

                if (!string.IsNullOrEmpty(paginacion.Search.Value))
                {
                    encuestas = encuestas.Where(x => x.Nombre.Contains(paginacion.Search.Value)).ToList();
                }

                switch (column)
                {
                    case 0:
                        if (order.Equals("asc"))
                            encuestas = encuestas.OrderBy(x => x.EstatusEncuestaIdNavigation.Descripcion).ToList();
                        else
                            encuestas = encuestas.OrderByDescending(x => x.EstatusEncuestaIdNavigation.Descripcion).ToList();
                        break;
                    case 1:
                        if (order.Equals("asc"))
                            encuestas = encuestas.OrderBy(x => x.Fecha).ToList();
                        else
                            encuestas = encuestas.OrderByDescending(x => x.Fecha).ToList();
                        break;
                    case 2:
                        if (order.Equals("asc"))
                            encuestas = encuestas.OrderBy(x => x.Nombre).ToList();
                        else
                            encuestas = encuestas.OrderByDescending(x => x.Nombre).ToList();
                        break;
                    case 3:
                        if (order.Equals("asc"))
                            encuestas = encuestas.OrderBy(x => x.CarreraIdNavigation.Nombre).ToList();
                        else
                            encuestas = encuestas.OrderByDescending(x => x.CarreraIdNavigation.Nombre).ToList();
                        break;
                    default:
                        break;
                }
                var encuestasResult = encuestas
                            .Skip(paginacion.Start)
                            .Take(paginacion.Length)
                            .Select(x => new
                            {
                                x.Id,
                                x.Fecha,
                                x.Nombre,
                                CarreraIdNavigation = new
                                {
                                    x.CarreraIdNavigation.Nombre
                                },
                                x.EstatusEncuestaId,
                                EstatusEncuestaIdNavigation = new
                                {
                                    x.EstatusEncuestaIdNavigation.Descripcion
                                }
                            })
                            .ToList();


                var registrosFiltrados = encuestasResult.Count();
                var totalRegistrosFiltrados = totalRegistros;
                var datosPaginados = encuestasResult;
                return Json(new { draw = paginacion.Draw, recordsFiltered = registrosFiltrados, recordsTotal = totalRegistrosFiltrados, data = datosPaginados });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            #endregion
        }
        #endregion

        #region ELIMINAR ENCUESTA
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            #region Delete
            try
            {
                var encuesta = await _repository.GetById<Encuesta>(id);
                encuesta.EstatusEncuestaId = (int)EstatusEncuestaId.Eliminada;
                await _repository.Update<Encuesta>(encuesta);
                ShowMessageSuccess(Constantes.Mensajes.Encuesta_msj07);

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
            return RedirectToAction(nameof(Creadas));
            #endregion
        }

        [HttpPost]
        public async Task<IActionResult> Restore(int id)
        {
            #region Restore
            try
            {
                var encuesta = await _repository.GetById<Encuesta>(id);
                encuesta.EstatusEncuestaId = (int)EstatusEncuestaId.Inactiva;
                await _repository.Update<Encuesta>(encuesta);
                ShowMessageSuccess(Constantes.Mensajes.Encuesta_msj09);
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
            return RedirectToAction(nameof(Creadas));
            #endregion
        }
        #endregion

        #region CAMBIAR ESTATUS DE ENCUESTA
        [HttpPost]
        public async Task<IActionResult> CambiarActivo(int id, bool activo)
        {
            #region CambiarActivo
            try
            {
                var encuesta = await _repository.GetById<Encuesta>(id);
                if (activo)
                {
                    encuesta.EstatusEncuestaId = (int)EstatusEncuestaId.Activa;
                }
                else
                {
                    encuesta.EstatusEncuestaId = (int)EstatusEncuestaId.Inactiva;
                }
                await _repository.Update<Encuesta>(encuesta);
            }
            catch (MessageAlertException ex)
            {
                _logger.LogInformation(ex.Message);
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
            return Ok();
            #endregion
        }
        #endregion

        #region GET

        [HttpGet]
        public async Task<IActionResult> GetEncuesta(int id)
        {
            #region EditarNombreDescripcion
            try
            {
                var encuesta = await _repository.FirstOrDefault<Encuesta>(x => x.Id == id, x => x.EncuestaSecciones);
                encuesta.EncuestaSecciones = encuesta.EncuestaSecciones.OrderBy(x => x.Orden).ToList();
                return Ok(encuesta);
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
            return BadRequest();
            #endregion
        }

        [HttpGet]
        public async Task<IActionResult> GetPregunta(int preguntaId, int seccionId, int encuestaId)
        {
            #region EditarNombreDescripcion
            try
            {
                var pregunta = await _repository
                    .FirstOrDefault<EncuestaPregunta>(x => x.Id == preguntaId
                                                    && x.EncuestaSeccionId == seccionId
                                                    && x.EncuestaId == encuestaId, x => x.Opciones);
                pregunta.Opciones = pregunta.Opciones.Where(x => !x.Eliminado).OrderBy(x=>x.Orden).ToList();
                return Ok(JsonConvert.SerializeObject(pregunta, Formatting.None,
                            new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            }));
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
            return BadRequest();
            #endregion
        }

        #endregion

        #region AGREGAR

        [HttpPost]
        public async Task<IActionResult> CrearSeccion(int encuestaId, string nombre)
        {
            #region CrearSeccion
            try
            {
                var secciones = await _repository.FindBy<EncuestaSeccion>(x => x.EncuestaId == encuestaId);
                int orden = 1;
                if (secciones.Any())
                    orden = secciones.OrderBy(x => x.Orden).Last().Orden + 1;
                var newSeccion = new EncuestaSeccion
                {
                    EncuestaId = encuestaId,
                    Nombre = nombre,
                    Orden = orden,
                };
                await _repository.Add<EncuestaSeccion>(newSeccion);
                return Ok(newSeccion.Id);

            }
            catch (MessageAlertException ex)
            {
                _logger.LogInformation(ex.Message);
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
            #endregion
        }

        [HttpPost]
        public async Task<IActionResult> CrearPregunta(EncuestaPreguntaViewModel model)
        {
            #region EditarNombreDescripcion
            try
            {
                var newPregunta = _mapper.Map<EncuestaPregunta>(model);
                var orden = 1;
                var preguntas = await _repository.FindBy<EncuestaPregunta>(x => x.EncuestaId == model.EncuestaId && x.EncuestaSeccionId == model.EncuestaSeccionId && !x.Eliminado, x => x.OrderByDescending(x => x.Orden));
                if (preguntas.Any())
                    orden = preguntas.First().Orden + 1;
                newPregunta.Orden = orden;
                await _repository.Add<EncuestaPregunta>(newPregunta);
                return Ok(newPregunta.Id);
            }
            catch (MessageAlertException ex)
            {
                _logger.LogInformation(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return BadRequest();
            #endregion
        }

        [HttpPost]
        public async Task<IActionResult> EditarPregunta(EncuestaPreguntaViewModel model)
        {
            #region EditarNombreDescripcion
            try
            {
                var pregunta = await _repository.FirstOrDefault<EncuestaPregunta>(x => x.Id == model.Id, x => x.Opciones);

                if (model.TipoPreguntaId == (int)TipoPreguntaId.Multiple || model.TipoPreguntaId == (int)TipoPreguntaId.SelectList || model.TipoPreguntaId == (int)TipoPreguntaId.UnicaOpcion)
                {
                    var idsOpcionesSinEliminar = model.Opciones.Select(x => x.Id);
                    var preguntassss = pregunta.Opciones.Where(x => !idsOpcionesSinEliminar.Contains(x.Id)).ToList();//.ForEach(x => x.Eliminado = true);
                    model.Opciones.ToList().ForEach(x => {
                        if (x.Id != 0)
                        {
                            var opcionEditar = pregunta.Opciones.FirstOrDefault(y => y.Id == x.Id);
                            opcionEditar.Descripcion = x.Descripcion;
                            opcionEditar.Orden = x.Orden;
                        }
                        else
                        {
                            var nuevaOpcion = _mapper.Map<EncuestaPreguntaOpcion>(x);
                            pregunta.Opciones.Add(nuevaOpcion);
                        }                        
                    });                   
                }
                pregunta.Descripcion = model.Descripcion;
                pregunta.Obligatoria = model.Obligatoria;
               await _repository.Update<EncuestaPregunta>(pregunta);
                return Ok(model.Id);
            }
            catch (MessageAlertException ex)
            {
                _logger.LogInformation(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return BadRequest();
            #endregion
        }
        #endregion

        #region MODIFICAR
        [HttpPost]
        public async Task<IActionResult> CambiarNombre(int id, string nombre, int carreraId, string descripcion)
        {
            #region EditarNombreDescripcion
            try
            {
                var encuesta = await _repository.GetById<Encuesta>(id);
                encuesta.Nombre = nombre;
                encuesta.CarreraId = carreraId;
                encuesta.Descripcion = descripcion;
                await _repository.Update<Encuesta>(encuesta);
                return Ok();
            }
            catch (MessageAlertException ex)
            {
                _logger.LogInformation(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return BadRequest();
            #endregion
        }

        [HttpPost]
        public async Task<IActionResult> CambiarSeccionNombre(int id, int encuestaId, string nombre)
        {
            #region EditarNombreDescripcion
            try
            {
                var seccion = await _encuestasRepository.GetEncuestaSeccionById(id, encuestaId);
                seccion.Nombre = nombre;
                await _repository.Update<EncuestaSeccion>(seccion);
                return Ok();
            }
            catch (MessageAlertException ex)
            {
                _logger.LogInformation(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return BadRequest();
            #endregion
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarPosicionSecciones(int encuestaId, List<int> seccionId)
        {
            #region ActualizarPosicionSecciones
            try
            {
                var encuesta = await _repository.FirstOrDefault<Encuesta>(x => x.Id == encuestaId, x => x.EncuestaSecciones);
                var secciones = encuesta.EncuestaSecciones.OrderBy(x => seccionId.IndexOf(x.Id)).ToList();
                int orden = 1;
                secciones.ForEach(x => { x.Orden = orden; orden++; });
                encuesta.EncuestaSecciones = secciones;
                await _repository.Update<Encuesta>(encuesta);
                return Ok();
            }
            catch (MessageAlertException ex)
            {
                _logger.LogInformation(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return BadRequest();
            #endregion
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarPosicionPreguntas(int seccionId, int encuestaId, List<int> preguntaId)
        {
            #region ActualizarPosicionPreguntas
            try
            {
                var seccion = await _repository.FirstOrDefault<EncuestaSeccion>(x => x.Id == seccionId && x.EncuestaId == encuestaId, x => x.EncuestaPreguntas);
                var preguntas = seccion.EncuestaPreguntas.OrderBy(x => preguntaId.IndexOf(x.Id)).ToList();
                int orden = 1;
                preguntas.ForEach(x => { x.Orden = orden; orden++; });
                seccion.EncuestaPreguntas = preguntas;
                await _repository.Update<EncuestaSeccion>(seccion);
                return Ok();
            }
            catch (MessageAlertException ex)
            {
                _logger.LogInformation(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return BadRequest();
            #endregion
        }

        #endregion

        #region ELIMINAR SECCION
        [HttpPut]
        public async Task<IActionResult> DeleteSeccion(int id, int encuestaId)
        {
            #region DeleteSeccion
            try
            {
                var encuestaSeccion = await _encuestasRepository.GetEncuestaSeccionById(id, encuestaId);
                encuestaSeccion.Eliminado = true;
                await _repository.Update<EncuestaSeccion>(encuestaSeccion);
            }
            catch (MessageAlertException ex)
            {
                _logger.LogInformation(ex.Message);
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
            return Ok();
            #endregion
        }
        #endregion

        #region ELIMINAR PREGUNTA

        [HttpPut]
        public async Task<IActionResult> DeletePregunta(int id, int encuestaId, int seccionId)
        {
            #region DeletePregunta
            try
            {
                var encuestaPregunta = await _encuestasRepository.GetPreguntaById(id, encuestaId, seccionId);
                encuestaPregunta.Eliminado = true;
                await _repository.Update<EncuestaPregunta>(encuestaPregunta);
            }
            catch (MessageAlertException ex)
            {
                _logger.LogInformation(ex.Message);
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
            return Ok();
            #endregion
        }

        #endregion

        #endregion

        #endregion


        #region VIEWBAGS
        public async Task Campus()
        {
            #region Campus

            ViewBag.Campus = new SelectList(await _repository.GetAll<Campus>(), "Id", "Nombre");

            #endregion
        }

        #endregion


    }
}