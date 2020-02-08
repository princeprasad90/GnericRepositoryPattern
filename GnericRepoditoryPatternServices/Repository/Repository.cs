using GenericRepositoryPatternData.Model;
using GenericRepositoryPatternServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryPatternServices.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public SampleEntities _context = null;
        private readonly DbSet<T> table = null;
        public Repository()
        {
            _context = new SampleEntities();
            table = _context.Set<T>();
        }
        public void Add(T entity)
        {
            table.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
