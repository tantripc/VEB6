using MiddlewareTool.Common;

namespace MiddlewareTool.Dto
{
    public class MonthlyMemberSaleDto
    {
        public System.Guid Id { get; set; }
        public string Membercode { get; set; }
        public string Companycode { get; set; }
        public string Transactionmonth { get; set; }
        public string Transactionyear { get; set; }
        public double Monthlysalesamount { get; set; }
        public string Currency { get; set; }
        public string Memberlevel { get; set; }
        public double Percentageredemption { get; set; }
        public int Point { get; set; }
        public string URL { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public byte ActiveFlag { get; set; }

        public static MonthlyMemberSaleDto Create()
        {
            return new MonthlyMemberSaleDto()
            {
                Id = Guid.NewGuid(),
                URL = "",
                CreateBy = "system",
                UpdateBy = "system",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                ActiveFlag = (byte)AppValue.ActiveFlag.Active,
            };
        }

        public static MonthlyMemberSaleDto Clone(MemberSales member)
        {
            var memberDto = Create();
            memberDto.Membercode = member.Membercode;
            memberDto.Companycode = member.Companycode;
            memberDto.Transactionmonth = member.Transactionmonth;
            memberDto.Transactionyear = member.Transactionyear;
            memberDto.Monthlysalesamount = member.Monthlysalesamount;
            memberDto.Currency = member.Currency;
            memberDto.Memberlevel = member.Memberlevel;
            memberDto.Percentageredemption = member.Percentageredemption;
            memberDto.Point = member.Point;

            return memberDto;
        }
    }
    public class MonthlyMemberSaleFilterDto
    {
        public string KeyWord { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public DateTime? FromDate
        {
            get
            {
                if (!string.IsNullOrEmpty(DateFrom))
                {
                    return DateTime.Parse(DateFrom);
                }
                return null;
            }
        }
        public DateTime? ToDate
        {
            get
            {
                if (!string.IsNullOrEmpty(DateTo))
                {
                    return DateTime.Parse(DateTo);
                }
                return null;
            }
        }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
    #region API Model
    public class Metadata
    {
        public string id { get; set; }
        public string uri { get; set; }
        public string type { get; set; }
    }

    public class MemberSales
    {
        public string Membercode { get; set; }
        public string Companycode { get; set; }
        public string Transactionmonth { get; set; }
        public string Transactionyear { get; set; }
        public double Monthlysalesamount { get; set; }
        public string Currency { get; set; }
        public string Memberlevel { get; set; }
        public double Percentageredemption { get; set; }
        public int Point { get; set; }
    }

    public class ResponseMemberSalesData
    {
        public List<MemberSales> results { get; set; }
    }

    public class RootMemberSalesObject
    {
        public ResponseMemberSalesData d { get; set; }
    }
    #endregion

}
