using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Domain.Entities;
using Blog.Domain.ActionsWithDB;
using Blog.WebUI.Models;

namespace Blog.WebUI.Controllers
{
    public class HomeController : Controller
    {
        //Access to the main view using ViewModel for pagination
        int pageSize = 4;
        public ViewResult Index(int page = 1)
        {
            ArticlesViewModel model = new ArticlesViewModel
            {
                Articles = Methods.context.Articles.Where(p => p.IsDeleted != true)
                        .OrderBy(art => art.ID)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = Methods.context.Articles.Where(p => p.IsDeleted != true).Count()
                }
            };
            return View(model);
        }

        //View for the questionnaire
        public ActionResult Questionnaire()
        {
            return View();
        }
        //Access to the view to create new articles
        public ActionResult NewArticle()
        {
            return View();
        }

        [HttpPost]
        //HttpPost query that is used to create new articles with tags 
        public ActionResult NewArticle(FormCollection collection, Article article)
        {
            if (string.IsNullOrEmpty(collection["Name"]))
            {
                ModelState.AddModelError("Name", "Извините, вы не ввели название для статьи, попробуйте ещё раз");
            }
            if (string.IsNullOrEmpty(collection["Content"]))
            {
                ModelState.AddModelError("Content", "Извините, вы не ввели текст статьи, попробуйте ещё раз");
            }
            if (ModelState.IsValid)
            {
                string tagContent = collection["tag.Content"];
                Methods.CreateItem(article,tagContent);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult FullArticle(int id = 3)
        {
            return View(Methods.FindIdInArticles(id));
        }

        //Commentaries view
        [HttpGet]
        public ActionResult Commentaries()
        {
            return View(Methods.context.Commentaries);
        }
        [HttpPost]
        //Creating new feedback
        public ActionResult Commentaries(FormCollection collection, Commentary commentary)
        {
            if ((string.IsNullOrEmpty(collection["Name"])) || (string.IsNullOrEmpty(collection["Content"])))
            {
                ModelState.AddModelError("Content", "Убедитесь, что все поля заполнены!");
            }
            else
            {
                Methods.CreateItem(commentary);
            }
            return View(Methods.context.Commentaries);
        }

        [HttpGet]
        //Feedback editing
        public ActionResult EditItem(int id)
        {
            return View(Methods.context.Commentaries.Find(id));
        }

        [HttpPost]
        public ActionResult EditItem(int id, FormCollection collection)
        {
            UpdateModel(Methods.context.Commentaries.Find(id));
            Methods.context.SaveChanges();
            return RedirectToAction("Commentaries");
        }

        //Relocating to 'Questionnaire' page
        [HttpGet]
        public ActionResult SendForm()
        {            
            return View("Questionnaire");
        }
       
        //Saving details for the view  "Result" and adding the details to DB 
        [HttpPost]
        public ActionResult Result(FormCollection collection, Questionnaire question)
        {
            if (!string.IsNullOrEmpty(collection["Name"]))
            {
                ViewBag.Name = collection["Name"];
            }
            if (!string.IsNullOrEmpty(collection["Color"]))
            {
                ViewBag.Color = collection["Color"];
            }
            if (!string.IsNullOrEmpty(collection["Hobbies"]))
            {
                ViewBag.Hobbies = collection["Hobbies"];
                question.Hobbies = collection["Hobbies"];
            }
            if ((string.IsNullOrEmpty(collection["Hobbies"])) || (string.IsNullOrEmpty(collection["Name"])) || (string.IsNullOrEmpty(collection["Color"])))
            {
                ModelState.AddModelError("Hobbies", "Убедитесь, что все поля выбраны!");
                return View("Questionnaire");
            }
            else
            {
                Methods.CreateItem(question);
                return View();
            }
        }
        //Soft removal of article
        [HttpPost]
        public ActionResult RemoveItemSoftly(int id)
        {
            Methods.RemoveArticleSoftly(id);
            return RedirectToAction("Index");
        }
        //Permanent removal of feedback
        public ActionResult RemoveItemPermanently(int id)
        {
            Methods.RemoveCommentaryPermanently(id);
            return RedirectToAction("Commentaries");
        }
    }
}
