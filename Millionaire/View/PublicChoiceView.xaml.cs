using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Millionaire.ViewModel;

namespace Millionaire.View
{
    /// <summary>
    /// Interaction logic for PublicChoiceView.xaml
    /// </summary>
    public partial class PublicChoiceView : UserControl
    {
        public PublicChoiceView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(this);
        }

        private void btnDebug_Click(object sender, RoutedEventArgs e)
        {

        }

        private PublicChoiceViewModel _vm;
        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _vm = DataContext as PublicChoiceViewModel;
            if (_vm != null)
            {
                _vm.ReferenceHeight = 351.526;
            }
        }
    }
}
