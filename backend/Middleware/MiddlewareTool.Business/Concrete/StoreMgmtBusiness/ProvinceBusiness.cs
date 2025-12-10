using MiddlewareTool.Business.Interface;
using MiddlewareTool.Common;
using System.Data.Entity;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MiddlewareTool.Dto.StoreMgmtDto;
using AutoMapper;

namespace MiddlewareTool.Business.Concrete
{
    public class ProvinceBusiness : BaseBusiness, IProvinceBusiness
    {
        public ProvinceBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public async Task<List<ProvinceDto>> GetAllProvince()
        {
            try
            {
                var iquery = await this.UnitOfWork.GetAllAsync<Province>(x => x.CityName != null);
                if (iquery != null)
                {
                    var data = Mapper.Map<List<ProvinceDto>>(iquery);
                    List<ProvinceDto> dataNew = data.GroupBy(x => x.CityCode).Select(y => y.FirstOrDefault()).ToList();
                    return dataNew;
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<ProvinceDto>();
        }
        public async Task<List<ProvinceDto>> GetAllProvinceByCityCode(string cityCode)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetAllAsync<Province>(x => x.CityCode == cityCode);
                if (iquery != null)
                {
                    var data = Mapper.Map<List<ProvinceDto>>(iquery);

                    List<ProvinceDto> dataNew = data.GroupBy(x => x.DistrictCode).Select(y => y.FirstOrDefault()).ToList();
                    return dataNew;
                }
               
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<ProvinceDto>();
        }
        public async Task<List<ProvinceDto>> GetAllProvinceByDistrictCode(string districtCode)
        {
            try
            {
                var iquery = await this.UnitOfWork.GetAllAsync<Province>(x => x.DistrictCode == districtCode);
                if (iquery != null)
                {
                    var data = Mapper.Map<List<ProvinceDto>>(iquery);
                    List<ProvinceDto> dataNew = data.GroupBy(x => x.WardCode).Select(y => y.FirstOrDefault()).ToList();
                    return dataNew;
                }
               
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<ProvinceDto>();
        }      
        public async Task<List<ProvinceDto>> GetAll()
        {
            try
            {
                var iquery = await this.UnitOfWork.GetAllAsync<Province>(x => x.CityName != null);
                if (iquery != null)
                {
                    var data = Mapper.Map<List<ProvinceDto>>(iquery);
                    return data;
                }
            }
            catch (Exception ex) { LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex); }
            return new List<ProvinceDto>();
        }
    }
}
