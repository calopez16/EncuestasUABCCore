using EncuestasUABC.AccesoDatos.Data;
using EncuestasUABC.AccesoDatos.Repository.Interfaces;
using EncuestasUABC.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace EncuestasUABC.AccesoDatos.Repository
{
    public class EncuestasRepository : IEncuestasRepository
    {
        protected readonly ApplicationDbContext _context;

        public EncuestasRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

        public async Task<EncuestaSeccion> GetEncuestaSeccionById(int id, int idEncuesta)
        {
            #region GetAll
            return await _context.EncuestaSecciones.FindAsync(id, idEncuesta);
            #endregion
        }

        public async Task<Encuesta> GetById(int id)
        {
            #region GetById

            return await _context.Encuestas
                .Include(x => x.EncuestaSecciones)
                .Include(x => x.CarreraIdNavigation)
                .ThenInclude(x => x.UnidadAcademicaIdNavigation)
                .ThenInclude(x => x.CampusIdNavigation)
                .Select(x => new Encuesta
                {
                    Id = x.Id,
                    Fecha = x.Fecha,
                    UsuarioId = x.UsuarioId,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    EstatusEncuestaId = x.EstatusEncuestaId,
                    CarreraId = x.CarreraId,
                    EncuestaSecciones = x.EncuestaSecciones.Where(x => !x.Eliminado).ToList(),
                    CarreraIdNavigation = new Carrera
                    {
                        Nombre = x.CarreraIdNavigation.Nombre,
                        UnidadAcademicaIdNavigation = new UnidadAcademica
                        {
                            Nombre = x.CarreraIdNavigation.UnidadAcademicaIdNavigation.Nombre,
                            CampusIdNavigation = new Campus
                            {
                                Nombre = x.CarreraIdNavigation.UnidadAcademicaIdNavigation.CampusIdNavigation.Nombre,
                            }
                        }
                    }
                }).FirstOrDefaultAsync(x => x.Id == id);

            #endregion
        }

        public async Task<EncuestaSeccion> GetSeccionById(int id, int encuestaId)
        {
            #region GetSeccionById

            return await _context.EncuestaSecciones
                .Include(x => x.EncuestaPreguntas).ThenInclude(x => x.TipoPreguntaIdNavigation)
                .Include(x => x.EncuestaIdNavigation)
                .Select(x => new EncuestaSeccion
                {
                    Id = x.Id,
                    EncuestaId = x.EncuestaId,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    Orden = x.Orden,
                    EncuestaPreguntas = x.EncuestaPreguntas.Where(x => !x.Eliminado).OrderBy(x => x.Orden).Select(y => new EncuestaPregunta
                    {
                        Id = y.Id,
                        Orden = y.Orden,
                        Descripcion = y.Descripcion,
                        TipoPreguntaId = y.TipoPreguntaId,
                        TipoPreguntaIdNavigation = new TipoPregunta
                        {
                            Descripcion = y.TipoPreguntaIdNavigation.Descripcion
                        },
                        Opciones = y.Opciones.Where(x => !x.Eliminado).OrderBy(x => x.Orden).ToList(),
                        Obligatoria = y.Obligatoria
                    }).ToList(),
                    EncuestaIdNavigation = new Encuesta
                    {
                        Nombre = x.EncuestaIdNavigation.Nombre
                    }
                }).FirstOrDefaultAsync(x => x.Id == id && x.EncuestaId == encuestaId);

            #endregion
        }

        public async Task<EncuestaSeccion> GetPrimeraSeccionById(int encuestaId)
        {
            #region GetPrimeraSeccionById

            return await _context.EncuestaSecciones
                .Include(x => x.EncuestaPreguntas)
                .ThenInclude(x => x.TipoPreguntaIdNavigation)
                .Include(x => x.EncuestaIdNavigation)
                .Select(x => new EncuestaSeccion
                {
                    Id = x.Id,
                    EncuestaId = x.EncuestaId,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    Orden = x.Orden,
                    EncuestaPreguntas = x.EncuestaPreguntas.Where(x => !x.Eliminado).OrderBy(x => x.Orden).Select(y => new EncuestaPregunta
                    {
                        Id = y.Id,
                        Orden = y.Orden,
                        Descripcion = y.Descripcion,
                        TipoPreguntaId = y.TipoPreguntaId,
                        TipoPreguntaIdNavigation = new TipoPregunta
                        {
                            Descripcion = y.TipoPreguntaIdNavigation.Descripcion
                        },
                        Opciones = y.Opciones.Where(x => !x.Eliminado).OrderBy(x => x.Orden).ToList(),
                        Obligatoria = y.Obligatoria
                    }).ToList(),
                    EncuestaIdNavigation = new Encuesta
                    {
                        Nombre = x.EncuestaIdNavigation.Nombre
                    }
                })
                .OrderBy(x => x.Orden)
                .FirstOrDefaultAsync(x => x.EncuestaId == encuestaId);

            #endregion
        }

        public async Task<List<EncuestaSeccion>> GetSecciones(int encuestaId)
        {
            #region GetSecciones
            return await _context.EncuestaSecciones
              .Where(x => x.EncuestaId == encuestaId && !x.Eliminado)
              .Select(x => new EncuestaSeccion
              {
                  Id = x.Id,
                  EncuestaId = x.EncuestaId,
                  Orden = x.Orden
              })
              .OrderBy(x => x.Orden)
              .ToListAsync();
            #endregion
        }
        public async Task<EncuestaPregunta> GetPreguntaById(int id, int encuestaId, int seccionId)
        {
            #region GetPreguntaById
            return await _context.EncuestaPreguntas.FindAsync(id, encuestaId, seccionId);
            #endregion
        }


        public async Task<EncuestaPregunta> GetPreguntaByIdDetalle(int id, int encuestaId, int seccionId)
        {
            #region GetPreguntaByIdDetalle
            return await _context.EncuestaPreguntas
                .Include(x => x.TipoPreguntaIdNavigation)
                .Include(x => x.EncuestaPreguntaIdPadreNavigation)
                .Include(x => x.Opciones)
                .Where(x => !x.Eliminado)
                .OrderBy(x => x.Orden)
                .Select(x => new EncuestaPregunta
                {
                    Id = x.Id,
                    EncuestaId = x.EncuestaId,
                    EncuestaSeccionId = x.EncuestaSeccionId,
                    Descripcion = x.Descripcion,
                    Obligatoria = x.Obligatoria,
                    Eliminado = x.Eliminado,
                    TipoPreguntaId = x.TipoPreguntaId,
                    TipoPreguntaIdNavigation = new TipoPregunta
                    {
                        Descripcion = x.TipoPreguntaIdNavigation.Descripcion
                    },
                    Opciones = x.Opciones
                    .OrderBy(o => o.Orden)
                    .Where(w => !w.Eliminado)
                    .Select(w => new EncuestaPreguntaOpcion
                    {
                        Id = w.Id,
                        Descripcion = w.Descripcion
                    }).ToList()
                }).FirstOrDefaultAsync(x => x.Id == id && x.EncuestaId == encuestaId && x.EncuestaSeccionId == seccionId);
            #endregion
        }

    }
}
