namespace CloudEase.API.Models
{
    public class UploadedFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Url { get; set; }
        public string UserId { get; set; }
        public DateTime UploadedAt { get; set; }
    
    }
}
