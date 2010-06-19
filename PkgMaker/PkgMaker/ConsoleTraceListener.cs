using System;
using System.Diagnostics;
using System.Globalization;

namespace PkgMaker
{
    public class ConsoleTraceListener : TextWriterTraceListener
    {
        public ConsoleTraceListener()
            : base(Console.Out)
        {
        }

        public bool UseColors { get; set; }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            if ((this.Filter == null) || this.Filter.ShouldTrace(eventCache, source, eventType, id, message, null, null, null))
            {
                var color = SetColor(eventType);
                this.Write(string.Format("[{0}] ", eventType));
                Console.ForegroundColor = color;
                this.WriteLine(message);
            }

        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
            if ((this.Filter == null) || this.Filter.ShouldTrace(eventCache, source, eventType, id, format, args, null, null))
            {
                var color = SetColor(eventType);
                this.Write(string.Format("[{0}] ", eventType));
                Console.ForegroundColor = color;
                if (args != null)
                {
                    this.WriteLine(string.Format(CultureInfo.InvariantCulture, format, args));
                }
                else
                {
                    this.WriteLine(format);
                }
            }
        }

        private ConsoleColor SetColor(TraceEventType eventType)
        {
            var oldColor = Console.ForegroundColor;
            if (UseColors)
            {
                switch (eventType)
                {
                    case TraceEventType.Error:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case TraceEventType.Information:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case TraceEventType.Warning:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    default:
                        break;
                }
            }
            return oldColor;
        }
    }
}
