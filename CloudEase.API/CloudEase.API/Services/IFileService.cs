using CloudEase.API.Models;

namespace CloudEase.API.Services
{
    public interface IFileService
    {
        Task<string>UploadAsync(IFormFile file,string userID);
        Task<IEnumerable<UploadedFile>> ListAsync(string userId);


    }

}
