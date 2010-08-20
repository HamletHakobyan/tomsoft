using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleships.Service
{
    public interface INavigationService
    {
        object Current { get; }
        void Navigate(object destination);
    }
}
