using ApplicationCore.DTO;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/video-progress")]
    public class VideoProgressController : ControllerBase
    {
        private readonly IVideoProgressService _service;

        public VideoProgressController(
            IVideoProgressService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Update(
            UpdateVideoProgressRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _service
                .UpdateProgressAsync(userId!, request);

            return Ok(result);
        }
    }
}
