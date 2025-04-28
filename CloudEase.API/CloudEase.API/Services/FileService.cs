
using CloudEase.API.Data;
using Google.Cloud.Storage.V1;
using CloudEase.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CloudEase.API.Services
{
    public class FileService : IFileService
    {
        private readonly AppDbContext _db;
        private readonly StorageClient _storage;
        private readonly string _bucket = "cloud-ease";

        public FileService (AppDbContext db)
        {
            _db = db;
            _storage = StorageClient.Create();
        }

        public async Task<IEnumerable<UploadedFile>> ListAsync(string userId)
        {
            return await _db.Files
                .where(f => f.UserId == userId)
                .OrderByDescending(f => f.UploadedAt)
                .ToListAsync();
        }

        public async Task<string> UploadAsync(IFormFile file, string userID)
        {
            var objectName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            using var stream = file.OpenReadStream();
            await _storage.UploadObjectAsync(_bucket,objectName,file.ContentType,stream);
            var url = $"https://storage.googleapis.com/{_bucket}/{objectName}";

            var uploadedFile = new UploadedFile
            {
                FileName = file.FileName,
                ContentType = file.ContentType,
                Url = url,
                UserId = userID,
                UploadedAt = DateTime.Now

            };
            _db.Files.Add(uploadedFile);
            await _db.SaveChangesAsync();

            return url;
        }
    }
}
