using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using MediaTek.DataModel;
using MediaTek.Utilities;
using MediaTek.Controls;
using System.Windows.Controls.Primitives;

namespace MediaTek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

#if DEBUG
        public App app
        {
            get { return App.Current; }
        }
#endif

        public MainWindow()
        {
            InitializeComponent();
            //this.Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.IetfLanguageTag);

            App.Current.ModalRoot = modalGrid;

            this.DataContext = App.Current;
        }

        public Selector CurrentView
        {
            get
            {
                if (tabMain == null) return null;
                TabItem tab = tabMain.SelectedItem as TabItem;
                if (tab == tabMovies)
                    return lstMovies;
                else if (tab == tabDirectors)
                    return lstDirectors;
                else if (tab == tabCountries)
                    return lstCountries;
                else if (tab == tabLanguages)
                    return lstLanguages;
                else if (tab == tabMediaTypes)
                    return lstMediaTypes;
                else if (tab == tabLends)
                    return lstLends;
                else
                    return null;
            }
        }

        public object SelectedItem
        {
            get
            {
                Selector lst = CurrentView;
                if (lst == null) return null;
                else return lst.SelectedItem;
            }
        }

        private Selector GetViewForCommand(RoutedEventArgs e)
        {
            Selector s = e.Source as Selector;
            if (s == null)
                s = CurrentView;
            return s;
        }

        public object GetSelectedItem(RoutedEventArgs e)
        {
            Selector s = GetViewForCommand(e);
            if (s != null)
                return s.SelectedItem;
            else
                return SelectedItem;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!App.Current.ConfirmClose())
            {
                e.Cancel = true;
            }
        }

        private void tabMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsViewDirty(CurrentView))
                RefreshFilters(CurrentView);
        }

        #region Filtering

        private void RefreshFilters()
        {
            SetViewDirty(lstMovies, true);
            SetViewDirty(lstDirectors, true);
            SetViewDirty(lstCountries, true);
            SetViewDirty(lstLanguages, true);
            SetViewDirty(lstMediaTypes, true);
            SetViewDirty(lstLends, true);
            RefreshFilters(CurrentView);
        }

        private void RefreshFilters(Selector lst)
        {
            CollectionViewSource cvs = GetViewSource(lst);
            if (cvs != null)
            {
                cvs.View.Refresh();
            }
            SetViewDirty(lst, false);
        }

        private CollectionViewSource GetViewSource(Selector lst)
        {
            Binding bnd = BindingOperations.GetBinding(lst, ItemsControl.ItemsSourceProperty);
            if (bnd != null)
            {
                CollectionViewSource cvs = bnd.Source as CollectionViewSource;
                return cvs;
            }
            else
            {
                return null;
            }
        }

        private void SetViewDirty(Selector lst, bool value)
        {
            lst.SetAttachedValue("dirty", value);
        }

        private bool IsViewDirty(Selector lst)
        {
            return lst.GetAttachedValue<bool>("dirty");
        }
        
        private void txtFilter_FilterChanged(object sender, RoutedEventArgs e)
        {
            RefreshFilters();
        }

        private void btnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            txtFilter.Text = "";
        }

        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            IFilterable item = e.Item as IFilterable;
            if (item != null)
            {
                e.Accepted = item.IsMatch(txtFilter.Text);
            }
            else
            {
                e.Accepted = true;
            }
        }

        private void cvsMovies_Filter(object sender, FilterEventArgs e)
        {
            CollectionViewSource_Filter(sender, e);
            if (e.Accepted)
            {
                Movie m = e.Item as Movie;
                if (m != null && m.Lent && chkHideLentMovies.IsChecked == true)
                    e.Accepted = false;
            }
        }

        private void cvsLends_Filter(object sender, FilterEventArgs e)
        {
            CollectionViewSource_Filter(sender, e);
            if (e.Accepted)
            {
                Lend l = e.Item as Lend;
                if (l != null && l.Returned && chkHideReturnedMovies.IsChecked == true)
                    e.Accepted = false;
            }
        }
        
        #endregion

        #region App commands

        private void AppCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.New)
            {
                e.CanExecute = true;
                e.Handled = true;
            }
            else if (e.Command == ApplicationCommands.Open)
            {
                e.CanExecute = true;
                e.Handled = true;
            }
            else if (e.Command == ApplicationCommands.Save)
            {
                e.CanExecute = (App.Current.DataContext != null && App.Current.DataContext.Modified);
                e.Handled = true;
            }
            else if (e.Command == CustomCommands.Quit)
            {
                e.CanExecute = true;
                e.Handled = true;
            }
            else if (e.Command == CustomCommands.About)
            {
                e.CanExecute = true;
                e.Handled = true;
            }
        }

        private void AppCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.New)
            {
                App.Current.CreateNewDatabase();
                e.Handled = true;
            }
            else if (e.Command == ApplicationCommands.Open)
            {
                App.Current.Open();
                e.Handled = true;
            }
            else if (e.Command == ApplicationCommands.Save)
            {
                App.Current.Save();
                e.Handled = true;
            }
            else if (e.Command == CustomCommands.Quit)
            {
                App.Current.Quit();
                e.Handled = true;
            }
            else if (e.Command == CustomCommands.About)
            {
                //App.Current.About();
                e.Handled = true;
            }
        }

        #endregion

        #region Item command routing

        private void ItemCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Selector curView = CurrentView;

            if (e.Command != CustomCommands.Add)
            {
                curView = GetViewForCommand(e);
            }

            if (curView == null) return;

            if (curView == lstMovies || curView == lstDirectorMovies)
            {
                MovieCommand_CanExecute(sender, e);
            }
            else if (curView == lstDirectors)
            {
                DirectorCommand_CanExecute(sender, e);
            }
            else if (curView == lstCountries)
            {
                CountryCommand_CanExecute(sender, e);
            }
            else if (curView == lstLanguages)
            {
                LanguageCommand_CanExecute(sender, e);
            }
            else if (curView == lstMediaTypes)
            {
                MediaTypeCommand_CanExecute(sender, e);
            }
            else if (curView == lstLends)
            {
                LendCommand_CanExecute(sender, e);
            }
        }

        private void ItemCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Selector curView = CurrentView;
            
            if (e.Command != CustomCommands.Add)
            {
                curView = GetViewForCommand(e);
            }
            
            if (curView == null) return;

            if (curView == lstMovies || curView == lstDirectorMovies)
            {
                MovieCommand_Executed(sender, e);
            }
            else if (curView == lstDirectors)
            {
                DirectorCommand_Executed(sender, e);
            }
            else if (curView == lstCountries)
            {
                CountryCommand_Executed(sender, e);
            }
            else if (curView == lstLanguages)
            {
                LanguageCommand_Executed(sender, e);
            }
            else if (curView == lstMediaTypes)
            {
                MediaTypeCommand_Executed(sender, e);
            }
            else if (curView == lstLends)
            {
                LendCommand_Executed(sender, e);
            }
        }

        #endregion

        #region Movie commands

        private void MovieCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Properties)
            {
                object item = GetSelectedItem(e);
                if (item == null) return;
                e.CanExecute = true;
                e.Handled = true;
            }
            else if (e.Command == ApplicationCommands.Delete)
            {
                object item = GetSelectedItem(e);
                if (item == null) return;
                e.CanExecute = true;
                e.Handled = true;
            }
            else if (e.Command == CustomCommands.Add)
            {
                e.CanExecute = true;
                e.Handled = true;
            }
            else if (e.Command == CustomCommands.Lend)
            {
                object item = GetSelectedItem(e);
                if (item is Movie && !(item as Movie).Lent)
                {
                    e.CanExecute = true;
                    e.Handled = true;
                }
            }
            else if (e.Command == CustomCommands.Return)
            {
                object item = GetSelectedItem(e);
                if (item is Movie && (item as Movie).Lent)
                {
                    e.CanExecute = true;
                    e.Handled = true;
                }
            }
        }

        private void MovieCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Properties)
            {
                object item = GetSelectedItem(e);
                if (item == null) return;
                App.Current.EditEntity(item, "Movies", "Edit movie");
                e.Handled = true;
            }
            else if (e.Command == ApplicationCommands.Delete)
            {
                object item = GetSelectedItem(e);
                if (item == null) return;
                App.Current.DeleteEntity(item, "Movies");
                e.Handled = true;
            }
            else if (e.Command == CustomCommands.Add)
            {
                Movie m = new Movie();
                App.Current.NewEntity(m, "Movies", "New movie");
                e.Handled = true;
            }
            else if (e.Command == CustomCommands.Lend)
            {
                Movie m = GetSelectedItem(e) as Movie;
                Lend l = new Lend();
                l.Movie = m;
                l.LentDate = DateTime.Today;
                App.Current.NewEntity(l, "Lends", "Lend a movie");
                e.Handled = true;
            }
            else if (e.Command == CustomCommands.Return)
            {
                Movie m = GetSelectedItem(e) as Movie;
                if (m != null)
                {
                    Lend l = m.CurrentLend;
                    if (l != null)
                    {
                        l.ReturnDate = DateTime.Today;
                        App.Current.EditEntity(l, "Lends", "Recover lent movie");
                    }
                }
                e.Handled = true;
            }
        }

        #endregion

        #region Director commands

        private void DirectorCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Properties)
            {
                object item = GetSelectedItem(e);
                if (item == null) return;
                e.CanExecute = true;
                e.Handled = true;
            }
            else if (e.Command == ApplicationCommands.Delete)
            {
                object item = GetSelectedItem(e);
                if (item == null) return;
                e.CanExecute = true;
                e.Handled = true;
            }
            else if (e.Command == CustomCommands.Add)
            {
                e.CanExecute = true;
                e.Handled = true;
            }
        }

        private void DirectorCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Properties)
            {
                object item = GetSelectedItem(e);
                if (item == null) return;
                App.Current.EditEntity(item, "Directors", "Edit director");
                e.Handled = true;
            }
            else if (e.Command == ApplicationCommands.Delete)
            {
                object item = GetSelectedItem(e);
                if (item == null) return;
                App.Current.DeleteEntity(item, "Directors");
                e.Handled = true;
            }
            else if (e.Command == CustomCommands.Add)
            {
                Director d = new Director();
                App.Current.NewEntity(d, "Directors", "New director");
                e.Handled = true;
            }
        }

        #endregion

        #region Country commands

        private void CountryCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Properties)
            {
                object item = GetSelectedItem(e);
                if (item == null) return;
                e.CanExecute = true;
                e.Handled = true;
            }
            else if (e.Command == ApplicationCommands.Delete)
            {
                object item = GetSelectedItem(e);
                if (item == null) return;
                e.CanExecute = true;
                e.Handled = true;
            }
            else if (e.Command == CustomCommands.Add)
            {
                e.CanExecute = true;
                e.Handled = true;
            }
        }

        private void CountryCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Properties)
            {
                object item = GetSelectedItem(e);
                if (item == null) return;
                App.Current.EditEntity(item, "Countries", "Edit country");
                e.Handled = true;
            }
            else if (e.Command == ApplicationCommands.Delete)
            {
                object item = GetSelectedItem(e);
                if (item == null) return;
                App.Current.DeleteEntity(item, "Countries");
                e.Handled = true;
            }
            else if (e.Command == CustomCommands.Add)
            {
                Country c = new Country();
                App.Current.NewEntity(c, "Countries", "New country");
                e.Handled = true;
            }
        }

        #endregion

        #region Language commands

        private void LanguageCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Properties)
            {
                object item = GetSelectedItem(e);
                if (item == null) return;
                e.CanExecute = true;
                e.Handled = true;
            }
            else if (e.Command == ApplicationCommands.Delete)
            {
                object item = GetSelectedItem(e);
                if (item == null) return;
                e.CanExecute = true;
                e.Handled = true;
            }
            else if (e.Command == CustomCommands.Add)
            {
                e.CanExecute = true;
                e.Handled = true;
            }
        }

        private void LanguageCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Properties)
            {
                object item = GetSelectedItem(e);
                if (item == null) return;
                App.Current.EditEntity(item, "Languages", "Edit language");
                e.Handled = true;
            }
            else if (e.Command == ApplicationCommands.Delete)
            {
                object item = GetSelectedItem(e);
                if (item == null) return;
                App.Current.DeleteEntity(item, "Languages");
                e.Handled = true;
            }
            else if (e.Command == CustomCommands.Add)
            {
                Language l = new Language();
                App.Current.NewEntity(l, "Languages", "New language");
                e.Handled = true;
            }
        }

        #endregion

        #region MediaType commands

        private void MediaTypeCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Properties)
            {
                object item = GetSelectedItem(e);
                if (item == null) return;
                e.CanExecute = true;
                e.Handled = true;
            }
            else if (e.Command == ApplicationCommands.Delete)
            {
                object item = GetSelectedItem(e);
                if (item == null) return;
                e.CanExecute = true;
                e.Handled = true;
            }
            else if (e.Command == CustomCommands.Add)
            {
                e.CanExecute = true;
                e.Handled = true;
            }
        }

        private void MediaTypeCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Properties)
            {
                object item = GetSelectedItem(e);
                if (item == null) return;
                App.Current.EditEntity(item, "MediaTypes", "Edit media type");
                e.Handled = true;
            }
            else if (e.Command == ApplicationCommands.Delete)
            {
                object item = GetSelectedItem(e);
                if (item == null) return;
                App.Current.DeleteEntity(item, "MediaTypes");
                e.Handled = true;
            }
            else if (e.Command == CustomCommands.Add)
            {
                MediaType m = new MediaType();
                App.Current.NewEntity(m, "MediaTypes", "New media type");
                e.Handled = true;
            }
        }

        #endregion

        #region Lend commands

        private void LendCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Properties)
            {
                object item = GetSelectedItem(e);
                if (item == null) return;
                e.CanExecute = true;
                e.Handled = true;
            }
            else if (e.Command == ApplicationCommands.Delete)
            {
                object item = GetSelectedItem(e);
                if (item == null) return;
                e.CanExecute = true;
                e.Handled = true;
            }
            else if (e.Command == CustomCommands.Add)
            {
                e.CanExecute = true;
                e.Handled = true;
            }
            else if (e.Command == CustomCommands.Return)
            {
                object item = GetSelectedItem(e);
                if (item == null) return;
                if (item is Lend && !(item as Lend).Returned)
                {
                    e.CanExecute = true;
                    e.Handled = true;
                }
            }
        }

        private void LendCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Properties)
            {
                object item = GetSelectedItem(e);
                if (item == null) return;
                App.Current.EditEntity(item, "Lends", "Edit lend");
                e.Handled = true;
            }
            else if (e.Command == ApplicationCommands.Delete)
            {
                object item = GetSelectedItem(e);
                if (item == null) return;
                App.Current.DeleteEntity(item, "Lends");
                e.Handled = true;
            }
            else if (e.Command == CustomCommands.Add)
            {
                Lend l = new Lend();
                l.LentDate = DateTime.Today;
                App.Current.NewEntity(l, "Lends", "New lend");
                e.Handled = true;
            }
            else if (e.Command == CustomCommands.Return)
            {
                Lend l = GetSelectedItem(e) as Lend;
                if (l != null)
                {
                    l.ReturnDate = DateTime.Today;
                    App.Current.EditEntity(l, "Lends", "Recover lent movie");
                }
                e.Handled = true;
            }
        }

        #endregion



        public double Zoom
        {
            get { return (double)GetValue(ZoomProperty); }
            set { SetValue(ZoomProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Zoom.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ZoomProperty =
            DependencyProperty.Register("Zoom", typeof(double), typeof(MainWindow), new UIPropertyMetadata(1.0));

        private void btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            Zoom /= 1.1;
        }

        private void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            Zoom *= 1.1;
        }

        private void btnResetZoom_Click(object sender, RoutedEventArgs e)
        {
            Zoom = 1.0;
        }

        private void chkHideLentMovies_Checked(object sender, RoutedEventArgs e)
        {
            if (lstMovies == null) return;
            RefreshFilters(lstMovies);
        }

        private void chkHideReturnedMovies_Checked(object sender, RoutedEventArgs e)
        {
            if (lstLends == null) return;
            RefreshFilters(lstLends);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ExpanderHelper.RegisterExpanderInGrid(
                expDirectorMovies,
                splDirectorMovies,
                rowDirectorMovies,
                rowDirectorMovies.Height);
            ExpanderHelper.RegisterExpanderInGrid(
                expMovieDetails,
                splMovieDetails,
                rowMovieDetails,
                rowMovieDetails.Height);
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            ExpanderHelper.UnregisterExpanderInGrid(expDirectorMovies);
            ExpanderHelper.UnregisterExpanderInGrid(expMovieDetails);
        }

    }
}
