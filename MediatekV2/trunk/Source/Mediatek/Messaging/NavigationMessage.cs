
using System;
namespace Mediatek.Messaging
{
    enum NavigationDestination
    {
        Home,
        Movies,
        Albums,
        Books,
        People,
        ReferenceData,
        Settings,
        Loans,
        Custom
    }

    class NavigationMessage
    {
        public NavigationMessage(NavigationDestination destination)
        {
            if (destination == NavigationDestination.Custom)
                throw new ArgumentException("Custom destination must be specified");
            Destination = destination;
        }

        public NavigationMessage(object customDestination)
        {
            Destination = NavigationDestination.Custom;
            CustomDestination = customDestination;
        }

        public NavigationDestination Destination { get; private set; }
        public object CustomDestination { get; private set; }
    }
}
