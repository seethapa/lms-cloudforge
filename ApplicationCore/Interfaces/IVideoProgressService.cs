using ApplicationCore.DTO;
using ApplicationCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IVideoProgressService
    {
        Task<VideoProgress> UpdateProgress(
            string userId,
            UpdateVideoProgressRequest request);

        Task<VideoProgress?> GetProgress(
            string userId,
            string lessonId);
    }
}
