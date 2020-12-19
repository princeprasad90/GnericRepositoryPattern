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
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException("EmployeeContext");
            table = _unitOfWork.Db.Set<T>();
        }

        public IQueryable<T> Table => table;


        public T SingleOrDefault(Expression<Func<T, bool>> whereCondition)
        {
            var dbResult = table.Where(whereCondition).FirstOrDefault();
            return dbResult;
        }
        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> whereCondition)
        {
            var dbResult = await table.Where(whereCondition).FirstOrDefaultAsync();
            return dbResult;
        }

        public T SingleOrDefaultOrderBy(Expression<Func<T, bool>> whereCondition, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            IQueryable<T> query = table.Where(whereCondition);
            return orderBy(query).FirstOrDefault();
        }

        public T FindByIdInt(int? id)
        {
            return table.Find(id);
        }

        public T FindByIdLong(long? id)
        {
            return table.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public IQueryable<T> GetAllAsIQueryable()
        {
            return table;
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> whereCondition)
        {
            return table.Where(whereCondition).ToList();
        }

        public virtual T Insert(T entity)
        {
            table.Add(entity);
            _unitOfWork.Db.SaveChanges();
            return entity;
        }
        public virtual async Task<T> InsertAsync(T entity)
        {
            table.Add(entity);
            await _unitOfWork.Db.SaveChangesAsync();
            return entity;
        }
        public virtual void InsertList(IList<T> entityList)
        {
            table.AddRange(entityList);
            _unitOfWork.Db.SaveChanges();
        }

        public virtual T Update(T entity)
        {
            table.Attach(entity);
            _unitOfWork.Db.Entry(entity).State = EntityState.Modified;
            _unitOfWork.Db.SaveChanges();
            return entity;
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

        public bool Exists(Expression<Func<T, bool>> whereCondition)
        {
            return table.Any(whereCondition);
        }

        public int Count(Expression<Func<T, bool>> whereCondition)
        {
            return table.Where(whereCondition).Count();
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int? page = null,
           int? pageSize = null)
        {
            IQueryable<T> query = table;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (includeProperties != null)
            {
                foreach (
                    var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return query.ToList();
        }


        //    public IEnumerable<TResult> GetFilteredResult<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        //    {
        //        IQueryable<T> query = table;

        //        if (include != null)
        //        {
        //            query = include(query);
        //        }

        //        if (predicate != null)
        //        {
        //            query = query.Where(predicate);
        //        }

        //        if (orderBy != null)
        //        {
        //            return orderBy(query).Select(selector).AsEnumerable();
        //        }
        //        else
        //        {
        //            return query.Select(selector);
        //        }
        //    }
        //}
    }
}
