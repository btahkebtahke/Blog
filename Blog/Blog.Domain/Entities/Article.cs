using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class Article : BaseEntity
    {
        public string Content { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public Article()
            {
                Tags = new List<Tag>();
            }

    }
}
