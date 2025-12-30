using ApplicationCore.Interfaces;
using ApplicationCore.Model;
using Infrastructure.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{

    public class CourseService : ICourseService
    {
        private readonly MongoDbContext _db;

        public CourseService(MongoDbContext db)
        {
            _db = db;
        }

        // 🔹 Student / Public API
        public async Task<List<Course>> GetAllPublished()
        {
            return await _db.Courses
                .Find(c => c.IsPublished)
                .ToListAsync();
        }

        public async Task<Course?> GetById(string id)
        {
            return await _db.Courses
                .Find(c => c.Id == id && c.IsPublished)
                .FirstOrDefaultAsync();
        }

        // 🔹 Admin / Trainer APIs
        public async Task<Course> Create(Course course)
        {
            course.Id ??= Guid.NewGuid().ToString();
            course.CreatedAt = DateTime.UtcNow;

            try
            {
                await _db.Courses.InsertOneAsync(course);
                return course;
            }
            catch (MongoWriteException ex)
            {
                throw new Exception($"Mongo insert failed: {ex.Message}");
            }
        }

        public async Task<Course> Update(string id, Course course)
        {
            var update = Builders<Course>.Update
                .Set(x => x.Title, course.Title)
                .Set(x => x.Description, course.Description)
                .Set(x => x.Level, course.Level)
                .Set(x => x.ThumbnailUrl, course.ThumbnailUrl)
                .Set(x => x.IsPublished, course.IsPublished);

            var result = await _db.Courses.UpdateOneAsync(
                c => c.Id == id,
                update);

            if (result.MatchedCount == 0)
                throw new Exception("Course not found");

            return await _db.Courses.Find(c => c.Id == id).FirstAsync();
        }

        public async Task Delete(string id)
        {
            await _db.Courses.DeleteOneAsync(c => c.Id == id);
        }
    }
}
