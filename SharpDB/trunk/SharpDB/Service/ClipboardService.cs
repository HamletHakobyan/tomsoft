using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDB.Util.Service;
using System.Windows;

namespace SharpDB.Service
{
    public class ClipboardService : IClipboardService
    {
        public bool ContainsData(string format)
        {
            return Clipboard.ContainsData(format);
        }

        public void SetData(string format, object data)
        {
            Clipboard.SetData(format, data);
        }

        public object GetData(string format)
        {
            return Clipboard.GetData(format);
        }

        public bool ContainsText()
        {
            return Clipboard.ContainsText();
        }

        public bool ContainsText(TextDataFormat format)
        {
            return Clipboard.ContainsText(format);
        }

        public void SetText(string text)
        {
            Clipboard.SetText(text);
        }

        public void SetText(string text, System.Windows.TextDataFormat format)
        {
            Clipboard.SetText(text, format);
        }

        public string GetText()
        {
            return Clipboard.GetText();
        }

        public string GetText(System.Windows.TextDataFormat format)
        {
            return Clipboard.GetText(format);
        }
    }
}
