using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class Commentary : BaseEntity
    {
        public string Content { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
