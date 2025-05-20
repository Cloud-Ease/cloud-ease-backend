using CloudEase.API.DTOs;
using CloudEase.API.Models;

namespace CloudEase.API.Services
{
    public interface IFileService
    {
        Task<string>UploadAsync(IFormFile file,string userID);
        Task<IEnumerable<UploadedFile>> ListAsync(string userId);
        Task<(byte[] data, string fileName, string contentType)?> DownloadAsync(int fileId, string userId);
        Task DeleteFile (int fileId, string userId);


    }

}
