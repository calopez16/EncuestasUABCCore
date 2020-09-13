using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EncuestasUABC.AccesoDatos.Repository.Interfaces
{
    public interface IRepository : IDisposable
    {
        void Add<T>(T entity) where T : class;
        IEnumerable<T> FindBy<T>(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
            params Expression<Func<T, object>>[] includes
            ) where T : class;
        T FirstOrDefault<T>(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes) where T : class;
        IEnumerable<T> GetAll<T>() where T : class;
        T GetById<T>(int id) where T : class;
        void Remove<T>(T entity) where T : class;
        void RemoveById<T>(int id) where T : class;

    }
}
