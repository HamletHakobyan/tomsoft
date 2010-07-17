using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Util
{
    public class EventArgs<TEventData> : EventArgs
    {
        public EventArgs()
        {
        }

        public EventArgs(TEventData eventData)
        {
            this.EventData = eventData;
        }

        public TEventData EventData { get; set; }
    }
}
