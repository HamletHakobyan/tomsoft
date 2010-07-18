using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Developpez.Dotnet.Windows.Input;
using SharpDB.Util.Service;

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
            string filename = string.Empty;
            var options = new OpenFileDialogOptions
            {
                Filter = GetResource<string>("sql_file_filter_name") + "|*.sql",
            };
            var service = GetService<IFileDialogService>();
            if (service.ShowOpen(ref filename, options) == true)
            {
                OpenWorksheet(filename);
            }
        }

        public void OpenWorksheet(string fileName)
        {
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
            GetService<IJumpListService>().AddRecent(fileName);
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

        #endregion
    }
}
