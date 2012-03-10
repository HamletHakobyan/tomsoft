using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using CodeKicker.BBCode;

namespace VBulletinBox.Converters
{
    public class BBCodeToHtmlConverter : IValueConverter
    {
        private static readonly BBTag[] _tags;
        private static string css = @"<style type=""text/css"">
.code {
    background-color: lightgray;
    border: 1px solid black;

}

.quote {
    background-color: cyan;
    border: 1px solid black;
}
</style>

";

        static BBCodeToHtmlConverter()
        {
            _tags = new[]
                {
                    new BBTag("b", "<b>", "</b>"), 
                    new BBTag("i", "<span style=\"font-style:italic;\">", "</span>"), 
                    new BBTag("u", "<span style=\"text-decoration:underline;\">", "</span>"), 
                    new BBTag("color", "<span style=\"color:${val};\">", "</span>", new BBAttribute("val", "")), 
                    new BBTag("size", "<span style=\"font-size:${val}pt;\">", "</span>", new BBAttribute("val", "")), 
                    new BBTag("code", "<pre class=\"code\"><strong>Code ${lang}</strong>\n", "</pre>", new BBAttribute("lang", "")), 
                    new BBTag("codeinline", "<span class=\"code\">", "</span>", new BBAttribute("lang", "")), 
                    new BBTag("pre", "<pre>", "</pre>"), 
                    new BBTag("img", "<img src=\"${content}\" />", "", false, true), 
                    new BBTag("quote", "<table class=\"quote\"><tr><td><strong>${from}</strong></td></tr><tr><td><blockquote>", "</blockquote></td></tr><table>", new BBAttribute("from", "")), 
                    new BBTag("list", "<ul>", "</ul>"), 
                    new BBTag("*", "<li>", "</li>", true, false), 
                    new BBTag("url", "<a href=\"${href}\">", "</a>", new BBAttribute("href", ""), new BBAttribute("href", "href")), 
                };
        }

        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string bbCode = value as string;
            if (bbCode == null)
                return null;
            var parser = new BBCodeParser(_tags);
            string html = parser.ToHtml(bbCode).Replace("\r", "").Replace("\n", "<br/>");
            return css + html;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
