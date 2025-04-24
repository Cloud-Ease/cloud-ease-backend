namespace CloudEase.API.Services
{
    public interface IFileService
    {
        Task<string>UploadAsync(IFormFile file,string userID);
    }
}
