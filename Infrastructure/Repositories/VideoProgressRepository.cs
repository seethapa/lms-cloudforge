using ApplicationCore.Model;
using Infrastructure.Data;
using MongoDB.Driver;

namespace ApplicationCore.Interfaces.Repositories
{
    public class VideoProgressRepository : IVideoProgressRepository
    {
        private readonly IMongoCollection<VideoProgress> _collection;

        public VideoProgressRepository(MongoDbContext context)
        {
            _collection = context.VideoProgress;
        }

        public async Task<VideoProgress?> GetByIdAsync(string id)
        {
            return await _collection
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task UpsertAsync(VideoProgress progress)
        {
            await _collection.ReplaceOneAsync(
                x => x.Id == progress.Id,
                progress,
                new ReplaceOptions { IsUpsert = true });
        }
    }
}
