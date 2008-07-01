using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using MediaTek.Properties;
using System.Windows.Markup;
using System.Threading;
using System.Linq;

namespace MediaTek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MediaTekDataContext db;
        private PatternFilter filter;

        public MainWindow()
        {
            InitializeComponent();
            this.Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentCulture.IetfLanguageTag);
            SqlConnection cnx = new SqlConnection(Settings.Default.SQLServerCS);
            db = new MediaTekDataContext(cnx);
            App.Current.DataContext = db;
            db.Connection.Open();
            db.Transaction = db.Connection.BeginTransaction();
            InitDataBindings();
            InitFilters();
        }

        private void InitFilters()
        {
            filter = new PatternFilter(txtFilter.Text);
            viewMovies.Filter = filter.Predicate;
            viewDirectors.Filter = filter.Predicate;
            viewCountries.Filter = filter.Predicate;
            viewLanguages.Filter = filter.Predicate;
            viewMediaTypes.Filter = filter.Predicate;
            viewLends.Filter = filter.Predicate;
        }

        private CollectionView viewMovies;
        private CollectionView viewDirectors;
        private CollectionView viewCountries;
        private CollectionView viewLanguages;
        private CollectionView viewMediaTypes;
        private CollectionView viewLends;

        private void InitDataBindings()
        {
            viewMovies = new CollectionView(db.Movies);
            viewDirectors = new CollectionView(db.Directors);
            viewCountries = new CollectionView(db.Countries);
            viewLanguages = new CollectionView(db.Languages);
            viewMediaTypes = new CollectionView(db.MediaTypes);
            viewLends = new CollectionView(db.Lends);
            lstMovies.ItemsSource = viewMovies;
            lstDirectors.ItemsSource = viewDirectors;
            lstCountries.ItemsSource = viewCountries;
            lstLanguages.ItemsSource = viewLanguages;
            lstMediaTypes.ItemsSource = viewMediaTypes;
            lstLends.ItemsSource = viewLends;
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            RoutedCommand cmd = e.Command as RoutedCommand;
            ICommandSource src = e.Source as ICommandSource;
            e.Handled = true;
            if (e.Command == ApplicationCommands.New)
            {
                e.CanExecute = true;
            }
            else if (e.Command == CustomCommands.Quit)
            {
                e.CanExecute = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ICommandSource src = e.Source as ICommandSource;
            e.Handled = true;
            if (e.Command == ApplicationCommands.New)
            {
                CreateNew();
            }
            else if (e.Command == CustomCommands.Quit)
            {
                Application.Current.Shutdown();
            }
            else
            {
                e.Handled = false;
            }

        }

        private void CreateNew()
        {
            throw new NotImplementedException();
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            filter.Pattern = txtFilter.Text;
            RefreshIfNeeded();
        }

        private void RefreshIfNeeded()
        {
            if (tabMain.SelectedItem == tabMovies)
                viewMovies.Refresh();
            else if (tabMain.SelectedItem == tabDirectors)
                viewDirectors.Refresh();
            else if (tabMain.SelectedItem == tabCountries)
                viewCountries.Refresh();
            else if (tabMain.SelectedItem == tabLanguages)
                viewLanguages.Refresh();
            else if (tabMain.SelectedItem == tabMediaTypes)
                viewMediaTypes.Refresh();
            else if (tabMain.SelectedItem == tabLends)
                viewLends.Refresh();
        }

        private void lstMovies_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Control ed = EntityEditorContainer.CreateEditor<MovieEditor>(lstMovies.SelectedItem, "Edit movie");
            ed.ShowModal(modalGrid, null);
        }

        private void lstDirectors_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Control ed = EntityEditorContainer.CreateEditor<DirectorEditor>(lstDirectors.SelectedItem, "Edit director");
            ed.ShowModal(modalGrid, null);
        }
    }
}
