using System;
using System.Security.Cryptography;
using System.Windows;
using PasteBinSharp.UI.Properties;

namespace PasteBinSharp.UI
{
    /// <summary>
    /// Logique d'interaction pour SettingsWindow.xaml
    /// </summary>
    partial class SettingsWindow : Window
    {
        private readonly Settings _settings;

        public SettingsWindow(Settings settings)
        {
            _settings = settings;
            
            InitializeComponent();

            txtApiDevKey.Text = _settings.ApiDevKey;
            txtUserName.Text = _settings.UserName;
            pwdPassword.Password = string.Empty;
            if (!string.IsNullOrEmpty(_settings.Password))
            {
                try
                {
                    pwdPassword.Password = PasswordHelper.UnprotectPassword(_settings.Password);
                }
                catch (PasswordDecodeException)
                {
                    MessageBox.Show(
                        Properties.Resources.Settings_PasswordDecodeFailed,
                        Properties.Resources.Error_Title,
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            _settings.ApiDevKey = txtApiDevKey.Text;
            _settings.UserName = txtUserName.Text;
            _settings.Password = string.Empty;
            if (!string.IsNullOrEmpty(pwdPassword.Password))
                _settings.Password = PasswordHelper.ProtectPassword(pwdPassword.Password);
            _settings.Save();
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(txtApiDevKey.Text))
                txtApiDevKey.SetErrorMessage(Properties.Resources.Validation_APIKeyMustBeSet);
            else
                txtApiDevKey.SetErrorMessage(null);

            if (!string.IsNullOrEmpty(txtUserName.Text) && string.IsNullOrEmpty(pwdPassword.Password))
                pwdPassword.SetErrorMessage(Properties.Resources.Validation_PasswordMustBeSet);
            else
                pwdPassword.SetErrorMessage(null);
        }

        private void SettingChanged(object sender, RoutedEventArgs e)
        {
            Validate();
        }
    }
}
