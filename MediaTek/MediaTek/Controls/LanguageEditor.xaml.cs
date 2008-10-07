using System.Windows.Controls;
using MediaTek.DataModel;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System;
using System.Windows;

namespace MediaTek.Controls
{
    /// <summary>
    /// Interaction logic for LanguageEditor.xaml
    /// </summary>
    public partial class LanguageEditor : UserControl
    {
        public LanguageEditor()
        {
            InitializeComponent();
        }

        public Language Target
        {
            get { return this.DataContext as Language; }
        }

    }
}
