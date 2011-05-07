using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Millionaire.View
{
    /// <summary>
    /// Interaction logic for SlideShowView.xaml
    /// </summary>
    public partial class SlideShowView : UserControl
    {
        public SlideShowView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(this);
        }
    }
}
