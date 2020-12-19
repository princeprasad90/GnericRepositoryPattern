using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryPatternServices.Interfaces
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Retrieve a single item by it's primary key or return null if not found
        /// </summary>
        /// <param name="primaryKey">Prmary key to find</param>
        /// <returns>T</returns>
        T SingleOrDefault(Expression<Func<T, bool>> whereCondition);

        /// <summary>
        /// Async single or default
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> whereCondition);

        /// <summary>
        /// Retrieve a single item by condition and using order by or return null if not found
        /// </summary>
        /// <param name="primaryKey">Prmary key to find</param>
        /// <returns>T</returns>
        T SingleOrDefaultOrderBy(Expression<Func<T, bool>> whereCondition, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);

        /// <summary>
        /// Find by primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T FindByIdInt(int? id);

        /// <summary>
        /// Find by primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T FindByIdLong(long? id);

        /// <summary>
        /// Returns all the rows for type T
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Return as Iqueryable
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAllAsIQueryable();

        /// <summary>
        /// Returns all the rows for type T on basis of filter condition
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll(Expression<Func<T, bool>> whereCondition);

        /// <summary>
        /// Inserts the data into the table
        /// </summary>
        /// <param name="entity">The entity to insert</param>
        /// <param name="userId">The user performing the insert</param>
        /// <returns></returns>
        T Insert(T entity);

        /// <summary>
        /// Async Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> InsertAsync(T entity);

        /// <summary>
        /// Insert a collection of records
        /// </summary>
        /// <param name="entityList"></param>
        void InsertList(IList<T> entityList);

        /// <summary>
        /// Updates this entity in the database using it's primary key
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="userId">The user performing the update</param>
        T Update(T entity);

        /// <summary>
        /// Updates all the passed entities in the database 
        /// </summary>
        /// <param name="entities">Entities to update</param>
        void UpdateAll(IList<T> entities);

        /// <summary>
        /// Deletes this entry fro the database
        /// ** WARNING - Most items should be marked inactive and Updated, not deleted
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="userId">The user Id who deleted the entity</param>
        /// <returns></returns>
        void Delete(Expression<Func<T, bool>> whereCondition);

        /// <summary>
        /// Does this item exist by it's primary key
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        bool Exists(Expression<Func<T, bool>> whereCondition);

        /// <summary>
        /// Filter data using condition,order by,select fields,page,pagesize
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IEnumerable<T> Filter(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int? page = null,
            int? pageSize = null);

        /// <summary>
        /// Get Table reference
        /// </summary>
        IQueryable<T> Table { get; }

        ///// <summary>
        ///// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method default no-tracking query.
        ///// </summary>
        ///// <param name="selector">The selector for projection.</param>
        ///// <param name="predicate">A function to test each element for a condition.</param>
        ///// <param name="orderBy">A function to order elements.</param>
        ///// <param name="include">A function to include navigation properties</param>
        ///// <returns>An <see cref="IEnumerable{T}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
        //IEnumerable<TResult> GetFilteredResult<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null,
        //  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        //  Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
    }
}
