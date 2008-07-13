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
using Microsoft.Win32;

namespace MediaTek
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

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            if (Target == null) return;

            OpenFileDialog dlg = App.Current.OpenImageDialog;
            if (dlg.ShowDialog() == true)
            {
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(dlg.FileName);
                img.EndInit();
                Target.Cover = img;
            }
        }

        private void cmbDirector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (Target == null) return;
            
            if (cmbDirector.SelectedValue != null)
            {
                Target.Director = App.Current.DataContext.Directors.Where(d => d.Id == Target.DirectorId).FirstOrDefault();
            }
        }

        private void cmbLanguage_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (Target == null) return;

            if (cmbLanguage.SelectedValue != null)
            {
                Target.Language = App.Current.DataContext.Languages.Where(l => l.Id == Target.LanguageId).FirstOrDefault(); 
            }
        }

        private void cmbMediaType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (Target == null) return;

            if (cmbMediaType.SelectedValue != null)
            {
                Target.MediaType = App.Current.DataContext.MediaTypes.Where(m => m.Id == Target.MediaTypeId).FirstOrDefault();
            }
        }

        private void cmbDirector_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (Target == null) return;

            TextBox tb = e.OldFocus as TextBox;
            if (tb != null && tb.IsDescendantOf(sender as DependencyObject))
            {
                if (!string.IsNullOrEmpty(tb.Text))
                {
                    Director selected = cmbDirector.SelectedItem as Director;
                    if (selected == null || selected.Name != tb.Text)
                    {
                        MessageBoxResult r = MessageBox.Show("This director isn't in the database, would you like to add it ?", "Unknown director", MessageBoxButton.OKCancel);
                        if (r == MessageBoxResult.OK)
                        {
                            Director d = new Director();
                            d.Name = tb.Text;
                            Control ed = EntityEditorContainer.CreateEditor(d, "New movie");
                            ed.ShowModal(FindTopLevelParent<Panel>(), delegate(bool? result)
                            {
                                bool ok = result ?? false;
                                if (ok)
                                {
                                    App.Current.DataContext.Directors.InsertOnSubmit(d);
                                    App.Current.DataContext.SubmitChanges();
                                    App.Current.Directors.Refresh();
                                    cmbDirector.SelectedItem = d;
                                }
                                else
                                {
                                    e.Handled = true;
                                    e.OldFocus.Focus();
                                }
                            });
                        }
                        else
                        {
                            e.Handled = true;
                            e.OldFocus.Focus();
                        }
                    }
                }
            }
        }

        private T FindTopLevelParent<T>() where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(this);
            T topPanel = null;
            while (parent != null)
            {
                if (parent is T)
                    topPanel = parent as T;
                parent = VisualTreeHelper.GetParent(parent);
            }
            return topPanel;
        }

        private void cmbLanguage_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

        }

        private void cmbMediaType_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

        }
    }
}
