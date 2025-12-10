namespace MiddlewareTool.Dto
{
    public class UploadFileDto : BaseDto
    {
        public string Name { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
    }

    public class UploadFileCompactDto
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
    }
}
