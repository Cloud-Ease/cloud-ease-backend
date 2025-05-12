using CloudEase.API.Data;
using CloudEase.API.DTOs;
using CloudEase.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudEase.API.Services
{
    public class ProfileService : IProfileService
    {
        private readonly AppDbContext _context;

        public ProfileService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Profile> GetByUserIdAsync(string userId)
        {
          return await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<Profile> UpdateAsync(string userId, ProfileUpdate dto)
        {
            var profile = await GetByUserIdAsync(userId);
            if (profile == null) return null;

            profile.FullName = dto.Fullname;
            profile.Phone = dto.Phone;
            profile.ImageUrl = dto.ImageUrl;
            profile.LastLoginAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return profile;
        }
    }
}
