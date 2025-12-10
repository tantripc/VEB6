using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.StoreMgmtDto;

namespace MiddlewareTool.Business.Interface
{
    public interface IProvinceBusiness
    {      
        Task<List<ProvinceDto>> GetAllProvince();
        Task<List<ProvinceDto>> GetAllProvinceByCityCode(string cityCode);
        Task<List<ProvinceDto>> GetAllProvinceByDistrictCode(string districtCode);
        Task<List<ProvinceDto>> GetAll();
    }
}
