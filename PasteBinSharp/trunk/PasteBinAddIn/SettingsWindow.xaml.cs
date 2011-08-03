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
using PasteBinAddIn.Properties;

namespace PasteBinAddIn
{
    /// <summary>
    /// Logique d'interaction pour SettingsWindow.xaml
    /// </summary>
    partial class SettingsWindow : Window
    {
        private readonly Settings _settings;

        public SettingsWindow(Settings settings, string errorMessage = null)
        {
            _settings = settings;
            
            InitializeComponent();

            txtApiDevKey.Text = _settings.ApiDevKey;
            txtUserName.Text = _settings.UserName;
            pwdPassword.Password = string.Empty;
            if (!string.IsNullOrEmpty(_settings.Password))
                pwdPassword.Password = PasswordHelper.UnprotectPassword(_settings.Password);
            lblError.Content = errorMessage;
            imgError.Visibility = lblError.Visibility = string.IsNullOrEmpty(errorMessage) ? Visibility.Hidden : Visibility.Visible;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            _settings.ApiDevKey = txtApiDevKey.Text;
            _settings.UserName = txtUserName.Text;
            _settings.Password = string.Empty;
            if (!string.IsNullOrEmpty(pwdPassword.Password))
                _settings.Password = PasswordHelper.ProtectPassword(pwdPassword.Password);
            _settings.Save();
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
