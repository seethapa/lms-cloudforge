using ApplicationCore.DTO;
using ApplicationCore.Interfaces;
using ApplicationCore.Model;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<VideoProgressService> _logger;

        public VideoProgressService(
         MongoDbContext db,
         ILogger<VideoProgressService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<VideoProgress> UpdateProgress(
            string userId,
            UpdateVideoProgressRequest request)
        {
            _logger.LogInformation(
          "Updating progress. UserId={UserId}, LessonId={LessonId}",
          userId, request.LessonId);

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

            _logger.LogInformation("Progress updated successfully: {Id}", progressId);

            return await _db.VideoProgress
                .Find(x => x.Id == progressId)
                .FirstAsync();
        }

        public async Task<VideoProgress?> GetProgress(
            string userId,
            string lessonId)
        {
            _logger.LogInformation(
         "Get progress. UserId={UserId}, LessonId={lessonId}",
         userId, lessonId);
            var id = $"{userId}_{lessonId}";

            return await _db.VideoProgress
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}