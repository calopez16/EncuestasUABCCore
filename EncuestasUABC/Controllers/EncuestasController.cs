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

namespace EncuestasUABC.Controllers
{
    public class EncuestasController : BaseController
    {
        private readonly ILogger<EncuestasController> _logger;
        private readonly IEncuestasRepository _encuestasRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRepository _repository;

        public EncuestasController(ILogger<EncuestasController> logger,
            IEncuestasRepository encuestasRepository,
            IUsuarioRepository usuarioRepository,
            IRepository repository)
        {
            _encuestasRepository = encuestasRepository;
            _usuarioRepository = usuarioRepository;
            _logger = logger;
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Creadas(int pagina)
        {
            #region Creadas
            try
            {
                var paginacion = await PaginacionEncuestas(pagina);
                return View(paginacion);
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

            return RedirectToAction("Index", "Home");
            #endregion
        }

        public async Task<IActionResult> Editar(int id)
        {
            #region Edit
            try
            {
                var encuesta = await _encuestasRepository.Get(id);
                return View(encuesta);
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

        #region Ajax
        [HttpPost]
        public async Task<IActionResult> EditarNombreDescripcion(Encuesta model, bool encuestaEstatus = false)
        {
            #region EditarNombreDescripcion
            try
            {
                if (encuestaEstatus)
                    model.EstatusEncuestaId = (int)Enumerador.EstatusEncuesta.ACTIVA;
                else
                    model.EstatusEncuestaId = (int)Enumerador.EstatusEncuesta.INACTIVA;

                var encuesta = await _encuestasRepository.Get(model.Id);
                encuesta.Nombre = model.Nombre;
                encuesta.Descripcion = model.Descripcion;
                encuesta.EstatusEncuestaId = model.EstatusEncuestaId;
                await _encuestasRepository.Update(encuesta);
                ShowMessageSuccess(Constantes.Mensajes.ENCUESTAS_MSJ03);
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
            return RedirectToAction(nameof(Editar), new { id = model.Id });
            #endregion
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> Crear(Encuesta model)
        {
            #region Crear
            try
            {
                var user = await _usuarioRepository.Get(User.Identity.Name);
                model.UsuarioId = user.Id;
                model.Fecha = DateTime.Now;
                model.EstatusEncuestaId = (int)Enumerador.EstatusEncuesta.INACTIVA;
                var result = await _encuestasRepository.Add(model);
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

        [HttpPost]
        public async Task<IActionResult> CrearSeccion(EncuestaSeccion model)
        {
            #region Crear
            try
            {
                var user = await _usuarioRepository.Get(User.Identity.Name);
                var encuesta = await _encuestasRepository.Get(model.EncuestaId);
                int orden = 1;
                if (encuesta.EncuestaSecciones.Any())
                    orden = encuesta.EncuestaSecciones.OrderBy(x => x.Orden).Last().Orden + 1;
                model.Orden = orden;
                encuesta.EncuestaSecciones.Add(model);
                await _encuestasRepository.Update(encuesta);
                ShowMessageSuccess(Constantes.Mensajes.ENCUESTAS_MSJ05);
                return RedirectToAction(nameof(Editar), new { id = model.EncuestaId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ShowMessageException(ex.Message);
            }
            return RedirectToAction(nameof(Creadas));

            #endregion
        }

        private async Task<PaginacionViewModel<List<Encuesta>>> PaginacionEncuestas(int numPagina)
        {
            #region PaginacionEncuestas

            PaginacionViewModel<List<Encuesta>> paginacion;
            int numeroPorPagina = 10;
            var user = await _usuarioRepository.Get(User.Identity.Name);
            var encuestas = (await _encuestasRepository.GetByUser(user.Id)).OrderByDescending(x => x.Fecha).ToList();

            decimal paginas = encuestas.Count() / numeroPorPagina;
            int totalPaginas = (int)Math.Ceiling(paginas);
            int totalRegistros = encuestas.Count();

            if (numPagina > totalPaginas || numPagina == 0)
            {
                numPagina = 1;
                encuestas = encuestas
                       .Skip(0)
                       .Take(numeroPorPagina)
                       .ToList();
            }
            else
            {
                int inicio = (numPagina * numeroPorPagina) - numeroPorPagina;
                encuestas = encuestas
                                       .Skip(inicio)
                                       .Take(numeroPorPagina)
                                       .ToList();

            }
            paginacion = new PaginacionViewModel<List<Encuesta>>
            {
                Result = encuestas.ToList(),
                NumeroPagina = numPagina,
                TotalRegistros = totalRegistros,
                TotalPaginas = totalPaginas

            };

            return paginacion;
            #endregion
        }

        public async Task Campus()
        {
            #region Campus

            ViewBag.Campus = new SelectList(_repository.GetAll<Campus>(), "Id", "Nombre");

            #endregion
        }
    }
}