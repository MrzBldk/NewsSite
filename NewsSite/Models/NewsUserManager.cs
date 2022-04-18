using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace NewsSite.Models
{
    public class NewsUserManager : UserManager<NewsUser>
    {
        public NewsUserManager(IUserStore<NewsUser> store) : base(store)
        {
        }
        public static NewsUserManager Create(IdentityFactoryOptions<NewsUserManager> options, IOwinContext context)
        {
            var db = context.Get<NewsContext>();
            var manager = new NewsUserManager(new UserStore<NewsUser>(db));
            return manager;
        }
    }
}