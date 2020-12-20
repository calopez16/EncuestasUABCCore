using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EncuestasUABC.AccesoDatos.Repository.Interfaces;
using EncuestasUABC.Models;
using EncuestasUABC.Models.SelectViewModel;
using EncuestasUABC.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EncuestasUABC.Controllers
{
    public class CarreraController : Controller
    {
        private readonly ILogger<CarreraController> _logger;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public CarreraController(ILogger<CarreraController> logger,
            IRepository repository,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;

        }
        [HttpGet]
        public async Task<IActionResult> Select(SelectViewModel selectViewModel)
        {
            #region Select
            try
            {
                var carreras = await _repository
                    .FindBy<Carrera>(null, x => x.OrderBy(x => x.Nombre), x => x.UnidadAcademicaIdNavigation, x => x.UnidadAcademicaIdNavigation.CampusIdNavigation);
                int totalRegistros = carreras.Count();
                if (!string.IsNullOrEmpty(selectViewModel.search))
                {
                    selectViewModel.search = selectViewModel.search.ToLower();
                    carreras = carreras.Where(x =>
                                              x.Nombre.ToLower().Contains(selectViewModel.search)
                                              || x.UnidadAcademicaIdNavigation.Nombre.ToLower().Contains(selectViewModel.search)
                                              || x.UnidadAcademicaIdNavigation.CampusIdNavigation.Nombre.ToLower().Contains(selectViewModel.search)).ToList();
                    totalRegistros = carreras.Count();
                }
                var skip = (selectViewModel.page * selectViewModel.perPage) - selectViewModel.perPage;
                var carrerasResult = carreras.Skip(skip).Take(selectViewModel.perPage).ToList();

                if (selectViewModel.id != 0)
                {
                    var carrera = await _repository.GetById<Carrera>(selectViewModel.id);
                    carrerasResult.Add(carrera);
                }

                return Ok(new
                {
                    results = carrerasResult.Select(x => new
                    {
                        x.Id,
                        text = x.Nombre,
                        carrera=x.Nombre,
                        unidadacademica = x.UnidadAcademicaIdNavigation.Nombre,
                        campus = x.UnidadAcademicaIdNavigation.CampusIdNavigation.Nombre
                    }),
                    totalRegistros
                });
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

        [HttpGet]
        public async Task<IActionResult> SelectById(int id)
        {
            #region Select
            try
            {
                var carrera = (await _repository.FindBy<Carrera>(null, null, x => x.UnidadAcademicaIdNavigation, x => x.UnidadAcademicaIdNavigation.CampusIdNavigation)).Select(x => new
                {
                    x.Id,
                    Carrera = x.Nombre,
                    UnidadAcademica = x.UnidadAcademicaIdNavigation.Nombre,
                    Campus = x.UnidadAcademicaIdNavigation.CampusIdNavigation.Nombre
                }).FirstOrDefault(x=>x.Id==id);
                return Ok(carrera);
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
    }
}
