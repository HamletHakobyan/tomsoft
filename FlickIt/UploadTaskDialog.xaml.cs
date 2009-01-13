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

namespace FlickIt
{
    /// <summary>
    /// Interaction logic for UploadTaskDialog.xaml
    /// </summary>
    public partial class UploadTaskDialog : Window
    {
        public UploadTaskDialog()
        {
            InitializeComponent();
        }

        public UploadTask Task
        {
            get { return DataContext as UploadTask; }
            set { this.DataContext = value; }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
