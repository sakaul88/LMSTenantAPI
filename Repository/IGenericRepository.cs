using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DeviceManager.Api.Repository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Gets all and offers to include a related table
        /// </summary>
        /// <param name="include">Te sub.entity to include</param>
        /// <returns>List of entities</returns>
        Task<IEnumerable<T>> GetAll(string include);
        Task<IEnumerable<T>> GetAll(string include = null, string include2 = null, string include3 = null, string include4 = null, string include5 = null, string include6 = null, string include7 = null, string include8 = null, string include9 = null, string include10 = null, string include11 = null, string include12 = null, string include13 = null);
        Task<IEnumerable<T>> GetAllWithInclude(Expression<Func<T, bool>> predicate, string include = null, string include2 = null, string include3 = null, string include4 = null, string include5 = null, string include6 = null, string include7 = null, string include8 = null, string include9 = null, string include10 = null, string include11 = null, string include12 = null, string include13 = null);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// Gets the list of devices.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAll(int page, int pageSize);

        Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets the device by identifier.
        /// </summary>
        /// <param name="id">The device identifier.</param>
        Task<T> GetById(int id);

        /// <summary>
        /// Creates the device.
        /// </summary>
        /// <param name="entity">The device view model.</param>
        T Create(T entity);

        /// <summary>
        /// Updates the device.
        /// </summary>
        /// <param name="id">The device identifier.</param>
        /// <param name="entity">The device view model.</param>
        void Update(int id, T entity);

        /// <summary>
        /// Updates the device.
        /// </summary>
        /// <param name="entity">The device view model.</param>
        void Delete(T entity);

        /// <summary>
        /// Update the entity in bulk
        /// </summary>
        /// <param name="entities"></param>
        void BulkUpdate(IEnumerable<T> entities);
    }
}
