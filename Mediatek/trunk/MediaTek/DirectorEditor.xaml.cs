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

namespace MediaTek
{
    /// <summary>
    /// Interaction logic for DirectorEditor.xaml
    /// </summary>
    public partial class DirectorEditor : UserControl
    {
        public DirectorEditor()
        {
            InitializeComponent();
        }

        public Director Target
        {
            get { return this.DataContext as Director; }
        }

        private void cmbCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Target.Country = App.Current.DataContext.Countries.Where(c => c.Id == Target.CountryId).FirstOrDefault();
        }
    }
}
