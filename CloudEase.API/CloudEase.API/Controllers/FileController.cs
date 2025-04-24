using CloudEase.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
       private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            var userId = HttpContext.Items["UserId"]?.ToString();
            if (userId == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            if (file == null || file.Length == 0)
            {

                return BadRequest("Dosya bulunamadı.");
            }

            var url = await _fileService.UploadAsync(file, userId);

            return Ok(new { fileUrl = url });

        }
        }

}
