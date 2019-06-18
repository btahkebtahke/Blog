using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Blog.Domain.Entities;

namespace Blog.Domain.ActionsWithDB
{
    //CLass with the methods for working with database
    public static class Methods
    {
        //Variable for connecting to DB 
        public static BlogContext context = new BlogContext("Blog");
        //3 overloads for Creating items (article/commentary/questionaire)
        public static void CreateItem(Article article, string tagContent)
        {
            article.Date = DateTime.Now;
            context.Articles.Add(article);
            context.SaveChanges();
            Article tempArticle = context.Articles.FirstOrDefault(p => p.Name == article.Name);
            string splContentIndex;
            string[] splittedContent = tagContent.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            Tag[] tempTags = new Tag[splittedContent.Length];
            for (int i = 0; i < splittedContent.Length; i++)
            {
                splContentIndex = splittedContent[i];
                Tag tag = context.Tags.FirstOrDefault(p => p.Content == splContentIndex);

                if (tag != null)
                {
                    if (tag.Content == splContentIndex)
                    {
                        tag.Articles.Add(tempArticle);
                        context.SaveChanges();
                    }
                    else
                    {
                        tempTags[i] = new Tag();
                        tempTags[i].Content = splittedContent[i];
                        tempArticle.Tags.Add(tempTags[i]);
                        context.SaveChanges();
                    }
                }
                else
                {
                    tempTags[i] = new Tag();
                    tempTags[i].Content = splittedContent[i];
                    tempArticle.Tags.Add(tempTags[i]);
                    context.SaveChanges();
                }
            }
        }
        public static void CreateItem(Commentary comment)
        {
            comment.Date = DateTime.Now;
            context.Commentaries.Add(comment);
            context.SaveChanges();
        }
        public static void CreateItem(Questionnaire question)
        {
            context.Questionnaires.Add(question);
            context.SaveChanges();
        }
        //Soft and permanent removal methods
        public static string CutContent(string content)
        {
            string a = "";
            if (content.Length > 100)
            {
                a = content.Substring(0, 100) + "...";
                return a;
            }
            else return content;
        }


        public static void RemoveArticleSoftly(int id)
        {
            Article b = context.Articles.Find(id);
            b.IsDeleted = true;
            context.Entry(b).State = EntityState.Modified;
            context.SaveChanges();
        }
        public static void RemoveCommentaryPermanently(int id)
        {
            Commentary b = context.Commentaries.Find(id);
            context.Commentaries.Remove(b);
            context.SaveChanges();
        }
        public static Article FindIdInArticles(int id)
        {
            Article a = context.Articles.Include(p => p.Tags).FirstOrDefault(p => p.ID == id);
            return a;
        }
    }
}
