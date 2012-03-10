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
using VBulletinBox.ViewModels;

namespace VBulletinBox.Views
{
    /// <summary>
    /// Interaction logic for AccountView.xaml
    /// </summary>
    public partial class AccountView : UserControl
    {
        public AccountView()
        {
            InitializeComponent();
        }

        //void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        //{
        //    AccountViewModel vm = this.DataContext as AccountViewModel;
        //    if (vm != null)
        //    {
        //        vm.Password = passwordBox.Password;
        //    }
        //}

        //private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    AccountViewModel vm = this.DataContext as AccountViewModel;
        //    if (vm != null)
        //    {
        //        passwordBox.Password = vm.Password;
        //    }
        //}
    }
}
