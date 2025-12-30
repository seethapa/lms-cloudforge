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

        public async Task<List<Course>> GetAll()
        {
            return await _db.Courses.Find(_ => true).ToListAsync();
        }

        public async Task Create(Course course)
        {
            course.Id = Guid.NewGuid().ToString();
            await _db.Courses.InsertOneAsync(course);
        }
    }
}
