using DeviceManager.Api.Data.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DeviceManager.Api.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private IUnitOfWork UnitOfWork;
        public GenericRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public Task<IEnumerable<T>> GetAll()
        {
            return Task.Run(() => UnitOfWork.GetRepository<T>().GetAll().AsEnumerable());
        }

        public Task<IEnumerable<T>> GetAll(string include)
        {
            return Task.Run(() => UnitOfWork.GetRepository<T>().GetAll(include).AsEnumerable());
        }
        public Task<IEnumerable<T>> GetAll(int page, int pageSize)
        {
            return Task.Run(() => UnitOfWork.GetRepository<T>().GetAll(page, pageSize).AsEnumerable());
        }

        public Task<T> GetById(int id)
        {
            //var s = UnitOfWork.GetRepository<T>().FindBy(x => x.Equals("sandeep"));
            //object a = "sandeep";
            return Task.Run(() => UnitOfWork.GetRepository<T>().Get(id));
        }

        public T Create(T entity)
        {
            UnitOfWork.GetRepository<T>().Add(entity);

            // Commit changes
            UnitOfWork.Commit();
            return entity;
        }

        public void Update(int id, T entity)
        {
            //var repo = UnitOfWork.GetRepository<T>();
            //var existingModel = repo.Get(id);

            //if (existingModel == null)
            //{
            //    throw new NullReferenceException();
            //}

            UnitOfWork.GetRepository<T>().Update(entity);

            // Commit changes
            UnitOfWork.Commit();

        }

        public void Delete(T entity)
        {
            UnitOfWork.GetRepository<T>().Delete(entity);

            // Commit changes
            UnitOfWork.Commit();
        }

        public Task<IEnumerable<T>> GetAll(string include = null, string include2 = null, string include3 = null, string include4 = null, string include5 = null, string include6 = null, string include7 = null, string include8 = null, string include9 = null, string include10 = null, string include11 = null, string include12 = null, string include13 = null)
        {
            return Task.Run(() => UnitOfWork.GetRepository<T>().GetAll(include, include2, include3, include4, include5, include6, include7, include8, include9, include10, include11, include12, include13).AsEnumerable());
        }

        public Task<IEnumerable<T>> GetAllWithInclude(Expression<Func<T, bool>> predicate, string include = null, string include2 = null, string include3 = null, string include4 = null, string include5 = null, string include6 = null, string include7 = null, string include8 = null, string include9 = null, string include10 = null, string include11 = null, string include12 = null, string include13 = null)
        {
            return Task.Run(() => UnitOfWork.GetRepository<T>().GetAllWithInclude(predicate, include, include2, include3, include4, include5, include6, include7, include8, include9, include10, include11, include12, include13).AsEnumerable());
        }
        public Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate)
        {
            return Task.Run(() => UnitOfWork.GetRepository<T>().FindBy(predicate).AsEnumerable());
        }


        /// <summary>
        /// Bulk updates of entities
        /// </summary>
        /// <param name="entities"></param>
        public void BulkUpdate(IEnumerable<T> entities)
        {
           Task.Run(()=> UnitOfWork.GetRepository<T>().BulkUpdate(entities));

            // Commit changes
            UnitOfWork.Commit();
        }
    }
}