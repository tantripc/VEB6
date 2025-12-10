using MiddlewareTool.Common;
using System.ComponentModel.DataAnnotations;

namespace MiddlewareTool.Dto
{
    public class SkuMappingMgmtDto
    {
        public class LocationGroupDto
        {
            public Guid Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public string URL { get; set; }
            public string CreateBy { get; set; }
            public string UpdateBy { get; set; }
            public DateTime CreateDate { get; set; }
            public DateTime UpdateDate { get; set; }
            public byte ActiveFlag { get; set; }

            public void SetDefaultValueInsert()
            {
                this.Id = Guid.NewGuid();
                this.CreateDate = DateTime.Now;
                this.UpdateDate = DateTime.Now;
                this.ActiveFlag = (byte)AppValue.ActiveFlag.Active;
            }
        }

        public class LocationDto : BaseDto
        {
            public string Name { get; set; }
            [MaxLength(10)]
            public string CityCode { get; set; }
            public string CityName { get; set; }
            [MaxLength(10)]
            public string DistrictCode { get; set; }
            public string DistrictName { get; set; }
            [MaxLength(10)]
            public string WardCode { get; set; }
            public string WardName { get; set; }
        }

        public class SkuMappingDto
        {
            public Guid Id { get; set; }
            public string MallCode { get; set; }
            public string MallName { get; set; }
            public string ExpressLocationGroupName { get; set; }
            public string ParcelLocationGroupName { get; set; }
            public string URL { get; set; }
            public string CreateBy { get; set; }
            public string UpdateBy { get; set; }
            public System.DateTime CreateDate { get; set; }
            public System.DateTime UpdateDate { get; set; }
            public byte ActiveFlag { get; set; }
            public void SetDefaultValueInsert()
            {
                this.Id = Guid.NewGuid();
                this.CreateDate = DateTime.Now;
                this.UpdateDate = DateTime.Now;
                this.ActiveFlag = (byte)AppValue.ActiveFlag.Active;
            }
        }

        public class SkuMappingExportDto
        {
            public string MallCode { get; set; }
            public string MallName { get; set; }
            public string ExpressLocationGroupName { get; set; }
            public string ParcelLocationGroupName { get; set; }
            public string Infor { get; set; }
        }

        public class SkuUploadMonitorDto : BaseDto
        {
            public string FileName { get; set; }
            public byte[] FileContent { get; set; }
            public string FileExt { get; set; }
            public int TotalRow { get; set; }
            public string Curent { get; set; }
        }

        public class SkuUploadErrorDto : BaseDto
        {
            public Guid UploadId { get; set; }
            public string MallCode { get; set; }
            public string ExpressLocationGroupName { get; set; }
            public string ParcelLocationGroupName { get; set; }
            public string Infor { get; set; }
        }

        public class LocationUploadMonitorDto : SkuUploadMonitorDto
        {
        }

        public class LocationUploadErrorDto : BaseDto
        {
            public Guid UploadId { get; set; }
            public string CityCode { get; set; }
            public string CityName { get; set; }
            public string DistrictCode { get; set; }
            public string DistrictName { get; set; }
            public string WardCode { get; set; }
            public string WardName { get; set; }
            public string LocationGroupName { get; set; }
            public string Infor { get; set; }
        }
    }
}
