using MiddlewareTool.Business.Interface;
using MiddlewareTool.Common;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.SystemMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class SystemSettingMgmtBusiness : BaseBusiness, ISystemSettingMgmtBusiness
    {
        private readonly ISystemSettingBusiness _repoBusiness;
        public SystemSettingMgmtBusiness(IUnitOfWork unitOfWork, ISystemSettingBusiness repoBusiness) : base(unitOfWork)
        {
            _repoBusiness = repoBusiness;
        }
        public async Task<Tuple<int, List<SystemSettingDto>>> GetPagingAsync(string userName, string keyWord, int layout, int pageIndex, int pageSize)
        {
            return await _repoBusiness.GetPagingAsync(userName, keyWord, layout, pageIndex, pageSize);
        }
        public async Task<SystemSettingDto> GetByIdAsync(Guid id)
        {
            return await _repoBusiness.GetByIdAsync(id);
        }
        public async Task<SystemSettingDto> GetByTypeAsync(AppType.Setting type)
        {
            return await _repoBusiness.GetByTypeAsync(type);
        }
        public List<SystemSettingDto> GetByType(AppType.Setting type)
        {
            return _repoBusiness.GetByType(type);
        }
        public async Task<SystemSettingDto> GetByCodeAsync(string code)
        {
            return await _repoBusiness.GetByCodeAsync(code);
        }
        public SystemSettingDto GetByCode(string code)
        {
            return _repoBusiness.GetByCode(code);
        }
        public bool CheckExistByCode(string code)
        {
            return _repoBusiness.CheckExistByCode(code);
        }
        public async Task<bool> InsertAsync(SystemSettingDto dto)
        {
            return await _repoBusiness.InsertAsync(dto);
        }
        public async Task<bool> UpdateAsync(SystemSettingDto dto)
        {
            return await _repoBusiness.UpdateAsync(dto);
        }
        public bool Update(SystemSettingDto dto)
        {
            return _repoBusiness.Update(dto);
        }
        public async Task<bool> DeleteAsync(Guid id, string userName)
        {
            return await _repoBusiness.DeleteAsync(id, userName);
        }
        public async Task<List<SystemSettingDto>> GetListByLayoutAsync(AppType.Layout layout)
        {
            return await _repoBusiness.GetListByLayoutAsync(layout);
        }
        public List<SystemSettingDto> GetListByLayout(AppType.Layout layout)
        {
            return _repoBusiness.GetListByLayout(layout);
        }
    }
}
