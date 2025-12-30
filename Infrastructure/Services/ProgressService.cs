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
    public class ProgressService : IProgressService
    {
        private readonly MongoDbContext _db;

        public ProgressService(MongoDbContext db)
        {
            _db = db;
        }

        public async Task UpdateProgress(VideoProgress progress)
        {
            await _db.VideoProgress.ReplaceOneAsync(
                x => x.UserId == progress.UserId &&
                     x.CourseId == progress.CourseId &&
                     x.LessonId == progress.LessonId,
                progress,
                new ReplaceOptions { IsUpsert = true });
        }
    }
}
