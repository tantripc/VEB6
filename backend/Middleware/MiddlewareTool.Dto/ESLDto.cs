namespace MiddlewareTool.Dto
{
    public class ESL_Store_Dto
    {
        public string StoreCode { get; set; }
    }
    public class ESL_M_Dto
    {
        public string Item_Code { get; set; }
        public string Barcode { get; set; }
        public string Origin { get; set; }
        public string Product_Image { get; set; }
        public string Brand_Name { get; set; }
        public string Brand_Image { get; set; }
        public string QRCode { get; set; }
        public string Product_Attributes { get; set; }
    }
    public class ESL_INV_Dto
    {
        public string Item_Code { get; set; }
        public string Barcode { get; set; }
        public string Inventory { get; set; }
        public string OOS_Date { get; set; }
    }
}
