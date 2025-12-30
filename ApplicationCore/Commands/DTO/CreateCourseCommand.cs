using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Commands.DTO
{
    public class CreateCourseCommand
    {
        public string Id { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Level { get; set; } = default!;
        public string ThumbnailUrl { get; set; } = default!;
    }
}
