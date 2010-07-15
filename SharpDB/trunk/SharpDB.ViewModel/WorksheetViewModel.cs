using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Windows.Input;
using System.Windows.Input;
using SharpDB.Util.Service;
using System.IO;
using System.Collections.ObjectModel;
using SharpDB.Util;
using Developpez.Dotnet;
using System.Data;
using SharpDB.Model.Data;
using System.Windows;
using System.Data.Common;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;

namespace SharpDB.ViewModel
{
    public class WorksheetViewModel : ViewModelBase
    {
        private static int _worksheetNum = 0;

        private readonly DatabaseManagerViewModel _manager;
        private IDataReader _reader;

        public WorksheetViewModel(DatabaseManagerViewModel manager)
        {
            _manager = manager;
            _title = string.Format(ResourceManager.GetString("new_worksheet_name_format"), ++_worksheetNum);
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

        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set
            {
                if (value != _fileName)
                {
                    _fileName = value;
                    OnPropertyChanged("FileName");
                }
            }
        }


        private bool _isModified;
        public bool IsModified
        {
            get { return _isModified; }
            set
            {
                if (value != _isModified)
                {
                    _isModified = value;
                    OnPropertyChanged("IsModified");
                }
            }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                if (value != _text)
                {
                    _text = value;
                    OnPropertyChanged("Text");
                    IsModified = true;
                }
            }
        }

        private TextSelection _selection;
        public TextSelection Selection
        {
            get { return _selection; }
            set
            {
                if (!value.Equals(_selection))
                {
                    _selection = value;
                    OnPropertyChanged("Selection");
                }
            }
        }

        private CaretPosition _caretPosition;
        public CaretPosition CaretPosition
        {
            get { return _caretPosition; }
            set
            {
                if (!value.Equals(_caretPosition))
                {
                    _caretPosition = value;
                    OnPropertyChanged("CaretPosition");
                }
            }
        }


        private string _selectedText;
        public string SelectedText
        {
            get { return _selectedText; }
            set
            {
                if (value != _selectedText)
                {
                    _selectedText = value;
                    OnPropertyChanged("SelectedText");
                }
            }
        }

        private DatabaseViewModel _currentDatabase;
        public DatabaseViewModel CurrentDatabase
        {
            get { return _currentDatabase; }
            set
            {
                if (value != _currentDatabase)
                {
                    _currentDatabase = value;
                    if (_currentDatabase != null && !_currentDatabase.IsConnected)
                    {
                        _currentDatabase.Connect();
                    }
                    OnPropertyChanged("CurrentDatabase");
                }
            }
        }

        public ObservableCollection<DatabaseViewModel> Databases
        {
            get
            {
                return _manager.Databases;
            }
        }

        private DataTable _results;
        public DataTable Results
        {
            get { return _results; }
            set
            {
                if (value != _results)
                {
                    _results = value;
                    OnPropertyChanged("Results");
                }
            }
        }

        private bool _fetchComplete;
        public bool FetchComplete
        {
            get { return _fetchComplete; }
            set
            {
                if (value != _fetchComplete)
                {
                    _fetchComplete = value;
                    OnPropertyChanged("FetchComplete");
                }
            }
        }

        private bool _hasMoreRows;
        public bool HasMoreRows
        {
            get { return _hasMoreRows; }
            set
            {
                if (value != _hasMoreRows)
                {
                    _hasMoreRows = value;
                    OnPropertyChanged("HasMoreRows");
                }
            }
        }


        private int _fetchedRows;
        public int FetchedRows
        {
            get { return _fetchedRows; }
            set
            {
                if (value != _fetchedRows)
                {
                    _fetchedRows = value;
                    OnPropertyChanged("FetchedRows");
                }
            }
        }


        private bool _showResults = true;
        public bool ShowResults
        {
            get { return _showResults; }
            set
            {
                if (value != _showResults)
                {
                    _showResults = value;
                    OnPropertyChanged("ShowResults");
                }
            }
        }

        #endregion

        #region Commands

        private DelegateCommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new DelegateCommand(
                            Save,
                            () => IsModified);
                }
                return _saveCommand;
            }
        }

        private DelegateCommand _cutCommand;
        public ICommand CutCommand
        {
            get
            {
                if (_cutCommand == null)
                {
                    _cutCommand = new DelegateCommand(
                        Cut,
                        () => !SelectedText.IsNullOrEmpty());
                }
                return _cutCommand;
            }
        }

        private DelegateCommand _copyCommand;
        public ICommand CopyCommand
        {
            get
            {
                if (_copyCommand == null)
                {
                    _copyCommand = new DelegateCommand(
                        Copy,
                        () => !SelectedText.IsNullOrEmpty());
                }
                return _copyCommand;
            }
        }

        private DelegateCommand _pasteCommand;
        public ICommand PasteCommand
        {
            get
            {
                if (_pasteCommand == null)
                {
                    _pasteCommand = new DelegateCommand(
                        Paste,
                        () => ClipboardContainsText());
                }
                return _pasteCommand;
            }
        }

        private DelegateCommand _executeCurrentCommand;
        public ICommand ExecuteCurrentCommand
        {
            get
            {
                if (_executeCurrentCommand == null)
                {
                    _executeCurrentCommand = new DelegateCommand(
                        ExecuteCurrent,
                        () => _currentDatabase != null && _currentDatabase.IsConnected);
                }
                return _executeCurrentCommand;
            }
        }

        private DelegateCommand _executeScriptCommand;
        public ICommand ExecuteScriptCommand
        {
            get
            {
                if (_executeScriptCommand == null)
                {
                    _executeScriptCommand = new DelegateCommand(
                        ExecuteScript,
                        () => _currentDatabase != null && _currentDatabase.IsConnected);
                }
                return _executeScriptCommand;
            }
        }

        private DelegateCommand _explainPlanCommand;
        public ICommand ExplainPlanCommand
        {
            get
            {
                if (_explainPlanCommand == null)
                {
                    _explainPlanCommand = new DelegateCommand(
                        ExplainPlan,
                        () => _currentDatabase != null && _currentDatabase.IsConnected);
                }
                return _explainPlanCommand;
            }
        }

        private DelegateCommand<ScrollValueChangedEventArgs> _resultsVerticalScrollCommand;
        public ICommand ResultsVerticalScrollCommand
        {
            get
            {
                if (_resultsVerticalScrollCommand == null)
                {
                    _resultsVerticalScrollCommand = new DelegateCommand<ScrollValueChangedEventArgs>(ResultsVerticalScroll);
                }
                return _resultsVerticalScrollCommand;
            }
        }


        #endregion

        #region Public methods

        public void Save()
        {
            string filename = _fileName;
            if (_fileName.IsNullOrEmpty())
            {
                filename = _title;
                var options = new SaveFileDialogOptions
                {
                    Filter = ResourceManager.GetString("sql_file_filter_name") + "|*.sql",
                };
                var service = GetService<IFileDialogService>();
                if (service.ShowSave(ref filename, options) != true)
                {
                    return;
                }
            }
            File.WriteAllText(filename, Text);
            Title = Path.GetFileName(filename);
            FileName = filename;
            IsModified = false;
        }

        public void Cut()
        {
            if (!SelectedText.IsNullOrEmpty())
            {
                GetService<IClipboardService>().SetText(SelectedText);
                SelectedText = string.Empty;
            }
        }

        public void Copy()
        {
            if (!SelectedText.IsNullOrEmpty())
            {
                GetService<IClipboardService>().SetText(SelectedText);
            }
        }

        public void Paste()
        {
            var service = GetService<IClipboardService>();
            if (service.ContainsText())
            {
                SelectedText = service.GetText();
            }
        }

        public void ExecuteCurrent()
        {
            if (_reader != null && !_reader.IsClosed)
                _reader.Dispose();
            
            Results = null;
            HasMoreRows = false;
            FetchComplete = false;
            FetchedRows = 0;

            if (!CheckDatabaseConnected())
                return;

            string sql = GetCurrentStatement();
            if (sql.IsNullOrEmpty())
                return;

            try
            {
                if (sql.StartsWith("select ", StringComparison.InvariantCultureIgnoreCase))
                {
                    var reader = _currentDatabase.ExecuteReader(sql);
                    DataTable table = new DataTable();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        table.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                    }
                    FetchRecords(table, reader);
                    _reader = reader;
                    Results = table;
                }
                else
                {
                    _currentDatabase.ExecuteNonQuery(sql);
                }
            }
            catch(DbException ex)
            {
                var mbox = GetService<IMessageBoxService>();
                string text = string.Format("{0}\n{1}", ResourceManager.GetString("query_error"), ex.Message);
                string title = ResourceManager.GetString("error");
                mbox.Show(text, title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ExecuteScript()
        {
            if (!CheckDatabaseConnected())
                return;

            GetService<IMessageBoxService>().Show("Not implemented");
        }

        public void ExplainPlan()
        {
            if (!CheckDatabaseConnected())
                return;

            GetService<IMessageBoxService>().Show("Not implemented");
        }

        #endregion

        private void FetchRecords(DataTable table, IDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                int n = 0;
                object[] values = new object[reader.FieldCount];
                bool hasMoreRows = false;
                while (n < 50 && (hasMoreRows = reader.Read()))
                {
                    reader.GetValues(values);
                    var r = table.LoadDataRow(values, LoadOption.PreserveChanges);
                    n++;
                }
                FetchedRows += n;
                if (hasMoreRows)
                {
                    FetchComplete = false;
                    HasMoreRows = true;
                }
                else
                {
                    HasMoreRows = false;
                    FetchComplete = true;
                    reader.Dispose();
                }
            }
        }

        private void ResultsVerticalScroll(ScrollValueChangedEventArgs e)
        {
            if (e.Orientation == Orientation.Vertical)
            {
                var threshold = e.Maximum - (e.Maximum - e.Minimum) / 20; // 5% from the bottom
                if (e.NewValue > threshold)
                    FetchRecords(_results, _reader);
            }
        }

        private bool ClipboardContainsText()
        {
            if (IsInDesignMode)
                return true;
            return GetService<IClipboardService>().ContainsText();
        }

        private bool CheckDatabaseConnected()
        {
            if (_currentDatabase == null)
            {
                var service = GetService<IMessageBoxService>();
                string message = ResourceManager.GetString("no_database_selected");
                service.Show(message);
                return false;
            }

            if (!_currentDatabase.IsConnected)
            {
                var service = GetService<IMessageBoxService>();
                string message = ResourceManager.GetString("database_not_connected");
                service.Show(message);
                return false;
            }

            return true;
        }

        private string GetCurrentStatement()
        {
            string stmt = SelectedText;
            if (stmt.IsNullOrEmpty())
            {
                int start;
                int length;
                stmt = GetCurrentStatement(Text, CaretPosition.Offset, out start, out length);
                Selection = new TextSelection(start, length);
            }
            return stmt.Trim('\r', '\n', ' ', '\t', ';');
        }

        private string GetCurrentStatement(string text, int position, out int start, out int length)
        {
            if (position >= text.Length)
                position = text.Length - 1;
            while (text[position].In('\r', '\n') && position > 0)
                position--;

            start = 0;
            length = text.Length;

            // Find start
            int curPos = position;
            int charPos = -1;
            bool nonWhiteSpaceOnLine = false;
            while (curPos >= 0)
            {
                char c = text[curPos];

                if (!char.IsWhiteSpace(c))
                    nonWhiteSpaceOnLine = true;

                // Ignore CR
                if (c == '\r')
                {
                    curPos--;
                    continue;
                }

                if (c != '\n' && nonWhiteSpaceOnLine)
                    charPos = curPos;
                else
                {
                    if (nonWhiteSpaceOnLine)
                        nonWhiteSpaceOnLine = false;
                    else if (charPos > -1)
                        break;
                }

                curPos--;
            }
            start = Math.Max(charPos, 0);

            // Find end
            curPos = position;
            charPos = -1;
            nonWhiteSpaceOnLine = false;
            while (curPos < text.Length)
            {
                char c = text[curPos];

                if (!char.IsWhiteSpace(c))
                    nonWhiteSpaceOnLine = true;

                // Ignore CR
                if (c == '\r')
                {
                    curPos++;
                    continue;
                }

                if (c != '\n' && nonWhiteSpaceOnLine)
                    charPos = curPos;
                else
                {
                    if (nonWhiteSpaceOnLine)
                        nonWhiteSpaceOnLine = false;
                    else if (charPos > -1)
                        break;
                }

                curPos++;
            }
            length = Math.Max(charPos, 0) - start + 1;

            return text.Substring(start, length);
        }

    }
}
