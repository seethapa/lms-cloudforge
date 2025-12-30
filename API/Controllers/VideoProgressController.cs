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
        private readonly IProgressService _progress;

        public VideoProgressController(IProgressService progress)
        {
            _progress = progress;
        }

        [HttpPost]
        public async Task<IActionResult> Update(VideoProgress progress)
        {
            progress.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _progress.UpdateProgress(progress);
            return Ok();
        }
    }
}
