using System.Windows.Controls;
using MediaTek.DataModel;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System;
using System.Windows;
using MediaTek.Utilities;

namespace MediaTek.Controls
{
    /// <summary>
    /// Interaction logic for MediaTypeEditor.xaml
    /// </summary>
    public partial class MediaTypeEditor : UserControl
    {
        public MediaTypeEditor()
        {
            InitializeComponent();
        }

        public MediaType Target
        {
            get { return this.DataContext as MediaType; }
        }
    }
}
