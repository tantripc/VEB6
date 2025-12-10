using MiddlewareTool.Business.Interface;
using MiddlewareTool.Business.Concrete;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MiddlewareTool.Business.Concrete
{
    public class SharedBusiness : BaseBusiness, ISharedBusiness
    {
        #region Constructors
        public SharedBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #endregion

        #region Methods

        #region GetItem
        public TDTO GetItem<TDTO, TEntity>() where TEntity : class
        {
            return this.UnitOfWork.GetItem<TDTO, TEntity>();
        }
        public TDTO GetItem<TDTO, TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            return this.UnitOfWork.GetItem<TDTO, TEntity>(filter);
        }
        public TDTO GetItem<TDTO, TEntity>(Expression<Func<TEntity, bool>> filter, List<Expression<Func<TEntity, object>>> includes) where TEntity : class
        {
            return this.UnitOfWork.GetItem<TDTO, TEntity>(filter, includes);
        }
        public async Task<TDTO> GetItemAsync<TDTO, TEntity>() where TEntity : class
        {
            return await this.UnitOfWork.GetItemAsync<TDTO, TEntity>();
        }
        public async Task<TDTO> GetItemAsync<TDTO, TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            return await this.UnitOfWork.GetItemAsync<TDTO, TEntity>(filter);
        }
        public async Task<TDTO> GetItemAsync<TDTO, TEntity>(Expression<Func<TEntity, bool>> filter, List<Expression<Func<TEntity, object>>> includes) where TEntity : class
        {
            return await this.UnitOfWork.GetItemAsync<TDTO, TEntity>(filter, includes);
        }

        #endregion

        #region GetItems
        public IList<TDTO> GetItems<TDTO, TEntity>() where TEntity : class
        {
            return this.UnitOfWork.GetItems<TDTO, TEntity>();
        }
        public IList<TDTO> GetItems<TDTO, TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            return this.UnitOfWork.GetItems<TDTO, TEntity>(filter);
        }
        public IList<TDTO> GetItems<TDTO, TEntity>(Expression<Func<TEntity, bool>> filter, List<Expression<Func<TEntity, object>>> includes) where TEntity : class
        {
            return this.UnitOfWork.GetItems<TDTO, TEntity>(filter, includes);
        }
        public IList<TDTO> GetItems<TDTO, TEntity>(Expression<Func<TEntity, bool>> filter, List<Expression<Func<TEntity, object>>> includes, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, int? pageIndex = null, int? pageSize = null) where TEntity : class
        {
            return this.UnitOfWork.GetItems<TDTO, TEntity>(filter, includes, orderBy, pageIndex, pageSize);
        }
        public async Task<IList<TDTO>> GetItemsAsync<TDTO, TEntity>() where TEntity : class
        {
            return await this.UnitOfWork.GetItemsAsync<TDTO, TEntity>();
        }
        public async Task<IList<TDTO>> GetItemsAsync<TDTO, TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            return await this.UnitOfWork.GetItemsAsync<TDTO, TEntity>(filter);
        }
        public async Task<IList<TDTO>> GetItemsAsync<TDTO, TEntity>(Expression<Func<TEntity, bool>> filter, List<Expression<Func<TEntity, object>>> includes) where TEntity : class
        {
            return await this.UnitOfWork.GetItemsAsync<TDTO, TEntity>(filter, includes);
        }

        #endregion

        #region Insert
        public bool Insert<TDTO, TEntity>(TDTO m_DTO) where TEntity : class
        {
            return this.UnitOfWork.Insert<TDTO, TEntity>(m_DTO);
        }
        public object Insert<TDTO, TEntity>(TDTO m_DTO, Expression<Func<TEntity, object>> property) where TEntity : class
        {
            return this.UnitOfWork.Insert<TDTO, TEntity>(m_DTO, property);
        }
        public async Task<object> InsertAsync<TDTO, TEntity>(TDTO m_DTO) where TEntity : class
        {
            return await this.UnitOfWork.InsertAsync<TDTO, TEntity>(m_DTO);
        }

        #endregion

        #region Update
        public bool Update<TDTO, TEntity>(TDTO m_DTO) where TEntity : class
        {
            return this.UnitOfWork.Update<TDTO, TEntity>(m_DTO);
        }
        public bool Update<TDTO, TEntity>(TDTO m_DTO, List<Expression<Func<TEntity, object>>> properties) where TEntity : class
        {
            return this.UnitOfWork.Update<TDTO, TEntity>(m_DTO, properties);
        }
        public async Task<bool> UpdateAsync<TDTO, TEntity>(TDTO m_DTO) where TEntity : class, new()
        {
            return await this.UnitOfWork.UpdateAsync<TDTO, TEntity>(m_DTO);
        }

        #endregion

        #region Delete
        public bool Delete<TDTO, TEntity>(TDTO m_DTO) where TEntity : class
        {
            return this.UnitOfWork.Delete<TDTO, TEntity>(m_DTO);
        }
        public bool Delete<TDTO, TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            return this.UnitOfWork.Delete<TDTO, TEntity>(filter);
        }
        public async Task<bool> DeleteAsync<TDTO, TEntity>(TDTO m_DTO) where TEntity : class
        {
            return await this.UnitOfWork.DeleteAsync<TDTO, TEntity>(m_DTO);
        }

        #endregion

        #region Count
        public int Count<TDTO, TEntity>(Expression<Func<TEntity, bool>> filter, List<Expression<Func<TEntity, object>>> includes = null) where TEntity : class
        {
            return this.UnitOfWork.Count<TDTO, TEntity>(filter, includes);
        }
        public async Task<int> CountAsync<TDTO, TEntity>(Expression<Func<TEntity, bool>> filter, List<Expression<Func<TEntity, object>>> includes = null) where TEntity : class
        {
            return await this.UnitOfWork.CountAsync<TDTO, TEntity>(filter, includes);
        }

        #endregion

        #region Execute
        public DataSet ExecuteQuery(string procName, Dictionary<string, object> parameters)
        {
            return this.UnitOfWork.ExecuteQuery(procName, parameters);
        }
        public object ExecuteScalar(string procName, Dictionary<string, object> parameters)
        {
            return this.UnitOfWork.ExecuteScalar(procName, parameters);
        }
        public bool ExecuteNonQuery(string procName, Dictionary<string, object> parameters)
        {
            return this.UnitOfWork.ExecuteNonQuery(procName, parameters);
        }

        #endregion

        #endregion
    }
}
