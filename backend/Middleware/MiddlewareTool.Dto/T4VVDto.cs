namespace MiddlewareTool.Dto
{
    public class T4VVDto : BaseDto
    {
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public string StoreCode { get; set; }
        public string StoreName { get; set; }
        public byte T4VVFlag
        {
            get
            {
                var now = DateTime.Now.Date;
                if (StartDate <= now && EndDate >= now)
                    return 1; // Active
                else if (StartDate > now && EndDate > now)
                    return 2; // Upcoming
                else
                    return 3; // Expired
            }
        }
        public DateTime StartDate
        {
            get
            {
                if (DateTime.TryParseExact(PRC_START_DATE, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime startDate))
                {
                    return startDate;
                }
                return DateTime.Now;
            }
        }
        public DateTime EndDate
        {
            get
            {
                if (DateTime.TryParseExact(PRC_END_DATE, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime endDate))
                {
                    return endDate;
                }
                return DateTime.Now;
            }
        }
        public TimeSpan StartTime
        {
            get
            {
                if (DateTime.TryParseExact(PRC_START_TIME, "HHmm", null, System.Globalization.DateTimeStyles.None, out var dt))
                {
                    return dt.TimeOfDay;
                }
                return TimeSpan.Zero;
            }
        }
        public TimeSpan EndTime
        {
            get
            {
                if (DateTime.TryParseExact(PRC_END_TIME, "HHmm", null, System.Globalization.DateTimeStyles.None, out var dt))
                {
                    return dt.TimeOfDay;
                }
                return TimeSpan.Zero;
            }
        }
        public bool IsTransferESL { get; set; } = false;
        public string PRC_START_DATE { get; set; }
        public string PRC_END_DATE { get; set; }
        public string PRC_START_TIME { get; set; }
        public string PRC_END_TIME { get; set; }
        public string T4VVFlagDisplay
        {
            get
            {
                switch (T4VVFlag)
                {

                    case 1:
                        return "Active";
                    case 2:
                        return "Upcoming";
                    case 3:
                        return "Expired";
                    default:
                        return "";
                }
            }

        }
        public int TotalCount { get; set; }
        public string UpdateDateDisplay
        {
            get
            {
                return UpdateDate.ToString("dd/MM/yyyy") ?? string.Empty;
            }
        }

    }
    public class T4VVDtoSearchModel
    {
        public string SKU { get; set; }
        public string Keyword { get; set; }
        public string StoreCode { get; set; }
        public int T4VVFlag { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<int?> LineId { get; set; }
        public List<int?> DivisionId { get; set; }
        public List<int?> GroupId { get; set; }
        public List<string> StoreList { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? Action { get; set; }
    }

    public class T4VVDtoPaging : T4VVDto
    {

        public int? LineId { get; set; }
        public int? DivisionId { get; set; }
        public int? GroupId { get; set; }
        public string LineName { get; set; }
        public string DivisionName { get; set; }
        public string GroupName { get; set; }
        public string StartDateDisplay
        {
            get
            {
                return StartDate.ToString("dd/MM/yyyy") ?? string.Empty;
            }
        }
        public string EndDateDisplay
        {
            get
            {
                return EndDate.ToString("dd/MM/yyyy") ?? string.Empty;
            }
        }

    }
    public class T4VVHistoryDto : T4VVDto
    {
        public int Action { get; set; }
        public string ActionDisplay
        {
            get
            {
                switch (Action)
                {
                    case 0:
                        return "Insert";
                    case 1:
                        return "Update";
                    case 2:
                        return "Delete";
                    case 3:
                        return "Import";
                    case 4:
                        return "Import";
                    case 21:
                        return "Import P files";
                    case 28:
                        return "Auto update flag";
                    case 29:
                        return "Export M files";
                    default:
                        return "";
                }
            }
        }
        public string TransData { get; set; }
        public string Source { get; set; }
        public string T4VVFlagHistory { get; set; }
    }
}