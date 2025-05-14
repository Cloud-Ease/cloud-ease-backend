using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudEase.API.Models
{
    public class Profile
    {
            public int Id { get; set; }
             public string UserId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string ImageUrl { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime LastLoginAt { get; set; }
            public bool IsActive { get; set; }

    }
}
