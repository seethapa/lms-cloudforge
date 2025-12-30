using ApplicationCore.Commands.DTO;
using ApplicationCore.Commands.Interfaces;
using ApplicationCore.Queries.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseControllerCQRS : ControllerBase
    {
        private readonly ICourseQuery _query;
        private readonly ICourseCommand _command;

        public CourseControllerCQRS(
            ICourseQuery query,
            ICourseCommand command)
        {
            _query = query;
            _command = command;
        }

        // 🔓 READ
        [HttpGet]
        public async Task<IActionResult> GetPublished()
            => Ok(await _query.GetPublishedAsync());

        // 🔐 WRITE
        [Authorize(Roles = "trainer,admin")]
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateCourseCommand command)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var result = await _command.CreateAsync(command, userId);

            return CreatedAtAction(
                nameof(GetPublished),
                new { id = result.Id },
                result);
        }
    }
}
