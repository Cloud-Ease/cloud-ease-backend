using CloudEase.API.Models;
using CloudEase.API.DTOs;

namespace CloudEase.API.Services
{
    public interface IProfileService
    {
        Task<Profile>GetByUserIdAsync(string userId);
        Task<Profile> UpdateAsync(string userId, ProfileUpdateDto dto);
        Task<Profile> CreateAsync(string userId, ProfileDto dto);
    }
}
