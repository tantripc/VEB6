using System;

namespace MiddlewareTool.Common
{
    public sealed class AppType
    {
        public static readonly string AUTHORIZATION = "Authorization";
        public static readonly Guid RoleAdminID = new Guid("11111111-1111-1111-1111-111111111111");
        public enum UserRole : byte
        {
            SuperAdmin = 0,
            Admin = 1
        }
        public enum Layout : byte
        {
            System = 0,
            MailServer = 1,
            EmailTemplate = 2
        }
        public enum Setting : byte
        {
            MailCc = 1,
            MailTo = 2,
            MailBcc = 3,
            MailHost = 4,
            MailPort = 5,
            MailSSL = 6,
            MailAccount = 7,
            MailPassword = 8,
            ProfitAlertSuccess = 9,
            ProfitAlertError = 10,
            BoxedAlertSuccess = 11,
            BoxedAlertError = 12,
            ManualExportZip_Enable = 13,
            EInvoice = 14,
            Product = 15,
            Service = 16,
            BrandImageMap = 17,
            ManualExportZip_NextDayBy = 18,
            SAP_BTP_Sale = 19,
            SAP_BTP_MonthlyMember = 20,
            SAP_S4HAHA = 21,
            ESLAlertSuccess = 22,
            ESLAlertError = 23,
            CustomerType = 24,
            AutoUpdateDatabaseVersion = 99
        }
        public enum ProfitFile : byte
        {
            MasterItem,
            CategoryMaster,
            PriceChange,
            GroupPriceChange,
            Stock
        }
        public enum BoxedFile
        {
            Category,
            Catalog_Enrichment,
            Pricing,
            Inventory,
            Inventory_Delta,
            Products,
            TRANS_ECRECORDSALES,
            TRANS_ECRECORDREFUND,
        }
        public enum ESLFile
        {
            M,
            INV
        }
        public enum BoxedFolder
        {
            category,
            catalogEnrichment,
            pricing,
            inventory,
            inventoryDelta,
            newProduct
        }
        public enum PaymentType
        {
            ECDSLS,
            ECDRTN,
            ECMSLS,
            ECMRTN,
            COC
        }
        public enum DeliveryCode
        {
            INHOUSE,
            BOPIS,
            AHAMOVE,
            NINJAVAN,
            CARRIER
        }
        public enum ProductType
        {
            Consignment = 1,
            Outright = 2,
            Manufacturing = 3,
            ConsignmentEC = 4
        }

        public enum FulfillmentTypes
        {
            Express = 1,
            Parcel = 0,
            BOPIS = 2,
        }

        public class SelectSetting
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
        public class FulfillmentTypesSetting : SelectSetting
        {
        }

        public class CustomerTypeSetting : SelectSetting
        {
        }

        public enum ImportProductTypes
        {
            ProductType = 0,
            Products = 1,
            ManualStock = 2,
        }

        public enum StoreTypes
        {
            Boxed = 1,
            B2B = 2,
            All = 3
        }

        public enum CustomerScopes
        {
            All = 0,
            B2C = 2,
            B2B = 1,
            Optional = 3
        }

        public class VariantTypes
        {
            public const string dry_good = "dry good";
            public const string fresh_food = "fresh food";
        }

        public enum TransactionTypes
        {
            P,
            MM,
            PD,
            PWP
        }

        public class S4HANATypes
        {
            public const string Sale = "01";
            public const string Refund = "09";
        }
        public class S4HANAAssignments
        {
            public const string Sale = "SLS";
            public const string Refund = "SV1RTN";
        }
        public class S4HANAStatuses
        {
            public const string Success = "S";
            public const string Error = "E";
        }
        public enum PaymentTypeScopes
        {
            All = 0,
            RecordSale = 1,
            RecordRefund = 2
        }
        public enum RecordType
        {
            Boxed,
            Profit
        }
        public enum CustomerTypes
        {
            B2C = 1,
            B2B = 2,
        }
        public class PaymentTypeCustomerTypes
        {
            /// <summary>
            /// B2B Online
            /// </summary>
            public const string B2BOnline = "B";
            /// <summary>
            /// E-commerce (EC)
            /// </summary>
            public const string EC = "C";
        }
        public enum PaymentMethods : byte
        {
            Original = 1,
            Voucher = 2,
            Promotion = 3,
            Others = 9,
        }
    }
}
