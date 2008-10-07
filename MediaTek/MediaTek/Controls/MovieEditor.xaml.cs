using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using MediaTek.DataModel;
using MediaTek.Utilities;
using Microsoft.Win32;

namespace MediaTek.Controls
{
    /// <summary>
    /// Interaction logic for MovieEditor.xaml
    /// </summary>
    public partial class MovieEditor : UserControl
    {
        public MovieEditor()
        {
            InitializeComponent();
        }

        public Movie Target
        {
            get { return this.DataContext as Movie; }
        }

        private void cmbDirector_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            if (combo.NewItemTyped())
            {
                Director d = new Director();
                d.Name = combo.Text;
                combo.QueryAddItem(
                    e,
                    d,
                    "Directors",
                    "This director isn't in the database, would you like to add it ?",
                    "Unknown director",
                    "New Director");
            }
        }

        private void cmbLanguage_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
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

        private void cmbMediaType_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            if (combo.NewItemTyped())
            {
                MediaType m = new MediaType();
                m.Name = combo.Text;
                combo.QueryAddItem(
                    e,
                    m,
                    "MediaTypes",
                    "This media type isn't in the database, would you like to add it ?",
                    "Unknown media type",
                    "New Media Type");
            }
        }
    }
}
