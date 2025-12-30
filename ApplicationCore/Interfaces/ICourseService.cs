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
        Task<List<Course>> GetAll();
        Task Create(Course course);
    }
}
