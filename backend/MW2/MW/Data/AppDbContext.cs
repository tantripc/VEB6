using System;
using System.Collections.Generic;
using MW.Entities;
using Microsoft.EntityFrameworkCore;

namespace MW.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<B2btax> B2btaxes { get; set; }

    public virtual DbSet<BarcodeMaster> BarcodeMasters { get; set; }

    public virtual DbSet<BillNumber> BillNumbers { get; set; }

    public virtual DbSet<BillNumberHotfix> BillNumberHotfixes { get; set; }

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

    public virtual DbSet<Header> Headers { get; set; }

    public virtual DbSet<Header1> Headers1 { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<HpriceChange> HpriceChanges { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<InventoryBk> InventoryBks { get; set; }

    public virtual DbSet<InventoryDeltum> InventoryDelta { get; set; }

    public virtual DbSet<InventoryHistory> InventoryHistories { get; set; }

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

    public virtual DbSet<MpriceChange> MpriceChanges { get; set; }

    public virtual DbSet<NpriceChange> NpriceChanges { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentByStore> PaymentByStores { get; set; }

    public virtual DbSet<PaymentByStore1> PaymentByStores1 { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<PopmasterItem> PopmasterItems { get; set; }

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

    public virtual DbSet<ProductsBk> ProductsBks { get; set; }

    public virtual DbSet<ProductsInSale> ProductsInSales { get; set; }

    public virtual DbSet<ProductsRealDatum> ProductsRealData { get; set; }

    public virtual DbSet<ProfitFile> ProfitFiles { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Promotion1> Promotions1 { get; set; }

    public virtual DbSet<PromotionEsl> PromotionEsls { get; set; }

    public virtual DbSet<PromotionEslhistory> PromotionEslhistories { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<ReceiptNumber> ReceiptNumbers { get; set; }

    public virtual DbSet<RecordRefund> RecordRefunds { get; set; }

    public virtual DbSet<RecordRefundFile> RecordRefundFiles { get; set; }

    public virtual DbSet<RecordSale> RecordSales { get; set; }

    public virtual DbSet<RecordSaleFile> RecordSaleFiles { get; set; }

    public virtual DbSet<RecordSalesBk> RecordSalesBks { get; set; }

    public virtual DbSet<RecordSalesBk2610> RecordSalesBk2610s { get; set; }

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

    public virtual DbSet<TblBoxed> TblBoxeds { get; set; }

    public virtual DbSet<UploadError> UploadErrors { get; set; }

    public virtual DbSet<UploadFile> UploadFiles { get; set; }

    public virtual DbSet<UploadMonitor> UploadMonitors { get; set; }

    public virtual DbSet<UserDepartment> UserDepartments { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    public virtual DbSet<UserPermissionDept> UserPermissionDepts { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserStore> UserStores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LTTRI\\SQL2019;Database=MiddlewareTool;User ID=sa;Password=@bc123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<B2btax>(entity =>
        {
            entity.ToTable("B2BTax", "so");

            entity.HasIndex(e => new { e.ActiveFlag, e.Sku }, "IX_B2BTax");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.No).HasMaxLength(50);
            entity.Property(e => e.ProductName).HasMaxLength(4000);
            entity.Property(e => e.Sku)
                .HasMaxLength(13)
                .HasColumnName("SKU");
            entity.Property(e => e.TaxCodeB2b).HasColumnName("TaxCode_B2B");
            entity.Property(e => e.TaxCodeNormal).HasColumnName("TaxCode_Normal");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<BarcodeMaster>(entity =>
        {
            entity.ToTable("BarcodeMaster", "core");

            entity.HasIndex(e => e.ActiveFlag, "IX_ActiveFlag");

            entity.HasIndex(e => e.BarNo, "IX_BAR_NO");

            entity.HasIndex(e => e.BarSkuNo, "IX_BAR_SKU_NO");

            entity.HasIndex(e => new { e.ActiveFlag, e.Id, e.BarSkuNo, e.StoreCode, e.IsTransferM, e.IsTransferInv }, "IX_BarcodeMaster");

            entity.HasIndex(e => e.StoreCode, "IX_StoreCode");

            entity.HasIndex(e => new { e.ActiveFlag, e.Id, e.BarSkuNo, e.StoreCode, e.IsTransferM, e.IsTransferInv }, "IX_core_BarcodeMaster");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BarNo)
                .HasMaxLength(18)
                .HasColumnName("BAR_NO");
            entity.Property(e => e.BarSkuNo)
                .HasMaxLength(13)
                .HasColumnName("BAR_SKU_NO");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.IsTransferInv).HasColumnName("IsTransferINV");
            entity.Property(e => e.RecId)
                .HasMaxLength(1)
                .HasColumnName("REC_ID");
            entity.Property(e => e.StoreCode).HasMaxLength(4);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<BillNumber>(entity =>
        {
            entity.ToTable("BillNumbers", "se");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CurrentDate).HasMaxLength(8);
            entity.Property(e => e.Posnumber).HasColumnName("POSNumber");
            entity.Property(e => e.StoreCode).HasMaxLength(10);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<BillNumberHotfix>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("BillNumber_Hotfix");

            entity.Property(e => e.BillNumber).HasMaxLength(100);
            entity.Property(e => e.OrderNumber).HasMaxLength(100);
            entity.Property(e => e.SalesDate).HasMaxLength(8);
            entity.Property(e => e.StoreCode).HasMaxLength(100);
        });

        modelBuilder.Entity<BoxedFile>(entity =>
        {
            entity.ToTable("BoxedFiles", "core");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Ext)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<Business>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Businesses");

            entity.ToTable("Business", "so");

            entity.HasIndex(e => new { e.Name, e.TaxName, e.TaxCode, e.TaxAddress, e.Email, e.Phone, e.Fax, e.CustomerName }, "NonClusteredIndex-20240407-173850");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.District).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(1000);
            entity.Property(e => e.Fax).HasMaxLength(1000);
            entity.Property(e => e.Name).HasMaxLength(1000);
            entity.Property(e => e.NoStreet).HasMaxLength(255);
            entity.Property(e => e.PayMethodCode).HasMaxLength(10);
            entity.Property(e => e.Phone).HasMaxLength(1000);
            entity.Property(e => e.TaxAddress).HasMaxLength(1000);
            entity.Property(e => e.TaxCode).HasMaxLength(255);
            entity.Property(e => e.TaxName).HasMaxLength(1000);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
            entity.Property(e => e.Ward).HasMaxLength(255);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.ToTable("Category", "cat");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(100);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<CategoryMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.ToTable("CategoryMaster", "cat");

            entity.Property(e => e.Id).HasMaxLength(50);
            entity.Property(e => e.AutoPa).HasColumnName("AutoPA");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.PosFlag).HasMaxLength(3);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");

            entity.HasOne(d => d.Department).WithMany(p => p.CategoryMasters)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CategoryMaster_DepartmentMaster");
        });

        modelBuilder.Entity<CustomerDatum>(entity =>
        {
            entity.ToTable("CustomerData", "se");

            entity.HasIndex(e => new { e.CustomerId, e.FoxtrotUserId, e.ActiveFlag }, "IX_seCustomerData");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(255)
                .HasColumnName("CustomerID");
            entity.Property(e => e.CustomerType).HasMaxLength(1);
            entity.Property(e => e.District).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.FoxtrotUserId)
                .HasMaxLength(32)
                .HasColumnName("FoxtrotUserID");
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(255);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
            entity.Property(e => e.Ward).HasMaxLength(255);
        });

        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.ToTable("Delivery", "se");

            entity.HasIndex(e => new { e.HeaderId, e.ActiveFlag }, "IX_Delivery");

            entity.HasIndex(e => new { e.ActiveFlag, e.HeaderId, e.DeliveryCode, e.TrackingNumber }, "IX_se_Delivery");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DeliveryCode).HasMaxLength(20);
            entity.Property(e => e.SubOrderNumber).HasMaxLength(20);
            entity.Property(e => e.TrackingNumber).HasMaxLength(256);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");

            entity.HasOne(d => d.Header).WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.HeaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Delivery_Headers");
        });

        modelBuilder.Entity<DeliveryCode>(entity =>
        {
            entity.ToTable("DeliveryCodes", "se");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(20);
        });

        modelBuilder.Entity<DeliverySku>(entity =>
        {
            entity.ToTable("DeliverySku", "se");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Sku).HasMaxLength(13);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(100);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Index).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<DepartmentMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.ToTable("DepartmentMaster", "cat");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");

            entity.HasOne(d => d.Group).WithMany(p => p.DepartmentMasters)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DepartmentMaster_GroupMaster");
        });

        modelBuilder.Entity<DiscountType>(entity =>
        {
            entity.ToTable("DiscountType", "se");

            entity.HasIndex(e => new { e.UpdateDate, e.TransactionType, e.Boxed, e.ActiveFlag }, "IX_DiscountType").IsDescending(true, false, false, false);

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Boxed)
                .HasMaxLength(10)
                .HasColumnName("BOXED");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Profit)
                .HasMaxLength(10)
                .HasColumnName("PROFIT");
            entity.Property(e => e.TransactionType).HasMaxLength(50);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<DivisionMaster>(entity =>
        {
            entity.ToTable("DivisionMaster", "cat");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");

            entity.HasOne(d => d.Line).WithMany(p => p.DivisionMasters)
                .HasForeignKey(d => d.LineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DivisionMaster_LineMaster");
        });

        modelBuilder.Entity<GroupMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.ToTable("GroupMaster", "cat");

            entity.HasIndex(e => e.DivisionId, "IX_GroupMaster");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");

            entity.HasOne(d => d.Division).WithMany(p => p.GroupMasters)
                .HasForeignKey(d => d.DivisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GroupMaster_DivisionMaster");
        });

        modelBuilder.Entity<GroupPriceChange>(entity =>
        {
            entity.ToTable("GroupPriceChange", "core");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.EndOfRecord).HasMaxLength(1);
            entity.Property(e => e.ExcludeSsnId)
                .HasMaxLength(6)
                .HasColumnName("EXCLUDE_SSN_ID");
            entity.Property(e => e.PrcDiscRate)
                .HasMaxLength(6)
                .HasColumnName("PRC_DISC_RATE");
            entity.Property(e => e.PrcEndDate)
                .HasMaxLength(8)
                .HasColumnName("PRC_END_DATE");
            entity.Property(e => e.PrcEndTime)
                .HasMaxLength(4)
                .HasColumnName("PRC_END_TIME");
            entity.Property(e => e.PrcNo)
                .HasMaxLength(10)
                .HasColumnName("PRC_NO");
            entity.Property(e => e.PrcStartDate)
                .HasMaxLength(8)
                .HasColumnName("PRC_START_DATE");
            entity.Property(e => e.PrcStartTime)
                .HasMaxLength(4)
                .HasColumnName("PRC_START_TIME");
            entity.Property(e => e.PrcType)
                .HasMaxLength(6)
                .HasColumnName("PRC_TYPE");
            entity.Property(e => e.RecId)
                .HasMaxLength(1)
                .HasColumnName("REC_ID");
            entity.Property(e => e.StoreCode).HasMaxLength(10);
            entity.Property(e => e.Subclass)
                .HasMaxLength(9)
                .HasColumnName("SUBCLASS");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<Header>(entity =>
        {
            entity.ToTable("Headers", "se");

            entity.HasIndex(e => e.ActualOrderNumber, "IX_ActualOrderNumber");

            entity.HasIndex(e => e.UpdateDate, "IX_UpdateDate").IsDescending();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ActualOrderNumber)
                .HasMaxLength(23)
                .HasComputedColumnSql("(case when len([OrderNumber])>(7) then substring([OrderNumber],(9),len([OrderNumber])) else concat('B2B',[OrderNumber]) end)", false);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("CustomerID");
            entity.Property(e => e.CustomerType).HasMaxLength(1);
            entity.Property(e => e.FoxtrotUserId)
                .HasMaxLength(32)
                .HasColumnName("FoxtrotUserID");
            entity.Property(e => e.FulfillmentDate).HasMaxLength(8);
            entity.Property(e => e.OrderNumber).HasMaxLength(20);
            entity.Property(e => e.SettlementTime).HasMaxLength(4);
            entity.Property(e => e.StoreCode).HasMaxLength(100);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<Header1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_so_Headers");

            entity.ToTable("Headers", "so");

            entity.HasIndex(e => new { e.ActiveFlag, e.CreateBy, e.StoreCode, e.BusinessId, e.OrderNumber, e.StatusId, e.ReceiptDate, e.CreateDate, e.UpdateDate }, "IX_so_Headers").IsDescending(false, false, false, false, false, false, true, true, true);

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.OrderNumber).HasMaxLength(20);
            entity.Property(e => e.ReceiptDate).HasColumnType("datetime");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.StoreCode).HasMaxLength(100);
            entity.Property(e => e.TotalAmountWithVat)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("TotalAmountWithVAT");
            entity.Property(e => e.TotalAmountWithoutVat)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("TotalAmountWithoutVAT");
            entity.Property(e => e.TotalVatamount)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("TotalVATAmount");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");

            entity.HasOne(d => d.Business).WithMany(p => p.Header1s)
                .HasForeignKey(d => d.BusinessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Headers_Bussiness");
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_so.History");

            entity.ToTable("History", "so");

            entity.HasIndex(e => new { e.HeaderId, e.CreatedDate, e.UserId }, "IX_so_History");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Log).HasMaxLength(1000);
            entity.Property(e => e.UserId).HasMaxLength(50);

            entity.HasOne(d => d.Header).WithMany(p => p.Histories)
                .HasForeignKey(d => d.HeaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_History_Headers");
        });

        modelBuilder.Entity<HpriceChange>(entity =>
        {
            entity.ToTable("HPriceChange", "core");

            entity.HasIndex(e => new { e.PrcNo, e.StoreCode, e.RecId }, "IX_HPriceChange");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ApplicableTo)
                .HasMaxLength(5)
                .HasColumnName("APPLICABLE_TO");
            entity.Property(e => e.AutoScanFoc)
                .HasMaxLength(1)
                .HasColumnName("AUTO_SCAN_FOC");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ExclusionCategory).HasColumnName("EXCLUSION_CATEGORY");
            entity.Property(e => e.ExclusionDepartment).HasColumnName("EXCLUSION_DEPARTMENT");
            entity.Property(e => e.ExclusionSku).HasColumnName("EXCLUSION_SKU");
            entity.Property(e => e.FocItemForMemberOnly)
                .HasMaxLength(1)
                .HasColumnName("FOC_ITEM_FOR_MEMBER_ONLY");
            entity.Property(e => e.FocQty)
                .HasMaxLength(2)
                .HasColumnName("FOC_QTY");
            entity.Property(e => e.FocShortSku)
                .HasMaxLength(8)
                .HasColumnName("FOC_SHORT_SKU");
            entity.Property(e => e.Gst)
                .HasMaxLength(1)
                .HasColumnName("GST");
            entity.Property(e => e.InclusionCategory).HasColumnName("INCLUSION_CATEGORY");
            entity.Property(e => e.InclusionDepartment).HasColumnName("INCLUSION_DEPARTMENT");
            entity.Property(e => e.InclusionDivision).HasColumnName("INCLUSION_DIVISION");
            entity.Property(e => e.InclusionSku).HasColumnName("INCLUSION_SKU");
            entity.Property(e => e.MaxEntitlementPwpQuantity)
                .HasMaxLength(8)
                .HasColumnName("MAX_ENTITLEMENT_PWP_QUANTITY");
            entity.Property(e => e.MaxFocQtyForMember)
                .HasMaxLength(2)
                .HasColumnName("MAX_FOC_QTY_FOR_MEMBER");
            entity.Property(e => e.MaxReceiptPwpQuantity)
                .HasMaxLength(8)
                .HasColumnName("MAX_RECEIPT_PWP_QUANTITY");
            entity.Property(e => e.MinEntitlementAmount)
                .HasMaxLength(17)
                .HasColumnName("MIN_ENTITLEMENT_AMOUNT");
            entity.Property(e => e.NewPwpSellingPrice).HasColumnName("NEW_PWP_SELLING_PRICE");
            entity.Property(e => e.PrcDesc)
                .HasMaxLength(30)
                .HasColumnName("PRC_DESC");
            entity.Property(e => e.PrcEndDate)
                .HasMaxLength(8)
                .HasColumnName("PRC_END_DATE");
            entity.Property(e => e.PrcEndTime)
                .HasMaxLength(4)
                .HasColumnName("PRC_END_TIME");
            entity.Property(e => e.PrcNo)
                .HasMaxLength(10)
                .HasColumnName("PRC_NO");
            entity.Property(e => e.PrcStartDate)
                .HasMaxLength(8)
                .HasColumnName("PRC_START_DATE");
            entity.Property(e => e.PrcStartTime)
                .HasMaxLength(4)
                .HasColumnName("PRC_START_TIME");
            entity.Property(e => e.RecId)
                .HasMaxLength(1)
                .HasColumnName("REC_ID");
            entity.Property(e => e.RewardEventDay)
                .HasMaxLength(7)
                .HasColumnName("REWARD_EVENT_DAY");
            entity.Property(e => e.ShortSkuCode).HasColumnName("SHORT_SKU_CODE");
            entity.Property(e => e.StoreCode).HasMaxLength(4);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.ToTable("Inventory", "core");

            entity.HasIndex(e => e.ActiveFlag, "IX_ActiveFlag");

            entity.HasIndex(e => new { e.Sku, e.StoreCode, e.ActiveFlag }, "IX_Inventory")
                .IsUnique()
                .HasFillFactor(90);

            entity.HasIndex(e => e.StoreCodeSku, "IX_SkuStoreCode");

            entity.HasIndex(e => e.Url, "IX_URL");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Sku).HasMaxLength(13);
            entity.Property(e => e.StoreCode).HasMaxLength(10);
            entity.Property(e => e.StoreCodeSku)
                .HasMaxLength(23)
                .HasComputedColumnSql("(concat([StoreCode],[Sku]))", false);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url)
                .HasMaxLength(400)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<InventoryBk>(entity =>
        {
            entity.ToTable("Inventory_BK", "core");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Sku).HasMaxLength(13);
            entity.Property(e => e.StoreCode).HasMaxLength(10);
            entity.Property(e => e.StoreCodeSku)
                .HasMaxLength(23)
                .HasComputedColumnSql("(concat([StoreCode],[Sku]))", false);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<InventoryDeltum>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.ToTable("InventoryDelta", "core");

            entity.HasIndex(e => new { e.Sku, e.StoreCode }, "IX_InventoryDelta")
                .IsUnique()
                .HasFillFactor(90);

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Sku).HasMaxLength(13);
            entity.Property(e => e.StoreCode).HasMaxLength(10);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<InventoryHistory>(entity =>
        {
            entity.ToTable("InventoryHistory", "core");

            entity.HasIndex(e => new { e.UpdateDate, e.StoreCode, e.Sku, e.Action, e.UpdateBy, e.CreateBy, e.CreateDate }, "IX_Inventory_History").IsDescending(true, false, false, false, true, false, true);

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Sku).HasMaxLength(13);
            entity.Property(e => e.Source).HasMaxLength(4000);
            entity.Property(e => e.StoreCode).HasMaxLength(10);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.ToTable("Invoices", "se");

            entity.HasIndex(e => new { e.HeaderId, e.StoreCode, e.ActiveFlag }, "IX_Invoices");

            entity.HasIndex(e => new { e.HeaderId, e.SerialNo, e.Number }, "IX_se_Invoices");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.Cqtcode)
                .HasMaxLength(250)
                .HasColumnName("CQTCode");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerName).HasMaxLength(500);
            entity.Property(e => e.Number).HasMaxLength(20);
            entity.Property(e => e.SerialNo).HasMaxLength(20);
            entity.Property(e => e.StoreCode).HasMaxLength(4);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
            entity.Property(e => e.VatCode).HasMaxLength(100);

            entity.HasOne(d => d.Header).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.HeaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoices_Headers");
        });

        modelBuilder.Entity<Invoice1>(entity =>
        {
            entity.HasKey(e => e.InvoiceKey).HasName("PK_so_Invoices");

            entity.ToTable("Invoices", "so");

            entity.HasIndex(e => new { e.HeaderId, e.InvoiceId, e.InvoiceNumber, e.InvoiceReceiveNumber }, "IX_so_Invoices");

            entity.Property(e => e.InvoiceKey).ValueGeneratedNever();
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.Cqtcode)
                .HasMaxLength(250)
                .HasColumnName("CQTCode");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerName).HasMaxLength(500);
            entity.Property(e => e.IntegrateKey).HasMaxLength(255);
            entity.Property(e => e.InvoiceId)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("InvoiceID");
            entity.Property(e => e.InvoiceIssuedDate).HasMaxLength(10);
            entity.Property(e => e.InvoiceNumber).HasMaxLength(20);
            entity.Property(e => e.InvoiceReceiveNumber).HasMaxLength(50);
            entity.Property(e => e.InvoiceSeries).HasMaxLength(20);
            entity.Property(e => e.InvoiceTemplateCode).HasMaxLength(50);
            entity.Property(e => e.StoreCode).HasMaxLength(4);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
            entity.Property(e => e.VatCode).HasMaxLength(100);

            entity.HasOne(d => d.Header).WithMany(p => p.Invoice1s)
                .HasForeignKey(d => d.HeaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_so_Invoices_Headers");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.ToTable("Items", "se");

            entity.HasIndex(e => new { e.HeaderId, e.StoreCode, e.Vatamount, e.ActiveFlag, e.QuantitySold }, "IX_Items");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Sku).HasMaxLength(13);
            entity.Property(e => e.StoreCode).HasMaxLength(4);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
            entity.Property(e => e.Vatamount).HasColumnName("VATAmount");
            entity.Property(e => e.Vatcode).HasColumnName("VATCode");

            entity.HasOne(d => d.Header).WithMany(p => p.Items)
                .HasForeignKey(d => d.HeaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Items_Headers");
        });

        modelBuilder.Entity<Item1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_so_Items");

            entity.ToTable("Items", "so");

            entity.HasIndex(e => new { e.HeaderId, e.Sku }, "IX_so_Items");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.IsTaxB2b).HasColumnName("IsTaxB2B");
            entity.Property(e => e.LineNumber).HasDefaultValue(1);
            entity.Property(e => e.Pnlallocation)
                .HasMaxLength(100)
                .HasColumnName("PNLAllocation");
            entity.Property(e => e.Poprice).HasColumnName("POPrice");
            entity.Property(e => e.Sku).HasMaxLength(13);
            entity.Property(e => e.TransactionType).HasMaxLength(10);
            entity.Property(e => e.UnitType).HasMaxLength(50);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
            entity.Property(e => e.Vatamount).HasColumnName("VATAmount");
            entity.Property(e => e.Vatcode).HasColumnName("VATCode");

            entity.HasOne(d => d.Header).WithMany(p => p.Item1s)
                .HasForeignKey(d => d.HeaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_so_Items_Headers");
        });

        modelBuilder.Entity<ItemForDelivery>(entity =>
        {
            entity.ToTable("ItemForDelivery", "se");

            entity.HasIndex(e => new { e.HeaderId, e.StoreCode, e.ActiveFlag }, "IX_ItemForDelivery");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Sku).HasMaxLength(13);
            entity.Property(e => e.StoreCode).HasMaxLength(4);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
            entity.Property(e => e.Vatamount).HasColumnName("VATAmount");
            entity.Property(e => e.Vatcode).HasColumnName("VATCode");
        });

        modelBuilder.Entity<ItemForRefund>(entity =>
        {
            entity.ToTable("ItemForRefund", "se");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Sku).HasMaxLength(13);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<LineMaster>(entity =>
        {
            entity.ToTable("LineMaster", "cat");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK_LocationGroup")
                .HasFillFactor(90);

            entity.ToTable("Location", "sku");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CityCode)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.CityName).HasMaxLength(250);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.DistrictName).HasMaxLength(250);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
            entity.Property(e => e.WardCode)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.WardName).HasMaxLength(250);
        });

        modelBuilder.Entity<LocationGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__3214EC0796A63A6E");

            entity.ToTable("LocationGroup", "sku");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<LocationUploadError>(entity =>
        {
            entity.ToTable("LocationUploadErrors", "sku");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CityCode)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.CityName)
                .HasMaxLength(1000)
                .IsFixedLength();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.DistrictName)
                .HasMaxLength(1000)
                .IsFixedLength();
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
            entity.Property(e => e.WardCode)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.WardName)
                .HasMaxLength(1000)
                .IsFixedLength();

            entity.HasOne(d => d.Upload).WithMany(p => p.LocationUploadErrors)
                .HasForeignKey(d => d.UploadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LocationUploadErrors_LocationUploadMonitors");
        });

        modelBuilder.Entity<LocationUploadMonitor>(entity =>
        {
            entity.ToTable("LocationUploadMonitors", "sku");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Curent)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FileExt)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<Mailbox>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK_MailBoxes")
                .HasFillFactor(90);

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Subject).HasMaxLength(250);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<Mall>(entity =>
        {
            entity.ToTable("Mall", "sto");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CityCode).HasMaxLength(50);
            entity.Property(e => e.Code).HasMaxLength(100);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DistrictCode).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.MerchantId).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(100);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
            entity.Property(e => e.WardCode).HasMaxLength(50);
        });

        modelBuilder.Entity<ManualStock>(entity =>
        {
            entity.ToTable("ManualStock", "prod");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.RecordFlag).HasMaxLength(1);
            entity.Property(e => e.Sku).HasMaxLength(13);
            entity.Property(e => e.StoreCode).HasMaxLength(10);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<ManualStockUploadError>(entity =>
        {
            entity.ToTable("ManualStockUploadErrors", "prod");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Sku).HasMaxLength(13);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");

            entity.HasOne(d => d.Upload).WithMany(p => p.ManualStockUploadErrors)
                .HasForeignKey(d => d.UploadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ManualStockUploadErrors_ManualStockUploadMonitors");
        });

        modelBuilder.Entity<ManualStockUploadMonitor>(entity =>
        {
            entity.ToTable("ManualStockUploadMonitors", "prod");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Curent)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FileExt)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<Mapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.ToTable("Mapping", "cat");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CategoryMasterId).HasMaxLength(50);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");

            entity.HasOne(d => d.Category).WithMany(p => p.Mappings)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mapping_Category");
        });

        modelBuilder.Entity<MasterItem>(entity =>
        {
            entity.ToTable("MasterItems", "core");

            entity.HasIndex(e => new { e.ItemNo, e.ItemDiv, e.ItemDept, e.ItemCls, e.ItemSubcls, e.ItemBarcode }, "IX_ITEM_NO");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CardFlag)
                .HasMaxLength(1)
                .HasColumnName("CARD_FLAG");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Direction)
                .HasMaxLength(300)
                .HasColumnName("DIRECTION");
            entity.Property(e => e.ExpireLabelFormat)
                .HasMaxLength(1)
                .HasColumnName("EXPIRE_LABEL_FORMAT");
            entity.Property(e => e.ExpireTime)
                .HasMaxLength(4)
                .HasColumnName("EXPIRE_TIME");
            entity.Property(e => e.Ingredient)
                .HasMaxLength(475)
                .HasColumnName("INGREDIENT");
            entity.Property(e => e.InstructStorage)
                .HasMaxLength(66)
                .HasColumnName("INSTRUCT_STORAGE");
            entity.Property(e => e.ItemBarcode)
                .HasMaxLength(18)
                .HasColumnName("ITEM_BARCODE");
            entity.Property(e => e.ItemCls)
                .HasMaxLength(6)
                .HasColumnName("ITEM_CLS");
            entity.Property(e => e.ItemDate)
                .HasMaxLength(8)
                .HasColumnName("ITEM_DATE");
            entity.Property(e => e.ItemDept)
                .HasMaxLength(3)
                .HasColumnName("ITEM_DEPT");
            entity.Property(e => e.ItemDiv)
                .HasMaxLength(3)
                .HasColumnName("ITEM_DIV");
            entity.Property(e => e.ItemLongName)
                .HasMaxLength(30)
                .HasColumnName("ITEM_LONG_NAME");
            entity.Property(e => e.ItemLongNameChinese)
                .HasMaxLength(40)
                .HasColumnName("ITEM_LONG_NAME_CHINESE");
            entity.Property(e => e.ItemMemberSell)
                .HasMaxLength(17)
                .HasColumnName("ITEM_MEMBER_SELL");
            entity.Property(e => e.ItemNo)
                .HasMaxLength(13)
                .HasColumnName("ITEM_NO");
            entity.Property(e => e.ItemPluFlag)
                .HasMaxLength(1)
                .HasColumnName("ITEM_PLU_FLAG");
            entity.Property(e => e.ItemSell)
                .HasMaxLength(17)
                .HasColumnName("ITEM_SELL");
            entity.Property(e => e.ItemShortName)
                .HasMaxLength(15)
                .HasColumnName("ITEM_SHORT_NAME");
            entity.Property(e => e.ItemShortNameChinese)
                .HasMaxLength(20)
                .HasColumnName("ITEM_SHORT_NAME_CHINESE");
            entity.Property(e => e.ItemSubcls)
                .HasMaxLength(9)
                .HasColumnName("ITEM_SUBCLS");
            entity.Property(e => e.ItemUom)
                .HasMaxLength(5)
                .HasColumnName("ITEM_UOM");
            entity.Property(e => e.ItemUom2)
                .HasMaxLength(8)
                .HasColumnName("ITEM_UOM2");
            entity.Property(e => e.ItemVat)
                .HasMaxLength(3)
                .HasColumnName("ITEM_VAT");
            entity.Property(e => e.ItemVatFlag)
                .HasMaxLength(1)
                .HasColumnName("ITEM_VAT_FLAG");
            entity.Property(e => e.ItemWeigh)
                .HasMaxLength(1)
                .HasColumnName("ITEM_WEIGH");
            entity.Property(e => e.Kads1mFlag)
                .HasMaxLength(1)
                .HasColumnName("KADS1M_FLAG");
            entity.Property(e => e.NutriFacts)
                .HasMaxLength(100)
                .HasColumnName("NUTRI_FACTS");
            entity.Property(e => e.PrintProdFlag)
                .HasMaxLength(3)
                .HasColumnName("Print_prod_flag");
            entity.Property(e => e.RecId)
                .HasMaxLength(1)
                .HasColumnName("REC_ID");
            entity.Property(e => e.SalesTax)
                .HasMaxLength(3)
                .HasColumnName("SALES_TAX");
            entity.Property(e => e.SeasonId)
                .HasMaxLength(6)
                .HasColumnName("SEASON_ID");
            entity.Property(e => e.TaxCode)
                .HasMaxLength(5)
                .HasColumnName("TAX_CODE");
            entity.Property(e => e.TaxSign)
                .HasMaxLength(5)
                .HasColumnName("Tax_Sign");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
            entity.Property(e => e.ValidToUseDate)
                .HasMaxLength(3)
                .HasColumnName("Valid_to_use_date");
            entity.Property(e => e.Warning)
                .HasMaxLength(45)
                .HasColumnName("WARNING");
        });

        modelBuilder.Entity<MasterItemList>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MasterItemList", "core");

            entity.Property(e => e.CardFlag)
                .HasMaxLength(1)
                .HasColumnName("CARD_FLAG");
            entity.Property(e => e.Direction)
                .HasMaxLength(300)
                .HasColumnName("DIRECTION");
            entity.Property(e => e.ExpireLabelFormat)
                .HasMaxLength(1)
                .HasColumnName("EXPIRE_LABEL_FORMAT");
            entity.Property(e => e.ExpireTime)
                .HasMaxLength(4)
                .HasColumnName("EXPIRE_TIME");
            entity.Property(e => e.Ingredient)
                .HasMaxLength(475)
                .HasColumnName("INGREDIENT");
            entity.Property(e => e.InstructStorage)
                .HasMaxLength(66)
                .HasColumnName("INSTRUCT_STORAGE");
            entity.Property(e => e.ItemBarcode)
                .HasMaxLength(18)
                .HasColumnName("ITEM_BARCODE");
            entity.Property(e => e.ItemCls)
                .HasMaxLength(6)
                .HasColumnName("ITEM_CLS");
            entity.Property(e => e.ItemDate)
                .HasMaxLength(8)
                .HasColumnName("ITEM_DATE");
            entity.Property(e => e.ItemDept)
                .HasMaxLength(3)
                .HasColumnName("ITEM_DEPT");
            entity.Property(e => e.ItemDiv)
                .HasMaxLength(3)
                .HasColumnName("ITEM_DIV");
            entity.Property(e => e.ItemLongName)
                .HasMaxLength(30)
                .HasColumnName("ITEM_LONG_NAME");
            entity.Property(e => e.ItemLongNameChinese)
                .HasMaxLength(40)
                .HasColumnName("ITEM_LONG_NAME_CHINESE");
            entity.Property(e => e.ItemMemberSell)
                .HasMaxLength(17)
                .HasColumnName("ITEM_MEMBER_SELL");
            entity.Property(e => e.ItemNo)
                .HasMaxLength(13)
                .HasColumnName("ITEM_NO");
            entity.Property(e => e.ItemPluFlag)
                .HasMaxLength(1)
                .HasColumnName("ITEM_PLU_FLAG");
            entity.Property(e => e.ItemSell)
                .HasMaxLength(17)
                .HasColumnName("ITEM_SELL");
            entity.Property(e => e.ItemShortName)
                .HasMaxLength(15)
                .HasColumnName("ITEM_SHORT_NAME");
            entity.Property(e => e.ItemShortNameChinese)
                .HasMaxLength(20)
                .HasColumnName("ITEM_SHORT_NAME_CHINESE");
            entity.Property(e => e.ItemSubcls)
                .HasMaxLength(9)
                .HasColumnName("ITEM_SUBCLS");
            entity.Property(e => e.ItemUom)
                .HasMaxLength(5)
                .HasColumnName("ITEM_UOM");
            entity.Property(e => e.ItemUom2)
                .HasMaxLength(8)
                .HasColumnName("ITEM_UOM2");
            entity.Property(e => e.ItemVat)
                .HasMaxLength(3)
                .HasColumnName("ITEM_VAT");
            entity.Property(e => e.ItemVatFlag)
                .HasMaxLength(1)
                .HasColumnName("ITEM_VAT_FLAG");
            entity.Property(e => e.ItemWeigh)
                .HasMaxLength(1)
                .HasColumnName("ITEM_WEIGH");
            entity.Property(e => e.Kads1mFlag)
                .HasMaxLength(1)
                .HasColumnName("KADS1M_FLAG");
            entity.Property(e => e.NutriFacts)
                .HasMaxLength(100)
                .HasColumnName("NUTRI_FACTS");
            entity.Property(e => e.PrintProdFlag)
                .HasMaxLength(3)
                .HasColumnName("Print_prod_flag");
            entity.Property(e => e.RecId)
                .HasMaxLength(1)
                .HasColumnName("REC_ID");
            entity.Property(e => e.SalesTax)
                .HasMaxLength(3)
                .HasColumnName("SALES_TAX");
            entity.Property(e => e.SeasonId)
                .HasMaxLength(6)
                .HasColumnName("SEASON_ID");
            entity.Property(e => e.TaxCode)
                .HasMaxLength(5)
                .HasColumnName("TAX_CODE");
            entity.Property(e => e.TaxSign)
                .HasMaxLength(5)
                .HasColumnName("Tax_Sign");
            entity.Property(e => e.ValidToUseDate)
                .HasMaxLength(3)
                .HasColumnName("Valid_to_use_date");
            entity.Property(e => e.Warning)
                .HasMaxLength(45)
                .HasColumnName("WARNING");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.ToTable("Menu");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Action).HasMaxLength(50);
            entity.Property(e => e.Controller).HasMaxLength(50);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Icon).HasMaxLength(50);
            entity.Property(e => e.Method).HasMaxLength(250);
            entity.Property(e => e.NameEn)
                .HasMaxLength(250)
                .HasColumnName("NameEN");
            entity.Property(e => e.NameVi)
                .HasMaxLength(250)
                .HasColumnName("NameVI");
            entity.Property(e => e.ResourceId)
                .HasMaxLength(50)
                .HasColumnName("ResourceID");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<MenuAction>(entity =>
        {
            entity.ToTable("MenuAction");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Action).HasMaxLength(50);
            entity.Property(e => e.Controller).HasMaxLength(50);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Icon).HasMaxLength(50);
            entity.Property(e => e.MenuActionName).HasMaxLength(50);
            entity.Property(e => e.MenuController).HasMaxLength(50);
            entity.Property(e => e.NameEn)
                .HasMaxLength(250)
                .HasColumnName("NameEN");
            entity.Property(e => e.NameVi)
                .HasMaxLength(250)
                .HasColumnName("NameVI");
            entity.Property(e => e.ResourceId)
                .HasMaxLength(255)
                .HasColumnName("ResourceID");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<MenuRole>(entity =>
        {
            entity.ToTable("MenuRole", "acc");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.MenuRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MenuRole_Roles");
        });

        modelBuilder.Entity<MonthlyMemberSale>(entity =>
        {
            entity.ToTable("MonthlyMemberSales", "se");

            entity.HasIndex(e => new { e.ActiveFlag, e.YearMonth, e.Membercode, e.UpdateDate }, "IX_MonthlyMemberSales").IsDescending(false, true, false, true);

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Companycode).HasMaxLength(10);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Currency).HasMaxLength(10);
            entity.Property(e => e.Membercode).HasMaxLength(60);
            entity.Property(e => e.Memberlevel).HasMaxLength(27);
            entity.Property(e => e.Transactionmonth).HasMaxLength(2);
            entity.Property(e => e.Transactionyear).HasMaxLength(4);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
            entity.Property(e => e.YearMonth)
                .HasMaxLength(6)
                .HasComputedColumnSql("(concat([Transactionyear],[Transactionmonth]))", false);
        });

        modelBuilder.Entity<MpriceChange>(entity =>
        {
            entity.ToTable("MPriceChange", "core");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.FocForMemberOnly)
                .HasMaxLength(1)
                .HasColumnName("FOC_FOR_MEMBER_ONLY");
            entity.Property(e => e.FocQuantity)
                .HasMaxLength(2)
                .HasColumnName("FOC_QUANTITY");
            entity.Property(e => e.ItemNo)
                .HasMaxLength(13)
                .HasColumnName("ITEM_NO");
            entity.Property(e => e.MaximumFocQtyForMember)
                .HasMaxLength(2)
                .HasColumnName("MAXIMUM_FOC_QTY_FOR_MEMBER");
            entity.Property(e => e.MinimumQuantity)
                .HasMaxLength(2)
                .HasColumnName("MINIMUM_QUANTITY");
            entity.Property(e => e.OtherFocItemFlag)
                .HasMaxLength(1)
                .HasColumnName("OTHER_FOC_ITEM_FLAG");
            entity.Property(e => e.OtherFocSkuNumber)
                .HasMaxLength(13)
                .HasColumnName("OTHER_FOC_SKU_NUMBER");
            entity.Property(e => e.PrcDesc)
                .HasMaxLength(17)
                .HasColumnName("PRC_DESC");
            entity.Property(e => e.PrcEndDate)
                .HasMaxLength(8)
                .HasColumnName("PRC_END_DATE");
            entity.Property(e => e.PrcEndTime)
                .HasMaxLength(4)
                .HasColumnName("PRC_END_TIME");
            entity.Property(e => e.PrcNo)
                .HasMaxLength(10)
                .HasColumnName("PRC_NO");
            entity.Property(e => e.PrcSell)
                .HasMaxLength(17)
                .HasColumnName("PRC_SELL");
            entity.Property(e => e.PrcStartDate)
                .HasMaxLength(8)
                .HasColumnName("PRC_START_DATE");
            entity.Property(e => e.PrcStartTime)
                .HasMaxLength(4)
                .HasColumnName("PRC_START_TIME");
            entity.Property(e => e.PrcType)
                .HasMaxLength(1)
                .HasColumnName("PRC_TYPE");
            entity.Property(e => e.Quantity)
                .HasMaxLength(2)
                .HasColumnName("QUANTITY");
            entity.Property(e => e.QuantityDiscount)
                .HasMaxLength(8)
                .HasColumnName("QUANTITY_DISCOUNT");
            entity.Property(e => e.RecId)
                .HasMaxLength(1)
                .HasColumnName("REC_ID");
            entity.Property(e => e.StoreCode).HasMaxLength(4);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<NpriceChange>(entity =>
        {
            entity.ToTable("NPriceChange", "core");

            entity.HasIndex(e => new { e.PrcNo, e.StoreCode, e.PrcType }, "IX_NPriceChange");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.MmPromoPrice1)
                .HasMaxLength(17)
                .HasColumnName("MM_PROMO_PRICE1");
            entity.Property(e => e.MmPromoPrice2)
                .HasMaxLength(17)
                .HasColumnName("MM_PROMO_PRICE2");
            entity.Property(e => e.MmPromoPrice3)
                .HasMaxLength(17)
                .HasColumnName("MM_PROMO_PRICE3");
            entity.Property(e => e.MmPromoPrice4)
                .HasMaxLength(17)
                .HasColumnName("MM_PROMO_PRICE4");
            entity.Property(e => e.MmPromoPrice5)
                .HasMaxLength(17)
                .HasColumnName("MM_PROMO_PRICE5");
            entity.Property(e => e.MmPromoPrice6)
                .HasMaxLength(17)
                .HasColumnName("MM_PROMO_PRICE6");
            entity.Property(e => e.MmPromoQty)
                .HasMaxLength(8)
                .HasColumnName("MM_PROMO_QTY");
            entity.Property(e => e.MmPromoQty1)
                .HasMaxLength(6)
                .HasColumnName("MM_PROMO_QTY1");
            entity.Property(e => e.MmPromoQty2)
                .HasMaxLength(6)
                .HasColumnName("MM_PROMO_QTY2");
            entity.Property(e => e.MmPromoQty3)
                .HasMaxLength(6)
                .HasColumnName("MM_PROMO_QTY3");
            entity.Property(e => e.MmPromoQty4)
                .HasMaxLength(6)
                .HasColumnName("MM_PROMO_QTY4");
            entity.Property(e => e.MmPromoQty5)
                .HasMaxLength(6)
                .HasColumnName("MM_PROMO_QTY5");
            entity.Property(e => e.MmPromoQty6)
                .HasMaxLength(6)
                .HasColumnName("MM_PROMO_QTY6");
            entity.Property(e => e.MmTtPromoPrice)
                .HasMaxLength(17)
                .HasColumnName("MM_TT_PROMO_PRICE");
            entity.Property(e => e.PluCount)
                .HasMaxLength(8)
                .HasColumnName("PLU_COUNT");
            entity.Property(e => e.PrcEndDate)
                .HasMaxLength(8)
                .HasColumnName("PRC_END_DATE");
            entity.Property(e => e.PrcEndTime)
                .HasMaxLength(4)
                .HasColumnName("PRC_END_TIME");
            entity.Property(e => e.PrcNo)
                .HasMaxLength(10)
                .HasColumnName("PRC_NO");
            entity.Property(e => e.PrcStartDate)
                .HasMaxLength(8)
                .HasColumnName("PRC_START_DATE");
            entity.Property(e => e.PrcStartTime)
                .HasMaxLength(4)
                .HasColumnName("PRC_START_TIME");
            entity.Property(e => e.PrcType)
                .HasMaxLength(1)
                .HasColumnName("PRC_TYPE");
            entity.Property(e => e.PromotionDesc)
                .HasMaxLength(13)
                .HasColumnName("PROMOTION_DESC");
            entity.Property(e => e.RecId)
                .HasMaxLength(1)
                .HasColumnName("REC_ID");
            entity.Property(e => e.ShortSku1)
                .HasMaxLength(13)
                .HasColumnName("SHORT_SKU1");
            entity.Property(e => e.ShortSku2)
                .HasMaxLength(13)
                .HasColumnName("SHORT_SKU2");
            entity.Property(e => e.ShortSku3)
                .HasMaxLength(13)
                .HasColumnName("SHORT_SKU3");
            entity.Property(e => e.ShortSku4)
                .HasMaxLength(13)
                .HasColumnName("SHORT_SKU4");
            entity.Property(e => e.ShortSku5)
                .HasMaxLength(13)
                .HasColumnName("SHORT_SKU5");
            entity.Property(e => e.ShortSku6)
                .HasMaxLength(13)
                .HasColumnName("SHORT_SKU6");
            entity.Property(e => e.StoreCode).HasMaxLength(4);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("Payments", "se");

            entity.HasIndex(e => new { e.HeaderId, e.PaymentType, e.ActiveFlag }, "IX_Payments");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AuthId)
                .HasMaxLength(128)
                .HasColumnName("AuthID");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentType).HasMaxLength(10);
            entity.Property(e => e.SubOrderId)
                .HasMaxLength(30)
                .HasColumnName("SubOrderID");
            entity.Property(e => e.TotalAmountWithoutVatforTaxableItems).HasColumnName("TotalAmountWithoutVATForTaxableItems");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(30)
                .HasColumnName("TransactionID");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");

            entity.HasOne(d => d.Header).WithMany(p => p.Payments)
                .HasForeignKey(d => d.HeaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_Headers");
        });

        modelBuilder.Entity<PaymentByStore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_re_PaymentByStore");

            entity.ToTable("PaymentByStore", "re");

            entity.HasIndex(e => new { e.ActiveFlag, e.HeaderId, e.StoreCode, e.PaymentType }, "IX_re_PaymentByStore");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AuthorizationId)
                .HasMaxLength(128)
                .HasColumnName("AuthorizationID");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentType).HasMaxLength(10);
            entity.Property(e => e.StoreCode).HasMaxLength(13);
            entity.Property(e => e.TransactionId)
                .HasMaxLength(30)
                .HasColumnName("TransactionID");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url)
                .HasMaxLength(500)
                .HasColumnName("URL");
            entity.Property(e => e.UserId)
                .HasMaxLength(30)
                .HasColumnName("UserID");
        });

        modelBuilder.Entity<PaymentByStore1>(entity =>
        {
            entity.ToTable("PaymentByStore", "se");

            entity.HasIndex(e => new { e.ActiveFlag, e.HeaderId, e.StoreCode, e.PaymentType, e.UpdateDate }, "IX_PaymentByStore").IsDescending(false, false, false, false, true);

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AuthId)
                .HasMaxLength(128)
                .HasColumnName("AuthID");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentType).HasMaxLength(10);
            entity.Property(e => e.StoreCode).HasMaxLength(13);
            entity.Property(e => e.SubOrderId)
                .HasMaxLength(30)
                .HasColumnName("SubOrderID");
            entity.Property(e => e.TotalAmountWithoutVatforTaxableItems).HasColumnName("TotalAmountWithoutVATForTaxableItems");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(30)
                .HasColumnName("TransactionID");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url)
                .HasMaxLength(500)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.ToTable("PaymentTypes", "se");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsMethod).HasDefaultValue(false);
            entity.Property(e => e.Scope).HasDefaultValue(0);
            entity.Property(e => e.Type).HasMaxLength(10);
        });

        modelBuilder.Entity<PopmasterItem>(entity =>
        {
            entity.ToTable("POPMasterItems", "core");

            entity.HasIndex(e => e.Actived, "IX_ACTIVED");

            entity.HasIndex(e => e.ActiveFlag, "IX_ActiveFlag");

            entity.HasIndex(e => e.Deleted, "IX_DELETED");

            entity.HasIndex(e => e.IsTransferEsl, "IX_IsTransferESL");

            entity.HasIndex(e => e.Sku, "IX_SKU");

            entity.HasIndex(e => new { e.ActiveFlag, e.Actived, e.Deleted, e.Id, e.Sku, e.IsTransferEsl }, "IX_core_POPMasterItems");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Actived)
                .HasMaxLength(1)
                .HasColumnName("ACTIVED");
            entity.Property(e => e.AddAutoDiscItem)
                .HasMaxLength(1)
                .HasColumnName("ADD_AUTO_DISC_ITEM");
            entity.Property(e => e.AutoOrderEndDate)
                .HasMaxLength(8)
                .HasColumnName("AUTO_ORDER_END_DATE");
            entity.Property(e => e.AutoOrderStartDate)
                .HasMaxLength(8)
                .HasColumnName("AUTO_ORDER_START_DATE");
            entity.Property(e => e.AutoReplenishItem)
                .HasMaxLength(1)
                .HasColumnName("AUTO_REPLENISH_ITEM");
            entity.Property(e => e.Brand)
                .HasMaxLength(10)
                .HasColumnName("BRAND");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(9)
                .HasColumnName("CATEGORY_ID");
            entity.Property(e => e.Colour)
                .HasMaxLength(3)
                .HasColumnName("COLOUR");
            entity.Property(e => e.ColourSizeGrid)
                .HasMaxLength(1)
                .HasColumnName("COLOUR_SIZE_GRID");
            entity.Property(e => e.CostVatRate)
                .HasMaxLength(2)
                .HasColumnName("COST_VAT_RATE");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(8)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.CubicMeterM3)
                .HasMaxLength(22)
                .HasColumnName("CUBIC_METER_M3");
            entity.Property(e => e.DaisoDocSku)
                .HasMaxLength(1)
                .HasColumnName("DAISO_DOC_SKU");
            entity.Property(e => e.DateActived)
                .HasMaxLength(8)
                .HasColumnName("DATE_ACTIVED");
            entity.Property(e => e.DateCreate)
                .HasMaxLength(8)
                .HasColumnName("DATE_CREATE");
            entity.Property(e => e.DateDeleted)
                .HasMaxLength(8)
                .HasColumnName("DATE_DELETED");
            entity.Property(e => e.DateDiscontinue)
                .HasMaxLength(8)
                .HasColumnName("DATE_DISCONTINUE");
            entity.Property(e => e.Deleted)
                .HasMaxLength(1)
                .HasColumnName("DELETED");
            entity.Property(e => e.DeptId)
                .HasMaxLength(6)
                .HasColumnName("DEPT_ID");
            entity.Property(e => e.Discontinue)
                .HasMaxLength(1)
                .HasColumnName("DISCONTINUE");
            entity.Property(e => e.DivisionId)
                .HasMaxLength(3)
                .HasColumnName("DIVISION_ID");
            entity.Property(e => e.ExtraField1)
                .HasMaxLength(22)
                .HasColumnName("EXTRA_FIELD1");
            entity.Property(e => e.ExtraField2)
                .HasMaxLength(1)
                .HasColumnName("EXTRA_FIELD2");
            entity.Property(e => e.ExtraField3)
                .HasMaxLength(1)
                .HasColumnName("EXTRA_FIELD3");
            entity.Property(e => e.ExtraField4)
                .HasMaxLength(1)
                .HasColumnName("EXTRA_FIELD4");
            entity.Property(e => e.FocDescEng)
                .HasMaxLength(4000)
                .HasColumnName("FOC_DESC_ENG");
            entity.Property(e => e.FocDescVnm)
                .HasMaxLength(4000)
                .HasColumnName("FOC_DESC_VNM");
            entity.Property(e => e.FoodItem)
                .HasMaxLength(1)
                .HasColumnName("FOOD_ITEM");
            entity.Property(e => e.GrossWeightKg)
                .HasMaxLength(22)
                .HasColumnName("GROSS_WEIGHT_KG");
            entity.Property(e => e.GroupId)
                .HasMaxLength(3)
                .HasColumnName("GROUP_ID");
            entity.Property(e => e.HoldOrder)
                .HasMaxLength(1)
                .HasColumnName("HOLD_ORDER");
            entity.Property(e => e.HoldOrderEndDate)
                .HasMaxLength(8)
                .HasColumnName("HOLD_ORDER_END_DATE");
            entity.Property(e => e.HoldOrderStartDate)
                .HasMaxLength(8)
                .HasColumnName("HOLD_ORDER_START_DATE");
            entity.Property(e => e.HsCode)
                .HasMaxLength(50)
                .HasColumnName("HS_CODE");
            entity.Property(e => e.IngredientType)
                .HasMaxLength(1)
                .HasColumnName("INGREDIENT_TYPE");
            entity.Property(e => e.IsTransferEsl).HasColumnName("IsTransferESL");
            entity.Property(e => e.ItemDescEng)
                .HasMaxLength(4000)
                .HasColumnName("ITEM_DESC_ENG");
            entity.Property(e => e.ItemDescVnm)
                .HasMaxLength(4000)
                .HasColumnName("ITEM_DESC_VNM");
            entity.Property(e => e.ItemSource)
                .HasMaxLength(1)
                .HasColumnName("ITEM_SOURCE");
            entity.Property(e => e.ItemType)
                .HasMaxLength(1)
                .HasColumnName("ITEM_TYPE");
            entity.Property(e => e.Kads1mFlag)
                .HasMaxLength(1)
                .HasColumnName("KADS1M_FLAG");
            entity.Property(e => e.LineId)
                .HasMaxLength(3)
                .HasColumnName("LINE_ID");
            entity.Property(e => e.MemberDiscItem)
                .HasMaxLength(1)
                .HasColumnName("MEMBER_DISC_ITEM");
            entity.Property(e => e.MerchandisePlan)
                .HasMaxLength(1)
                .HasColumnName("MERCHANDISE_PLAN");
            entity.Property(e => e.ModifiedDate)
                .HasMaxLength(8)
                .HasColumnName("MODIFIED_DATE");
            entity.Property(e => e.MommyItem)
                .HasMaxLength(1)
                .HasColumnName("MOMMY_ITEM");
            entity.Property(e => e.MsdsCode)
                .HasMaxLength(50)
                .HasColumnName("MSDS_CODE");
            entity.Property(e => e.NeirePerc)
                .HasMaxLength(22)
                .HasColumnName("NEIRE_PERC");
            entity.Property(e => e.NetWeightKg)
                .HasMaxLength(22)
                .HasColumnName("NET_WEIGHT_KG");
            entity.Property(e => e.NonInventory)
                .HasMaxLength(1)
                .HasColumnName("NON_INVENTORY");
            entity.Property(e => e.NonInventoryCode)
                .HasMaxLength(3)
                .HasColumnName("NON_INVENTORY_CODE");
            entity.Property(e => e.NonPlu)
                .HasMaxLength(1)
                .HasColumnName("NON_PLU");
            entity.Property(e => e.OrderUom)
                .HasMaxLength(5)
                .HasColumnName("ORDER_UOM");
            entity.Property(e => e.PackItem)
                .HasMaxLength(1)
                .HasColumnName("PACK_ITEM");
            entity.Property(e => e.ParentSku)
                .HasMaxLength(13)
                .HasColumnName("PARENT_SKU");
            entity.Property(e => e.PerishItem)
                .HasMaxLength(1)
                .HasColumnName("PERISH_ITEM");
            entity.Property(e => e.PluDescEng)
                .HasMaxLength(1200)
                .HasColumnName("PLU_DESC_ENG");
            entity.Property(e => e.PluDescVnm)
                .HasMaxLength(1200)
                .HasColumnName("PLU_DESC_VNM");
            entity.Property(e => e.Pop1DescEng)
                .HasMaxLength(4000)
                .HasColumnName("POP1_DESC_ENG");
            entity.Property(e => e.Pop1DescVnm)
                .HasMaxLength(4000)
                .HasColumnName("POP1_DESC_VNM");
            entity.Property(e => e.Pop2DescEng)
                .HasMaxLength(4000)
                .HasColumnName("POP2_DESC_ENG");
            entity.Property(e => e.Pop2DescVnm)
                .HasMaxLength(4000)
                .HasColumnName("POP2_DESC_VNM");
            entity.Property(e => e.Pop3DescVnm)
                .HasMaxLength(50)
                .HasColumnName("POP3_DESC_VNM");
            entity.Property(e => e.PurchaseMethod)
                .HasMaxLength(1)
                .HasColumnName("PURCHASE_METHOD");
            entity.Property(e => e.RetailUom)
                .HasMaxLength(5)
                .HasColumnName("RETAIL_UOM");
            entity.Property(e => e.RetailVatCode)
                .HasMaxLength(2)
                .HasColumnName("RETAIL_VAT_CODE");
            entity.Property(e => e.RetailVatRate)
                .HasMaxLength(22)
                .HasColumnName("RETAIL_VAT_RATE");
            entity.Property(e => e.Returnable)
                .HasMaxLength(1)
                .HasColumnName("RETURNABLE");
            entity.Property(e => e.SalesTaxRate)
                .HasMaxLength(2)
                .HasColumnName("SALES_TAX_RATE");
            entity.Property(e => e.SeasonId)
                .HasMaxLength(6)
                .HasColumnName("SEASON_ID");
            entity.Property(e => e.SellingPoint1)
                .HasMaxLength(150)
                .HasColumnName("SELLING_POINT1");
            entity.Property(e => e.SellingPoint2)
                .HasMaxLength(150)
                .HasColumnName("SELLING_POINT2");
            entity.Property(e => e.SellingPoint3)
                .HasMaxLength(150)
                .HasColumnName("SELLING_POINT3");
            entity.Property(e => e.SellingPoint4)
                .HasMaxLength(150)
                .HasColumnName("SELLING_POINT4");
            entity.Property(e => e.SellingPoint5)
                .HasMaxLength(150)
                .HasColumnName("SELLING_POINT5");
            entity.Property(e => e.SizeId)
                .HasMaxLength(5)
                .HasColumnName("SIZE_ID");
            entity.Property(e => e.Sku)
                .HasMaxLength(13)
                .HasColumnName("SKU");
            entity.Property(e => e.StdCostUom)
                .HasMaxLength(100)
                .HasColumnName("STD_COST_UOM");
            entity.Property(e => e.Style)
                .HasMaxLength(100)
                .HasColumnName("STYLE");
            entity.Property(e => e.SubCategory)
                .HasMaxLength(2)
                .HasColumnName("SUB_CATEGORY");
            entity.Property(e => e.SugUnitRetailWovat)
                .HasMaxLength(22)
                .HasColumnName("SUG_UNIT_RETAIL_WOVAT");
            entity.Property(e => e.SugUnitRetailWvat)
                .HasMaxLength(22)
                .HasColumnName("SUG_UNIT_RETAIL_WVAT");
            entity.Property(e => e.SuperSaverItem)
                .HasMaxLength(1)
                .HasColumnName("SUPER_SAVER_ITEM");
            entity.Property(e => e.SupplierContract)
                .HasMaxLength(10)
                .HasColumnName("SUPPLIER_CONTRACT");
            entity.Property(e => e.SupplierId)
                .HasMaxLength(10)
                .HasColumnName("SUPPLIER_ID");
            entity.Property(e => e.Ticket1DescEng)
                .HasMaxLength(4000)
                .HasColumnName("TICKET1_DESC_ENG");
            entity.Property(e => e.Ticket1DescVnm)
                .HasMaxLength(4000)
                .HasColumnName("TICKET1_DESC_VNM");
            entity.Property(e => e.Ticket2DescEng)
                .HasMaxLength(4000)
                .HasColumnName("TICKET2_DESC_ENG");
            entity.Property(e => e.Ticket2DescVnm)
                .HasMaxLength(4000)
                .HasColumnName("TICKET2_DESC_VNM");
            entity.Property(e => e.TicketSku)
                .HasMaxLength(13)
                .HasColumnName("TICKET_SKU");
            entity.Property(e => e.TicketType)
                .HasMaxLength(3)
                .HasColumnName("TICKET_TYPE");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url)
                .HasMaxLength(1000)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<PriceChange>(entity =>
        {
            entity.ToTable("PriceChange", "core", tb => tb.HasTrigger("trg_PriceChange_UpdateComputedColumns"));

            entity.HasIndex(e => e.ActiveFlag, "IX_ActiveFlag");

            entity.HasIndex(e => e.ItemNo, "IX_ITEM_NO");

            entity.HasIndex(e => e.IsTransferEsl, "IX_IsTransferESL");

            entity.HasIndex(e => e.PrcNo, "IX_PRC_NO");

            entity.HasIndex(e => e.T4vvflag, "IX_PriceChange_T4VVFlag");

            entity.HasIndex(e => e.StoreCodeSku, "IX_SkuStoreCode");

            entity.HasIndex(e => e.StoreCode, "IX_StoreCode");

            entity.HasIndex(e => e.StoreCodeSkuPrcNo, "IX_StoreCodeSkuPRC_NO");

            entity.HasIndex(e => new { e.IsT4vv, e.T4vvflag, e.StartDateTime, e.EndDateTime }, "IX_T4VV").IsDescending(true, false, true, true);

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.EndDateTime).HasColumnType("datetime");
            entity.Property(e => e.IsT4vv).HasColumnName("IsT4VV");
            entity.Property(e => e.IsTransferEsl)
                .HasDefaultValue(false)
                .HasColumnName("IsTransferESL");
            entity.Property(e => e.ItemNo)
                .HasMaxLength(13)
                .HasColumnName("ITEM_NO");
            entity.Property(e => e.PrcDiscAmt)
                .HasMaxLength(12)
                .HasColumnName("PRC_DISC_AMT");
            entity.Property(e => e.PrcDiscRate)
                .HasMaxLength(6)
                .HasColumnName("PRC_DISC_RATE");
            entity.Property(e => e.PrcEndDate)
                .HasMaxLength(8)
                .HasColumnName("PRC_END_DATE");
            entity.Property(e => e.PrcEndTime)
                .HasMaxLength(4)
                .HasColumnName("PRC_END_TIME");
            entity.Property(e => e.PrcNo)
                .HasMaxLength(10)
                .HasColumnName("PRC_NO");
            entity.Property(e => e.PrcSell)
                .HasMaxLength(17)
                .HasColumnName("PRC_SELL");
            entity.Property(e => e.PrcStartDate)
                .HasMaxLength(8)
                .HasColumnName("PRC_START_DATE");
            entity.Property(e => e.PrcStartTime)
                .HasMaxLength(4)
                .HasColumnName("PRC_START_TIME");
            entity.Property(e => e.PrcType)
                .HasMaxLength(6)
                .HasColumnName("PRC_TYPE");
            entity.Property(e => e.RecId)
                .HasMaxLength(1)
                .HasColumnName("REC_ID");
            entity.Property(e => e.StartDateTime).HasColumnType("datetime");
            entity.Property(e => e.StoreCode).HasMaxLength(10);
            entity.Property(e => e.StoreCodeSku)
                .HasMaxLength(23)
                .HasComputedColumnSql("(concat([StoreCode],[ITEM_NO]))", false);
            entity.Property(e => e.StoreCodeSkuPrcNo)
                .HasMaxLength(33)
                .HasComputedColumnSql("(concat([StoreCode],[ITEM_NO],[PRC_NO]))", false)
                .HasColumnName("StoreCodeSkuPRC_NO");
            entity.Property(e => e.T4vvflag).HasColumnName("T4VVFlag");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<PriceChangeHistory>(entity =>
        {
            entity.ToTable("PriceChangeHistory", "core");

            entity.HasIndex(e => new { e.ItemNo, e.StoreCode, e.StartDateTime, e.EndDateTime, e.UpdateDate }, "IX_PriceChangeHistory").IsDescending(false, false, true, true, true);

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.EndDateTime).HasColumnType("datetime");
            entity.Property(e => e.IsT4vv).HasColumnName("IsT4VV");
            entity.Property(e => e.IsTransferEsl).HasColumnName("IsTransferESL");
            entity.Property(e => e.ItemNo)
                .HasMaxLength(13)
                .HasColumnName("ITEM_NO");
            entity.Property(e => e.PrcDiscAmt)
                .HasMaxLength(12)
                .HasColumnName("PRC_DISC_AMT");
            entity.Property(e => e.PrcDiscRate)
                .HasMaxLength(6)
                .HasColumnName("PRC_DISC_RATE");
            entity.Property(e => e.PrcEndDate)
                .HasMaxLength(8)
                .HasColumnName("PRC_END_DATE");
            entity.Property(e => e.PrcEndTime)
                .HasMaxLength(4)
                .HasColumnName("PRC_END_TIME");
            entity.Property(e => e.PrcNo)
                .HasMaxLength(10)
                .HasColumnName("PRC_NO");
            entity.Property(e => e.PrcSell)
                .HasMaxLength(17)
                .HasColumnName("PRC_SELL");
            entity.Property(e => e.PrcStartDate)
                .HasMaxLength(8)
                .HasColumnName("PRC_START_DATE");
            entity.Property(e => e.PrcStartTime)
                .HasMaxLength(4)
                .HasColumnName("PRC_START_TIME");
            entity.Property(e => e.PrcType)
                .HasMaxLength(6)
                .HasColumnName("PRC_TYPE");
            entity.Property(e => e.RecId)
                .HasMaxLength(1)
                .HasColumnName("REC_ID");
            entity.Property(e => e.Source).HasMaxLength(4000);
            entity.Property(e => e.StartDateTime).HasColumnType("datetime");
            entity.Property(e => e.StoreCode).HasMaxLength(10);
            entity.Property(e => e.StoreCodeSku)
                .HasMaxLength(23)
                .HasComputedColumnSql("(concat([StoreCode],[ITEM_NO]))", false);
            entity.Property(e => e.StoreCodeSkuPrcNo)
                .HasMaxLength(33)
                .HasComputedColumnSql("(concat([StoreCode],[ITEM_NO],[PRC_NO]))", false)
                .HasColumnName("StoreCodeSkuPRC_NO");
            entity.Property(e => e.T4vvflag).HasColumnName("T4VVFlag");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<Pricing>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.ToTable("Pricing", "core");

            entity.HasIndex(e => e.ActiveFlag, "IX_ActiveFlag");

            entity.HasIndex(e => new { e.Sku, e.StoreCode, e.ActiveFlag }, "IX_Pricing")
                .IsUnique()
                .HasFillFactor(90);

            entity.HasIndex(e => e.StoreCodeSku, "IX_SkuStoreCode");

            entity.HasIndex(e => e.Url, "IX_URL");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.EffectDate).HasMaxLength(12);
            entity.Property(e => e.Sku).HasMaxLength(13);
            entity.Property(e => e.StoreCode).HasMaxLength(10);
            entity.Property(e => e.StoreCodeSku)
                .HasMaxLength(23)
                .HasComputedColumnSql("(concat([StoreCode],[Sku]))", false);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url)
                .HasMaxLength(400)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<PricingHistory>(entity =>
        {
            entity.ToTable("PricingHistory", "core");

            entity.HasIndex(e => new { e.UpdateDate, e.StoreCode, e.Sku, e.Action, e.CreateBy, e.UpdateBy, e.CreateDate }, "IX-PricingHistory").IsDescending(true, false, false, false, false, false, true);

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ExpiredDate).HasMaxLength(12);
            entity.Property(e => e.Sku).HasMaxLength(13);
            entity.Property(e => e.Source).HasMaxLength(4000);
            entity.Property(e => e.StoreCode).HasMaxLength(10);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.ToTable("Products", "prod");

            entity.HasIndex(e => e.ActiveFlag, "IX_ActiveFlag");

            entity.HasIndex(e => e.IsTransferEsl, "IX_IsTransferESL");

            entity.HasIndex(e => e.Sku, "IX_Sku");

            entity.HasIndex(e => e.Tracking, "IX_Tracking");

            entity.HasIndex(e => new { e.Upc, e.Barcode, e.TaxRate }, "IX_Upc");

            entity.HasIndex(e => e.Id, "NonClusteredIndex-20240611-142442");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.B2btaxRate).HasColumnName("B2BTaxRate");
            entity.Property(e => e.Barcode).HasMaxLength(100);
            entity.Property(e => e.CategoryCode).HasMaxLength(100);
            entity.Property(e => e.Code).HasMaxLength(100);
            entity.Property(e => e.CompanyCode).HasMaxLength(100);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerScope).HasMaxLength(500);
            entity.Property(e => e.Grade).HasMaxLength(250);
            entity.Property(e => e.Height).HasMaxLength(250);
            entity.Property(e => e.IsTransferEsl).HasColumnName("IsTransferESL");
            entity.Property(e => e.Length).HasMaxLength(250);
            entity.Property(e => e.Origin).HasMaxLength(250);
            entity.Property(e => e.Seodescription).HasColumnName("SEODescription");
            entity.Property(e => e.Seokeywords).HasColumnName("SEOKeywords");
            entity.Property(e => e.Seotitle).HasColumnName("SEOTitle");
            entity.Property(e => e.Size).HasMaxLength(250);
            entity.Property(e => e.Sku).HasMaxLength(13);
            entity.Property(e => e.Slug).HasMaxLength(4000);
            entity.Property(e => e.StoreCode).HasMaxLength(10);
            entity.Property(e => e.Tracking).HasMaxLength(400);
            entity.Property(e => e.UnitType).HasMaxLength(250);
            entity.Property(e => e.Upc).HasMaxLength(100);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
            entity.Property(e => e.VariantType).HasMaxLength(50);
            entity.Property(e => e.Volume).HasMaxLength(250);
            entity.Property(e => e.Weight).HasMaxLength(250);
            entity.Property(e => e.Width).HasMaxLength(250);
        });

        modelBuilder.Entity<ProductByStore>(entity =>
        {
            entity.ToTable("ProductByStores", "prod");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Barcode).HasMaxLength(100);
            entity.Property(e => e.CategoryCode).HasMaxLength(100);
            entity.Property(e => e.Code).HasMaxLength(100);
            entity.Property(e => e.MallCode).HasMaxLength(100);
            entity.Property(e => e.Sku).HasMaxLength(13);
            entity.Property(e => e.StoreCode).HasMaxLength(10);
            entity.Property(e => e.Upc).HasMaxLength(100);
        });

        modelBuilder.Entity<ProductFeed>(entity =>
        {
            entity.ToTable("ProductFeed", "prod");

            entity.HasIndex(e => e.ActiveFlag, "IX_ActiveFlag");

            entity.HasIndex(e => e.Gtin, "IX_GTIN");

            entity.HasIndex(e => e.Id, "IX_Id");

            entity.HasIndex(e => e.IsTransferEsl, "IX_IsTransferESL");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Availability)
                .HasMaxLength(50)
                .HasColumnName("AVAILABILITY");
            entity.Property(e => e.Brand)
                .HasMaxLength(1000)
                .HasColumnName("BRAND");
            entity.Property(e => e.Condition)
                .HasMaxLength(50)
                .HasColumnName("CONDITION");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomLabel0)
                .HasMaxLength(1000)
                .HasColumnName("CUSTOM_LABEL_0");
            entity.Property(e => e.CustomLabel1)
                .HasMaxLength(1000)
                .HasColumnName("CUSTOM_LABEL_1");
            entity.Property(e => e.CustomLabel2)
                .HasMaxLength(1000)
                .HasColumnName("CUSTOM_LABEL_2");
            entity.Property(e => e.CustomLabel3)
                .HasMaxLength(1000)
                .HasColumnName("CUSTOM_LABEL_3");
            entity.Property(e => e.CustomLabel4)
                .HasMaxLength(1000)
                .HasColumnName("CUSTOM_LABEL_4");
            entity.Property(e => e.DeepLink)
                .HasMaxLength(1000)
                .HasColumnName("DEEP_LINK");
            entity.Property(e => e.Description).HasColumnName("DESCRIPTION");
            entity.Property(e => e.GoogleProductCategory)
                .HasMaxLength(1000)
                .HasColumnName("GOOGLE_PRODUCT_CATEGORY");
            entity.Property(e => e.Gtin)
                .HasMaxLength(100)
                .HasColumnName("GTIN");
            entity.Property(e => e.ImageLink).HasColumnName("IMAGE_LINK");
            entity.Property(e => e.IsTransferEsl).HasColumnName("IsTransferESL");
            entity.Property(e => e.Link)
                .HasMaxLength(1000)
                .HasColumnName("LINK");
            entity.Property(e => e.Price)
                .HasMaxLength(30)
                .HasColumnName("PRICE");
            entity.Property(e => e.ProductDetail)
                .HasMaxLength(2000)
                .HasColumnName("PRODUCT_DETAIL");
            entity.Property(e => e.ProductType)
                .HasMaxLength(1000)
                .HasColumnName("PRODUCT_TYPE");
            entity.Property(e => e.SalePrice)
                .HasMaxLength(30)
                .HasColumnName("SALE_PRICE");
            entity.Property(e => e.SkuId)
                .HasMaxLength(10)
                .HasColumnName("SKU_ID");
            entity.Property(e => e.Title)
                .HasMaxLength(2000)
                .HasColumnName("TITLE");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ProductHistory>(entity =>
        {
            entity.ToTable("ProductHistory", "prod");

            entity.HasIndex(e => new { e.UpdateDate, e.Sku, e.Action, e.CreateDate, e.CreateBy, e.UpdateBy }, "IX-ProductHistory").IsDescending(true, false, false, true, false, false);

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.B2btaxRate).HasColumnName("B2BTaxRate");
            entity.Property(e => e.Barcode).HasMaxLength(100);
            entity.Property(e => e.CategoryCode).HasMaxLength(100);
            entity.Property(e => e.Code).HasMaxLength(100);
            entity.Property(e => e.CompanyCode).HasMaxLength(100);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerScope).HasMaxLength(500);
            entity.Property(e => e.Grade).HasMaxLength(250);
            entity.Property(e => e.Height).HasMaxLength(250);
            entity.Property(e => e.IsTransferEsl).HasColumnName("IsTransferESL");
            entity.Property(e => e.Origin).HasMaxLength(250);
            entity.Property(e => e.Seodescription).HasColumnName("SEODescription");
            entity.Property(e => e.Seokeywords).HasColumnName("SEOKeywords");
            entity.Property(e => e.Seotitle).HasColumnName("SEOTitle");
            entity.Property(e => e.Size).HasMaxLength(250);
            entity.Property(e => e.Sku).HasMaxLength(13);
            entity.Property(e => e.Slug).HasMaxLength(4000);
            entity.Property(e => e.Source).HasMaxLength(4000);
            entity.Property(e => e.UnitType).HasMaxLength(250);
            entity.Property(e => e.Upc).HasMaxLength(100);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.VariantType).HasMaxLength(50);
            entity.Property(e => e.Volume).HasMaxLength(250);
            entity.Property(e => e.Weight).HasMaxLength(250);
        });

        modelBuilder.Entity<ProductHistoryAction>(entity =>
        {
            entity.ToTable("ProductHistoryAction");

            entity.HasIndex(e => e.Value, "IX_ProductHistoryAction");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Action).HasMaxLength(250);
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<ProductInfo>(entity =>
        {
            entity.ToTable("ProductInfo", "prod");

            entity.HasIndex(e => e.ActiveFlag, "IX_ActiveFlag");

            entity.HasIndex(e => new { e.Sku, e.StoreCode, e.ActiveFlag }, "IX_ProductInfo")
                .IsUnique()
                .HasFillFactor(90);

            entity.HasIndex(e => e.Sku, "IX_Sku");

            entity.HasIndex(e => new { e.StoreCode, e.MallCode }, "IX_StoreCode");

            entity.HasIndex(e => e.StoreCodeSku, "IX_StoreCode_Sku");

            entity.HasIndex(e => e.Url, "IX_URL");

            entity.HasIndex(e => new { e.ActiveFlag, e.UpdateDate, e.StoreCode, e.Sku, e.IsTransfer, e.IsNew, e.IsPublished, e.IsSyncProfit, e.Fulfillment, e.StoreCodeSku, e.QuickDelivery }, "IX__ProductInfo").IsDescending(false, true, false, false, false, false, false, false, false, false, false);

            entity.HasIndex(e => e.Id, "NonClusteredIndex-20240611-143502");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.MallCode).HasMaxLength(100);
            entity.Property(e => e.Sku).HasMaxLength(13);
            entity.Property(e => e.StoreCode).HasMaxLength(10);
            entity.Property(e => e.StoreCodeSku)
                .HasMaxLength(23)
                .HasComputedColumnSql("(concat([StoreCode],[Sku]))", false);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url)
                .HasMaxLength(400)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<ProductInfoHistory>(entity =>
        {
            entity.ToTable("ProductInfoHistory", "prod");

            entity.HasIndex(e => new { e.UpdateDate, e.StoreCode, e.Sku, e.Action, e.CreateBy, e.UpdateBy, e.CreateDate }, "IX-ProductInfoHistory").IsDescending(true, false, false, false, false, false, true);

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.MallCode).HasMaxLength(100);
            entity.Property(e => e.Sku).HasMaxLength(13);
            entity.Property(e => e.Source).HasMaxLength(4000);
            entity.Property(e => e.StoreCode).HasMaxLength(10);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ProductInfoUploadError>(entity =>
        {
            entity.ToTable("ProductInfoUploadErrors", "prod");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Sku).HasMaxLength(13);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");

            entity.HasOne(d => d.Upload).WithMany(p => p.ProductInfoUploadErrors)
                .HasForeignKey(d => d.UploadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductInfoUploadErrors_ProductInfoUploadMonitors");
        });

        modelBuilder.Entity<ProductInfoUploadMonitor>(entity =>
        {
            entity.ToTable("ProductInfoUploadMonitors", "prod");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Curent)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FileExt)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<ProductsBk>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.ToTable("Products_BK", "prod");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Barcode).HasMaxLength(100);
            entity.Property(e => e.CategoryCode).HasMaxLength(100);
            entity.Property(e => e.Code).HasMaxLength(100);
            entity.Property(e => e.CompanyCode).HasMaxLength(100);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerScope).HasMaxLength(500);
            entity.Property(e => e.Grade).HasMaxLength(250);
            entity.Property(e => e.Height).HasMaxLength(250);
            entity.Property(e => e.IsTransferEsl).HasColumnName("IsTransferESL");
            entity.Property(e => e.Length).HasMaxLength(250);
            entity.Property(e => e.Origin).HasMaxLength(250);
            entity.Property(e => e.Seodescription).HasColumnName("SEODescription");
            entity.Property(e => e.Seokeywords).HasColumnName("SEOKeywords");
            entity.Property(e => e.Seotitle).HasColumnName("SEOTitle");
            entity.Property(e => e.Size).HasMaxLength(250);
            entity.Property(e => e.Sku).HasMaxLength(13);
            entity.Property(e => e.Slug).HasMaxLength(4000);
            entity.Property(e => e.StoreCode).HasMaxLength(10);
            entity.Property(e => e.UnitType).HasMaxLength(250);
            entity.Property(e => e.Upc).HasMaxLength(100);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
            entity.Property(e => e.VariantType).HasMaxLength(50);
            entity.Property(e => e.Volume).HasMaxLength(250);
            entity.Property(e => e.Weight).HasMaxLength(250);
            entity.Property(e => e.Width).HasMaxLength(250);
        });

        modelBuilder.Entity<ProductsInSale>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Products_InSale");

            entity.Property(e => e.Sku)
                .HasMaxLength(13)
                .HasColumnName("SKU");
        });

        modelBuilder.Entity<ProductsRealDatum>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Products_RealData");

            entity.Property(e => e.Sku)
                .HasMaxLength(13)
                .HasColumnName("SKU");
            entity.Property(e => e.StartPublishDate).HasColumnType("datetime");
            entity.Property(e => e.StoreCode).HasMaxLength(10);
        });

        modelBuilder.Entity<ProfitFile>(entity =>
        {
            entity.ToTable("ProfitFiles", "core");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Ext)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.StoreCode).HasMaxLength(10);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.ToTable("Promotion", "se");

            entity.HasIndex(e => new { e.ItemId, e.ActiveFlag }, "IX_Promotion");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Pnlallocation)
                .HasMaxLength(100)
                .HasColumnName("PNLAllocation");
            entity.Property(e => e.TransactionType).HasMaxLength(10);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");

            entity.HasOne(d => d.Item).WithMany(p => p.Promotions)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Promotion_Items");
        });

        modelBuilder.Entity<Promotion1>(entity =>
        {
            entity.ToTable("Promotions", "sto");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Pnlallocation)
                .HasMaxLength(50)
                .HasColumnName("PNLAllocation");
            entity.Property(e => e.StoreCode).HasMaxLength(10);
            entity.Property(e => e.TransactionType).HasMaxLength(50);
        });

        modelBuilder.Entity<PromotionEsl>(entity =>
        {
            entity.ToTable("PromotionESL", "core");

            entity.HasIndex(e => new { e.UpdateDate, e.ActiveFlag, e.Sku, e.StoreCode, e.IsTransferEsl, e.Edlpflag, e.StartDate, e.EndDate, e.StartTime, e.EndTime, e.CreateBy, e.UpdateBy }, "IX_PromotionESL").IsDescending(true, false, false, false, false, false, false, true, false, true, false, false);

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Edlpflag).HasColumnName("EDLPFlag");
            entity.Property(e => e.EndDateTime)
                .HasComputedColumnSql("(CONVERT([datetime],[EndDate])+CONVERT([datetime],[EndTime]))", true)
                .HasColumnType("datetime");
            entity.Property(e => e.IsTransferEsl).HasColumnName("IsTransferESL");
            entity.Property(e => e.Sku)
                .HasMaxLength(13)
                .HasColumnName("SKU");
            entity.Property(e => e.StartDateTime)
                .HasComputedColumnSql("(CONVERT([datetime],[StartDate])+CONVERT([datetime],[StartTime]))", true)
                .HasColumnType("datetime");
            entity.Property(e => e.StoreCode).HasMaxLength(10);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<PromotionEslhistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PromotionESLHistory");

            entity.ToTable("PromotionESLHistory", "core");

            entity.HasIndex(e => new { e.Sku, e.StoreCode, e.StartDate, e.EndDate, e.StartTime, e.EndTime, e.CreateDate, e.UpdateDate, e.Action, e.Source }, "IX_PromotionESLHistory").IsDescending(false, false, false, true, false, true, false, true, false, false);

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Edlpflag).HasColumnName("EDLPFlag");
            entity.Property(e => e.EndDateTime)
                .HasComputedColumnSql("(CONVERT([datetime],[EndDate])+CONVERT([datetime],[EndTime]))", true)
                .HasColumnType("datetime");
            entity.Property(e => e.IsTransferEsl).HasColumnName("IsTransferESL");
            entity.Property(e => e.Sku)
                .HasMaxLength(13)
                .HasColumnName("SKU");
            entity.Property(e => e.Source).HasMaxLength(4000);
            entity.Property(e => e.StartDateTime)
                .HasComputedColumnSql("(CONVERT([datetime],[StartDate])+CONVERT([datetime],[StartTime]))", true)
                .HasColumnType("datetime");
            entity.Property(e => e.StoreCode).HasMaxLength(10);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url)
                .HasMaxLength(4000)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Province");

            entity.Property(e => e.CityCode)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.CityName).HasMaxLength(250);
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.DistrictName).HasMaxLength(250);
            entity.Property(e => e.EnglishName).HasMaxLength(250);
            entity.Property(e => e.Level).HasMaxLength(250);
            entity.Property(e => e.WardCode)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.WardName).HasMaxLength(250);
        });

        modelBuilder.Entity<ReceiptNumber>(entity =>
        {
            entity.ToTable("ReceiptNumbers", "re");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CurrentDate).HasMaxLength(8);
            entity.Property(e => e.Posnumber).HasColumnName("POSNumber");
            entity.Property(e => e.StoreCode).HasMaxLength(10);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<RecordRefund>(entity =>
        {
            entity.ToTable("RecordRefund", "re");

            entity.HasIndex(e => new { e.IsTransferSap, e.IsTransferS4, e.HeaderId, e.StoreCode }, "IX_RecordRefund");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.IsTransferS4).HasDefaultValue(true);
            entity.Property(e => e.IsTransferSap)
                .HasDefaultValue(true)
                .HasColumnName("IsTransferSAP");
            entity.Property(e => e.ReceiptNumber).HasMaxLength(20);
            entity.Property(e => e.StoreCode).HasMaxLength(100);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<RecordRefundFile>(entity =>
        {
            entity.ToTable("RecordRefundFiles", "re");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Ext).HasMaxLength(10);
            entity.Property(e => e.StoreCode).HasMaxLength(100);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<RecordSale>(entity =>
        {
            entity.ToTable("RecordSales", "se");

            entity.HasIndex(e => e.ActualOrderNumber, "IX_ActualOrderNumber");

            entity.HasIndex(e => new { e.IsTransferSap, e.IsTransferS4, e.HeaderId, e.StoreCode }, "IX_RecordSales");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ActualOrderNumber)
                .HasMaxLength(23)
                .HasComputedColumnSql("(case when len([OrderNumber])>(7) then substring([OrderNumber],(9),len([OrderNumber])) else concat('B2B',[OrderNumber]) end)", false);
            entity.Property(e => e.BillNumber).HasMaxLength(20);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.IsTransferS4).HasDefaultValue(true);
            entity.Property(e => e.IsTransferSap)
                .HasDefaultValue(true)
                .HasColumnName("IsTransferSAP");
            entity.Property(e => e.OrderNumber).HasMaxLength(20);
            entity.Property(e => e.PaymentType).HasMaxLength(100);
            entity.Property(e => e.SalesDate).HasMaxLength(8);
            entity.Property(e => e.SalesTime).HasMaxLength(4);
            entity.Property(e => e.StoreCode).HasMaxLength(100);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<RecordSaleFile>(entity =>
        {
            entity.ToTable("RecordSaleFiles", "se");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Ext)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.StoreCode).HasMaxLength(100);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<RecordSalesBk>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("RecordSales_BK", "se");

            entity.Property(e => e.BillNumber).HasMaxLength(20);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.OrderNumber).HasMaxLength(20);
            entity.Property(e => e.PaymentType).HasMaxLength(100);
            entity.Property(e => e.SalesDate).HasMaxLength(8);
            entity.Property(e => e.SalesTime).HasMaxLength(4);
            entity.Property(e => e.StoreCode).HasMaxLength(100);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<RecordSalesBk2610>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("RecordSales_BK2610", "se");

            entity.Property(e => e.BillNumber).HasMaxLength(20);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.OrderNumber).HasMaxLength(20);
            entity.Property(e => e.PaymentType).HasMaxLength(100);
            entity.Property(e => e.SalesDate).HasMaxLength(8);
            entity.Property(e => e.SalesTime).HasMaxLength(4);
            entity.Property(e => e.StoreCode).HasMaxLength(100);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<RefundHeader>(entity =>
        {
            entity.ToTable("RefundHeaders", "re");

            entity.HasIndex(e => e.ActualOrderNumber, "IX_ActualOrderNumber");

            entity.HasIndex(e => new { e.ActiveFlag, e.CustomerType }, "IX_reHeaders");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ActualOrderNumber)
                .HasMaxLength(23)
                .HasComputedColumnSql("(case when len([OrderNumber])>(7) then substring([OrderNumber],(9),len([OrderNumber])) else concat('B2B',[OrderNumber]) end)", false);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("CustomerID");
            entity.Property(e => e.CustomerType).HasMaxLength(1);
            entity.Property(e => e.FoxtrotUserId)
                .HasMaxLength(32)
                .HasColumnName("FoxtrotUserID");
            entity.Property(e => e.MallCode).HasMaxLength(100);
            entity.Property(e => e.OrderNumber).HasMaxLength(20);
            entity.Property(e => e.ReasonCode).HasMaxLength(10);
            entity.Property(e => e.RefundDate).HasMaxLength(8);
            entity.Property(e => e.RefundTime).HasMaxLength(40);
            entity.Property(e => e.SalesDate).HasMaxLength(8);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<RefundHeader1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_so_RefundHeaders");

            entity.ToTable("RefundHeaders", "so");

            entity.HasIndex(e => e.SaleOrderId, "IX_RefundHeaders_SaleOrderId");

            entity.HasIndex(e => new { e.ActiveFlag, e.StoreCode, e.RefundDate, e.OrderNumber, e.StatusId, e.ReasonCode }, "IX_so_RefundHeaders").IsDescending(false, false, true, false, false, false);

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerName).HasMaxLength(255);
            entity.Property(e => e.OrderNumber).HasMaxLength(20);
            entity.Property(e => e.ReasonCode).HasMaxLength(10);
            entity.Property(e => e.RefundDate).HasColumnType("datetime");
            entity.Property(e => e.RefundTime).HasMaxLength(4);
            entity.Property(e => e.SalesDate).HasMaxLength(8);
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.StoreCode).HasMaxLength(100);
            entity.Property(e => e.TotalAmountWithVat)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("TotalAmountWithVAT");
            entity.Property(e => e.TotalAmountWithoutVat)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("TotalAmountWithoutVAT");
            entity.Property(e => e.TotalVatamount)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("TotalVATAmount");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");

            entity.HasOne(d => d.SaleOrder).WithMany(p => p.RefundHeader1s)
                .HasForeignKey(d => d.SaleOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefundHeaders_Headers");
        });

        modelBuilder.Entity<RefundInvoice>(entity =>
        {
            entity.ToTable("RefundInvoices", "re");

            entity.HasIndex(e => new { e.HeaderId, e.SerialNo, e.Number }, "IX_re_RefundInvoices");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.Company).HasMaxLength(500);
            entity.Property(e => e.Cqtcode)
                .HasMaxLength(250)
                .HasColumnName("CQTCode");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerName).HasMaxLength(500);
            entity.Property(e => e.Number).HasMaxLength(20);
            entity.Property(e => e.SerialNo).HasMaxLength(20);
            entity.Property(e => e.StoreCode).HasMaxLength(4);
            entity.Property(e => e.TaxCode).HasMaxLength(100);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<RefundInvoice1>(entity =>
        {
            entity.HasKey(e => e.InvoiceKey).HasName("PK_so_RefundInvoices");

            entity.ToTable("RefundInvoices", "so");

            entity.HasIndex(e => new { e.HeaderId, e.InvoiceId, e.InvoiceNumber, e.InvoiceReceiveNumber }, "IX_so_RefundInvoices");

            entity.Property(e => e.InvoiceKey).ValueGeneratedNever();
            entity.Property(e => e.CompanyName).HasMaxLength(500);
            entity.Property(e => e.Cqtcode)
                .HasMaxLength(250)
                .HasColumnName("CQTCode");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerName).HasMaxLength(500);
            entity.Property(e => e.IntegrateKey).HasMaxLength(255);
            entity.Property(e => e.InvoiceId)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("InvoiceID");
            entity.Property(e => e.InvoiceIssuedDate).HasMaxLength(10);
            entity.Property(e => e.InvoiceNumber).HasMaxLength(20);
            entity.Property(e => e.InvoiceReceiveNumber).HasMaxLength(50);
            entity.Property(e => e.InvoiceSeries).HasMaxLength(20);
            entity.Property(e => e.InvoiceTemplateCode).HasMaxLength(50);
            entity.Property(e => e.RootIntegrateKey).HasMaxLength(255);
            entity.Property(e => e.RootInvoiceNumber).HasMaxLength(20);
            entity.Property(e => e.RootInvoiceSeries).HasMaxLength(20);
            entity.Property(e => e.RootInvoiceTemplateCode).HasMaxLength(50);
            entity.Property(e => e.StoreCode).HasMaxLength(4);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
            entity.Property(e => e.VatCode).HasMaxLength(100);

            entity.HasOne(d => d.Header).WithMany(p => p.RefundInvoice1s)
                .HasForeignKey(d => d.HeaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefundInvoices_RefundHeaders");
        });

        modelBuilder.Entity<RefundItem>(entity =>
        {
            entity.ToTable("RefundItems", "re");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Sku).HasMaxLength(13);
            entity.Property(e => e.StoreCode).HasMaxLength(4);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
            entity.Property(e => e.Vatamount).HasColumnName("VATAmount");
            entity.Property(e => e.Vatcode).HasColumnName("VATCode");

            entity.HasOne(d => d.Header).WithMany(p => p.RefundItems)
                .HasForeignKey(d => d.HeaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefundItems_RefundHeaders");
        });

        modelBuilder.Entity<RefundItem1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_so_RefundItems");

            entity.ToTable("RefundItems", "so");

            entity.HasIndex(e => new { e.HeaderId, e.Sku }, "IX_so_RefundItems");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.IsTaxB2b).HasColumnName("IsTaxB2B");
            entity.Property(e => e.LineNumber).HasDefaultValue(1);
            entity.Property(e => e.Pnlallocation)
                .HasMaxLength(100)
                .HasColumnName("PNLAllocation");
            entity.Property(e => e.Poprice).HasColumnName("POPrice");
            entity.Property(e => e.Sku).HasMaxLength(13);
            entity.Property(e => e.TransactionType).HasMaxLength(10);
            entity.Property(e => e.UnitType).HasMaxLength(50);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
            entity.Property(e => e.Vatamount).HasColumnName("VATAmount");
            entity.Property(e => e.Vatcode).HasColumnName("VATCode");

            entity.HasOne(d => d.Header).WithMany(p => p.RefundItem1s)
                .HasForeignKey(d => d.HeaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefundItems_RefundHeaders");
        });

        modelBuilder.Entity<RefundPayment>(entity =>
        {
            entity.ToTable("RefundPayments", "re");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AuthorizationId)
                .HasMaxLength(128)
                .HasColumnName("AuthorizationID");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentType).HasMaxLength(10);
            entity.Property(e => e.TransactionId)
                .HasMaxLength(30)
                .HasColumnName("TransactionID");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
            entity.Property(e => e.UserId)
                .HasMaxLength(30)
                .HasColumnName("UserID");

            entity.HasOne(d => d.Header).WithMany(p => p.RefundPayments)
                .HasForeignKey(d => d.HeaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefundPayments_RefundHeaders");
        });

        modelBuilder.Entity<RefundPromotion>(entity =>
        {
            entity.ToTable("RefundPromotion", "re");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Pnlallocation)
                .HasMaxLength(100)
                .HasColumnName("PNLAllocation");
            entity.Property(e => e.TransactionType).HasMaxLength(10);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");

            entity.HasOne(d => d.Item).WithMany(p => p.RefundPromotions)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefundPromotion_RefundItems");
        });

        modelBuilder.Entity<RefundReason>(entity =>
        {
            entity.HasKey(e => e.ReasonCode);

            entity.ToTable("RefundReason", "so");

            entity.Property(e => e.ReasonCode).HasMaxLength(10);
            entity.Property(e => e.ReasonName).HasMaxLength(155);
        });

        modelBuilder.Entity<RefundSku>(entity =>
        {
            entity.ToTable("RefundSku", "se");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Sku).HasMaxLength(13);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<Resource>(entity =>
        {
            entity.HasKey(e => e.ResourceId).HasFillFactor(90);

            entity.Property(e => e.ResourceId)
                .HasMaxLength(250)
                .HasColumnName("ResourceID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DefaultText0).HasMaxLength(400);
            entity.Property(e => e.DefaultText1).HasMaxLength(400);
            entity.Property(e => e.DefaultText2).HasMaxLength(400);
            entity.Property(e => e.DefaultText3).HasMaxLength(400);
            entity.Property(e => e.DefaultText4).HasMaxLength(400);
            entity.Property(e => e.DefaultText5).HasMaxLength(400);
            entity.Property(e => e.ResourceText0).HasMaxLength(400);
            entity.Property(e => e.ResourceText1).HasMaxLength(400);
            entity.Property(e => e.ResourceText2).HasMaxLength(400);
            entity.Property(e => e.ResourceText3).HasMaxLength(400);
            entity.Property(e => e.ResourceText4).HasMaxLength(400);
            entity.Property(e => e.ResourceText5).HasMaxLength(400);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Roles", "acc");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<SkuMapping>(entity =>
        {
            entity.ToTable("SkuMapping", "core");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.MallCode)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<SkuUploadError>(entity =>
        {
            entity.ToTable("SkuUploadErrors", "sku");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.MallCode)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Sku).HasMaxLength(13);
            entity.Property(e => e.StoreCode).HasMaxLength(10);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");

            entity.HasOne(d => d.Upload).WithMany(p => p.SkuUploadErrors)
                .HasForeignKey(d => d.UploadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UploadErrors_UploadMonitors");
        });

        modelBuilder.Entity<SkuUploadMonitor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UploadMonitors");

            entity.ToTable("SkuUploadMonitors", "sku");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Curent)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FileExt)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => new { e.Sku, e.StoreCode }).HasName("PK_Stock_1");

            entity.ToTable("Stock", "core");

            entity.HasIndex(e => e.ActiveFlag, "IX_ActiveFlag");

            entity.HasIndex(e => e.SkuStoreCode, "IX_Stock");

            entity.Property(e => e.Sku).HasMaxLength(13);
            entity.Property(e => e.StoreCode).HasMaxLength(10);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.OosDate)
                .HasMaxLength(8)
                .HasColumnName("OOS_Date");
            entity.Property(e => e.RecordFlag).HasMaxLength(1);
            entity.Property(e => e.SkuStoreCode)
                .HasMaxLength(23)
                .HasComputedColumnSql("(concat([StoreCode],[Sku]))", false);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.Id).HasFillFactor(90);

            entity.ToTable("Stores", "sto");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.MallCode).HasMaxLength(50);
            entity.Property(e => e.MerchantTax).HasMaxLength(250);
            entity.Property(e => e.Posnumber1).HasColumnName("POSNumber1");
            entity.Property(e => e.Posnumber2).HasColumnName("POSNumber2");
            entity.Property(e => e.StoreType).HasDefaultValue(1);
            entity.Property(e => e.TaxName).HasMaxLength(255);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<SubClassMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SubClass__3214EC0796BE598C");

            entity.ToTable("SubClassMaster", "core");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AcsFlag)
                .HasMaxLength(1)
                .HasColumnName("ACS_FLAG");
            entity.Property(e => e.BclsName)
                .HasMaxLength(30)
                .HasColumnName("BCLS_NAME");
            entity.Property(e => e.Cls)
                .HasMaxLength(6)
                .HasColumnName("CLS");
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.MbrDisc)
                .HasMaxLength(3)
                .HasColumnName("MBR_DISC");
            entity.Property(e => e.MommyDisc)
                .HasMaxLength(3)
                .HasColumnName("MOMMY_DISC");
            entity.Property(e => e.Perishable)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.RecId)
                .HasMaxLength(1)
                .HasColumnName("REC_ID");
            entity.Property(e => e.StoreCode).HasMaxLength(4);
            entity.Property(e => e.SubCls)
                .HasMaxLength(9)
                .HasColumnName("SUB_CLS");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<SystemLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasFillFactor(90);

            entity.HasIndex(e => e.EventResult, "IX_EventResult");

            entity.HasIndex(e => e.FuncDateTime, "IX_FuncDateTime").IsDescending();

            entity.HasIndex(e => e.Module, "IX_Module");

            entity.HasIndex(e => new { e.FuncDateTime, e.Module, e.UserFunction, e.EventResult, e.UserId }, "IX_SystemLogs").IsDescending(true, false, false, true, false);

            entity.HasIndex(e => e.UserFunction, "IX_UserFunction");

            entity.Property(e => e.LogId).ValueGeneratedNever();
            entity.Property(e => e.FuncDateTime).HasColumnType("datetime");
            entity.Property(e => e.Module).HasMaxLength(100);
            entity.Property(e => e.UserId).HasMaxLength(50);
            entity.Property(e => e.Wsname).HasColumnName("WSName");
        });

        modelBuilder.Entity<SystemLogAttachment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo_SystemLogAttachment");

            entity.ToTable("SystemLogAttachment");

            entity.HasIndex(e => e.LogId, "IX_SystemLogAttachment");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<SystemSetting>(entity =>
        {
            entity.ToTable("SystemSetting");

            entity.HasIndex(e => new { e.ActiveFlag, e.Type, e.Layout, e.Code, e.Name }, "IX_SystemSetting");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(100);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(1000);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<TblBoxed>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_BOXED");

            entity.Property(e => e.Sku)
                .HasMaxLength(50)
                .HasColumnName("SKU");
            entity.Property(e => e.StoreCode).HasMaxLength(10);
        });

        modelBuilder.Entity<UploadError>(entity =>
        {
            entity.ToTable("UploadErrors", "prod");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");

            entity.HasOne(d => d.Upload).WithMany(p => p.UploadErrors)
                .HasForeignKey(d => d.UploadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UploadErrors_UploadMonitors");
        });

        modelBuilder.Entity<UploadFile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_so_UploadFile");

            entity.ToTable("UploadFile", "so");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<UploadMonitor>(entity =>
        {
            entity.ToTable("UploadMonitors", "prod");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Curent)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FileExt)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<UserDepartment>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.DeptId });

            entity.ToTable("UserDepartments", "acc");

            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.JobDescription).HasMaxLength(150);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UserInfo");

            entity.ToTable("UserInfos", "acc");

            entity.HasIndex(e => e.UserId, "IX_UserInfo");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Ext).HasMaxLength(10);
            entity.Property(e => e.FullName).HasMaxLength(300);
            entity.Property(e => e.HomePhone).HasMaxLength(20);
            entity.Property(e => e.LanguageCode).HasMaxLength(50);
            entity.Property(e => e.Mobile).HasMaxLength(20);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
            entity.Property(e => e.UserId).HasMaxLength(200);
        });

        modelBuilder.Entity<UserPermissionDept>(entity =>
        {
            entity.ToTable("UserPermissionDept", "acc");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("UserRoles", "acc");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Roles");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_UserInfos");
        });

        modelBuilder.Entity<UserStore>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.StoreId });

            entity.ToTable("UserStores", "acc");

            entity.HasIndex(e => new { e.ActiveFlag, e.UserName, e.UserId, e.StoreId }, "IX_UserStores");

            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(150);
            entity.Property(e => e.StoreCode).HasMaxLength(150);
            entity.Property(e => e.StoreName).HasMaxLength(150);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Url).HasColumnName("URL");
            entity.Property(e => e.UserName).HasMaxLength(150);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
