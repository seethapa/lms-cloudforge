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
    public class Course
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; } = default!;   

        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Level { get; set; } = default!;
        public string ThumbnailUrl { get; set; } = default!;
        public string CreatedBy { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public bool IsPublished { get; set; }
    }
}
