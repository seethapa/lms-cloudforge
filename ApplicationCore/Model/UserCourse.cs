using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApplicationCore.Model
{
    public class UserCourse
    {
        public string Id { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public string CourseId { get; set; } = default!;
        public string CourseTitle { get; set; } = default!;
        public int Progress { get; set; }
        public DateTime EnrolledAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
