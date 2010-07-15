using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Windows.Input;
using System.Windows.Input;
using SharpDB.Util.Dialogs;
using SharpDB.Util;
using System.Collections.ObjectModel;
using System.IO;

namespace SharpDB.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            _title = "SharpDB";
            _databaseManager = new DatabaseManagerViewModel();
            _currentWorksheet = new WorksheetViewModel(_databaseManager);
            _worksheets = new ObservableCollection<WorksheetViewModel>();
            _worksheets.Add(_currentWorksheet);
        }

        #region Properties

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (value != _title)
                {
                    _title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        private ObservableCollection<WorksheetViewModel> _worksheets;
        public ObservableCollection<WorksheetViewModel> Worksheets
        {
            get { return _worksheets; }
            set
            {
                if (value != _worksheets)
                {
                    _worksheets = value;
                    OnPropertyChanged("Worksheets");
                }
            }
        }

        private WorksheetViewModel _currentWorksheet;
        public WorksheetViewModel CurrentWorksheet
        {
            get { return _currentWorksheet; }
            set
            {
                if (value != _currentWorksheet)
                {
                    _currentWorksheet = value;
                    OnPropertyChanged("CurrentWorksheet");
                }
            }
        }

        private DatabaseManagerViewModel _databaseManager;
        public DatabaseManagerViewModel DatabaseManager
        {
            get { return _databaseManager; }
            set
            {
                if (value != _databaseManager)
                {
                    _databaseManager = value;
                    OnPropertyChanged("DatabaseManager");
                }
            }
        }

        #endregion

        #region Commands

        private DelegateCommand _newWorksheetCommand;
        public ICommand NewWorksheetCommand
        {
            get
            {
                if (_newWorksheetCommand == null)
                {
                    _newWorksheetCommand = new DelegateCommand(NewWorksheet);
                }
                return _newWorksheetCommand;
            }
        }

        private DelegateCommand _openWorksheetCommand;
        public ICommand OpenWorksheetCommand
        {
            get
            {
                if (_openWorksheetCommand == null)
                {
                    _openWorksheetCommand = new DelegateCommand(OpenWorksheet);
                }
                return _openWorksheetCommand;
            }
        }

        #endregion

        #region Public methods

        public void NewWorksheet()
        {
            var worksheet = new WorksheetViewModel(_databaseManager);
            _worksheets.Add(worksheet);
            CurrentWorksheet = worksheet;
        }

        public void OpenWorksheet()
        {
            string filename = string.Empty;
            var options = new OpenFileDialogOptions
            {
                Filter = ResourceManager.GetString("sql_file_filter_name") + "|*.sql",
            };
            var service = GetService<IFileDialogService>();
            if (service.ShowOpen(ref filename, options) == true)
            {
                string text = File.ReadAllText(filename);
                string title = Path.GetFileName(filename);
                var worksheet = new WorksheetViewModel(_databaseManager)
                {
                    Title = title,
                    Text = text,
                    IsModified = false
                };
                Worksheets.Add(worksheet);
                CurrentWorksheet = worksheet;
            }
        }

        public void SaveCurrentWorksheet()
        {
            if (CurrentWorksheet == null)
                return;

            GetService<IMessageBoxService>().Show("Not implemented");
        }

        #endregion
    }
}
