using ApplicationCore.DTO;
using ApplicationCore.Interfaces;
using ApplicationCore.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VideoProgressController : ControllerBase
    {
        private readonly IVideoProgressService _progress;

        public VideoProgressController(IVideoProgressService progress)
        {
            _progress = progress;
        }

        // 🔹 Update / Save progress
        [HttpPost]
        public async Task<IActionResult> UpdateProgress(
            UpdateVideoProgressRequest request)
        {
            var userId =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _progress.UpdateProgress(userId, request);

            return Ok(result);
        }

        // 🔹 Get progress for a lesson
        [HttpGet("{lessonId}")]
        public async Task<IActionResult> GetProgress(string lessonId)
        {
            var userId =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            var progress =
                await _progress.GetProgress(userId!, lessonId);

            if (progress == null)
                return NotFound();

            return Ok(progress);
        }
    }
}
