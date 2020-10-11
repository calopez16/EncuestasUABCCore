using EncuestasUABC.AccesoDatos.Data;
using EncuestasUABC.AccesoDatos.Repository.Interfaces;
using EncuestasUABC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
                .Select(x => new Encuesta
                {
                    Id = x.Id,
                    Fecha = x.Fecha,
                    UsuarioId = x.UsuarioId,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    EstatusEncuestaId = x.EstatusEncuestaId,
                    CarreraId = x.CarreraId,
                    EncuestaSecciones = x.EncuestaSecciones.Where(x => !x.Eliminado).ToList()
                }).FirstOrDefaultAsync(x => x.Id == id);

            #endregion
        }

        public async Task<EncuestaSeccion> GetSeccionById(int id,int encuestaId)
        {
            #region GetSeccionById

            return await _context.EncuestaSecciones
                .Include(x => x.EncuestaPreguntas)
                .Select(x => new EncuestaSeccion
                {
                    Id = x.Id,
                    EncuestaId=x.EncuestaId,
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    Orden=x.Orden,
                    EncuestaPreguntas = x.EncuestaPreguntas.Where(x => !x.Eliminado).ToList()
                }).FirstOrDefaultAsync(x => x.Id == id && x.EncuestaId==encuestaId);

            #endregion
        }

    }
}
