using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaTek
{
    public interface IFilterable
    {
        bool IsMatch(string pattern);
    }
}
