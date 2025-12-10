namespace MiddlewareTool.Dto
{
    public class UpdateResultsDto : BaseDto
    {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public string FileExt { get; set; }
        public int TotalRow { get; set; }
        public string Curent { get; set; }
        public string CreateByFullName { get; set; }
        public string UpdateByFullName { get; set; }
    }
}
