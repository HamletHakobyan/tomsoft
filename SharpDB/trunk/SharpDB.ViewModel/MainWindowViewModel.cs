using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Developpez.Dotnet.Windows.Input;
using SharpDB.Util.Service;
using SharpDB.Model;
using System.Linq;
using Developpez.Dotnet;
using System;
using System.Windows;
using System.ComponentModel;

namespace SharpDB.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            _title = "SharpDB";
            _databaseManager = new DatabaseManagerViewModel();
            _currentWorksheet = new WorksheetViewModel(this);
            _worksheets = new ObservableCollection<WorksheetViewModel>();
            _worksheets.Add(_currentWorksheet);
            if (!IsInDesignMode)
            {
                var config = GetService<Config>();
                _recentFiles = new ObservableCollection<string>(config.RecentFiles);
            }
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

        private ObservableCollection<string> _recentFiles;
        public ObservableCollection<string> RecentFiles
        {
            get { return _recentFiles; }
            set
            {
                if (value != _recentFiles)
                {
                    _recentFiles = value;
                    OnPropertyChanged("RecentFiles");
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

        private DelegateCommand<string> _openWorksheetCommand;
        public ICommand OpenWorksheetCommand
        {
            get
            {
                if (_openWorksheetCommand == null)
                {
                    _openWorksheetCommand = new DelegateCommand<string>(OpenWorksheet);
                }
                return _openWorksheetCommand;
            }
        }

        private DelegateCommand<WorksheetViewModel> _closeWorksheetCommand;
        public ICommand CloseWorksheetCommand
        {
            get
            {
                if (_closeWorksheetCommand == null)
                {
                    _closeWorksheetCommand = new DelegateCommand<WorksheetViewModel>(CloseWorksheet);
                }
                return _closeWorksheetCommand;
            }
        }

        private DelegateCommand<CancelEventArgs> _closingCommand;
        public ICommand ClosingCommand
        {
            get
            {
                if (_closingCommand == null)
                {
                    _closingCommand = new DelegateCommand<CancelEventArgs>(WindowClosing);
                }
                return _closingCommand;
            }
        }


        #endregion

        #region Public methods

        public void NewWorksheet()
        {
            var worksheet = new WorksheetViewModel(this);
            _worksheets.Add(worksheet);
            CurrentWorksheet = worksheet;
        }

        public void OpenWorksheet()
        {
            OpenWorksheet(null);
        }

        public void OpenWorksheet(string fileName)
        {
            if (fileName.IsNullOrEmpty())
            {
                var options = new OpenFileDialogOptions
                {
                    Filter = GetResource<string>("sql_file_filter_name") + "|*.sql",
                };
                var service = GetService<IFileDialogService>();
                if (service.ShowOpen(ref fileName, options) != true)
                {
                    return;
                }
            }

            string text = File.ReadAllText(fileName);
            string title = Path.GetFileName(fileName);
            var worksheet = new WorksheetViewModel(this)
            {
                FileName = fileName,
                Title = title,
                Text = text,
                IsModified = false
            };
            Worksheets.Add(worksheet);
            CurrentWorksheet = worksheet;
            AddRecentFile(fileName);
        }

        public void CloseWorksheet(WorksheetViewModel worksheet)
        {
            if (worksheet != null)
            {
                if (worksheet.ConfirmClose())
                {
                    Worksheets.Remove(worksheet);
                }
            }
        }

        public void ProcessCommandLineArgs(IList<string> args)
        {
            bool expectDatabaseName = false;

            for (int i = 0; i < args.Count; i++)
            {
                if (expectDatabaseName)
                {
                    string databaseName = args[i];
                    _databaseManager.ConnectByName(databaseName);
                    expectDatabaseName = false;
                }
                else if (args[i] == "/connect")
                {
                    expectDatabaseName = true;
                }
                else
                {
                    OpenWorksheet(args[i]);
                }
            }
        }

        public void AddRecentFile(string filename)
        {
            var service = GetService<IJumpListService>();
            service.AddRecent(filename);
            
            _recentFiles.Remove(filename);
            _recentFiles.Insert(0, filename);
            while (_recentFiles.Count > service.MaxCountPerCategory)
                _recentFiles.RemoveAt(_recentFiles.Count - 1);

            GetService<Config>().RecentFiles = _recentFiles.ToList();
        }

        #endregion

        private void WindowClosing(CancelEventArgs e)
        {
            foreach (var worksheet in _worksheets)
            {
                if (!worksheet.ConfirmClose())
                {
                    e.Cancel = true;
                    return;
                }
            }
        }
    }
}
