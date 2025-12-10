using System.Data;
using System.Linq.Expressions;

namespace MiddlewareTool.Business.Interface
{
    public interface ISharedBusiness
    {
        #region GetItem

        /// <summary>
        /// Lấy biểu ghi.
        /// </summary>
        /// <returns>TDTO</returns>
        TDTO GetItem<TDTO, TEntity>() where TEntity : class;
        /// <summary>
        /// Lấy biểu ghi theo điều kiện trong fillter.
        /// </summary>
        /// <param name="filter">Điều kiện lọc</param>
        /// <returns>TDTO</returns>
        TDTO GetItem<TDTO, TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class;
        /// <summary>
        /// Lấy biểu ghi theo điều kiện trong fillter + includes.
        /// </summary>
        /// <param name="filter">Điều kiện lọc</param>
        /// <param name="includes">Biểu ghi liên quan</param>
        /// <returns>TDTO</returns>
        TDTO GetItem<TDTO, TEntity>(Expression<Func<TEntity, bool>> filter, List<Expression<Func<TEntity, object>>> includes) where TEntity : class;
        /// <summary>
        /// Lấy biểu ghi.
        /// </summary>
        /// <returns>TDTO</returns>
        Task<TDTO> GetItemAsync<TDTO, TEntity>() where TEntity : class;
        /// <summary>
        /// Lấy biểu ghi theo điều kiện trong fillter.
        /// </summary>
        /// <param name="filter">Điều kiện lọc</param>
        /// <returns>TDTO</returns>
        Task<TDTO> GetItemAsync<TDTO, TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class;
        /// <summary>
        /// Lấy biểu ghi theo điều kiện trong fillter + includes.
        /// </summary>
        /// <param name="filter">Điều kiện lọc</param>
        /// <param name="includes">Biểu ghi liên quan</param>
        /// <returns>TDTO</returns>
        Task<TDTO> GetItemAsync<TDTO, TEntity>(Expression<Func<TEntity, bool>> filter, List<Expression<Func<TEntity, object>>> includes) where TEntity : class;

        #endregion

        #region GetItems

        /// <summary>
        /// Lấy tất cả các biểu ghi.
        /// </summary>
        /// <returns>IList</returns>
        IList<TDTO> GetItems<TDTO, TEntity>() where TEntity : class;
        /// <summary>
        /// Lấy tất cả các biểu ghi theo điều kiện trong fillter.
        /// </summary>
        /// <param name="filter">Điều kiện lọc</param>
        /// <returns>IList</returns>
        IList<TDTO> GetItems<TDTO, TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class;
        /// <summary>
        /// Lấy tất cả các biểu ghi theo điều kiện trong fillter + includes.
        /// </summary>
        /// <param name="filter">Điều kiện lọc</param>
        /// <param name="includes">Biểu ghi liên quan</param>
        /// <returns>IList</returns>
        IList<TDTO> GetItems<TDTO, TEntity>(Expression<Func<TEntity, bool>> filter, List<Expression<Func<TEntity, object>>> includes) where TEntity : class;
        /// <summary>
        /// Lấy tất cả các biểu ghi phân trang theo điều kiện trong fillter + includes.
        /// </summary>
        /// <param name="filter">Điều kiện lọc</param>
        /// <param name="orderBy">Sắp xếp theo</param>
        /// <param name="includes">Biểu ghi liên quan</param>
        /// <param name="pageIndex">Trang bắt đầu</param>
        /// <param name="pageSize">Số biểu ghi của trang</param>
        /// <returns>IList</returns>
        IList<TDTO> GetItems<TDTO, TEntity>(Expression<Func<TEntity, bool>> filter, List<Expression<Func<TEntity, object>>> includes, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, int? pageIndex = null, int? pageSize = null) where TEntity : class;
        /// <summary>
        /// Lấy tất cả các biểu ghi.
        /// </summary>
        /// <returns>IList</returns>
        Task<IList<TDTO>> GetItemsAsync<TDTO, TEntity>() where TEntity : class;
        /// <summary>
        /// Lấy tất cả các biểu ghi theo điều kiện trong fillter.
        /// </summary>
        /// <param name="filter">Điều kiện lọc</param>
        /// <returns>IList</returns>
        Task<IList<TDTO>> GetItemsAsync<TDTO, TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class;
        /// <summary>
        /// Lấy tất cả các biểu ghi theo điều kiện trong fillter + includes.
        /// </summary>
        /// <param name="filter">Điều kiện lọc</param>
        /// <param name="includes">Biểu ghi liên quan</param>
        /// <returns>IList</returns>
        Task<IList<TDTO>> GetItemsAsync<TDTO, TEntity>(Expression<Func<TEntity, bool>> filter, List<Expression<Func<TEntity, object>>> includes) where TEntity : class;

        #endregion

        #region Insert

        /// <summary>
        /// Thêm mới biểu ghi.
        /// </summary>
        /// <param name="entity">Biểu ghi</param>
        /// <returns>True / False</returns>
        bool Insert<TDTO, TEntity>(TDTO m_DTO) where TEntity : class;
        /// <summary>
        /// Thêm mới biểu ghi và trả về dữ thuộc tính của đối tượng vừa thêm
        /// Thường áp dụng để lấy ID sau khi thêm trong trường hợp ID tự tăng.
        /// </summary>
        /// <param name="entity">Biểu ghi</param>
        /// <param name="property">Thuộc tính cần trả về sau khi thêm</param>
        /// <returns>object</returns>
        object Insert<TDTO, TEntity>(TDTO m_DTO, Expression<Func<TEntity, object>> property) where TEntity : class;
        /// <summary>
        /// Thêm mới biểu ghi và trả về dữ thuộc tính của đối tượng vừa thêm
        /// Thường áp dụng để lấy ID sau khi thêm trong trường hợp ID tự tăng.
        /// </summary>
        /// <param name="entity">Biểu ghi</param>
        /// <returns>object</returns>
        Task<object> InsertAsync<TDTO, TEntity>(TDTO m_DTO) where TEntity : class;

        #endregion

        #region Update

        /// <summary>
        /// Cập nhật biểu ghi.
        /// </summary>
        /// <param name="entity">Biểu ghi</param>
        /// <returns>True / False</returns>
        bool Update<TDTO, TEntity>(TDTO m_DTO) where TEntity : class;
        /// <summary>
        /// Cập nhật biểu ghi, nhưng chỉ cập nhật các field được chỉ định trong param properties.
        /// </summary>
        /// <param name="entity">Biểu ghi</param>
        /// <param name="properties">Các field cần cập nhật</param>
        /// <returns>True / False</returns>
        bool Update<TDTO, TEntity>(TDTO m_DTO, List<Expression<Func<TEntity, object>>> properties) where TEntity : class;
        /// <summary>
        /// Cập nhật biểu ghi, nhưng chỉ cập nhật các field được chỉ định trong param properties.
        /// </summary>
        /// <param name="entity">Biểu ghi</param>
        /// <returns>True / False</returns>
        Task<bool> UpdateAsync<TDTO, TEntity>(TDTO m_DTO) where TEntity : class, new();

        #endregion

        #region Delete

        /// <summary>
        /// Xóa biểu ghi. Yêu cầu đối tượng phải có khóa chính.
        /// </summary>
        /// <param name="entity">Biểu ghi</param>
        /// <returns>True / False</returns>
        bool Delete<TDTO, TEntity>(TDTO m_DTO) where TEntity : class;
        /// <summary>
        /// Xóa biểu ghi theo điều kiện trong expression.
        /// </summary>
        /// <param name="filter">Điều kiện lọc</param>
        /// <returns>True / False</returns>
        bool Delete<TDTO, TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class;
        /// <summary>
        /// Xóa biểu ghi. Yêu cầu đối tượng phải có khóa chính.
        /// </summary>
        /// <param name="entity">Biểu ghi</param>
        /// <returns>True / False</returns>
        Task<bool> DeleteAsync<TDTO, TEntity>(TDTO m_DTO) where TEntity : class;

        #endregion

        #region Count

        /// <summary>
        /// Đếm số biểu ghi theo điều kiện filter.
        /// </summary>
        /// <param name="filter">Điều kiện lọc</param>
        /// <param name="includes">Các đối tượng liên quan nếu có</param>
        /// <returns>int</returns>
        int Count<TDTO, TEntity>(Expression<Func<TEntity, bool>> filter, List<Expression<Func<TEntity, object>>> includes = null) where TEntity : class;
        Task<int> CountAsync<TDTO, TEntity>(Expression<Func<TEntity, bool>> filter, List<Expression<Func<TEntity, object>>> includes = null) where TEntity : class;

        #endregion

        #region Execute

        /// <summary>
        /// Thực thi thủ tục
        /// </summary>
        /// <param name="procName">Tên thủ tục</param>
        /// <param name="parameters">Tham số</param>
        /// <returns>DataSet</returns>
        DataSet ExecuteQuery(string procName, Dictionary<string, object> parameters);
        /// <summary>
        /// Thực thi thủ tục
        /// </summary>
        /// <param name="procName">Tên thủ tục</param>
        /// <param name="parameters">Tham số</param>
        /// <returns>object</returns>
        object ExecuteScalar(string procName, Dictionary<string, object> parameters);
        /// <summary>
        /// Thực thi thủ tục
        /// </summary>
        /// <param name="procName">Tên thủ tục</param>
        /// <param name="parameters">Tham số</param>
        /// <returns>true/false</returns>
        bool ExecuteNonQuery(string procName, Dictionary<string, object> parameters);

        #endregion
    }
}
