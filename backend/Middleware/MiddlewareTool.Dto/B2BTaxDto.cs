using MiddlewareTool.Common;

namespace MiddlewareTool.Dto
{
    public class B2BTaxDto
    {
        public System.Guid Id { get; set; }
        public string No { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public Nullable<double> TaxCode_Normal { get; set; }
        public double TaxCode_B2B { get; set; }
        public string CreateBy { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateByFullName { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public AppValue.ActiveFlag ActiveFlag { get; set; }
        public string Comment { get; set; }
        public void SetDefaultValueInsert()
        {
            this.CreateDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
            this.ActiveFlag = AppValue.ActiveFlag.Active;
        }
    }
    public class B2BTaxFilterDto
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Keyword { get; set; }
        public bool HasAllPermission { get; set; }
    }
    public class B2BTaxExportDto
    {
        public string No { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public string TaxCode_B2B { get; set; }
        public string Status { get; set; }
    }

}
