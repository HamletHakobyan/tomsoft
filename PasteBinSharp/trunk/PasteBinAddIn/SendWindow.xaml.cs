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
using PasteBinSharp;

namespace PasteBinAddIn
{
    /// <summary>
    /// Interaction logic for SendWindow.xaml
    /// </summary>
    public partial class SendWindow : Window
    {
        private readonly PasteBinEntry _entry;

        public SendWindow()
        {
            InitializeComponent();
            _entry = new PasteBinEntry();
            this.DataContext = this;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public PasteBinEntry Entry
        {
            get { return _entry; }
        }
    }
}
