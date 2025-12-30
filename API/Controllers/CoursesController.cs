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
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courses;

        public CoursesController(ICourseService courses)
        {
            _courses = courses;
        }

        // 🔓 Public – List published courses
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPublished()
        {
            return Ok(await _courses.GetAllPublished());
        }

        // 🔓 Public – Course details
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(string id)
        {
            var course = await _courses.GetById(id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        // 🔐 Trainer/Admin – Create course
        [Authorize(Roles = "trainer,admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            course.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var created = await _courses.Create(course);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // 🔐 Trainer/Admin – Update course
        [Authorize(Roles = "trainer,admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Course course)
        {
            var updated = await _courses.Update(id, course);
            return Ok(updated);
        }

        // 🔐 Admin – Delete course
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _courses.Delete(id);
            return NoContent();
        }
    }

}
