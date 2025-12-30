using ApplicationCore.Model;
using ApplicationCore.Queries.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data;
using MongoDB.Driver;

namespace Infrastructure.Queries
{
    public class CourseQuery :ICourseQuery
    {
        private readonly MongoDbContext _db;

        public CourseQuery(MongoDbContext db)
        {
            _db = db;
        }

        public async Task<List<Course>> GetPublishedAsync()
        {
            return await _db.Courses
                .Find(c => c.IsPublished)
                .ToListAsync();
        }

        public async Task<Course?> GetByIdAsync(string id)
        {
            return await _db.Courses
                .Find(c => c.Id == id && c.IsPublished)
                .FirstOrDefaultAsync();
        }
    }
}
