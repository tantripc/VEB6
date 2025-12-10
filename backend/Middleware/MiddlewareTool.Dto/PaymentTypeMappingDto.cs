using MiddlewareTool.Common;

namespace MiddlewareTool.Dto
{
    public class PaymentTypeMappingDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public Nullable<byte> Scope { get; set; }
        public string CustomerType { get; set; }
        public bool IsMapping { get; set; }
        public string DeliveryCode { get; set; }
        public string PaymentCodeOutput { get; set; }
        public Nullable<byte> Method { get; set; }
        public bool AllowRefund { get; set; }
        public string SaleToRefund { get; set; }
        public string URL { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public AppValue.ActiveFlag ActiveFlag { get; set; }

        #region Extend fields
        public string UpdateByFullName { get; set; }
        public string CreatedByFullName { get; set; }
        public string ScopeName { get; set; }
        public string CustomerTypeName { get; set; }
        public string MethodName { get; set; }
        #endregion

        public void SetDefaultValueInsert()
        {
            this.Id = Guid.NewGuid();
            this.CreateDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
            this.ActiveFlag = AppValue.ActiveFlag.Active;
        }
        public void SetDefaultValueUpdate()
        {
            this.UpdateDate = DateTime.Now;
        }
        public void SetDefaultValueDelete()
        {
            this.UpdateDate = DateTime.Now;
            this.ActiveFlag = AppValue.ActiveFlag.Delete;
        }
    }
    public class PaymentTypeMappingFilterDto
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Keyword { get; set; }
        public string CustomerType { get; set; }
        public bool? IsMapping { get; set; }
        public bool? AllowRefund { get; set; }
    }
}
