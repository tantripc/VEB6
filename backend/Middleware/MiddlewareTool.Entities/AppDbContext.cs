using Microsoft.EntityFrameworkCore;
using MiddlewareTool.Entities.Models;

namespace MiddlewareTool.Entities;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<B2BTax> B2BTaxes { get; set; }

    public virtual DbSet<BarcodeMaster> BarcodeMasters { get; set; }

    public virtual DbSet<BillNumber> BillNumbers { get; set; }

    public virtual DbSet<BillNumber_Hotfix> BillNumber_Hotfixes { get; set; }

    public virtual DbSet<BoxedFile> BoxedFiles { get; set; }

    public virtual DbSet<Business> Businesses { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CategoryMaster> CategoryMasters { get; set; }

    public virtual DbSet<CustomerDatum> CustomerData { get; set; }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<DeliveryCode> DeliveryCodes { get; set; }

    public virtual DbSet<DeliverySku> DeliverySkus { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<DepartmentMaster> DepartmentMasters { get; set; }

    public virtual DbSet<DiscountType> DiscountTypes { get; set; }

    public virtual DbSet<DivisionMaster> DivisionMasters { get; set; }

    public virtual DbSet<GroupMaster> GroupMasters { get; set; }

    public virtual DbSet<GroupPriceChange> GroupPriceChanges { get; set; }

    public virtual DbSet<HPriceChange> HPriceChanges { get; set; }

    public virtual DbSet<Header> Headers { get; set; }

    public virtual DbSet<Header1> Headers1 { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<InventoryDeltum> InventoryDelta { get; set; }

    public virtual DbSet<InventoryHistory> InventoryHistories { get; set; }

    public virtual DbSet<Inventory_BK> Inventory_BKs { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Invoice1> Invoices1 { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Item1> Items1 { get; set; }

    public virtual DbSet<ItemForDelivery> ItemForDeliveries { get; set; }

    public virtual DbSet<ItemForRefund> ItemForRefunds { get; set; }

    public virtual DbSet<LineMaster> LineMasters { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<LocationGroup> LocationGroups { get; set; }

    public virtual DbSet<LocationUploadError> LocationUploadErrors { get; set; }

    public virtual DbSet<LocationUploadMonitor> LocationUploadMonitors { get; set; }

    public virtual DbSet<MPriceChange> MPriceChanges { get; set; }

    public virtual DbSet<Mailbox> Mailboxes { get; set; }

    public virtual DbSet<Mall> Malls { get; set; }

    public virtual DbSet<ManualStock> ManualStocks { get; set; }

    public virtual DbSet<ManualStockUploadError> ManualStockUploadErrors { get; set; }

    public virtual DbSet<ManualStockUploadMonitor> ManualStockUploadMonitors { get; set; }

    public virtual DbSet<Mapping> Mappings { get; set; }

    public virtual DbSet<MasterItem> MasterItems { get; set; }

    public virtual DbSet<MasterItemList> MasterItemLists { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuAction> MenuActions { get; set; }

    public virtual DbSet<MenuRole> MenuRoles { get; set; }

    public virtual DbSet<MonthlyMemberSale> MonthlyMemberSales { get; set; }

    public virtual DbSet<NPriceChange> NPriceChanges { get; set; }

    public virtual DbSet<POPMasterItem> POPMasterItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentByStore> PaymentByStores { get; set; }

    public virtual DbSet<PaymentByStore1> PaymentByStores1 { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<PaymentTypeMapping> PaymentTypeMappings { get; set; }

    public virtual DbSet<PriceChange> PriceChanges { get; set; }

    public virtual DbSet<PriceChangeHistory> PriceChangeHistories { get; set; }

    public virtual DbSet<Pricing> Pricings { get; set; }

    public virtual DbSet<PricingHistory> PricingHistories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductByStore> ProductByStores { get; set; }

    public virtual DbSet<ProductFeed> ProductFeeds { get; set; }

    public virtual DbSet<ProductHistory> ProductHistories { get; set; }

    public virtual DbSet<ProductHistoryAction> ProductHistoryActions { get; set; }

    public virtual DbSet<ProductInfo> ProductInfos { get; set; }

    public virtual DbSet<ProductInfoHistory> ProductInfoHistories { get; set; }

    public virtual DbSet<ProductInfoUploadError> ProductInfoUploadErrors { get; set; }

    public virtual DbSet<ProductInfoUploadMonitor> ProductInfoUploadMonitors { get; set; }

    public virtual DbSet<Products_BK> Products_BKs { get; set; }

    public virtual DbSet<Products_InSale> Products_InSales { get; set; }

    public virtual DbSet<Products_RealDatum> Products_RealData { get; set; }

    public virtual DbSet<ProfitFile> ProfitFiles { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Promotion1> Promotions1 { get; set; }

    public virtual DbSet<PromotionESL> PromotionESLs { get; set; }

    public virtual DbSet<PromotionESLHistory> PromotionESLHistories { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<ReceiptNumber> ReceiptNumbers { get; set; }

    public virtual DbSet<RecordRefund> RecordRefunds { get; set; }

    public virtual DbSet<RecordRefundFile> RecordRefundFiles { get; set; }

    public virtual DbSet<RecordSale> RecordSales { get; set; }

    public virtual DbSet<RecordSaleFile> RecordSaleFiles { get; set; }

    public virtual DbSet<RecordSales_BK> RecordSales_BKs { get; set; }

    public virtual DbSet<RecordSales_BK2610> RecordSales_BK2610s { get; set; }

    public virtual DbSet<RefundHeader> RefundHeaders { get; set; }

    public virtual DbSet<RefundHeader1> RefundHeaders1 { get; set; }

    public virtual DbSet<RefundInvoice> RefundInvoices { get; set; }

    public virtual DbSet<RefundInvoice1> RefundInvoices1 { get; set; }

    public virtual DbSet<RefundItem> RefundItems { get; set; }

    public virtual DbSet<RefundItem1> RefundItems1 { get; set; }

    public virtual DbSet<RefundPayment> RefundPayments { get; set; }

    public virtual DbSet<RefundPromotion> RefundPromotions { get; set; }

    public virtual DbSet<RefundReason> RefundReasons { get; set; }

    public virtual DbSet<RefundSku> RefundSkus { get; set; }

    public virtual DbSet<Resource> Resources { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SkuMapping> SkuMappings { get; set; }

    public virtual DbSet<SkuUploadError> SkuUploadErrors { get; set; }

    public virtual DbSet<SkuUploadMonitor> SkuUploadMonitors { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<SubClassMaster> SubClassMasters { get; set; }

    public virtual DbSet<SystemLog> SystemLogs { get; set; }

    public virtual DbSet<SystemLogAttachment> SystemLogAttachments { get; set; }

    public virtual DbSet<SystemSetting> SystemSettings { get; set; }

    public virtual DbSet<UploadError> UploadErrors { get; set; }

    public virtual DbSet<UploadFile> UploadFiles { get; set; }

    public virtual DbSet<UploadMonitor> UploadMonitors { get; set; }

    public virtual DbSet<UserDepartment> UserDepartments { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    public virtual DbSet<UserPermissionDept> UserPermissionDepts { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserStore> UserStores { get; set; }

    public virtual DbSet<tbl_BOXED> tbl_BOXEDs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<B2BTax>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<BarcodeMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<BillNumber>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<BoxedFile>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Business>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Businesses");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<CategoryMaster>(entity =>
        {
            entity.HasOne(d => d.Department).WithMany(p => p.CategoryMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CategoryMaster_DepartmentMaster");
        });

        modelBuilder.Entity<CustomerDatum>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Header).WithMany(p => p.Deliveries)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Delivery_Headers");
        });

        modelBuilder.Entity<DeliveryCode>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<DeliverySku>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Index).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<DepartmentMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Group).WithMany(p => p.DepartmentMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DepartmentMaster_GroupMaster");
        });

        modelBuilder.Entity<DiscountType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<DivisionMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Line).WithMany(p => p.DivisionMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DivisionMaster_LineMaster");
        });

        modelBuilder.Entity<GroupMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Division).WithMany(p => p.GroupMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GroupMaster_DivisionMaster");
        });

        modelBuilder.Entity<GroupPriceChange>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<HPriceChange>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Header>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ActualOrderNumber).HasComputedColumnSql("(case when len([OrderNumber])>(7) then substring([OrderNumber],(9),len([OrderNumber])) else concat('B2B',[OrderNumber]) end)", false);
            entity.Property(e => e.CustomerID).HasDefaultValue("");
        });

        modelBuilder.Entity<Header1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_so_Headers");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Business).WithMany(p => p.Header1s)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Headers_Bussiness");
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_so.History");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Header).WithMany(p => p.Histories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_History_Headers");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.StoreCodeSku).HasComputedColumnSql("(concat([StoreCode],[Sku]))", false);
        });

        modelBuilder.Entity<InventoryDeltum>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<InventoryHistory>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Inventory_BK>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.StoreCodeSku).HasComputedColumnSql("(concat([StoreCode],[Sku]))", false);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Header).WithMany(p => p.Invoices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoices_Headers");
        });

        modelBuilder.Entity<Invoice1>(entity =>
        {
            entity.HasKey(e => e.InvoiceKey).HasName("PK_so_Invoices");

            entity.Property(e => e.InvoiceKey).ValueGeneratedNever();

            entity.HasOne(d => d.Header).WithMany(p => p.Invoice1s)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_so_Invoices_Headers");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Header).WithMany(p => p.Items)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Items_Headers");
        });

        modelBuilder.Entity<Item1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_so_Items");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.LineNumber).HasDefaultValue(1);

            entity.HasOne(d => d.Header).WithMany(p => p.Item1s)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_so_Items_Headers");
        });

        modelBuilder.Entity<ItemForDelivery>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ItemForRefund>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<LineMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_LocationGroup");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CityCode).IsFixedLength();
            entity.Property(e => e.DistrictCode).IsFixedLength();
            entity.Property(e => e.WardCode).IsFixedLength();
        });

        modelBuilder.Entity<LocationGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__3214EC0796A63A6E");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<LocationUploadError>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CityCode).IsFixedLength();
            entity.Property(e => e.CityName).IsFixedLength();
            entity.Property(e => e.DistrictCode).IsFixedLength();
            entity.Property(e => e.DistrictName).IsFixedLength();
            entity.Property(e => e.WardCode).IsFixedLength();
            entity.Property(e => e.WardName).IsFixedLength();

            entity.HasOne(d => d.Upload).WithMany(p => p.LocationUploadErrors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LocationUploadErrors_LocationUploadMonitors");
        });

        modelBuilder.Entity<LocationUploadMonitor>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<MPriceChange>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Mailbox>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_MailBoxes");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Mall>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ManualStock>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ManualStockUploadError>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Upload).WithMany(p => p.ManualStockUploadErrors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ManualStockUploadErrors_ManualStockUploadMonitors");
        });

        modelBuilder.Entity<ManualStockUploadMonitor>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Mapping>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Category).WithMany(p => p.Mappings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mapping_Category");
        });

        modelBuilder.Entity<MasterItem>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<MenuAction>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<MenuRole>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Role).WithMany(p => p.MenuRoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MenuRole_Roles");
        });

        modelBuilder.Entity<MonthlyMemberSale>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.YearMonth).HasComputedColumnSql("(concat([Transactionyear],[Transactionmonth]))", false);
        });

        modelBuilder.Entity<NPriceChange>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<POPMasterItem>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Header).WithMany(p => p.Payments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_Headers");
        });

        modelBuilder.Entity<PaymentByStore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_re_PaymentByStore");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<PaymentByStore1>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsMethod).HasDefaultValue(false);
            entity.Property(e => e.Scope).HasDefaultValue(0);
        });

        modelBuilder.Entity<PaymentTypeMapping>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<PriceChange>(entity =>
        {
            entity.ToTable("PriceChange", "core", tb => tb.HasTrigger("trg_PriceChange_UpdateComputedColumns"));

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsTransferESL).HasDefaultValue(false);
            entity.Property(e => e.StoreCodeSku).HasComputedColumnSql("(concat([StoreCode],[ITEM_NO]))", false);
            entity.Property(e => e.StoreCodeSkuPRC_NO).HasComputedColumnSql("(concat([StoreCode],[ITEM_NO],[PRC_NO]))", false);
        });

        modelBuilder.Entity<PriceChangeHistory>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.StoreCodeSku).HasComputedColumnSql("(concat([StoreCode],[ITEM_NO]))", false);
            entity.Property(e => e.StoreCodeSkuPRC_NO).HasComputedColumnSql("(concat([StoreCode],[ITEM_NO],[PRC_NO]))", false);
        });

        modelBuilder.Entity<Pricing>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.StoreCodeSku).HasComputedColumnSql("(concat([StoreCode],[Sku]))", false);
        });

        modelBuilder.Entity<PricingHistory>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ProductByStore>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ProductFeed>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ProductHistory>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ProductHistoryAction>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ProductInfo>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.StoreCodeSku).HasComputedColumnSql("(concat([StoreCode],[Sku]))", false);
        });

        modelBuilder.Entity<ProductInfoHistory>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ProductInfoUploadError>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Upload).WithMany(p => p.ProductInfoUploadErrors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductInfoUploadErrors_ProductInfoUploadMonitors");
        });

        modelBuilder.Entity<ProductInfoUploadMonitor>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Products_BK>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ProfitFile>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Item).WithMany(p => p.Promotions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Promotion_Items");
        });

        modelBuilder.Entity<Promotion1>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<PromotionESL>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EndDateTime).HasComputedColumnSql("(CONVERT([datetime],[EndDate])+CONVERT([datetime],[EndTime]))", true);
            entity.Property(e => e.StartDateTime).HasComputedColumnSql("(CONVERT([datetime],[StartDate])+CONVERT([datetime],[StartTime]))", true);
        });

        modelBuilder.Entity<PromotionESLHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PromotionESLHistory");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EndDateTime).HasComputedColumnSql("(CONVERT([datetime],[EndDate])+CONVERT([datetime],[EndTime]))", true);
            entity.Property(e => e.StartDateTime).HasComputedColumnSql("(CONVERT([datetime],[StartDate])+CONVERT([datetime],[StartTime]))", true);
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.Property(e => e.CityCode).IsFixedLength();
            entity.Property(e => e.DistrictCode).IsFixedLength();
            entity.Property(e => e.WardCode).IsFixedLength();
        });

        modelBuilder.Entity<ReceiptNumber>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<RecordRefund>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsTransferS4).HasDefaultValue(true);
            entity.Property(e => e.IsTransferSAP).HasDefaultValue(true);
        });

        modelBuilder.Entity<RecordRefundFile>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<RecordSale>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ActualOrderNumber).HasComputedColumnSql("(case when len([OrderNumber])>(7) then substring([OrderNumber],(9),len([OrderNumber])) else concat('B2B',[OrderNumber]) end)", false);
            entity.Property(e => e.IsTransferS4).HasDefaultValue(true);
            entity.Property(e => e.IsTransferSAP).HasDefaultValue(true);
        });

        modelBuilder.Entity<RecordSaleFile>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<RefundHeader>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ActualOrderNumber).HasComputedColumnSql("(case when len([OrderNumber])>(7) then substring([OrderNumber],(9),len([OrderNumber])) else concat('B2B',[OrderNumber]) end)", false);
            entity.Property(e => e.CustomerID).HasDefaultValue("");
        });

        modelBuilder.Entity<RefundHeader1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_so_RefundHeaders");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.SaleOrder).WithMany(p => p.RefundHeader1s)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefundHeaders_Headers");
        });

        modelBuilder.Entity<RefundInvoice>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<RefundInvoice1>(entity =>
        {
            entity.HasKey(e => e.InvoiceKey).HasName("PK_so_RefundInvoices");

            entity.Property(e => e.InvoiceKey).ValueGeneratedNever();

            entity.HasOne(d => d.Header).WithMany(p => p.RefundInvoice1s)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefundInvoices_RefundHeaders");
        });

        modelBuilder.Entity<RefundItem>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Header).WithMany(p => p.RefundItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefundItems_RefundHeaders");
        });

        modelBuilder.Entity<RefundItem1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_so_RefundItems");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.LineNumber).HasDefaultValue(1);

            entity.HasOne(d => d.Header).WithMany(p => p.RefundItem1s)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefundItems_RefundHeaders");
        });

        modelBuilder.Entity<RefundPayment>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Header).WithMany(p => p.RefundPayments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefundPayments_RefundHeaders");
        });

        modelBuilder.Entity<RefundPromotion>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Item).WithMany(p => p.RefundPromotions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefundPromotion_RefundItems");
        });

        modelBuilder.Entity<RefundSku>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<SkuMapping>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.MallCode).IsFixedLength();
        });

        modelBuilder.Entity<SkuUploadError>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.MallCode).IsFixedLength();

            entity.HasOne(d => d.Upload).WithMany(p => p.SkuUploadErrors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UploadErrors_UploadMonitors");
        });

        modelBuilder.Entity<SkuUploadMonitor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UploadMonitors");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => new { e.Sku, e.StoreCode }).HasName("PK_Stock_1");

            entity.Property(e => e.SkuStoreCode).HasComputedColumnSql("(concat([StoreCode],[Sku]))", false);
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.StoreType).HasDefaultValue(1);
        });

        modelBuilder.Entity<SubClassMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SubClass__3214EC0796BE598C");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<SystemLog>(entity =>
        {
            entity.Property(e => e.LogId).ValueGeneratedNever();
        });

        modelBuilder.Entity<SystemLogAttachment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo_SystemLogAttachment");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<SystemSetting>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<UploadError>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Upload).WithMany(p => p.UploadErrors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UploadErrors_UploadMonitors");
        });

        modelBuilder.Entity<UploadFile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_so_UploadFile");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<UploadMonitor>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UserInfo");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<UserPermissionDept>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Roles");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_UserInfos");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
