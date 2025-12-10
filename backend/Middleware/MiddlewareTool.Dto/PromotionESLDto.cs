namespace MiddlewareTool.Dto
{
    public class PromotionESLDto : BaseDto
    {
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public string StoreCode { get; set; }
        public string StoreName { get; set; }
        public byte EDLPFlag
        {
            get
            {
                var now = DateTime.Now;
                if (StartDateTime <= now && EndDateTime >= now)
                    return 1; // Active
                else if (StartDateTime > now && EndDateTime > now)
                    return 2; // Upcoming
                else
                    return 3; // Expired
            }
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsTransferESL { get; set; } = false;

        // Computed từ StartDate + StartTime
        public DateTime StartDateTime => StartDate.Add(StartTime);

        // Computed từ EndDate + EndTime
        public DateTime EndDateTime => EndDate.Add(EndTime);

        public string EDLPFlagDisplay
        {
            get
            {
                switch (EDLPFlag)
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
    public class PromotionESLSearchModel
    {
        public string SKU { get; set; }
        public string Keyword { get; set; }
        public string StoreCode { get; set; }
        public int ELDPFlag { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public List<int?> LineId { get; set; }
        public List<int?> DivisionId { get; set; }
        public List<int?> GroupId { get; set; }
        public List<string> StoreList { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
    public class PromotionHistorySearchModel
    {
        public string SKU { get; set; }
        public string Keyword { get; set; }
        public string StoreCode { get; set; }
        public int ELDPFlag { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? Action { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
    public class PromotionESLImport
    {
        public string SKU { get; set; }
        public string StoreCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime StartDateTime => StartDate.Add(StartTime);
        public DateTime EndDateTime => EndDate.Add(EndTime);

    }

    public class PromotionESLRow
    {
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public string StoreCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime UpdateDate { get; set; }

        public int DivisionId { get; set; }
        public string DivisionName { get; set; }

        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public int LineId { get; set; }
        public string LineName { get; set; }

        public string UpdateBy { get; set; }

        public int TotalCount { get; set; }
    }


    public class PromotionESLPaging : PromotionESLDto
    {

        public int? LineId { get; set; }
        public int? DivisionId { get; set; }
        public int? GroupId { get; set; }
        public string LineName { get; set; }
        public string DivisionName { get; set; }
        public string GroupName { get; set; }
        public int TotalCount { get; set; }
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
        public string StartTimeDisplay
        {
            get
            {
                return StartTime.ToString(@"hh\:mm") ?? string.Empty;

            }
        }
        public string EndTimeDisplay
        {
            get
            {
                return EndTime.ToString(@"hh\:mm") ?? string.Empty;
            }
        }

    }
    public class PromotionESLHistoryDto : PromotionESLDto
    {
        public int Action { get; set; }
        public string ActionDisplay
        {
            get
            {
                switch (Action)
                {
                    case 1:
                        return "Insert";
                    case 2:
                        return "Update";
                    case 3:
                        return "Delete";
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
        public string EDLPFLagHistory { get; set; }
    }
}