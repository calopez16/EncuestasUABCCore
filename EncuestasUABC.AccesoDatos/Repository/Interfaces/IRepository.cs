using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EncuestasUABC.AccesoDatos.Repository.Interfaces
{
    public interface IRepository : IDisposable
    {
        Task<T> Add<T>(T entity) where T : class;
        Task<IEnumerable<T>> FindBy<T>(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
            params Expression<Func<T, object>>[] includes
            ) where T : class;
        Task<T> FirstOrDefault<T>(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes) where T : class;
        Task<IEnumerable<T>> GetAll<T>() where T : class;
        Task<T> GetById<T>(int id) where T : class;
        Task Remove<T>(T entity) where T : class;
        Task RemoveById<T>(int id) where T : class;

    }
}
