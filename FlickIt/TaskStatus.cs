using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlickIt
{
    public enum TaskStatus : int
    {
        Error = -1,
        None = 0,
        Uploading,
        Downloading,
        Cancelled,
        Complete
    }
}
