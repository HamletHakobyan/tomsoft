using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MediaTek.Controls;
using MediaTek.DataModel;
using MediaTek.Utilities;
using Microsoft.Win32;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Data.Objects.DataClasses;

namespace MediaTek
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, INotifyPropertyChanged
    {

        private SplashScreen splash;

        public App()
        {
            InitializeComponent();

            this.Exit += new ExitEventHandler(App_Exit);

            splash = new SplashScreen();
            splash.Show();

        }

        void App_Exit(object sender, ExitEventArgs e)
        {
            CloseDatabase();
        }

        public DvdEntities DataContext { get; set; }

        public static new App Current
        {
            get
            {
                return Application.Current as App;
            }
        }

        public OpenFileDialog OpenImageDialog
        {
            get { return this.Resources["dlgOpenImage"] as OpenFileDialog; }
        }

        public OpenFileDialog OpenDbDialog
        {
            get { return this.Resources["dlgOpenDb"] as OpenFileDialog; }
        }

        public SaveFileDialog SaveDbDialog
        {
            get { return this.Resources["dlgSaveDb"] as SaveFileDialog; }
        }

        public Panel ModalRoot { get; set; }

        private Dictionary<string, IList> lists;

        public List<Movie> Movies
        {
            get { return lists["Movies"] as List<Movie>; }
            private set
            {
                lists["Movies"] = value;
                OnPropertyChanged("Movies");
            }
        }
        public List<Director> Directors
        {
            get { return lists["Directors"] as List<Director>; }
            private set
            {
                lists["Directors"] = value;
                OnPropertyChanged("Directors");
            }
        }
        public List<Country> Countries
        {
            get { return lists["Countries"] as List<Country>; }
            private set
            {
                lists["Countries"] = value;
                OnPropertyChanged("Countries");
            }
        }
        public List<Language> Languages
        {
            get { return lists["Languages"] as List<Language>; }
            private set
            {
                lists["Languages"] = value;
                OnPropertyChanged("Languages");
            }
        }
        public List<MediaType> MediaTypes
        {
            get { return lists["MediaTypes"] as List<MediaType>; }
            private set
            {
                lists["MediaTypes"] = value;
                OnPropertyChanged("MediaTypes");
            }
        }
        public List<Lend> Lends
        {
            get { return lists["Lends"] as List<Lend>; }
            private set
            {
                lists["Lends"] = value;
                OnPropertyChanged("Lends");
            }
        }

        private void InitializeLists()
        {
            lists = new Dictionary<string, IList>();

            this.Movies = DataContext.Movies.ToList();
            this.Directors = DataContext.Directors.ToList();
            this.Countries = DataContext.Countries.ToList();
            this.Languages = DataContext.Languages.ToList();
            this.MediaTypes =DataContext.MediaTypes.ToList();
            this.Lends = DataContext.Lends.ToList();
        }

        public void RefreshList(string name)
        {
            switch (name)
            {
                case "Movies":
                    this.Movies = DataContext.Movies.ToList();
                    break;
                case "Directors":
                    this.Directors = DataContext.Directors.ToList();
                    break;
                case "Countries":
                    this.Countries = DataContext.Countries.ToList();
                    break;
                case "Languages":
                    this.Languages = DataContext.Languages.ToList();
                    break;
                case "MediaTypes":
                    this.MediaTypes = DataContext.MediaTypes.ToList();
                    break;
                case "Lends":
                    this.Lends = DataContext.Lends.ToList();
                    break;
                default:
                    throw new ArgumentException("There is no entity set named " + name);
            }
        }

        public void CreateDatabase(string filename)
        {
            BinaryWriter wr = new BinaryWriter(File.Create(filename));
            wr.Write(MediaTek.Properties.Resources.template_db);
            wr.Close();
        }

        public void CloseDatabase()
        {
            if (this.DataContext != null)
            {
                this.DataContext.ModifiedChanged -= DataContext_ModifiedChanged;
                this.DataContext.Dispose();
            }
        }

        public void OpenDatabase(string filename)
        {
            try
            {
                EntityConnectionStringBuilder ecsb = new EntityConnectionStringBuilder();
                ecsb.Provider = "System.Data.SQLite";
                ecsb.Metadata = MediaTek.Properties.Resources.dbMetaData;
                ecsb.ProviderConnectionString = string.Format("data source={0}", filename);
                DvdEntities db = new DvdEntities(ecsb.ConnectionString);
                db.ModifiedChanged += new EventHandler(DataContext_ModifiedChanged);
                db.Connection.Open();
                db.BeginTransaction();
                this.DataContext = db;
                InitializeLists();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void DataContext_ModifiedChanged(object sender, EventArgs e)
        {
            if (this.DataContext.Modified)
            {
                if (!this.MainWindow.Title.EndsWith("*")) this.MainWindow.Title += "*";
            }
            else
            {
                this.MainWindow.Title = this.MainWindow.Title.TrimEnd('*');
            }
        }

        public void Save()
        {
            this.DataContext.Commit();
            this.DataContext.Modified = false;
            this.DataContext.BeginTransaction();
        }

        public void Open()
        {
            if (!ConfirmClose()) return;

            if (this.OpenDbDialog.ShowDialog() == true)
            {
                CloseDatabase();
                string filename = this.OpenDbDialog.FileName;
                OpenDatabase(filename);
            }
        }

        public bool ConfirmClose()
        {
            if (this.DataContext != null)
            {
                if (this.DataContext.Modified)
                {
                    MessageBoxResult r = MessageBox.Show("Save changes in current database ?", "Unsaved changes", MessageBoxButton.YesNoCancel);
                    if (r == MessageBoxResult.Yes)
                    {
                        Save();
                    }
                    else if (r == MessageBoxResult.Cancel)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void About()
        {
            MessageBox.Show("MediaTek v0.1");
        }

        public void Quit()
        {
            this.MainWindow.Close();
        }

        public void CreateNewDatabase()
        {
            if (!ConfirmClose()) return;

            if (this.SaveDbDialog.ShowDialog() == true)
            {
                CloseDatabase();
                string filename = this.SaveDbDialog.FileName;
                this.CreateDatabase(filename);
                this.OpenDatabase(filename);
            }
        }

        public void NewEntity(object entity, string entitySetName, string title)
        {
            Control ed = EntityEditorContainer.CreateEditorDialog(entity, title);
            ed.ShowModal(this.ModalRoot, delegate(bool? result)
            {
                if (result == true)
                {
                    ObjectStateEntry e = this.DataContext.ObjectStateManager.GetObjectStateEntry(entity);
                    if (e == null || e.State == EntityState.Detached)
                        this.DataContext.AddObject(entitySetName, entity);
                    this.DataContext.SaveChanges();
                    this.DataContext.Modified = true;
                    RefreshList(entitySetName);
                }
                else
                {
                    EntityObject e = (entity as EntityObject);
                    if (e != null && e.EntityState != EntityState.Detached)
                        this.DataContext.Detach(entity);
                }
            });
        }

        public void EditEntity(object entity, string entitySetName, string title)
        {
            Control ed = EntityEditorContainer.CreateEditorDialog(entity, title);
            ed.ShowModal(this.ModalRoot, delegate(bool? result)
            {
                if (result == true)
                {
                    this.DataContext.SaveChanges();
                    this.DataContext.Modified = true;
                    RefreshList(entitySetName);
                }
                else
                {
                    this.DataContext.Refresh(RefreshMode.StoreWins, entity);
                }
            });
        }

        public void DeleteEntity(object entity, string entitySetName)
        {
            MessageBoxResult r = MessageBox.Show("Do you really want to delete " + entity.ToString(), "Really delete ?", MessageBoxButton.YesNo);
            if (r == MessageBoxResult.Yes)
            {
                this.DataContext.DeleteObject(entity);
                this.DataContext.SaveChanges();
                this.DataContext.Modified = true;
                RefreshList(entitySetName);
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

}
