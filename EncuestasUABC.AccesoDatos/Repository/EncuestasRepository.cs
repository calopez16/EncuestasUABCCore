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

        public async Task<IEnumerable<Encuesta>> GetAll()
        {
            #region GetAll
            return await GetEncuestas();
            #endregion
        }

        public async Task<Encuesta> Get(int id)
        {
            #region GetAll
            return (await GetEncuestas())
                .FirstOrDefault(x => x.Id == id);
            #endregion
        }

        public async Task<Encuesta> Add(Encuesta encuesta)
        {
            #region Add
            await _context.Encuestas.AddAsync(encuesta);
            await _context.SaveChangesAsync();
            return encuesta;
            #endregion
        }


        public async Task<IEnumerable<Encuesta>> FindBy(Expression<Func<Encuesta, bool>> filter = null, Func<IQueryable<Encuesta>, IOrderedQueryable<Encuesta>> orderby = null, params Expression<Func<Encuesta, object>>[] includes)
        {
            #region FindBy
            IQueryable<Encuesta> query = _context.Set<Encuesta>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }

            if (orderby != null)
            {
                return orderby(query).ToList();
            }
            return query.ToList();
            #endregion
        }

        public async Task<IEnumerable<Encuesta>> GetByUser(string userId)
        {
            #region GetByUser
            return (await GetEncuestas())
                .Where(x => x.UsuarioId.Equals(userId))
                .ToList();
            #endregion
        }
        public async Task<int> Update(Encuesta encuesta)
        {
            #region Update
            _context.Entry(encuesta).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
            #endregion
        }
        private async Task<IEnumerable<Encuesta>> GetEncuestas()
        {
            #region 
            return await _context.Encuestas
                .Include(x => x.Usuario)
                .Include(x => x.EstatusEncuesta)
                .Include(x => x.Carrera)
                .ThenInclude(x => x.UnidadAcademica)
                .ThenInclude(x => x.Campus)
                .Include(x => x.EncuestaSecciones)
                .Where(x => x.EstatusEncuestaId != (int)Enumerador.EstatusEncuesta.ELIMINADA)
                .ToListAsync();
            #endregion
        }
    }
}
