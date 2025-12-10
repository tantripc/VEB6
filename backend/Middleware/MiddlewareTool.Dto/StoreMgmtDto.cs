using static MiddlewareTool.Common.AppType;

namespace MiddlewareTool.Dto
{
    public class StoreMgmtDto
    {
        public class StoreDto : MasterDto
        {
            public Guid Id { get; set; }
            public Guid MallId { get; set; }
            public string MallCode { get; set; }
            public string MallName { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            //public string AddressLine { get; set; }
            //public string City { get; set; }
            //public string CityName { get; set; }
            //public string District { get; set; }
            //public string DistrictName { get; set; }
            //public string Ward { get; set; }
            //public string WardName { get; set; }
            public string MerchantId { get; set; }
            public string MerchantTax { get; set; }
            public string TaxName { get; set; }
            public string TaxAddress { get; set; }
            public Nullable<int> POSNumber1 { get; set; }
            public Nullable<int> POSNumber2 { get; set; }
            public int StoreType { get; set; }
            public string StoreTypeName
            {
                get
                {
                    switch (StoreType)
                    {
                        case (int)StoreTypes.Boxed:
                            return StoreTypes.Boxed.ToString();
                        case (int)StoreTypes.B2B:
                            return StoreTypes.B2B.ToString();
                        case (int)StoreTypes.All:
                            return StoreTypes.All.ToString();
                    }
                    return "";
                }
            }
            public bool IsShow { get; set; }
            public bool ApplyPromotion { get; set; }
            //public int Ranking { get; set; }
            //public List<MallDto> LstMall { get; set; }
            //public string CityCode { get; set; }
            //public string DistrictCode { get; set; }
            //public string WardCode { get; set; }
            //public List<ProvinceDto> LstProvince { get; set; }
            //public List<ProvinceDto> LstDistrict { get; set; }
            //public List<ProvinceDto> LstWard { get; set; }
        }
        public class StoreCompactDto
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string MallName { get; set; }
        }
        public class MallDto : BaseDto
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string AddressLine { get; set; }
            public string CityName { get; set; }
            public string CityCode { get; set; }
            public string DistrictName { get; set; }
            public string DistrictCode { get; set; }
            public string WardName { get; set; }
            public string WardCode { get; set; }
            public string MerchantId { get; set; }
        }
        public class ProvinceDto
        {
            public string CityCode { get; set; }
            public string CityName { get; set; }
            public string DistrictCode { get; set; }
            public string DistrictName { get; set; }
            public string WardCode { get; set; }
            public string WardName { get; set; }
            public string Level { get; set; }
            public string EnglishName { get; set; }
        }
        public class StoreCreationDto
        {
            public string MallCode { get; set; }
            public string MallName { get; set; }
            public string MallPhone { get; set; }
            public string MallEmail { get; set; }
            public string MallAddressLine { get; set; }
            public string MallCity { get; set; }
            public string MallDistrict { get; set; }
            public string MallWard { get; set; }
            public string MallMerchantId { get; set; }

            public string StoreCode1 { get; set; }
            public string StoreName1 { get; set; }
            public string StoreTaxName1 { get; set; }
            public string StoreTaxAddress1 { get; set; }
            public string StoreMerchantTaxId1 { get; set; }

            public string StoreCode2 { get; set; }
            public string StoreName2 { get; set; }
            public string StoreTaxName2 { get; set; }
            public string StoreTaxAddress2 { get; set; }
            public string StoreMerchantTaxId2 { get; set; }

            public string StoreCode3 { get; set; }
            public string StoreName3 { get; set; }
            public string StoreTaxName3 { get; set; }
            public string StoreTaxAddress3 { get; set; }
            public string StoreMerchantTaxId3 { get; set; }

            public string StoreCode4 { get; set; }
            public string StoreName4 { get; set; }
            public string StoreTaxName4 { get; set; }
            public string StoreTaxAddress4 { get; set; }
            public string StoreMerchantTaxId4 { get; set; }
        }

        public class PromotionStoreDto
        {
            public Guid Id { get; set; }
            public string StoreCode { get; set; }
            /// <summary>
            /// Promotion Amount > 0 => false
            /// Promotion Amount < 0 => true
            /// </summary>
            public bool CasePromotion { get; set; }
            public string PNLAllocation { get; set; }
            public string TransactionType { get; set; }
            public string TransactionTypeName
            {
                get
                {
                    switch (TransactionType)
                    {
                        case "P":
                            return "P";
                        case "MM":
                            return "MM";
                        case "PD":
                            return "PD";
                        case "PWP":
                            return "PWP";
                    }
                    return "";
                }
            }
        }
    }
}
