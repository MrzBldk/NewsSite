using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace NewsSite.Models
{
    public class NewsContext : IdentityDbContext<NewsUser>
    {
        public DbSet<News> News { get; set; }
        public NewsContext() : base("NewsDb") { }

        public static NewsContext Create()
        {
            return new NewsContext();
        }
    }
}