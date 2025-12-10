using MiddlewareTool.Common;
using MiddlewareTool.Logs;
using System.Data;
using System.Reflection;

namespace MiddlewareTool.Dto
{
    public class BaseDto
    {
        public BaseDto()
        {
            Id = Guid.Empty;
            OrderNumber = 0;
            URL = string.Empty;
            Description = string.Empty;
            CreateBy = string.Empty;
            UpdateBy = string.Empty;
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
            ActiveFlag = (int)AppValue.ActiveFlag.Active;
        }
        public Guid Id { get; set; }
        public int OrderNumber { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public AppValue.ActiveFlag ActiveFlag { get; set; }
        public string ActiveFlagDisplay => ActiveFlag.ToString();

        public void SetDefaultValueInsert()
        {
            this.Id = Guid.NewGuid();
            this.CreateDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
            this.ActiveFlag = AppValue.ActiveFlag.Active;
        }
        public void SetDefaultValueUpdate()
        {
            this.UpdateDate = DateTime.Now;
        }
        public void NormalizeSql()
        {
            try
            {
                foreach (var item in this.GetType().GetProperties())
                {
                    if (item.PropertyType == typeof(string))
                    {
                        item.SetValue(this, this.NormalizeString(((string)item.GetValue(this))));
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
        }
        public string NormalizeString(string str)
        {
            Dictionary<string, string> _strReplace = new Dictionary<string, string>()
            {
                {"'","''" }
            };
            try
            {
                foreach (var item in _strReplace)
                {
                    str = str?.Replace(item.Key, item.Value)?.Trim();
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return str;
        }
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
            catch (Exception ex)
            {
                Logging.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                return false;
            }
        }
        public string ParseDate(DateTime? dt)
        {
            if (dt == null)
            {
                return string.Empty;
            }
            return DateTime.Parse(dt.ToString()).ToString("dd/MM/yyyy");
        }
        public string ConvertBoolean(bool? value)
        {
            return value == true ? "Yes" : "No";
        }
    }
    public class MasterDto
    {
        public MasterDto()
        {
            OrderNumber = 0;
            URL = string.Empty;
            Description = string.Empty;
            CreateBy = string.Empty;
            UpdateBy = string.Empty;
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
            ActiveFlag = (int)AppValue.ActiveFlag.Active;
        }
        public int? OrderNumber { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public string CreateByFullName { get; set; }
        public string UpdateByFullName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public AppValue.ActiveFlag ActiveFlag { get; set; }
        public void SetDefaultValueInsert()
        {
            this.CreateDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
            this.ActiveFlag = AppValue.ActiveFlag.Active;
        }
        public void SetDefaultValueUpdate()
        {
            this.UpdateDate = DateTime.Now;
        }
        public void NormalizeSql()
        {
            try
            {
                foreach (var item in this.GetType().GetProperties())
                {
                    if (item.PropertyType == typeof(string))
                    {
                        item.SetValue(this, this.NormalizeString(((string)item.GetValue(this))));
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
        }
        public string NormalizeString(string str)
        {
            Dictionary<string, string> _strReplace = new Dictionary<string, string>()
            {
                {"'","''" }
            };
            try
            {
                foreach (var item in _strReplace)
                {
                    str = str?.Replace(item.Key, item.Value)?.Trim();
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
            }
            return str;
        }
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
            catch (Exception ex)
            {
                Logging.LogError(MethodBase.GetCurrentMethod().DeclaringType.Name, ex);
                return false;
            }
        }
        public string ParseDate(DateTime? dt)
        {
            if (dt == null)
            {
                return string.Empty;
            }
            return DateTime.Parse(dt.ToString()).ToString("dd/MM/yyyy");
        }
        public string ConvertBoolean(bool? value)
        {
            return value == true ? "Yes" : "No";
        }
    }
}
