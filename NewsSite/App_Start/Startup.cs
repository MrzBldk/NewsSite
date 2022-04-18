using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using NewsSite.Models;
using Owin;

[assembly: OwinStartup(typeof(NewsSite.Startup))]

namespace NewsSite
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<NewsContext>(NewsContext.Create);
            app.CreatePerOwinContext<NewsUserManager>(NewsUserManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}