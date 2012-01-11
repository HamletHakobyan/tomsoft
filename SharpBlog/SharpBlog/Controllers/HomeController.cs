using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SharpBlog.Models;

namespace SharpBlog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new BlogEntities())
            {
                ViewBag.BlogName = context.Properties.BlogName;
                var articles = context.Articles.Take(5).ToList();
                return View(articles);
            }
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
