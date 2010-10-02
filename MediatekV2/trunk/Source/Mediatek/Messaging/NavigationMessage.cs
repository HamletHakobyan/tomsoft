
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
        Loans
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
