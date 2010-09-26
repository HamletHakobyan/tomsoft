using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediatek.Messaging
{
    enum NavigationDestination
    {
        Home,
        Movies,
        Albums,
        Books
    }

    class NavigationMessage
    {
        public NavigationMessage(NavigationDestination destination)
        {
            Destination = destination;
        }

        public NavigationDestination Destination { get; private set; }
    }
}
