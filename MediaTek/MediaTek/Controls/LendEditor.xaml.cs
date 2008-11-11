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
            {
                Movie current = null;
                if (this.DataContext is Lend)
                    current = (this.DataContext as Lend).Movie;
                e.Accepted = !m.Lent || m.Equals(current);
            }
            else
                e.Accepted = false;
        }

        private void Grid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (cmbMovie.ItemsSource is ListCollectionView)
                (cmbMovie.ItemsSource as ListCollectionView).Refresh();
        }
    }
}
