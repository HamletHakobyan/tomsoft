using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Text;
using System.ComponentModel;
using System.Web.Routing;

namespace Mediatek.Web.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString BulletedList<T>(this HtmlHelper helper, IEnumerable<T> items)
        {
            Func<object, string> stringSelector = obj => helper.Encode(obj);
            if (typeof(MvcHtmlString).IsAssignableFrom(typeof(T)))
                stringSelector = obj => ((MvcHtmlString) obj).ToHtmlString();

            StringBuilder sb = new StringBuilder();
            sb.Append("<ul>");
            foreach (var item in items)
            {
                sb.AppendFormat("<li>{0}</li>", stringSelector(item));
            }
            sb.Append("</ul>");
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString SortLink(this HtmlHelper helper, string linkText, string sortField)
        {
            return helper.SortLink(
                MvcHtmlString.Create(helper.Encode(linkText)),
                sortField);
        }
        
        public static MvcHtmlString SortLink(this HtmlHelper helper, MvcHtmlString linkContent, string sortField)
        {
            var routeValues = helper.GetAttemptedRouteValues();
            
            var sortDirection = ListSortDirection.Ascending;
            var currentSortField = routeValues["sortField"] as string;
            if (sortField == currentSortField && routeValues.ContainsKey("sortDirection"))
            {
                var currentSortDirection = (ListSortDirection)routeValues["sortField"];
                sortDirection =
                    currentSortDirection == ListSortDirection.Ascending
                        ? ListSortDirection.Descending
                        : ListSortDirection.Ascending;
            }

            return helper.SortLink(
                linkContent,
                sortField,
                sortDirection);
        }

        public static MvcHtmlString SortLink(this HtmlHelper helper, string linkText, string sortField, ListSortDirection sortDirection)
        {
            return helper.SortLink(
                MvcHtmlString.Create(helper.Encode(linkText)),
                sortField,
                sortDirection);
        }

        public static MvcHtmlString SortLink(this HtmlHelper helper, MvcHtmlString linkContent, string sortField, ListSortDirection sortDirection)
        {
            var routeValues = helper.GetAttemptedRouteValues();
            routeValues["sortField"] = sortField;
            routeValues["sortField"] = sortDirection;

            UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            string currentAction = helper.GetCurrentAction();
            string url = urlHelper.Action(currentAction, routeValues);

            TagBuilder tag = new TagBuilder("a");
            tag.MergeAttribute("href", url);
            tag.InnerHtml = linkContent.ToHtmlString();
            return MvcHtmlString.Create(tag.ToString());
        }

        public static MvcHtmlString CriteriaLink(this HtmlHelper helper, string linkText, object criteria)
        {
            return helper.CriteriaLink(
                MvcHtmlString.Create(helper.Encode(linkText)),
                criteria);
        }

        public static MvcHtmlString CriteriaLink(this HtmlHelper helper, MvcHtmlString linkContent, object criteria)
        {
            var routeValues = helper.GetAttemptedRouteValues();
            var criteriaValues = new RouteValueDictionary(criteria);
            foreach (var item in criteriaValues)
            {
                routeValues[item.Key] = item.Value;
            }

            UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            string currentAction = helper.GetCurrentAction();
            string url = urlHelper.Action(currentAction, routeValues);

            TagBuilder tag = new TagBuilder("a");
            tag.MergeAttribute("href", url);
            tag.InnerHtml = linkContent.ToHtmlString();
            return MvcHtmlString.Create(tag.ToString());
        }

        private static string GetCurrentAction(this HtmlHelper helper)
        {
            //return helper.ViewContext.Controller.ValueProvider["action"].RawValue as string;
            return helper.ViewContext.Controller.ValueProvider.GetValue("action").RawValue as string;
        }

        private static RouteValueDictionary GetAttemptedRouteValues(this HtmlHelper helper)
        {
            var valueProvider = helper.ViewContext.Controller.ValueProvider;
            //var routeValues = new RouteValueDictionary(
            //    helper.ViewContext.Controller.ValueProvider
            //        .ToDictionary(x => x.Key, x => (object)x.Value.AttemptedValue));
            var routeValues = new RouteValueDictionary(
                helper.ViewContext.RouteData.Values.Keys
                    .ToDictionary(key => key, key => (object)valueProvider.GetValue(key).AttemptedValue));
            return routeValues;
        }
    }
}
