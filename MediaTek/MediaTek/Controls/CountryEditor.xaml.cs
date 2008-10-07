using System.Windows.Controls;
using MediaTek.DataModel;
using MediaTek.Utilities;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System;

namespace MediaTek.Controls
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

        private void cmbLanguage_PreviewLostKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            if (combo.NewItemTyped())
            {
                Language l = new Language();
                l.Name = combo.Text;
                combo.QueryAddItem(
                    e,
                    l,
                    "Languages",
                    "This language isn't in the database, would you like to add it ?",
                    "Unknown language",
                    "New Language");
            }
        }
    }
}
