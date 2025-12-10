using System.Data;
using System.Reflection;

namespace MiddlewareTool.Dto
{
    public class SystemMgmtDto
    {
        public class ResourceDto
        {
            public string ResourceID { get; set; }
            public string ResourceText0 { get; set; }
            public string DefaultText0 { get; set; }
            public string ResourceText1 { get; set; }
            public string DefaultText1 { get; set; }
            public string ResourceText2 { get; set; }
            public string DefaultText2 { get; set; }
            public string ResourceText3 { get; set; }
            public string DefaultText3 { get; set; }
            public string ResourceText4 { get; set; }
            public string DefaultText4 { get; set; }
            public string ResourceText5 { get; set; }
            public string DefaultText5 { get; set; }
            public DateTime CreateDate { get; set; }
            public bool ParseData(DataRow dr)
            {
                try
                {
                    for (int i = 0; i < dr.Table.Columns.Count; i++)
                    {
                        string _colName = dr.Table.Columns[i].ColumnName;
                        PropertyInfo _prop = this.GetType().GetProperty(_colName);
                        if (!(dr[_colName] is DBNull) && _prop != null)
                        {
                            _prop.SetValue(this, dr[_colName], null);
                        }
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            public ResourceDto()
            {
                this.CreateDate = DateTime.Now;
            }
        }
        public class SystemLogDto : BaseDto
        {
            public Guid LogId { get; set; }
            public string Module { get; set; }
            public string UserId { get; set; }
            public Nullable<int> UserFunction { get; set; }
            public Nullable<int> EventResult { get; set; }
            public Nullable<DateTime> FuncDateTime { get; set; }
            public string Source { get; set; }
            public string Transdata { get; set; }
            public string WSName { get; set; }
            public Guid? AttachmentId { get; set; }
            public string CreateByFullName { get; set; }
            public string UpdateByFullName { get; set; }
            public int TotalRecord { get; set; }
        }
        public class SystemLogAttachmentDto : BaseDto
        {
            public System.Guid LogId { get; set; }
            public string Name { get; set; }
            public string FileName { get; set; }
            public byte[] FileContent { get; set; }
        }
        public class SystemFunctionDto : BaseDto
        {
            public string FunctionID { get; set; }
            public Guid MenuId { get; set; }
            public string FunctionName { get; set; }
            public string FunctionDesc { get; set; }
            public bool IsCustomer { get; set; }
            public byte Type { get; set; }
            public virtual ICollection<RoleDto> Roles { get; set; }
        }
        public class RoleDto : BaseDto
        {
            public byte Type { get; set; }
            public string Name { get; set; }
            public string DataMenu { get; set; }
            public RoleDto()
            {
                this.DataMenu = string.Empty;
            }
            public string CreateByFullName { get; set; }
            public string UpdateByFullName { get; set; }
        }
        public class RoleFunctionDto : BaseDto
        {
            public Guid RoleID { get; set; }
            public string FunctionID { get; set; }
        }
        public class SystemSettingDto : BaseDto
        {
            public string Code { get; set; }
            public byte Layout { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }
            public byte Type { get; set; }
            public string CreateByFullName { get; set; }
            public string UpdateByFullName { get; set; }
        }
        public class MailboxDto : BaseDto
        {
            public string Subject { get; set; }
            public string Body { get; set; }
            public string MailTo { get; set; }
            public string MailCc { get; set; }
            public int NumSend { get; set; }
            public Nullable<bool> Sent { get; set; }
            public bool IsSeen { get; set; }
            public MailboxDto()
            {
                NumSend = 0;
                IsSeen = false;
                Sent = false;
            }
        }
    }
}
