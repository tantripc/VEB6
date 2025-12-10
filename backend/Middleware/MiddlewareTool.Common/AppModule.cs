namespace MiddlewareTool.Common
{
    public enum AppModule : byte
    {
        None = 0,
        /// <summary>
        /// Login
        /// </summary>
        Login = 1,
        /// <summary>
        /// Quản lý hệ thống
        /// </summary>
        SystemManagement = 2,
        /// <summary>
        /// Quản lý người dùng
        /// </summary>
        UserManagement = 3,
        /// <summary>
        /// Quản lý đơn vị
        /// </summary>
        DepartmentManagement = 4,
        /// <summary>
        /// Quản lý cửa hàng
        /// </summary>
        StoreManagement = 5,
        /// <summary>
        /// Quản lý phân loại
        /// </summary>
        CategoryManagement = 6,
        /// <summary>
        /// Quản lý sản phẩm
        /// </summary>
        ProductManagement = 7,
        /// <summary>
        /// Quản lý SKU
        /// </summary>
        SKUManagement = 8,
        /// <summary>
        /// PROFIT
        /// </summary>
        PROFIT = 9,
        /// <summary>
        /// BOXED
        /// </summary>
        BOXED = 10,
        /// <summary>
        /// DataImport
        /// </summary>
        DataImport = 11,
        /// <summary>
        /// Quản lý Location
        /// </summary>
        LocationManagement = 12,
        /// <summary>
        /// Sale Order
        /// </summary>
        SaleOrder = 13,
        /// <summary>
        /// Refund
        /// </summary>
        Refund = 14,
        /// <summary>
        /// Business Mgmt
        /// </summary>
        BusinessMgmt = 15,
        /// <summary>
        /// ESL service
        /// </summary>
        ESL = 16,
        /// <summary>
        /// POP Master item service
        /// </summary>
        POP = 17,
        /// <summary>
        /// B2BTax
        /// </summary>
        B2BTax = 18,
        /// <summary>
        /// SAP_BTP_Sale
        /// </summary>
        SAP_BTP_Sale = 19,
        /// <summary>
        /// SAP_BTP_MonthlyMember
        /// </summary>
        SAP_BTP_MonthlyMember = 20,
        /// <summary>
        /// SAP_S4HAHA
        /// </summary>
        SAP_S4HAHA = 21,
        /// <summary>
        /// PromotionESL
        /// </summary>
        PromotionESL = 22,
        /// <summary>
        /// RecordSale
        /// </summary>
        RecordSale = 23,
    }
}
