using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Model
{
    public class VideoProgress
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        public string UserId { get; set; }
        public string CourseId { get; set; }
        public string LessonId { get; set; }

        public int WatchedPercentage { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime LastWatchedAt { get; set; } = DateTime.UtcNow;
    }
}
