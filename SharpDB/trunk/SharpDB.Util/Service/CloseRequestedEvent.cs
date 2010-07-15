using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Util.Service
{
    public delegate void CloseRequestedEventHandler(object sender, CloseRequestedEventArgs e);

    public class CloseRequestedEventArgs : EventArgs
    {
        public bool? Result { get; set; }
    }
}
