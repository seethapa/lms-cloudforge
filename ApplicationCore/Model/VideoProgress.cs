using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
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
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; } = default!;
        // userId_lessonId

        public string UserId { get; set; } = default!;
        public string CourseId { get; set; } = default!;
        public string LessonId { get; set; } = default!;

        public int WatchedSeconds { get; set; }
        public int TotalSeconds { get; set; }
        public bool Completed { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
