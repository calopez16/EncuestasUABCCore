using EncuestasUABC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EncuestasUABC.AccesoDatos.Repository.Interfaces
{
    public interface IEncuestasRepository : IDisposable
    {
        Task<IEnumerable<Encuesta>> GetAll();
        Task<IEnumerable<Encuesta>> GetByUser(string userId);
        //Task<Encuesta> Add(Encuesta encuesta);
        //Task<Encuesta> Get(int id);
        //Task<int> Update(Encuesta encuesta);
        //Task<IEnumerable<Encuesta>> FindBy(Expression<Func<Encuesta, bool>> filter = null, Func<IQueryable<Encuesta>, IOrderedQueryable<Encuesta>> orderby = null, params Expression<Func<Encuesta, object>>[] includes);
    }
}
