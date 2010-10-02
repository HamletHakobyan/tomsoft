using System.Web.Mvc;
using System.Web.Routing;

namespace Mediatek.Web.Views
{
    public class SearchViewPage : ViewPage
    {
        public RouteValueDictionary GetCriteria()
        {
            return (ViewData["criteria"] as RouteValueDictionary) ?? new RouteValueDictionary();
        }

        public RouteValueDictionary AddCriteria(object values)
        {
            var newCriteria = new RouteValueDictionary(values);
            var criteria = ViewData["criteria"] as RouteValueDictionary;
            if (criteria != null)
            {
                foreach (var item in criteria)
                {
                    if (!newCriteria.ContainsKey(item.Key))
                        newCriteria.Add(item.Key, item.Value);
                }
            }
            return newCriteria;
        }
    }
}