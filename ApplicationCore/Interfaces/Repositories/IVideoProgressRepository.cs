using ApplicationCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface IVideoProgressRepository
    {
        Task<VideoProgress?> GetByIdAsync(string id);
        Task UpsertAsync(VideoProgress progress);
    }
}
