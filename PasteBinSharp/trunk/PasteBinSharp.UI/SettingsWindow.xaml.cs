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
                pwdPassword.Password = PasswordHelper.UnprotectPassword(_settings.Password);
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
                txtApiDevKey.SetErrorMessage("API key must be set");
            else
                txtApiDevKey.SetErrorMessage(null);

            if (!string.IsNullOrEmpty(txtUserName.Text) && string.IsNullOrEmpty(pwdPassword.Password))
                pwdPassword.SetErrorMessage("Password must be set if user name is set");
            else
                pwdPassword.SetErrorMessage(null);
        }

        private void SettingChanged(object sender, RoutedEventArgs e)
        {
            Validate();
        }
    }
}
