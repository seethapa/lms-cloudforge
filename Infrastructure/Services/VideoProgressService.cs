using ApplicationCore.DTO;
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
    public class VideoProgressService : IVideoProgressService
    {
        private readonly MongoDbContext _db;

        public VideoProgressService(MongoDbContext db)
        {
            _db = db;
        }

        public async Task<VideoProgress> UpdateProgress(
            string userId,
            UpdateVideoProgressRequest request)
        {
            var progressId = $"{userId}_{request.LessonId}";

            var completed =
                request.TotalSeconds > 0 &&
                request.WatchedSeconds >= request.TotalSeconds;

            var update = Builders<VideoProgress>.Update
                .Set(x => x.UserId, userId)
                .Set(x => x.CourseId, request.CourseId)
                .Set(x => x.LessonId, request.LessonId)
                .Set(x => x.WatchedSeconds, request.WatchedSeconds)
                .Set(x => x.TotalSeconds, request.TotalSeconds)
                .Set(x => x.Completed, completed)
                .Set(x => x.UpdatedAt, DateTime.UtcNow);

            await _db.VideoProgress.UpdateOneAsync(
                x => x.Id == progressId,
                update,
                new UpdateOptions { IsUpsert = true });

            return await _db.VideoProgress
                .Find(x => x.Id == progressId)
                .FirstAsync();
        }

        public async Task<VideoProgress?> GetProgress(
            string userId,
            string lessonId)
        {
            var id = $"{userId}_{lessonId}";

            return await _db.VideoProgress
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}