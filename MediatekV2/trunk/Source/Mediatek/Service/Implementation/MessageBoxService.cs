using System.Windows;

namespace Mediatek.Service.Implementation
{
    public class MessageBoxService : IMessageBoxService
    {
        #region IMessageBoxService Members

        public MessageBoxResult ShowDialog(string message, string title, MessageBoxButton buttons = MessageBoxButton.OK, MessageBoxImage image = MessageBoxImage.None)
        {
            return MessageBox.Show(message, title, buttons, image);
        }

        #endregion
    }
}
