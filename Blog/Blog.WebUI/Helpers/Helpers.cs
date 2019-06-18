using Blog.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Blog.WebUI.Helpers
{
    public static class Helpers
    {
        //Helper that is used to create navigation bar on the pages
        public static MvcHtmlString CreateList(this HtmlHelper html)
        {
            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("navbar-nav flex-grow-1");

            TagBuilder li = new TagBuilder("li");
            TagBuilder a = new TagBuilder("a");
            li.AddCssClass("nav-item");
            a.AddCssClass("nav-link");
            a.MergeAttribute("href", "/");
            a.SetInnerText("Главная");
            li.InnerHtml += a.ToString();
            ul.InnerHtml += li.ToString();

            li = new TagBuilder("li");
            a = new TagBuilder("a");
            li.AddCssClass("nav-item");
            a.AddCssClass("nav-link");
            a.MergeAttribute("href", "/Home/Commentaries");
            a.SetInnerText("Гостевая");
            li.InnerHtml += a.ToString();
            ul.InnerHtml += li.ToString();

            li = new TagBuilder("li");
            a = new TagBuilder("a");
            a.MergeAttribute("href", "/Home/Sendform");
            li.AddCssClass("nav-item");
            a.AddCssClass("nav-link");
            a.SetInnerText("Анкета");
            li.InnerHtml += a.ToString();
            ul.InnerHtml += li.ToString();

            return new MvcHtmlString(ul.ToString());
        }
        //Helper that creates elements for switching between the pages
        public static MvcHtmlString ShowPages(this HtmlHelper html,
                                          PagingInfo pagingInfo,
                                          Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-secondary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}