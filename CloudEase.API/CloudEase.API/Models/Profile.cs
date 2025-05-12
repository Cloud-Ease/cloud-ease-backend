using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudEase.API.Models
{
    public class Profile
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime LastLoginAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
