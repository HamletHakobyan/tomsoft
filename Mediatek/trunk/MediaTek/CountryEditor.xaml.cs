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
    /// Interaction logic for CountryEditor.xaml
    /// </summary>
    public partial class CountryEditor : UserControl
    {
        public CountryEditor()
        {
            InitializeComponent();
        }

        public Country Target
        {
            get { return this.DataContext as Country; }
        }


        private void cmbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Target.Language = App.Current.DataContext.Languages.Where(l => l.Id == Target.LanguageId).FirstOrDefault();
        }
    }
}
