using MiddlewareTool.Common;

namespace MiddlewareTool.Dto
{
    public class DiscountTypeDto : BaseDto
    {
        public string TransactionType { get; set; }
        public string BOXED { get; set; }
        public string PROFIT { get; set; }
        public bool Remove { get; set; }
        public void SetDefaultValueInsert(AppValue.ActiveFlag active)
        {
            this.Id = Guid.NewGuid();
            this.CreateDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
            this.ActiveFlag = active;
        }
    }

    public class DiscountTypeFilterDto
    {
        public string Keyword { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
