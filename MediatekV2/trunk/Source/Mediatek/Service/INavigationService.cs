namespace Mediatek.Service
{
    public interface INavigationService
    {
        object Current { get; }
        bool Navigate(object dest);
        bool GoBack();
        bool GoForward();
        bool CanGoBack { get; }
        bool CanGoForward { get; }
    }
}
