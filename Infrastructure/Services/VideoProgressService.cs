using ApplicationCore.DTO;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Model;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class VideoProgressService : IVideoProgressService
    {
        private readonly IVideoProgressRepository _repository;
        private readonly ILogger<VideoProgressService> _logger;

        public VideoProgressService(
            IVideoProgressRepository repository,
            ILogger<VideoProgressService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<VideoProgress> UpdateProgressAsync(
            string userId,
            UpdateVideoProgressRequest request)
        {
            var id = $"{userId}_{request.LessonId}";

            _logger.LogInformation(
                "Updating video progress {ProgressId}", id);

            var completed =
                request.TotalSeconds > 0 &&
                request.WatchedSeconds >= request.TotalSeconds;

            var progress = new VideoProgress
            {
                Id = id,
                UserId = userId,
                CourseId = request.CourseId,
                LessonId = request.LessonId,
                WatchedSeconds = request.WatchedSeconds,
                TotalSeconds = request.TotalSeconds,
                Completed = completed,
                UpdatedAt = DateTime.UtcNow
            };

            await _repository.UpsertAsync(progress);

            return progress;
        }

    }
}