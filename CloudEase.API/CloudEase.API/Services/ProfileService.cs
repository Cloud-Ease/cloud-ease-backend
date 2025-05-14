using CloudEase.API.Data;
using CloudEase.API.DTOs;
using CloudEase.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

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

        public async Task<Profile> UpdateAsync(string userId, ProfileUpdateDto dto)
        {
            var profile = await GetByUserIdAsync(userId);
            if (profile == null) return null;

            profile.FirstName = dto.FirstName;
            profile.LastName = dto.LastName;
            profile.Phone = dto.Phone;
            profile.ImageUrl = dto.AvatarUrl;
            profile.LastLoginAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return profile;
        }

        public async Task<Profile> CreateAsync(string userId, ProfileDto dto)
        {
            var profile = new Profile
            {
                UserId = userId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Phone = dto.Phone,
                IsActive = true,
                Email = dto.Email,
                ImageUrl = dto.AvatarUrl,
                CreatedAt = DateTime.UtcNow,
                LastLoginAt = DateTime.UtcNow
            };

            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();
            return profile;
        }

    }
}
