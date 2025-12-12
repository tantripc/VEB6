using AutoMapper;
using MiddlewareTool.Dto;
using MiddlewareTool.Entities.Models;
using static MiddlewareTool.Dto.CategoryMgmtDto;
using static MiddlewareTool.Dto.StoreMgmtDto;
using static MiddlewareTool.Dto.SystemMgmtDto;
using static MiddlewareTool.Dto.UserMgmtDto;
using static MiddlewareTool.Dto.CoreDto;
using static MiddlewareTool.Dto.ProductMgmtDto;
using static MiddlewareTool.Dto.RefundDto;
using static MiddlewareTool.Dto.SaleDto;

namespace MiddlewareTool
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Mapping Entity & EntityDto

            #region System Management

            CreateMap<Resource, ResourceDto>().ReverseMap();
            CreateMap<SystemLog, SystemLogDto>().ReverseMap();
            CreateMap<SystemLogAttachment, SystemLogAttachmentDto>().ReverseMap();
            CreateMap<UserInfo, UserInfoDto>().ReverseMap();
            CreateMap<Menu, MenuDto>().ReverseMap();
            CreateMap<SystemSetting, SystemSettingDto>().ReverseMap();
            CreateMap<Mailbox, MailboxDto>().ReverseMap();
            CreateMap<DiscountType, DiscountTypeDto>().ReverseMap();

            #endregion

            #region User Management  

            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<UserPermissionDept, UserPermissionDeptDto>().ReverseMap();
            CreateMap<Department, DepartmentDto>().ReverseMap();

            #endregion

            #region Category Management

            CreateMap<LineMaster, LineMasterDto>().ReverseMap();
            CreateMap<DivisionMaster, DivisionMasterDto>().ReverseMap();
            CreateMap<GroupMaster, GroupMasterDto>().ReverseMap();
            CreateMap<DepartmentMaster, DepartmentMasterDto>().ReverseMap();
            CreateMap<CategoryMaster, CategoryMasterDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Mapping, MappingDto>().ReverseMap();
            CreateMap<Category, CategoriesDto>().ReverseMap();

            #endregion

            #region Store Management

            CreateMap<Store, StoreDto>().ReverseMap();
            CreateMap<Store, StoreCompactDto>();

            #endregion

            #region Core

            CreateMap<MasterItem, MasterItemDto>().ReverseMap();
            CreateMap<PriceChange, PriceChangeDto>().ReverseMap();
            CreateMap<GroupPriceChange, GroupPriceChangeDto>().ReverseMap();
            CreateMap<Stock, StockDto>().ReverseMap();

            #endregion

            #region Sale Management

            CreateMap<Header, HeaderDto>().ReverseMap();
            CreateMap<Item, ItemDto>().ReverseMap();
            CreateMap<ItemForDelivery, ItemForDeliveryDto>().ReverseMap();
            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<Invoice, InvoiceDto>().ReverseMap();
            CreateMap<Promotion, PromotionDto>().ReverseMap();
            CreateMap<Delivery, DeliveryDto>().ReverseMap();
            CreateMap<PaymentType, PaymentTypeDto>().ReverseMap();
            CreateMap<DeliverySku, DeliverySkuDto>().ReverseMap();
            CreateMap<RefundSku, RefundSkuDto>().ReverseMap();
            CreateMap<RecordSaleFile, RecordSaleFileDto>().ReverseMap();
            CreateMap<RecordSale, RecordSaleDto>().ReverseMap();

            #endregion

            #region Refund Management

            CreateMap<RefundHeader, RefundHeaderDto>().ReverseMap();
            CreateMap<RefundItem, RefundItemDto>().ForMember(x => x.Promotions, y => y.MapFrom(z => z.RefundPromotions)).ReverseMap();
            CreateMap<RefundInvoice, RefundInvoiceDto>().ReverseMap();
            CreateMap<RefundPaymentDto, RefundPaymentDto>().ReverseMap();
            CreateMap<RecordRefundFile, RecordRefundFileDto>().ReverseMap();
            CreateMap<RecordRefund, RecordRefundDto>().ReverseMap();

            #endregion
            #region Product Management
            CreateMap<ProductInfo, InventoryAndPublishDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            #endregion
            #region History
            CreateMap<Product, ProductHistoryDto>().ReverseMap();
            CreateMap<ProductHistory, ProductHistoryDto>().ReverseMap();
            CreateMap<ProductInfo, ProductInfoHistoryDto>().ReverseMap();
            CreateMap<ProductInfoHistory, ProductInfoHistoryDto>().ReverseMap();
            CreateMap<InventoryHistory, InventoryHistoryDto>().ReverseMap();
            CreateMap<Inventory, InventoryHistoryDto>().ReverseMap();
            CreateMap<Inventory, InventoryHistory>().ReverseMap();
            CreateMap<PricingHistory, PricingHistoryDto>().ReverseMap();
            CreateMap<Pricing, PricingHistoryDto>().ReverseMap();
            #endregion
            #region SaleOrder
            //CreateMap<Item, SaleOrderItemDto>()
            //    .ForMember(x => x.Quantity, y => y.MapFrom(z => z.QuantitySold))
            //    .ForMember(x => x.Price, y => y.MapFrom(z => z.SellingPrice))
            //    .ForMember(x => x.UnitType, y => y.MapFrom(z => "CAI"))
            //    .ForMember(x => x.WarningMess, y => y.MapFrom(z => ""))
            //    .ForMember(x => x.LineNumber, y => y.MapFrom(z => 0))
            //    .ForMember(x => x.ErrorMess, y => y.MapFrom(z => ""))
            //    .ForMember(x => x.UnitPriceWithoutVAT, y => y.MapFrom(z => 0))
            //    .ForMember(x => x.POPrice, y => y.MapFrom(z => z.SellingPrice))
            //    .ForMember(x => x.PromotionAmount, y => y.MapFrom(z => 0))
            //    .ForMember(x => x.PNLAllocation, y => y.MapFrom(z => 0))
            //    .ForMember(x => x.TransactionType, y => y.MapFrom(z => ""))
            //    .ForMember(x => x.IsTaxB2B, y => y.MapFrom(z => false))
            //    .ForMember(x => x.OrderNumber, y => y.MapFrom(z => 0))
            //    .ForMember(x => x.Description, y => y.MapFrom(z => ""))
            //    .ReverseMap();

            //CreateMap<Header, SaleOrderDto>()
            //    .ForMember(x => x.Items, y => y.MapFrom(z => z.Items))
            //    .ForMember(x => x.Invoices, y => y.MapFrom(z => z.Invoices))
            //    .PreserveReferences()
            //    .ReverseMap();

            //            Unmapped properties: Item
            //Name
            //Quantity
            //Price
            //UnitType
            //WarningMess
            //LineNumber
            //ErrorMess
            //UnitPriceWithoutVAT
            //POPrice
            //PromotionAmount
            //PNLAllocation
            //TransactionType
            //IsTaxB2B
            //OrderNumber
            //Description

            CreateMap<Entities.Models.Business, BusinessDto>().ReverseMap();
            CreateMap<Header1, SaleOrderDto>()
                .ForMember(x => x.Items, y => y.MapFrom(z => z.Item1s))
                .ForMember(x => x.Invoices, y => y.MapFrom(z => z.Invoice1s))
                .ForMember(x => x.RefundHeaders, y => y.MapFrom(z => z.RefundHeader1s))
                .PreserveReferences()
                .ReverseMap();
            CreateMap<Header1, SaleOrderCompactDto>().ReverseMap();
            CreateMap<Item1, SaleOrderItemDto>().ReverseMap();
            CreateMap<Invoice1, SaleOrderInvoiceDto>().ReverseMap();
            CreateMap<UploadFile, UploadFileDto>().ReverseMap();

            CreateMap<RefundHeader1, SaleOrderRefundDto>()
                .ForMember(x => x.Items, y => y.MapFrom(z => z.RefundItem1s))
                .ForMember(x => x.Invoices, y => y.MapFrom(z => z.RefundInvoice1s))
                .ForMember(x => x.Root, y => y.MapFrom(z => z.SaleOrder))
                .ReverseMap();
            CreateMap<RefundItem1, SaleOrderRefundItemDto>().ReverseMap();
            CreateMap<B2BTax, B2BTaxDto>().ReverseMap();

            CreateMap<RefundInvoice1, SaleOrderRefundInvoiceDto>().ReverseMap();
            CreateMap<RefundReasonDto, RefundReason>().ReverseMap();
            #endregion

            #region SAP_BTP
            CreateMap<MonthlyMemberSale, MonthlyMemberSaleDto>().ReverseMap();
            #endregion

            #region PromotionESL
            CreateMap<PromotionESL, PromotionDto>().ReverseMap();
            #endregion

            #region PaymentType
            CreateMap<PaymentTypeMapping, PaymentTypeMappingDto>().ReverseMap();
            #endregion

            #region Refund COD
            CreateMap<RefundHeader1, SaleOrderRefundCODDto>()
                .ForMember(x => x.Items, y => y.MapFrom(z => z.RefundItem1s))
                .ForMember(x => x.Invoices, y => y.MapFrom(z => z.RefundInvoice1s))
                .ReverseMap();
            #endregion

            #endregion

            #region Mapping EnityDto & EntityModel

            #region System Management

            CreateMap<ResourceDto, ResourceModel>().ReverseMap();
            CreateMap<SystemLogDto, SystemLogModel>().ReverseMap();
            CreateMap<UserInfoDto, UserInfoModel>().ReverseMap();
            CreateMap<UserInfoDto, AccountInfoModel>().ReverseMap();
            CreateMap<SystemSettingDto, SystemSettingModel>().ReverseMap();
            CreateMap<SystemSettingDto, ListSystemSettingModel>().ReverseMap();
            CreateMap<SystemSettingDto, SystemSettingModel>().ReverseMap();
            CreateMap<SystemSettingDto, SystemSettingUpdateModel>().ReverseMap();

            #endregion

            #region User Management

            CreateMap<UserDto, UserModel>().ReverseMap();
            CreateMap<RoleDto, RoleModel>().ReverseMap();
            CreateMap<UserDto, AccountInfoModel>().ReverseMap();
            CreateMap<UsersDto, AccountInfoModel>().ReverseMap();
            CreateMap<UserInfoDto, AccountInfoModel>().ReverseMap();
            CreateMap<UserInfoDto, ListUserInfoModel>().ReverseMap();
            CreateMap<DepartmentDto, DepartmentModel>().ReverseMap();
            CreateMap<DepartmentDto, DepartmentUpdateModel>().ReverseMap();
            #endregion

            #region Category Management

            CreateMap<LineMasterDto, LineMasterModel>().ReverseMap();
            CreateMap<LineMasterDto, LineMasterUpdateModel>().ReverseMap();

            CreateMap<DivisionMasterDto, DivisionMasterModel>().ReverseMap();
            CreateMap<DivisionMasterDto, DivisionMasterUpdateModel>().ReverseMap();

            CreateMap<GroupMasterDto, GroupMasterModel>().ReverseMap();
            CreateMap<GroupMasterDto, GroupMasterUpdateModel>().ReverseMap();

            CreateMap<DepartmentMasterDto, DepartmentMasterModel>().ReverseMap();
            CreateMap<DepartmentMasterDto, DepartmentMasterUpdateModel>().ReverseMap();

            CreateMap<CategoryMasterDto, CategoryMasterModel>().ReverseMap();
            CreateMap<CategoryMasterDto, CategoryMasterUpdateModel>().ReverseMap();

            #endregion

            #region Store Management

            CreateMap<StoreDto, StoreModel>().ReverseMap();
            CreateMap<MallDto, MallModel>().ReverseMap();
            CreateMap<ProvinceDto, ProvinceModel>().ReverseMap();

            #endregion

            #region Product Management
            CreateMap<ProductDto, ProductUpdateModel>().ReverseMap();
            CreateMap<InventoryAndPublishDto, InventoryAndPublishModel>().ReverseMap();
            #endregion

            #region Update Results
            CreateMap<UploadMonitor, UpdateResultsDto>();
            CreateMap<UploadMonitor, UpdateResultsDto>().ReverseMap();
            CreateMap<UploadMonitorModel, UpdateResultsDto>();
            CreateMap<UploadMonitorModel, UpdateResultsDto>().ReverseMap();

            CreateMap<ProductInfoUploadMonitor, UpdateResultsDto>();
            CreateMap<ProductInfoUploadMonitor, UpdateResultsDto>().ReverseMap();
            #endregion

            #region Sale Order /Refund
            CreateMap<SaleOrderHeaderModel, SaleOrderDto>().ReverseMap();
            CreateMap<SaleOrderModel, SaleOrderDto>().ReverseMap();
            CreateMap<SaleOrderItemModel, SaleOrderItemDto>().ReverseMap();
            CreateMap<BusinessModel, BusinessDto>().ReverseMap();

            CreateMap<RefundHeaderModel, SaleOrderDto>().ReverseMap();
            CreateMap<RefundHeaderModel, SaleOrderRefundDto>().ReverseMap();
            CreateMap<RefundItemModel, SaleOrderRefundItemDto>().ReverseMap();
            CreateMap<B2BTaxDto, B2BTaxModel>().ReverseMap();
            CreateMap<RefundInvoiceModel, SaleOrderRefundInvoiceDto>().ReverseMap();

            CreateMap<RefundHeaderCODModel, SaleOrderRefundDto>().ReverseMap();
            CreateMap<RefundHeaderCODModel, SaleOrderRefundCODDto>()
                .ReverseMap();

            #endregion

            #endregion
        }
    }
}
