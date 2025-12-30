using ApplicationCore.Commands.DTO;
using ApplicationCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Commands.Interfaces
{
    public interface ICourseCommand
    {
        Task<Course> CreateAsync(
            CreateCourseCommand command,
            string createdBy);
    }
}
