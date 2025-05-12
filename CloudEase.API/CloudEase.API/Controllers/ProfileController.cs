using Microsoft.AspNetCore.Mvc;
using CloudEase.API.Services;
using CloudEase.API.DTOs;

namespace CloudEase.API.Controllers
{
    [ApiController]
    [Route("api/profile")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _service;
        public ProfileController(IProfileService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = HttpContext.Items["UserId"]?.ToString();
            if (userId == null) return Unauthorized();

            var profile = await _service.GetByUserIdAsync(userId);
            return Ok(profile);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]ProfileUpdate dto)
        {
            var userId = HttpContext.Items["UserId"]?.ToString();
            if (userId == null) return Unauthorized();
            
            var update = await _service.UpdateAsync(userId, dto);
            return Ok(update);
        }
    }

}
