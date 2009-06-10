using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Millionaire.ViewModel;
using System.ComponentModel;

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
