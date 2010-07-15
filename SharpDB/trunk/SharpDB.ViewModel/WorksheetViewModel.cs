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

namespace SharpDB.ViewModel
{
    public class WorksheetViewModel : ViewModelBase
    {
        private static int _worksheetNum = 0;

        private readonly DatabaseManagerViewModel _manager;

        public WorksheetViewModel(DatabaseManagerViewModel manager)
        {
            _manager = manager;
            _title = string.Format(ResourceManager.GetString("new_worksheet_name_format"), ++_worksheetNum);
            _text = "SELECT * FROM foo";
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
                        () => GetService<IClipboardService>().ContainsText());
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

        #endregion

        #region Public methods

        public void Save()
        {
            string filename = _title;
            var options = new SaveFileDialogOptions
            {
                Filter = ResourceManager.GetString("sql_file_filter_name") + "|*.sql",
            };
            var service = GetService<IFileDialogService>();
            if (service.ShowSave(ref filename, options) == true)
            {
                File.WriteAllText(filename, Text);
                Title = Path.GetFileName(filename);
                IsModified = false;
            }
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
            if (!CheckDatabaseConnected())
                return;

            string sql = GetCurrentStatement();
            if (sql == null)
                return;

            try
            {
                if (sql.StartsWith("select ", StringComparison.InvariantCultureIgnoreCase))
                {
                    var reader = new PagingDataReader(_currentDatabase.ExecuteReader(sql));
                    DataTable table = new DataTable();
                    table.Load(reader);
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
            while (text[position].In('\r', '\n') && position >= 0)
                position--;

            start = 0;
            length = text.Length;

            // Find start
            int curPos = position;
            int charPos = curPos;
            bool nonWhiteSpaceOnLine = true;
            while (curPos >= 0)
            {
                char c = text[curPos];

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
                    else
                        break;
                }

                if (!char.IsWhiteSpace(c))
                    nonWhiteSpaceOnLine = true;

                curPos--;
            }
            start = charPos;

            // Find end
            curPos = position;
            charPos = curPos;
            nonWhiteSpaceOnLine = true;
            while (curPos < text.Length)
            {
                char c = text[curPos];

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
                    else
                        break;
                }

                if (!char.IsWhiteSpace(c))
                    nonWhiteSpaceOnLine = true;

                curPos++;
            }
            length = charPos - start + 1;

            return text.Substring(start, length);
        }

    }
}
