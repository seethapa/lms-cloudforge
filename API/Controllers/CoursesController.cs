using ApplicationCore.Interfaces;
using ApplicationCore.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _courses.GetAll());

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            await _courses.Create(course);
            return Ok(course);
        }
    }

}
