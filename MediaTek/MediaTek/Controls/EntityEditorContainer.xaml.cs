using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MediaTek.Utilities;
using System.Diagnostics;

namespace MediaTek.Controls
{
    /// <summary>
    /// Interaction logic for EntityEditorContainer.xaml
    /// </summary>
    public partial class EntityEditorContainer : UserControl
    {
        public EntityEditorContainer()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (ValidationService.ValidateGroup(ValidationGroupName))
            {
                ValidationService.ClearGroup(ValidationGroupName);
                scvEditor.BindingGroup.UpdateSources();
                this.ReturnModal(true);
            }
            else
            {
                MessageBox.Show("Please correct all errors before validating");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ValidationService.ClearGroup(ValidationGroupName);
            this.ReturnModal(false);
        }

        private string ValidationGroupName
        {
            get
            {
                if (scvEditor.Content != null)
                {
                    return ValidationService.GetGroupName(scvEditor.Content as DependencyObject);
                }
                else
                {
                    return null;
                }
            }
        }

        public static Control CreateEditorDialog(object entity, string title)
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

        public static Control CreateEditorDialog<T>(object entity, string title) where T : Control, new()
        {
            EntityEditorContainer container = CreateEditorDialog<T>(title);
            container.DataContext = entity;
            return container;
        }

        public static EntityEditorContainer CreateEditorDialog<T>(string title) where T : Control, new()
        {
            T dlg = new T();
            EntityEditorContainer container = CreateContainer(dlg);
            container.lblTitle.Content = title;
            container.scvEditor.Content = dlg;
            return container;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //ValidationService.ValidateGroup("editorFields");
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            ValidationService.ClearGroup("editorFields");
        }
    }
}
