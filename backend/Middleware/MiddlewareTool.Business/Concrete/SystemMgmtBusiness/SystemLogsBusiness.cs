using AutoMapper;
using MiddlewareTool.Business.Interface;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.SystemMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class SystemLogsBusiness : BaseBusiness, ISystemLogsBusiness
    {
        #region Constructors
        public SystemLogsBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        #endregion

        #region Methods
        public Tuple<int, List<SystemLogDto>> GetPaging(string userName, string keyWord, string module, int userFunction, int eventResult, string dateFrom, string dateTo, int pageIndex, int pageSize, string source)
        {
            return new SystemLogBusiness(this.UnitOfWork).GetPaging(userName, keyWord, module, userFunction, eventResult, dateFrom, dateTo, pageIndex, pageSize, source);
        }
        public List<SystemLogDto> Export(string userName, string keyWord, string module, int userFunction, int eventResult, string dateFrom, string dateTo)
        {
            return new SystemLogBusiness(this.UnitOfWork).Export(userName, keyWord, module, userFunction, eventResult, dateFrom, dateTo);
        }
        public bool Insert(SystemLogDto dto)
        {
            return new SystemLogBusiness(this.UnitOfWork).Insert(dto);
        }
        public async Task<bool> InsertAsync(SystemLogDto dto)
        {
            return await new SystemLogBusiness(this.UnitOfWork).InsertAsync(dto);
        }
        public SystemLogAttachmentDto GetLogAttachment(Guid id)
        {
            return new SystemLogBusiness(this.UnitOfWork).GetLogAttachment(id);
        }

        public bool InsertSystemLogAttachment(SystemLogAttachmentDto systemLogAttachmentDto)
        {
            bool result = false;
            try
            {
                var entity = Mapper.Map<SystemLogAttachment>(systemLogAttachmentDto);
                var res = this.UnitOfWork.Insert(entity);
                if (res != null) { result = true; }
            }
            catch (Exception ex) { this.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return result;

        }
        #endregion
    }
}
