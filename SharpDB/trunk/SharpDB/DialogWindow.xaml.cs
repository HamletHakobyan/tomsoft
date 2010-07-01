using System;
using System.Windows;
using System.Windows.Controls;
using SharpDB.Util.Dialogs;

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
            viewModel.OnShow();
            return this.ShowDialog();
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
                try
                {
                    viewModel.OnClose(this.DialogResult);
                }
                finally
                {
                    viewModel.CloseRequested -= viewModel_CloseRequested;
                }
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
