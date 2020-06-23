using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;

namespace ASPMVCAuthentication.Data.Repositories
{
    public interface IRepository<T> where T : class, new()
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(long? id);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task DeleteById(long? id);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
        void Dispose();
    }
}