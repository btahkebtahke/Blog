using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class Questionnaire : BaseEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string Hobbies { get; set; }
    }
}
