using ApplicationCore.DTO;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Model;
using Infrastructure.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;
namespace UnitTests.Services
{
    public class VideoProgressServiceTests
    {
        [Fact]
        public async Task UpdateProgress_Should_MarkCompleted_WhenWatchedFully()
        {
            // Arrange
            var repoMock = new Mock<IVideoProgressRepository>();

            var service = new VideoProgressService(
                repoMock.Object,
                NullLogger<VideoProgressService>.Instance);

            var request = new UpdateVideoProgressRequest
            {
                CourseId = "aws-devops",
                LessonId = "lesson-2",
                WatchedSeconds = 100,
                TotalSeconds = 100
            };

            // Act
            var result = await service.UpdateProgressAsync("user-1", request);

            // Assert
            Assert.True(result.Completed);
            repoMock.Verify(
                r => r.UpsertAsync(It.IsAny<VideoProgress>()),
                Times.Once);
        }
    }
}
