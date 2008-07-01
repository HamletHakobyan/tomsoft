using System.Windows.Input;

namespace MediaTek
{
    public static class CustomCommands
    {
        static CustomCommands()
        {
            Quit = new RoutedUICommand("Quit", "Quit", typeof(CustomCommands));
        }

        public static RoutedUICommand Quit { get; set; }
    }
}
