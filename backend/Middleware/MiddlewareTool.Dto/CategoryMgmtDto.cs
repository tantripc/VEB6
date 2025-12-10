namespace MiddlewareTool.Dto
{
    public class CategoryMgmtDto
    {
        public class CategoryDto : BaseDto
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Path { get; set; }
            public Nullable<bool> IsTransfer { get; set; }
            public Nullable<bool> IsNew { get; set; }
            public Nullable<Guid> ParentId { get; set; }
            public ICollection<MappingDto> Mappings { get; set; }
            public List<CategoryMasterDto> Masters { get; set; }
        }

        public class CategoryCompactDto
        {
            public Guid Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
        }

        public class CategoryBoxedDto
        {
            public string category_id { get; set; }
            public string category_path { get; set; }
        }
        public class MappingDto : BaseDto
        {
            public Guid CategoryId { get; set; }
            public string CategoryMasterId { get; set; }
        }
        public class CategoryMasterDto : MasterDto
        {
            public string Id { get; set; }
            public int DepartmentId { get; set; }
            public Nullable<bool> AutoPA { get; set; }
            public string PosFlag { get; set; }
            public Nullable<bool> PwpExclusion { get; set; }
            public Nullable<int> AgeStockRetenPeriod { get; set; }
            public Nullable<bool> MbrDiscFlag { get; set; }
            public Nullable<int> MbrDiscPerc { get; set; }
            public Nullable<int> MommyDiscPerc { get; set; }
            public string HsCode { get; set; }
            public string MsdsCode { get; set; }

            public string DepartmentName { get; set; }
            public int GroupId { get; set; }
            public string GroupName { get; set; }
            public int DivisionId { get; set; }
            public string DivisionName { get; set; }
            public int LineId { get; set; }
            public string LineName { get; set; }
        }
        public class DepartmentMasterDto : MasterDto
        {
            public int Id { get; set; }
            public int GroupId { get; set; }
            public string GroupName { get; set; }
            public int DivisionId { get; set; }
            public string DivisionName { get; set; }
            public int LineId { get; set; }
            public string LineName { get; set; }
        }
        public class GroupMasterDto : MasterDto
        {
            public int Id { get; set; }
            public int DivisionId { get; set; }
            public string DivisionName { get; set; }
            public int LineId { get; set; }
            public string LineName { get; set; }
        }
        public class DivisionMasterDto : MasterDto
        {
            public int Id { get; set; }
            public int LineId { get; set; }
            public string LineName { get; set; }
        }
        public class LineMasterDto : MasterDto
        {
            public int Id { get; set; }
        }
    }
}
