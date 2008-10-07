using System.Windows.Input;

namespace MediaTek.Utilities
{
    public static class CustomCommands
    {
        static CustomCommands()
        {
            About = new RoutedUICommand("About", "About", typeof(CustomCommands));
            Add = new RoutedUICommand("Add", "Add", typeof(CustomCommands));
            Lend = new RoutedUICommand("Lend", "Lend", typeof(CustomCommands));
            Return = new RoutedUICommand("Return", "Return", typeof(CustomCommands));
            Quit = new RoutedUICommand("Quit", "Quit", typeof(CustomCommands));
            Quit.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));
        }

        public static RoutedUICommand About { get; set; }
        public static RoutedUICommand Add { get; set; }
        public static RoutedUICommand Lend { get; set; }
        public static RoutedUICommand Return { get; set; }
        public static RoutedUICommand Quit { get; set; }
    }
}
