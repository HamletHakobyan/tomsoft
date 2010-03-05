using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Mediatek.Entities;
using Mediatek.Web.Helpers;
using System.Dynamic;
using System.Web.Routing;

namespace Mediatek.Web.Controllers
{
    public class MovieController : Controller
    {
        // GET: /Movie/
        public ActionResult Index()
        {
            var repository = MvcApplication.GetRepository();
            var movies = repository.Medias.OfType<Movie>();

            movies = AddCriteriaToQuery(movies);
            ViewData["criteria"] = MakeCriteriaRouteValues();
            return View(movies);
        }

        private RouteValueDictionary MakeCriteriaRouteValues()
        {
            var criteria = new RouteValueDictionary();
            int year = Request.Get<int>("year");
            if (year > 0)
                criteria["year"] = year;

            Guid contributor = Request.Get<Guid>("contributor");
            if (contributor != Guid.Empty)
                criteria["contributor"] = contributor;

            return criteria;
        }

        private IQueryable<Movie> AddCriteriaToQuery(IQueryable<Movie> movies)
        {
            int year = Request.Get<int>("year");
            if (year > 0)
                movies = movies.Where(m => m.Year == year);

            Guid contributor = Request.Get<Guid>("contributor");
            if (contributor != Guid.Empty)
                movies = movies.Where(m => m.Contributions.Any(c => c.PersonId == contributor));

            return movies;
        }
    }
}
