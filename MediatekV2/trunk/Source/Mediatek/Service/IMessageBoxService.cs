using System.Windows;

namespace Mediatek.Service
{
    public interface IMessageBoxService
    {
        MessageBoxResult ShowDialog(string message, string title, MessageBoxButton buttons = MessageBoxButton.OK, MessageBoxImage image = MessageBoxImage.None);
    }
}
