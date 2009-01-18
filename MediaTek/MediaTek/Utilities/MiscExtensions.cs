using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MediaTek.Controls;
using System.Windows.Media;

namespace MediaTek.Utilities
{
    public static class MiscExtensions
    {
        public static bool NewItemTyped(this ComboBox combo)
        {
            if (combo.SelectedItem == null && !string.IsNullOrEmpty(combo.Text))
            {
                return true;
            }
            else if (combo.SelectedItem != null)
            {
                var entity = (from e in combo.Items.Cast<object>()
                             where e.ToString().Equals(combo.Text)
                             select e).FirstOrDefault();
                if (entity == null)
                {
                    return true;
                }
                else
                {
                    combo.SelectedItem = entity;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static void QueryAddItem(this ComboBox combo, KeyboardFocusChangedEventArgs ev, object entity, string entitySetName, string prompt, string promptTitle, string editorTitle)
        {
            MessageBoxResult r = MessageBox.Show(prompt, promptTitle, MessageBoxButton.YesNo);
            if (r == MessageBoxResult.Yes)
            {
                Control ed = EntityEditorContainer.CreateEditorDialog(entity, editorTitle);
                ed.ShowModal(App.Current.ModalRoot, delegate(bool? result)
                {
                    if (result == true)
                    {
                        App.Current.DataContext.AddObject(entitySetName, entity);
                        App.Current.DataContext.SaveChanges();
                        App.Current.RefreshList(entitySetName);
                        combo.SelectedItem = entity;
                    }
                    else
                    {
                        ev.Handled = true;
                        ev.OldFocus.Focus();
                    }
                });
            }
            else
            {
                ev.Handled = true;
                ev.OldFocus.Focus();
            }
        }

        public static T FindAncestor<T>(this DependencyObject obj) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(obj);
            if (parent is T)
                return (parent as T);
            else if (parent == null)
                return null;
            else
                return parent.FindAncestor<T>();
        }

    }
}
