using MiddlewareTool.Business.Interface;
using MiddlewareTool.Business.Concrete;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.SystemMgmtDto;

namespace MiddlewareTool.Business.Concrete
{
    public class ResourceMgmtBusiness : BaseBusiness, IResourceMgmtBusiness
    {
        private readonly IResourceBusiness _repoBusiness;
        public ResourceMgmtBusiness(IUnitOfWork unitOfWork, IResourceBusiness repoBusiness) : base(unitOfWork)
        {
            _repoBusiness = repoBusiness;
        }
        public Task<Tuple<int, List<ResourceDto>>> GetPagingAsync(string keyWord, int pageIndex, int pageSize)
        {
            return _repoBusiness.GetPagingAsync(keyWord, pageIndex, pageSize);
        }
        public Dictionary<string, ResourceDto> GetAll()
        {
            return _repoBusiness.GetAll();
        }
        public bool Import(DataTable resource)
        {
            return _repoBusiness.Import(resource);
        }
        public async Task<ResourceDto> GetByIdAsync(string id)
        {
            return await _repoBusiness.GetByIdAsync(id);
        }
        public bool Insert(ResourceDto dto)
        {
            return _repoBusiness.Insert(dto);
        }
        public async Task<bool> InsertAsync(ResourceDto dto)
        {
            return await _repoBusiness.InsertAsync(dto);
        }
        public async Task<bool> InsertToListAsync(List<ResourceDto> lstDto)
        {
            return await _repoBusiness.InsertToListAsync(lstDto);
        }
        public bool Update(ResourceDto dto)
        {
            return _repoBusiness.Update(dto);
        }
        public async Task<bool> UpdateAsync(ResourceDto dto)
        {
            return await _repoBusiness.UpdateAsync(dto);
        }
        public async Task<bool> DeleteAsync(string id)
        {
            return await _repoBusiness.DeleteAsync(id);
        }
        public async Task<bool> DeleteToListAsync(List<string> lstId)
        {
            return await _repoBusiness.DeleteToListAsync(lstId);
        }
    }
}
