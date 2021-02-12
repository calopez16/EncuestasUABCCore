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
                .Include(x => x.IdCarreraNavigation)
                .ThenInclude(x => x.IdUnidadAcademicaNavigation)
                .ThenInclude(x => x.IdCampusNavigation)
                .Select(x => new Encuesta
                {
                    Id = x.Id,
                    Fecha = x.Fecha,
                    IdUsuarioRegistro = x.IdUsuarioRegistro,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    IdEstatusEncuesta = x.IdEstatusEncuesta,
                    IdCarrera = x.IdCarrera,
                    EncuestaSecciones = x.EncuestaSecciones.Where(x => !(bool)x.Eliminado).ToList(),
                    IdCarreraNavigation = new Carrera
                    {
                        Nombre = x.IdCarreraNavigation.Nombre,
                        IdUnidadAcademicaNavigation = new UnidadAcademica
                        {
                            Nombre = x.IdCarreraNavigation.IdUnidadAcademicaNavigation.Nombre,
                            IdCampusNavigation = new Campus
                            {
                                Nombre = x.IdCarreraNavigation.IdUnidadAcademicaNavigation.IdCampusNavigation.Nombre,
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
                .Include(x => x.EncuestaPreguntas).ThenInclude(x => x.IdTipoPreguntaNavigation)
                .Include(x => x.IdEncuestaNavigation)
                .Select(x => new EncuestaSeccion
                {
                    Id = x.Id,
                    IdEncuesta = x.IdEncuesta,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    Orden = x.Orden,
                    EncuestaPreguntas = x.EncuestaPreguntas.Where(x => !x.Eliminado).OrderBy(x => x.Orden).Select(y => new EncuestaPregunta
                    {
                        Id = y.Id,
                        Orden = y.Orden,
                        Descripcion = y.Descripcion,
                        IdTipoPregunta = y.IdTipoPregunta,
                        IdTipoPreguntaNavigation = new TipoPregunta
                        {
                            Descripcion = y.IdTipoPreguntaNavigation.Descripcion
                        },
                        Opciones = y.Opciones.Where(x => !x.Eliminado).OrderBy(x => x.Orden).ToList(),
                        Obligatoria = y.Obligatoria
                    }).ToList(),
                    IdEncuestaNavigation = new Encuesta
                    {
                        Nombre = x.IdEncuestaNavigation.Nombre
                    }
                }).FirstOrDefaultAsync(x => x.Id == id && x.IdEncuesta == encuestaId);

            #endregion
        }

        public async Task<EncuestaSeccion> GetPrimeraSeccionById(int encuestaId)
        {
            #region GetPrimeraSeccionById

            return await _context.EncuestaSecciones
                .Include(x => x.EncuestaPreguntas)
                .ThenInclude(x => x.IdTipoPreguntaNavigation)
                .Include(x => x.IdEncuestaNavigation)
                .Select(x => new EncuestaSeccion
                {
                    Id = x.Id,
                    IdEncuesta = x.IdEncuesta,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    Orden = x.Orden,
                    EncuestaPreguntas = x.EncuestaPreguntas.Where(x => !x.Eliminado).OrderBy(x => x.Orden).Select(y => new EncuestaPregunta
                    {
                        Id = y.Id,
                        Orden = y.Orden,
                        Descripcion = y.Descripcion,
                        IdTipoPregunta = y.IdTipoPregunta,
                        IdTipoPreguntaNavigation = new TipoPregunta
                        {
                            Descripcion = y.IdTipoPreguntaNavigation.Descripcion
                        },
                        Opciones = y.Opciones.Where(x => !x.Eliminado).OrderBy(x => x.Orden).ToList(),
                        Obligatoria = y.Obligatoria
                    }).ToList(),
                    IdEncuestaNavigation = new Encuesta
                    {
                        Nombre = x.IdEncuestaNavigation.Nombre
                    }
                })
                .OrderBy(x => x.Orden)
                .FirstOrDefaultAsync(x => x.IdEncuesta == encuestaId);

            #endregion
        }

        public async Task<List<EncuestaSeccion>> GetSecciones(int encuestaId)
        {
            #region GetSecciones
            return await _context.EncuestaSecciones
              .Where(x => x.IdEncuesta == encuestaId && !(bool)x.Eliminado)
              .Select(x => new EncuestaSeccion
              {
                  Id = x.Id,
                  IdEncuesta = x.IdEncuesta,
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
                .Include(x => x.IdTipoPreguntaNavigation)
                .Include(x => x.IdEncuestaPreguntaPadreNavigation)
                .Include(x => x.Opciones)
                .Where(x => !x.Eliminado)
                .OrderBy(x => x.Orden)
                .Select(x => new EncuestaPregunta
                {
                    Id = x.Id,
                    IdEncuesta = x.IdEncuesta,
                    IdEncuestaSeccion = x.IdEncuestaSeccion,
                    Descripcion = x.Descripcion,
                    Obligatoria = x.Obligatoria,
                    Eliminado = x.Eliminado,
                    IdTipoPregunta = x.IdTipoPregunta,
                    IdTipoPreguntaNavigation = new TipoPregunta
                    {
                        Descripcion = x.IdTipoPreguntaNavigation.Descripcion
                    },
                    Opciones = x.Opciones
                    .OrderBy(o => o.Orden)
                    .Where(w => !w.Eliminado)
                    .Select(w => new EncuestaPreguntaOpcion
                    {
                        Id = w.Id,
                        Descripcion = w.Descripcion
                    }).ToList()
                }).FirstOrDefaultAsync(x => x.Id == id && x.IdEncuesta == encuestaId && x.IdEncuestaSeccion == seccionId);
            #endregion
        }

    }
}
