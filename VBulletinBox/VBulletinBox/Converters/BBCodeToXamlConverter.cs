using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Markup;
using CodeKicker.BBCode;
using CodeKicker.BBCode.SyntaxTree;

namespace VBulletinBox.Converters
{
    [ValueConversion(typeof(string), typeof(FlowDocument))]
    public class BBCodeToXamlConverter : IValueConverter
    {
        private static readonly BBTag[] _tags;

        static BBCodeToXamlConverter()
        {
            string codeOpen = @"<LineBreak /><Floater BorderBrush=""Black"" BorderThickness=""1"" Background=""LightGray"" FontFamily=""Courier New""><Paragraph>";
            string codeClose = @"</Paragraph></Floater><LineBreak />";
            string quoteOpen = @"<LineBreak /><Floater BorderBrush=""Black"" BorderThickness=""1"" Background=""Cyan""><Paragraph>";
            string quoteClose = @"</Paragraph></Floater><LineBreak />";

            //string codeOpen = @"</Paragraph><Paragraph BorderBrush=""Black"" BorderThickness=""1"" Background=""LightGray"" FontFamily=""Courier New"">";
            //string codeClose = @"</Paragraph><Paragraph>";
            //string quoteOpen = @"</Paragraph><Paragraph BorderBrush=""Black"" BorderThickness=""1"" Background=""Cyan"">";
            //string quoteClose = @"</Paragraph><Paragraph>";

            _tags = new[]
            {
                new BBTag("b", "<Bold>", "</Bold>"),
                new BBTag("i", "<Italic>", "</Italic>"),
                new BBTag("u", "<Underline>", "</Underline>"),
                new BBTag("code", codeOpen, codeClose, new BBAttribute("lang", "")),
                new BBTag("quote", quoteOpen, quoteClose, new BBAttribute("from", "")),
                new BBTag("url", "<Hyperlink NavigateUri=\"${href}\">", "</Hyperlink>", new BBAttribute("href", ""), new BBAttribute("href", "href")),
                    /*new BBTag("img", "<img src=\"${content}\" />", "", false, true), 
                    new BBTag("list", "<ul>", "</ul>"), 
                    new BBTag("*", "<li>", "</li>", true, false), */
                };
        }

        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string bbCode = value as string;
            if (bbCode == null)
                return null;            
            return CreateDocument(bbCode);
        }

        private static FlowDocument CreateDocument(string bbCode)
        {
            var parser = new BBCodeParser(_tags);
            var tree = parser.ParseSyntaxTree(bbCode);

            string xaml = parser.ToHtml(bbCode).Replace("\r", "").Replace("\n", "<LineBreak />");
            //var visitor = new BBSyntaxVisitor(parser.Tags);
            //var newTree = visitor.Visit(tree);
            //string xaml = newTree.ToHtml();
            string docXaml = string.Format("<FlowDocument><Paragraph>{0}</Paragraph></FlowDocument>", xaml);

            var context = new ParserContext();
            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            var doc = (FlowDocument)XamlReader.Parse(docXaml, context);
            return doc;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    class BBSyntaxVisitor : SyntaxTreeVisitor
    {
        private readonly IList<BBTag> _tags;
        private readonly BBTag _paraTag;

        public BBSyntaxVisitor(IList<BBTag> tags)
        {
            _tags = tags;
            _paraTag = _tags.First(t => t.Name == "para");
        }

        private bool _inCode;

        protected override SyntaxTreeNode Visit(SequenceNode node)
        {
            var newNode = base.Visit(node);
            return newNode;
        }

        protected override SyntaxTreeNode Visit(TextNode node)
        {
            if (!_inCode)
            {
                var lines = node.Text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                if (lines.Length > 1)
                {
                    var nodes = new SyntaxTreeNodeCollection();
                    foreach (var line in lines)
                    {
                        var subNodes = new SyntaxTreeNode[] { new TextNode(line) };
                        nodes.Add(new TagNode(_paraTag, subNodes));
                    }
                    return new SequenceNode(nodes);
                }
            }

            var newNode = base.Visit(node);
            return newNode;
        }

        protected override SyntaxTreeNode Visit(TagNode node)
        {
            if (node.Tag.Name.Equals("code", StringComparison.InvariantCultureIgnoreCase))
                _inCode = true;
            
            var newNode = base.Visit(node);

            _inCode = false;
            return newNode;
        }
    }
}
