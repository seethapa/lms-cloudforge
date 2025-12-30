using ApplicationCore.Commands.DTO;
using ApplicationCore.Commands.Interfaces;
using ApplicationCore.Model;
using Infrastructure.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Commands
{
    public class CourseCommand : ICourseCommand
    {
        private readonly MongoDbContext _db;

        public CourseCommand(MongoDbContext db)
        {
            _db = db;
        }

        public async Task<Course> CreateAsync(
            CreateCourseCommand command,
            string createdBy)
        {
            var course = new Course
            {
                Id = string.IsNullOrWhiteSpace(command.Id)
                    ? Guid.NewGuid().ToString()
                    : command.Id,
                Title = command.Title,
                Description = command.Description,
                Level = command.Level,
                ThumbnailUrl = command.ThumbnailUrl,
                CreatedBy = createdBy,
                CreatedAt = DateTime.UtcNow,
                IsPublished = false
            };

            await _db.Courses.InsertOneAsync(course);
            return course;
        }
    }
}
