
using CloudEase.API.Data;
using Google.Cloud.Storage.V1;
using CloudEase.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Google.Apis.Auth.OAuth2;

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

            var credentialPath = Path.Combine(Directory.GetCurrentDirectory(), "google-cloud-key.json");
            var credential = GoogleCredential.FromFile(credentialPath);

            _storage = StorageClient.Create(credential);
        }

        public async Task<IEnumerable<UploadedFile>> ListAsync(string userId)
        {
            return await _db.Files
                .Where(f => f.UserId == userId)
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
                UploadedAt = DateTime.UtcNow

            };
            _db.Files.Add(uploadedFile);
            await _db.SaveChangesAsync();

            return url;
        }
    }
}
