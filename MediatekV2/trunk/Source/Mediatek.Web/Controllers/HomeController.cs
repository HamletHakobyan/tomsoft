using System.Linq;
using System.Web.Mvc;
using Mediatek.Entities;
using Mediatek.Web.Properties;

namespace Mediatek.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var repository = MvcApplication.GetRepository())
            {
                var movies = repository.Medias.OfType<Movie>();
                ViewData["Message"] = Resources.Welcome_to_Mediatek_Web_;
                ViewData["MovieCount"] = movies.Count();
                return View();
            }
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
