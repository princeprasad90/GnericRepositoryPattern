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
        private readonly IUnitOfWork _unitOfWork;
        protected readonly DbSet<T> table = null;
        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException("UnitOfWork");
            table = _unitOfWork.Db.Set<T>();
        }

        public T SingleOrDefault(Expression<Func<T, bool>> whereCondition)
        {
            var dbResult = table.Where(whereCondition).FirstOrDefault();
            return dbResult;
        }
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> whereCondition)
        {
            return table.Where(whereCondition).ToList();
        }

        public virtual T Insert(T entity)
        {
            dynamic obj = table.Add(entity);
            _unitOfWork.Db.SaveChanges();
            return obj;
        }
        public virtual void Update(T entity)
        {
            table.Attach(entity);
            _unitOfWork.Db.Entry(entity).State = EntityState.Modified;
            _unitOfWork.Db.SaveChanges();
        }

        public virtual void UpdateAll(IList<T> entities)
        {
            foreach (var entity in entities)
            {
                table.Attach(entity);
                _unitOfWork.Db.Entry(entity).State = EntityState.Modified;
            }
            _unitOfWork.Db.SaveChanges();
        }

        public void Delete(Expression<Func<T, bool>> whereCondition)
        {
            IEnumerable<T> entities = this.GetAll(whereCondition);
            foreach (T entity in entities)
            {
                if (_unitOfWork.Db.Entry(entity).State == EntityState.Detached)
                {
                    table.Attach(entity);
                }
                table.Remove(entity);
            }
            _unitOfWork.Db.SaveChanges();
        }
        public T SingleOrDefaultOrderBy(Expression<Func<T, bool>> whereCondition, Expression<Func<T, int>> orderBy, string direction)
        {
            if (direction == "ASC")
            {
                return table.Where(whereCondition).OrderBy(orderBy).FirstOrDefault();

            }
            else
            {
                return table.Where(whereCondition).OrderByDescending(orderBy).FirstOrDefault();
            }
        }

        public bool Exists(Expression<Func<T, bool>> whereCondition)
        {
            return table.Any(whereCondition);
        }

        public int Count(Expression<Func<T, bool>> whereCondition)
        {
            return table.Where(whereCondition).Count();
        }

        public IEnumerable<T> GetPagedRecords(Expression<Func<T, bool>> whereCondition, Expression<Func<T, string>> orderBy, int pageNo, int pageSize)
        {
            return (table.Where(whereCondition).OrderBy(orderBy).Skip((pageNo - 1) * pageSize).Take(pageSize)).AsEnumerable();
        }

        public IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            return table.SqlQuery(query, parameters);
        }

    }
}
