using Microsoft.EntityFrameworkCore;
using CloudEase.API.Models;

namespace CloudEase.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<UploadedFile> Files { get; set; }
    }
}
