using System.ComponentModel.DataAnnotations;

namespace CloudEase.API.DTOs
{
    public class ProfileUpdate
    {
        public string Fullname { get; set; }
        public string Phone    { get; set; }
        public string ImageUrl { get; set; }
    }
}
