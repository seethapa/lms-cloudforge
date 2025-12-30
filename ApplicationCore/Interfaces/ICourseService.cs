using ApplicationCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ICourseService
    {
        Task<List<Course>> GetAllPublished();
        Task<Course?> GetById(string id);
        Task<Course> Create(Course course);
        Task<Course> Update(string id, Course course);
        Task Delete(string id);
    }
}
