using System;
using System.Windows;
using System.Windows.Controls;
using SharpDB.Util.Service;

namespace SharpDB
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        public DialogWindow()
        {
            InitializeComponent();
        }

        public bool? Show(IDialogViewModel viewModel)
        {
            this.DataContext = viewModel;
            viewModel.CloseRequested += viewModel_CloseRequested;
            try
            {
                viewModel.OnShow();
                return this.ShowDialog();
            }
            finally
            {
                viewModel.CloseRequested -= viewModel_CloseRequested;
            }

        }

        void viewModel_CloseRequested(object sender, CloseRequestedEventArgs e)
        {
            this.DialogResult = e.Result;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            var viewModel = this.DataContext as IDialogViewModel;
            if (viewModel != null)
            {
                viewModel.OnClose(this.DialogResult);
            }
        }

        private void DialogButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var dButton = button.DataContext as DialogButton;
                if (dButton != null && dButton.DialogResult != null)
                {
                    this.DialogResult = dButton.DialogResult;
                }
            }
        }

    }
}
