namespace MW.DTO
{
    public class SaleOrderFilterDto
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string? Keyword { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Guid? BusinessId { get; set; }
        public string? StoreCode { get; set; }
        public byte? StatusId { get; set; }
        public string? CreatedBy { get; set; }
        public bool HasAllPermission { get; set; }
        public bool? Refunded { get; set; }
        public string? ReasonCode { get; set; }
        public string? OrderNumber { get; set; }
        public string? CustomerType { get; set; }
        public List<Guid> HeaderIds { get; set; } = new List<Guid>();
    }
    public class SaleOrderCompactDto
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public string StoreCode { get; set; }
    }
}
