using System.Windows.Controls;
using MediaTek.DataModel;
using MediaTek.Utilities;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System;

namespace MediaTek.Controls
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

        private void cmbCountry_PreviewLostKeyboardFocus(object sender, System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            if (combo.NewItemTyped())
            {
                Country d = new Country();
                d.Name = combo.Text;
                combo.QueryAddItem(
                    e,
                    d,
                    "Countries",
                    "This country isn't in the database, would you like to add it ?",
                    "Unknown country",
                    "New Country");
            }
        }
    }
}
