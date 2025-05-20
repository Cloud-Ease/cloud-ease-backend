using CloudEase.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CloudEase.API.DTOs;
using Microsoft.EntityFrameworkCore;

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
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload([FromForm] FileUploadDto dto)
        {
            var userId = HttpContext.Items["UserId"]?.ToString();
            if (userId == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }
          
            if (dto.File == null || dto.File.Length == 0)
            {
                return BadRequest("Dosya bulunamadı.");
            }

            var url = await _fileService.UploadAsync(dto.File, userId);

            return Ok(new { fileUrl = url });
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var userId = HttpContext.Items["UserId"]?.ToString();
            if (userId == null) {
                return BadRequest("Okunamadı.");
            }
            var files = await _fileService.ListAsync(userId);

            return Ok(files);
        }


        [HttpGet("download/{id}")]
        public async Task<IActionResult> SingleDownload(int id)
        {
            var userId = HttpContext.Items["UserId"]?.ToString();
            if (userId == null)
            {
                return BadRequest("Kullanıcı kimliği alınamadı.");
            }

            var result = await _fileService.DownloadAsync(id, userId);
            if (result == null)
            {
                return NotFound("Dosya bulunamadı.");
            }

            var (data, fileName, contentType) = result.Value;
            return File(data, contentType ?? "application/octet-stream", fileName);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SingleDelete(int id)
        {
            var userId = HttpContext.Items["UserId"]?.ToString();
            if (userId == null)
            {
                return BadRequest("Kullanıcı kimliği alınamadı.");
            }

            await _fileService.DeleteFile(id, userId);

            return Ok();
        }

    }

}
