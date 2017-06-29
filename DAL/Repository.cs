using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Domain;
using System.Linq.Expressions;
using System.Data.Entity;

namespace DAL
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbContext _context;
        private DbSet<T> _entities;
        string _errorMessage;

        public Repository(IWebDBContext context)
        {
            _context = context as DbContext;
            _entities = _context.Set<T>();

            // Load navigation properties explicitly (avoid serialization trouble)
            _context.Configuration.LazyLoadingEnabled = false;

            // Do NOT enable proxied entities, else serialization fails.
            _context.Configuration.ProxyCreationEnabled = false;

            // Because Web API will perform validation, we don't need/want EF to do so
            _context.Configuration.ValidateOnSaveEnabled = false;
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public T Get(long id)
        {
            return _entities.SingleOrDefault(s => s.id == id);
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity null");
            }
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity null");
            }
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity null");
            }
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity null");
            }
            _entities.Remove(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IQueryable<T> GetAllAsQueryable()
        {
            return _entities.AsQueryable();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
