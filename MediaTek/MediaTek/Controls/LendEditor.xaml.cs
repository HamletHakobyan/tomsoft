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
using MediaTek.DataModel;

namespace MediaTek.Controls
{
    /// <summary>
    /// Interaction logic for LendEditor.xaml
    /// </summary>
    public partial class LendEditor : UserControl
    {
        public LendEditor()
        {
            InitializeComponent();
        }

        private void cvsAvailableMovies_Filter(object sender, FilterEventArgs e)
        {
            Movie m = e.Item as Movie;
            if (m != null)
                e.Accepted = !m.Lent;
            else
                e.Accepted = false;
        }
    }
}
