using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlickIt
{
    public enum SessionStatus : int
    {
        Error = -1,
        None = 0,
        Authenticating,
        LoggedIn,
        Uploading,
        Downloading
    }
}
