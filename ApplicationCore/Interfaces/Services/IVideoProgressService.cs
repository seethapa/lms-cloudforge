using ApplicationCore.DTO;
using ApplicationCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Services
{
    public interface IVideoProgressService
    {
        Task<VideoProgress> UpdateProgressAsync(
     string userId,
     UpdateVideoProgressRequest request);
    }
}
