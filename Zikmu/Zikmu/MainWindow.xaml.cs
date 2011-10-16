using System.Windows;
using Zikmu.ViewModel;

namespace Zikmu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Prevent resizing
            // ResizeMode can't be used because it removes the glass border
            MinWidth = MaxWidth = Width;
            MinHeight = MaxHeight = Height;

            DataContext = new MainWindowViewModel();
        }
    }
}
