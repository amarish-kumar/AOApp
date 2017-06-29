using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAllAsQueryable();
        IEnumerable<T> GetAll();

        T Get(long id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Remove(T entity);

        void SaveChanges();
        void Dispose();
    }
}
