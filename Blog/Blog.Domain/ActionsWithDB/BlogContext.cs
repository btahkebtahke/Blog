using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.Entities;

namespace Blog.Domain.ActionsWithDB
{
    public class BlogContext : DbContext
    {
        static BlogContext()
        {
            Database.SetInitializer(new BlogContextInitializer());
        }
        public BlogContext(string nameOrConnectionString) :
            base(nameOrConnectionString)
        { }
        //Creation of the relation many-to-many (Articles-Tags)
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasMany(c => c.Tags)
                .WithMany(p => p.Articles)
                .Map(m =>
                {
                    // Reference to the relations table
                    m.ToTable("TagsARticlesTable");

                    // Foreign keys for relations table
                    m.MapLeftKey("ArticleId");
                    m.MapRightKey("TagId");
                });
        }
        public IDbSet<Article> Articles { get; set; }
        public IDbSet<Commentary> Commentaries { get; set; }
        public IDbSet<Questionnaire> Questionnaires { get; set; }
        public IDbSet<Tag> Tags { get; set; }
    }

    public class BlogContextInitializer : DropCreateDatabaseIfModelChanges<BlogContext>
    {

        //Initialization of the database
        protected override void Seed(BlogContext context)
        {
            var articles = new List<Article>
            {
                new Article
                {
                    Content = "asdjkaksdj aksdjaksdj akjsdakjs dkajsdkja  qwrsd asd as asd" +
                    " asdadas das das dwqe we qwe qr qwrsd asd as asd asdadas" +
                    " das das dwqe we qwe qr qwrsd asd as" +
                    " asdadas das das dwqe we qwe qr qwrsd asd as asd asdadas das das dwqe we " +
                    "qwe qr qwrsd asd as asd asdadas das das dwqe we qwe qr qwrsd asd as"+
                    " asdadas das das dwqe we qwe qr qwrsd asd as asd asdadas das das dwqe we " +
                    "qwe qr qwrsd asd as asd asdadas das das dwqe we qwe qr qwrsd asd as", Name = "1st article", Date = DateTime.Now
                },
                new Article
                {
                    Content = "Hello asd asdadas das das dwqe we qwe qr qwrsd asd as asd" +
                    " asdadas das das dwqe we qwe qr qwrsd asd as asd asdadas das das dwqe we " +
                    "qwe qr qwrsd asd as asd asdadas das das dwqe we qwe qr qwrsd asd as"+
                    " asdadas das das dwqe we qwe qr qwrsd asd as asd asdadas das das dwqe we " +
                    "qwe qr qwrsd asd as asd asdadas das das dwqe we qwe qr qwrsd asd as", Name = "2nd article", Date = DateTime.Now
                },
                new Article
                {
                    Content = "Hello asd rqw qwj rqwr lqwrjkwq rasdasd asd as"+
                    " asdadas das das dwqe we qwe qr qwrsd asd as asd asdadas das das dwqe we " +
                    "qwe qr qwrsd asd as asd asdadas das das dwqe we qwe qr qwrsd asd as"+
                    " asdadas das das dwqe we qwe qr qwrsd asd as asd asdadas das das dwqe we " +
                    "qwe qr qwrsd asd as asd asdadas das das dwqe we qwe qr qwrsd asd as", Name = "3rd article", Date = DateTime.Now
                },
                new Article
                {
                    Content = "Hello asd asdwrqrwrwqqwrwqrasd asqwrwrqd as"+
                    " asdadas das das dwqe we qwe qr qwrsd asd as asd asdadas das das dwqe we " +
                    "qwe qr qwrsd asd as asd asdadas das das dwqe we qwe qr qwrsd asd as"+
                    " asdadas das das dwqe we qwe qr qwrsd asd as asd asdadas das das dwqe we " +
                    "qwe qr qwrsd asd as asd asdadas das das dwqe we qwe qr qwrsd asd as", Name = "4th article", Date = DateTime.Now
                }
            };
            articles.ForEach(art => context.Articles.Add(art));
            context.SaveChanges();

            var Commentaries = new List<Commentary>
            {
                new Commentary
                {
                    Content = "Привет, спасибо, что проделали такую работу! :)", Name = "Валерий", Date = DateTime.Now
                },
                new Commentary
                {
                    Content = "Нужно что-то менять, ребят...", Name = "Иван", Date = DateTime.Now
                },
            };
            Commentaries.ForEach(com => context.Commentaries.Add(com));
            context.SaveChanges();

            var Tag = new List<Tag>
            {
                new Tag
                {
                    Content = "Спорт"
                },
                new Tag
                {
                    Content = "Природа"
                },
            };
            Article a = context.Articles.Include(p => p.Tags).FirstOrDefault(p => p.Name == "1st article");
            foreach (Tag e in Tag)
            {
                a.Tags.Add(e);
                context.SaveChanges();
            }


        }



    }
}
