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

        public static Control CreateEditor<T>(object entity, string title) where T : Control, new()
        {
            Control editor = Activator.CreateInstance<T>();
            EntityEditorContainer container = CreateContainer(editor);
            container.scvEditor.Content = editor;
            container.lblTitle.Content = title;
            container.DataContext = entity;
            return container;
        }

        public static Control CreateEditor(object entity, string title)
        {
            Type type = entity.GetType();
            EditorControlAttribute[] attr = type.GetCustomAttributes(typeof(EditorControlAttribute), true).Cast<EditorControlAttribute>().ToArray();
            if (attr.Length > 0)
            {
                Type editorType = attr[0].EditorType;
                Control editor = Activator.CreateInstance(editorType) as Control;
                EntityEditorContainer container = CreateContainer(editor);
                container.scvEditor.Content = editor;
                container.lblTitle.Content = title;
                container.DataContext = entity;
                return container;

            }
            else
            {
                throw new NotSupportedException("No editor defined for type " + type.FullName);
            }
        }

        private static EntityEditorContainer CreateContainer(Control editor)
        {
            EntityEditorContainer container = new EntityEditorContainer();
            container.Height = editor.Height + 80;
            container.Width = editor.Width + 20;
            return container;
        }
    }
}
