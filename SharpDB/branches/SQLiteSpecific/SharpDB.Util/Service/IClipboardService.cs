using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SharpDB.Util.Service
{
    public interface IClipboardService
    {
        bool ContainsData(string format);
        void SetData(string format, object data);
        object GetData(string format);

        bool ContainsText();
        bool ContainsText(TextDataFormat format);
        void SetText(string text);
        void SetText(string text, TextDataFormat format);
        string GetText();
        string GetText(TextDataFormat format);
    }
}
