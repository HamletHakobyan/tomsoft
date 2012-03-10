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
using System.Windows.Shapes;

namespace VBulletinBox.Util
{
    /// <summary>
    /// Interaction logic for EditorDialog.xaml
    /// </summary>
    public partial class EditorDialog : Window
    {
        public EditorDialog(object data, Type editorType)
        {
            InitializeComponent();
            DataTemplate template = new DataTemplate();
            template.VisualTree = new FrameworkElementFactory(editorType);
            content.ContentTemplate = template;
            this.DataContext = data;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
