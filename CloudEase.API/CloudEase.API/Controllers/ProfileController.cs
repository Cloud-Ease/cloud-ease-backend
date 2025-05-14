using Microsoft.AspNetCore.Mvc;
using CloudEase.API.Services;
using CloudEase.API.DTOs;

namespace CloudEase.API.Controllers
{
    
    [Route("api/profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _service;
        public ProfileController(IProfileService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfile([FromBody] ProfileCreateDto dto)
        {
            var userId = HttpContext.Items["UserId"]?.ToString();
            if (userId == null) return Unauthorized();

            var createdProfile = await _service.CreateAsync(userId, dto);

            
            var response = new ProfileDto
            {
                FirstName = createdProfile.FirstName,
                LastName = createdProfile.LastName,
                AvatarUrl = createdProfile.ImageUrl,
                Email = createdProfile.Email,
                Phone = createdProfile.Phone,
                IsActive = createdProfile.IsActive,
                CreatedAt = createdProfile.CreatedAt,
                LastLoginAt = createdProfile.LastLoginAt
            };

            return Ok(response); 
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = HttpContext.Items["UserId"]?.ToString();
            if (userId == null) return Unauthorized();

            var profile = await _service.GetByUserIdAsync(userId);
            if (profile == null) return NotFound();

            var dto = new ProfileDto
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                AvatarUrl = profile.ImageUrl,
                Phone = profile.Phone,
                Email = profile.Email,
                IsActive = profile.IsActive,
                CreatedAt = profile.CreatedAt,
                LastLoginAt = profile.LastLoginAt
            };



            return Ok(dto);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]ProfileUpdateDto dto)
        {
            var userId = HttpContext.Items["UserId"]?.ToString();
            if (userId == null) return Unauthorized();
            
            var update = await _service.UpdateAsync(userId, dto);
            return Ok(update);
        }
    }

}
