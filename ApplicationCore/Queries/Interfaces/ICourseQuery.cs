using ApplicationCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Queries.Interfaces
{
    public interface ICourseQuery
    {
        Task<List<Course>> GetPublishedAsync();
        Task<Course?> GetByIdAsync(string id);
    }
}
