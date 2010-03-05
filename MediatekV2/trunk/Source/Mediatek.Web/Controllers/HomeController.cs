using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mediatek.Entities;

namespace Mediatek.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var repository = MvcApplication.GetRepository();
            var movies = repository.Medias.OfType<Movie>();
            ViewData["Message"] = "Welcome to Mediatek Web!";
            ViewData["MovieCount"] = movies.Count();
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
