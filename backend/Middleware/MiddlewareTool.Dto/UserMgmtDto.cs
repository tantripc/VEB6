using MiddlewareTool.Common;
using System.ComponentModel.DataAnnotations;
using static MiddlewareTool.Dto.StoreMgmtDto;

namespace MiddlewareTool.Dto
{
    public class UserMgmtDto
    {
        public class UserInfoDto : BaseDto
        {
            public string UserId { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Mobile { get; set; }
            public byte[] Picture { get; set; }
            public string Address { get; set; }
            public string HomePhone { get; set; }
            public bool IsActive { get; set; }
            public string Ext { get; set; }
            public DateOnly? Birthday { get; set; }
            public bool? Gender { get; set; }
            public string LanguageCode { get; set; }
            public string RoleName { get; set; }
            public string DisplayStoreCode { get; set; }
            public bool IsSuperAdmin { get; set; }
            public Guid? DepartmentId { get; set; }
            public List<DeptDto> LstDepartment { get; set; }
            public List<StoreDto> LstStore { get; set; }
            public List<string> StoreIdSelect { get; set; }
            public UserInfoDto()
            {
                LstDepartment = new List<DeptDto>();
            }
            public string CreateByFullName { get; set; }
            public string UpdateByFullName { get; set; }
        }
        public class UsersDto
        {
            public Guid Id { get; set; }
            public List<Guid> LstDepartmentId { get; set; }
            public string UserName { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Mobile { get; set; }
        }
        public class MenuDto : BaseDto
        {
            public Guid? ParentId { get; set; }
            public bool IsParent { get; set; }
            public string Controller { get; set; }
            public string Action { get; set; }
            public string NameVI { get; set; }
            public string NameEN { get; set; }
            public string ResourceID { get; set; }
            public string NameFunction { get; set; }
            public int Type { get; set; }
            public AppMenu.Status MenuStatus { get; set; }
            public string Icon { get; set; }
            public string Method { get; set; }
        }
        public class MenuRoleDto : BaseDto
        {
            public Guid RoleId { get; set; }
            public Guid MenuId { get; set; }
            public Guid? MenuActionId { get; set; }
            public string UserName { get; set; }
            public AppMenu.Role Type { get; set; }
        }
        public class UserDto
        {
            public Guid Id { get; set; }
            public string UserName { get; set; }
            public string UserCode { get; set; }
            public string Password { get; set; }
            public byte Type { get; set; }
            public string Position { get; set; }
            public bool IsActive { get; set; }
            public string Email { get; set; }
            public string FullName { get; set; }
            public string PhoneNumber { get; set; }
            public string UpdateBy { get; set; }
            public byte[] Picture { get; set; }
            public string LanguageCode { get; set; }
        }
        public class MenuActionDto : BaseDto
        {
            public AppMenu.Action Type { get; set; }
            public Guid MenuId { get; set; }
            public string MenuController { get; set; }
            public string MenuActionName { get; set; }
            public string Controller { get; set; }
            public string Action { get; set; }
            public string NameVI { get; set; }
            public string NameEN { get; set; }
            public string ResourceID { get; set; }
            public string Icon { get; set; }
            public string Params { get; set; }
            public bool IsPopup { get; set; }
            public string JSContent { get; set; }
            public string DataTarget { get; set; }
            public string ClssCss { get; set; }
        }
        public class MenuActionCompactDto
        {
            public Guid MenuId { get; set; }
            public string MenuController { get; set; }
            public string MenuActionName { get; set; }
        }
        public class NameMenuRoleDto
        {
            public string FunctionName { get; set; }
            public Guid RoleId { get; set; }
            public Guid MenuId { get; set; }
            public Guid? MenuParentId { get; set; }
            public Guid? MenuActionId { get; set; }
            public int Order { get; set; }
        }
        public class RoleByUserDto
        {
            public Guid RoleId { get; set; }
            public string RoleName { get; set; }
            public string UserId { get; set; }
            public byte Type { get; set; }
        }
        public class UserRoleDto
        {
            public string UserId { get; set; }
            public Guid RoleId { get; set; }
        }
        public class UserPermissionDeptDto : BaseDto
        {
            public Guid UserId { get; set; }
            public Guid DepartmentId { get; set; }
            public string DepartmentName { get; set; }
        }
        public class DeptDto
        {
            public Guid Id { get; set; }
            public Guid? ParentId { get; set; }
            public string Name { get; set; }
            public string DisplayName { get; set; }
            public int OrderNumber { get; set; }
            public int Level { get; set; }
            public bool Checked { get; set; }
        }
        public class UserDepartmentDto
        {
            public Guid UserId { get; set; }
            public Guid DeptId { get; set; }
            public string JobDescription { get; set; }
            public Nullable<int> OrderNumber { get; set; }
            public Nullable<bool> IsManager { get; set; }
            public string URL { get; set; }
            public string CreateBy { get; set; }
            public string UpdateBy { get; set; }
            public DateTime CreateDate { get; set; }
            public DateTime UpdateDate { get; set; }
            public byte ActiveFag { get; set; }
        }
        public class DepartmentDto : BaseDto
        {
            public Guid? ParentId { get; set; }
            public string ParentName { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
            public int Level { get; set; }
            public string CreateByFullName { get; set; }
            public string UpdateByFullName { get; set; }
        }
        public class DepartmentTreeDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int Level { get; set; }
            public string Location { get; set; }
            public string CreateBy { get; set; }
        }
        public class RecursiveDeptDto
        {
            public Guid id { get; set; }
            public string text { get; set; }
            public bool @checked { get; set; }
            public Guid? ParentId { get; set; }
            public int Level { get; set; }
            public int OrderNumber { get; set; }
            public string location { get; set; }
            public List<RecursiveDeptDto> children { get; set; }
            public RecursiveDeptDto()
            {
                this.children = new List<RecursiveDeptDto>();
            }
        }
        public class DictDeptDto : DeptDto
        {
            public List<DictDeptDto> LstDept { get; set; }
            public DictDeptDto()
            {
                this.LstDept = new List<DictDeptDto>();
            }
        }
        public class LoginModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public bool RememberMe { get; set; } = false;
        }
        public class LoginResultDto
        {
            public string Token { get; set; }
        }
    }
}
