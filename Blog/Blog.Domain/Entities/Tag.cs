using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class Tag :BaseEntity
    {
        public string Content { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public Tag()
        {
            Articles = new List<Article>();
        }
    }

}
