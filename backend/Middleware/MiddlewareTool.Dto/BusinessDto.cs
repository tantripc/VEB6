namespace MiddlewareTool.Dto
{
    public class BusinessDto : BaseDto
    {
        public string Name { get; set; }
        public string TaxName { get; set; }
        public string TaxCode { get; set; }
        public string TaxAddress { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string PayMethodCode { get; set; }
        public string CustomerName { get; set; }
        public string NoStreet { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string City { get; set; }

        public void SetDefaultValueInsert()
        {
            this.Id = Guid.NewGuid();
            this.CreateDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
        }

        #region Extensions
        public string Comment { get; set; }
        #endregion
    }
}
