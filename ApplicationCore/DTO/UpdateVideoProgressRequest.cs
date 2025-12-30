using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTO
{
    public class UpdateVideoProgressRequest
    {
        public string CourseId { get; set; } = default!;
        public string LessonId { get; set; } = default!;
        public int WatchedSeconds { get; set; }
        public int TotalSeconds { get; set; }
    }
}
