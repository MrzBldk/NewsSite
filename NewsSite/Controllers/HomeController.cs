using NewsSite.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System;

namespace NewsSite.Controllers
{
    public class HomeController : Controller
    {

        NewsContext db = new NewsContext();

        public async Task<ActionResult> Index()
        {
            ViewBag.News = await db.News.OrderByDescending(x=>x.Id).Take(3).ToListAsync();
            return View();
        }

        public async Task<ActionResult> News(int? id)
        {
            var page = id ?? 0;
            if (Request.IsAjaxRequest())
            {
                return PartialView("NewsScrolling", await GetItemsPage(page));
            }
            return View(await GetItemsPage(page));
        }

        private async Task<List<News>> GetItemsPage(int page = 1)
        {
            var itemsToSkip = page * 3;
            return await db.News.OrderByDescending(x => x.Id).Skip(itemsToSkip).Take(3).ToListAsync();
        }

        public async Task<ActionResult> NewsArticle(int id)
        {
            return View(await db.News.FindAsync(id));
        }

        public ActionResult CreateNews()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return Content(Resources.Resource.AccessViolation);
        }

        [HttpPost]
        public async Task<ActionResult> CreateNews(NewsModel model, HttpPostedFileBase image)
        {
            if (ModelState.IsValid && image != null)
            {
                byte[] data;
                using (Stream inputStream = image.InputStream)
                {
                    var memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    data = memoryStream.ToArray();
                }
                var news = new News() { Header = model.Header, Subheader = model.Subheader, Text = model.Text, Image = data };
                db.News.Add(news);
                await db.SaveChangesAsync();
                return RedirectToAction("News", "Home");
            }
            return View(model);
        }

        public ActionResult EditNews(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(new NewsModel { Id = id });
            }
            return Content(Resources.Resource.AccessViolation);
        }

        [HttpPost]
        public async Task<ActionResult> EditNews(NewsModel model)
        {
            if (ModelState.IsValid)
            {
                News news = await db.News.FindAsync(model.Id);
                news.Header = model.Header;
                news.Subheader = model.Subheader;
                news.Text = model.Text;
                await db.SaveChangesAsync();
                return RedirectToAction("News", "Home");
            }
            return View(model);
        }

        public async Task<ActionResult> DeleteNews(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                News news = await db.News.FindAsync(id);
                db.News.Remove(news);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("News", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult ChangeCulture(string lang)
        {
            string returnUrl = Request.UrlReferrer.AbsolutePath;
            var cultures = new List<string>() { "ru", "en"};
            if (!cultures.Contains(lang))
            {
                lang = "ru";
            }
            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;
            else
            {

                cookie = new HttpCookie("lang");
                cookie.HttpOnly = false;
                cookie.Value = lang;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(returnUrl);
        }
    }
}