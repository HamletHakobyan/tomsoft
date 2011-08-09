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
using System.Windows.Shapes;
using Mediatek.Service;

namespace Mediatek
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : MediatekWindow
    {
        public DialogWindow()
        {
            InitializeComponent();
        }

        protected virtual void OnDialogButtonClicked(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
                return;

            DialogButton dButton = button.DataContext as DialogButton;
            if (dButton == null)
                return;

            if (dButton.DialogResult.HasValue)
            {
                this.DialogResult = dButton.DialogResult;
            }
        }
    }
}
