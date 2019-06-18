using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.WebUI.Models
{
    //Model for pagination usage
    public class ArticlesViewModel
    {
        public IEnumerable<Article> Articles { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}