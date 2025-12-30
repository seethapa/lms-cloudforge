using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Model
{
    public class Module
    {
        public string ModuleId { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public List<Lesson> Lessons { get; set; }
    }
}
