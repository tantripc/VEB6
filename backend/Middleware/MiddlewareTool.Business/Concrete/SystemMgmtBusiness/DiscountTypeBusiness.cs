using AutoMapper;
using MiddlewareTool.Business.Interface;
using MiddlewareTool.Dto;
using MiddlewareTool.Entities;
using MiddlewareTool.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MiddlewareTool.Business.Concrete
{
    public class DiscountTypeBusiness : BaseBusiness, IDiscountTypeBusiness
    {

        public DiscountTypeBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public bool Delete(Guid id)
        {
            try
            {
                var entity = UnitOfWork.GetAll<DiscountType>().FirstOrDefault(x => x.Id == id);
                var rs = UnitOfWork.Delete(entity);
                return rs;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                throw (ex);
            }
        }

        public DiscountTypeDto Get(Guid id)
        {
            try
            {
                var entity = UnitOfWork.GetAllNoTracking<DiscountType>().FirstOrDefault(x => x.Id == id);
                return Mapper.Map<DiscountTypeDto>(entity);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                throw (ex);
            }
        }

        public Tuple<int, List<DiscountTypeDto>> GetPaging(DiscountTypeFilterDto filter)
        {
            try
            {
                var count = 0;
                var dtos = new List<DiscountTypeDto>();
                var query = UnitOfWork.GetAllNoTracking<DiscountType>()
                    .Where(x => x.ActiveFlag != STATUS_DELETE);
                if (!string.IsNullOrEmpty(filter.Keyword))
                {
                    query = query.Where(x =>
                    x.TransactionType.Contains(filter.Keyword)
                    || x.BOXED.Contains(filter.Keyword)
                    || x.PROFIT.Contains(filter.Keyword)
                    );
                }
                count = query.Count();
                var entities = query
                    .OrderByDescending(x => x.UpdateDate)
                    .Skip(Math.Max((filter.PageIndex - 1) * filter.PageSize, 0))
                    .Take(filter.PageSize)
                    .ToList();
                dtos = Mapper.Map<List<DiscountTypeDto>>(entities);
                return Tuple.Create(count, dtos);
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                throw (ex);
            }
        }
        public List<DiscountTypeDto> GetActive()
        {
            try
            {
                var dtos = new List<DiscountTypeDto>();
                var entities = UnitOfWork.GetAllNoTracking<DiscountType>()
                    .Where(x => x.ActiveFlag == STATUS_ACTIVE).ToList();
                dtos = Mapper.Map<List<DiscountTypeDto>>(entities);
                return dtos;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                throw (ex);
            }
        }
        public DiscountTypeDto Insert(DiscountTypeDto dto)
        {
            try
            {
                // Kiểm tra trùng mã BOXED
                var checkExist = UnitOfWork.GetAllNoTracking<DiscountType>()
                    .Any(x => x.BOXED == dto.BOXED
                        && x.Id != dto.Id
                        && (x.ActiveFlag == STATUS_ACTIVE && dto.ActiveFlag == STATUS_ACTIVE));
                if (!checkExist)
                {
                    var entity = Mapper.Map<DiscountType>(dto);

                    entity = UnitOfWork.Insert(entity);
                    return Mapper.Map<DiscountTypeDto>(entity);
                }
                else
                {
                    dto.Description = "Có mã BOXED đang được áp dụng";
                    return dto;
                }
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                throw (ex);
            }
        }

        public DiscountTypeDto Update(DiscountTypeDto dto)
        {
            try
            {
                var checkExist = UnitOfWork.GetAllNoTracking<DiscountType>()
                    .Any(x => x.BOXED == dto.BOXED
                        && x.Id != dto.Id
                        && (x.ActiveFlag == STATUS_ACTIVE && dto.ActiveFlag == STATUS_ACTIVE));
                if (!checkExist)
                {
                    var entity = UnitOfWork.GetAll<DiscountType>().FirstOrDefault(x => x.Id == dto.Id);
                    entity.TransactionType = dto.TransactionType;
                    entity.BOXED = dto.BOXED;
                    entity.PROFIT = dto.PROFIT;
                    entity.Remove = dto.Remove;
                    entity.ActiveFlag = (byte)dto.ActiveFlag;
                    var rs = UnitOfWork.Update(entity);
                    if (!rs)
                    {
                        dto.Description = "Có lỗi xảy ra";
                    }
                }
                else
                {
                    dto.Description = "Có mã BOXED đang được áp dụng";
                }
                return dto;
            }
            catch (Exception ex)
            {
                LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                throw (ex);
            }
        }
    }
}
