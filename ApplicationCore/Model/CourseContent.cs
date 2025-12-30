using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Model
{
    public class CourseContent
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("courseId")]
        public string CourseId { get; set; }

        public List<Module> Modules { get; set; }
    }
}
