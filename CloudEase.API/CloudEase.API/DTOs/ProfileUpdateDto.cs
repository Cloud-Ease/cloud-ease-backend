using System.ComponentModel.DataAnnotations;

namespace CloudEase.API.DTOs
{
    public class ProfileUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string AvatarUrl { get; set; }
    }

}
