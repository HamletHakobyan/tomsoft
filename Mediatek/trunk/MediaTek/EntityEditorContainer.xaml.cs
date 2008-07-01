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
    /// Interaction logic for EntityEditor.xaml
    /// </summary>
    public partial class EntityEditorContainer : UserControl
    {
        public EntityEditorContainer()
        {
            InitializeComponent();
        }

        private ObjectState objState;

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.ReturnModal(true);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null && objState != null)
            {
                objState.RestoreState(this.DataContext);
            }
            this.ReturnModal(false);
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != null && objState != null)
            {
                objState.RestoreState(e.OldValue);
                objState = null;
            }

            objState = null;

            if (e.NewValue != null)
                objState = new ObjectState(e.NewValue);
        }

        public static Control CreateEditor<T>(object entity) where T : Control, new()
        {
            Control editor = Activator.CreateInstance<T>();
            EntityEditorContainer container = new EntityEditorContainer();
            container.scvEditor.Content = editor;
            container.DataContext = entity;
            return container;
        }
    }
}
