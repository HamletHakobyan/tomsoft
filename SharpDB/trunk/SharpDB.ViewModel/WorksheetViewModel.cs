using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Developpez.Dotnet;
using Developpez.Dotnet.Windows.Input;
using Developpez.Dotnet.Windows.Service;
using SharpDB.Util;
using SharpDB.Util.Service;
using System.Windows.Media;
using System.Text.RegularExpressions;
using System.Linq;

namespace SharpDB.ViewModel
{
    public class WorksheetViewModel : ViewModelBase
    {
        private static int _worksheetNum = 0;

        private readonly MainWindowViewModel _mainWindow;
        private readonly DatabaseManagerViewModel _manager;
        private IDataReader _reader;

        public WorksheetViewModel(MainWindowViewModel mainWindow)
        {
            _mainWindow = mainWindow;
            _manager = mainWindow.DatabaseManager;
            _title = string.Format(GetResource<string>("new_worksheet_name_format"), ++_worksheetNum);
            Mediator.Instance.Subscribe<ConnectionStateChangedMessage>(OnConnectionStateChanged);
            Mediator.Instance.Subscribe<DatabaseRemovedMessage>(OnDatabaseRemoved);
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

        private TextEditorSelection _selection;
        public TextEditorSelection Selection
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

        private FlowDocument _output;
        public FlowDocument Output
        {
            get { return _output; }
            set
            {
                if (value != _output)
                {
                    _output = value;
                    OnPropertyChanged("Output");
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
                    ShowOutput = !value;
                }
            }
        }

        private bool _showOutput;
        public bool ShowOutput
        {
            get { return _showOutput; }
            set
            {
                if (value != _showOutput)
                {
                    _showOutput = value;
                    OnPropertyChanged("ShowOutput");
                    ShowResults = !value;
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
                            DoSave,
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

        private DelegateCommand<MouseWheelEventArgs> _worksheetZoomCommand;
        public ICommand WorksheetZoomCommand
        {
            get
            {
                if (_worksheetZoomCommand == null)
                {
                    _worksheetZoomCommand = new DelegateCommand<MouseWheelEventArgs>(WorksheetZoom);
                }
                return _worksheetZoomCommand;
            }
        }

        private DelegateCommand<MouseWheelEventArgs> _outputZoomCommand;
        public ICommand OutputZoomCommand
        {
            get
            {
                if (_outputZoomCommand == null)
                {
                    _outputZoomCommand = new DelegateCommand<MouseWheelEventArgs>(OutputZoom);
                }
                return _outputZoomCommand;
            }
        }

        private DelegateCommand<MouseWheelEventArgs> _resultsZoomCommand;
        public ICommand ResultsZoomCommand
        {
            get
            {
                if (_resultsZoomCommand == null)
                {
                    _resultsZoomCommand = new DelegateCommand<MouseWheelEventArgs>(ResultsZoom);
                }
                return _resultsZoomCommand;
            }
        }

        private void DoSave()
        {
            Save();
        }

        #endregion

        #region Public methods

        public bool Save()
        {
            string filename = _fileName;
            if (_fileName.IsNullOrEmpty())
            {
                filename = _title;
                var options = new SaveFileDialogOptions
                {
                    Filter = GetResource<string>("sql_file_filter_name") + "|*.sql",
                };
                var service = GetService<IFileDialogService>();
                if (service.ShowSave(ref filename, options) != true)
                {
                    return false;
                }
            }
            File.WriteAllText(filename, Text);
            Title = Path.GetFileName(filename);
            FileName = filename;
            IsModified = false;
            _mainWindow.AddRecentFile(filename);
            return true;
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

        private static Regex _queryRegex = new Regex(@"^(select|explain)\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public void ExecuteCurrent()
        {
            if (!CheckDatabaseConnected())
                return;

            string sql = GetCurrentStatement();
            if (sql.IsNullOrEmpty())
                return;

            CleanupPreviousResults();

           try
            {
                if (_queryRegex.IsMatch(sql))
                {
                    ExecuteQuery(sql);
                }
                else
                {
                    ExecuteNonQuery(sql);
                }
            }
            catch(DbException ex)
            {
                ReportSqlError(ex);
            }
        }

        public void ExecuteScript()
        {
            if (!CheckDatabaseConnected())
                return;

            string script = SelectedText;
            if (script.IsNullOrEmpty())
                script = Text;

            script = StripComments(script);
            if (script.IsNullOrEmpty())
                return;

            string[] statements = script.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(s => s.Trim())
                                        .Where(s => !s.IsNullOrEmpty())
                                        .ToArray();

            if (statements.Length == 0)
                return;

            CleanupPreviousResults();

            foreach (var stmt in statements)
            {
                try
                {
                    ExecuteNonQuery(stmt);
                }
                catch (DbException ex)
                {
                    ReportSqlError(ex);
                    break;
                }
            }
        }

        public void ExplainPlan()
        {
            if (!CheckDatabaseConnected())
                return;

            GetService<IMessageBoxService>().Show("Not implemented");
        }

        public bool ConfirmClose()
        {
            if (!IsModified)
                return true;

            string title = GetResource<string>("confirmation");
            string text = GetResource<string>("save_changes_before_close");
            var service = GetService<IMessageBoxService>();
            var r = service.Show(text, title, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (r == MessageBoxResult.Yes)
            {
                return Save();
            }
            else if (r == MessageBoxResult.No)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region Private methods

        private void CleanupPreviousResults()
        {
            if (_reader != null && !_reader.IsClosed)
                _reader.Dispose();

            Results = null;
            HasMoreRows = false;
            FetchComplete = false;
            FetchedRows = 0;
        }

        private const string blockCommentStart = "/*";
        private const string blockCommentEnd = "*/";
        private const string lineCommentStart = "--";

        private string StripComments(string sql)
        {
            // Remove line comments
            int i = sql.IndexOf(lineCommentStart);
            while (i > -1)
            {
                int j = sql.IndexOfAny(new[] { '\r', '\n' }, i + lineCommentStart.Length);
                if (j > -1)
                {
                    sql = sql.Remove(i, j - i);
                }
                i = sql.IndexOf(lineCommentStart);
            }

            // Remove block comments
            i = sql.IndexOf(blockCommentStart);
            while (i > -1)
            {
                int j = sql.IndexOf(blockCommentEnd, i + blockCommentStart.Length);
                if (j > -1)
                {
                    sql = sql.Remove(i, j - i + blockCommentEnd.Length);
                }
                i = sql.IndexOf("/*");
            }

            return sql;
        }

        private void ReportSqlError(DbException ex)
        {
            string text = string.Format("{0}\n{1}", GetResource<string>("query_error"), ex.Message);

            //string title = GetResource<string>("error");
            //var mbox = GetService<IMessageBoxService>();
            //mbox.Show(text, title, MessageBoxButton.OK, MessageBoxImage.Error);

            AppendOutput(text, OutputType.Error);
            ShowOutput = true;
        }

        private void ExecuteNonQuery(string sql)
        {
            int affectedRows = _currentDatabase.ExecuteNonQuery(sql);
            string affectedRowsText = string.Format(GetResource<string>("affected_rows_format"), affectedRows);
            string text =
                GetResource<string>("query_success")
                + Environment.NewLine
                + affectedRowsText;
            AppendOutput(text, OutputType.Info);
            ShowOutput = true;
        }

        private void ExecuteQuery(string sql)
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
            ShowResults = true;
        }



        private enum OutputType
        {
            Text,
            Info,
            Error
        }

        private void AppendOutput(string text, OutputType outputType)
        {
            if (_output == null)
            {
                Output = new FlowDocument();
            }

            var run = new Run(text);
            switch (outputType)
            {
                case OutputType.Info:
                    run.Foreground = Brushes.Blue;
                    break;
                case OutputType.Error:
                    run.Foreground = Brushes.Red;
                    break;
                case OutputType.Text:
                default:
                    run.Foreground = Brushes.Black;
                    break;
            }
            var paragraph = new Paragraph(run);
            _output.Blocks.Add(paragraph);
        }

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
                string message = GetResource<string>("no_database_selected");
                service.Show(message);
                return false;
            }

            if (!_currentDatabase.IsConnected)
            {
                var service = GetService<IMessageBoxService>();
                string message = GetResource<string>("database_not_connected");
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
                if (length > 0)
                    Selection = new TextEditorSelection(start, length);
            }
            return stmt.Trim('\r', '\n', ' ', '\t', ';');
        }

        private string GetCurrentStatement(string text, int position, out int start, out int length)
        {
            start = position;
            length = 0;

            if (position >= text.Length)
                position = text.Length - 1;

            int first;
            int last;
            string currentLine = GetLine(text, position, out first, out last);

            if (currentLine.Trim().IsNullOrEmpty())
                return string.Empty;

            int startOfFirstLine = first;
            int endOfLastLine = last;

            // Check previous lines
            int i = startOfFirstLine - 1;
            while (i >= 0)
            {
                currentLine = GetLine(text, i, out first, out last);
                if (currentLine.Trim().IsNullOrEmpty())
                    break;
                startOfFirstLine = first;
                i = first - 1;
            }

            // Check next lines
            i = text.IndexOf('\n', endOfLastLine) + 1;
            while (i > 0 && i < text.Length)
            {
                currentLine = GetLine(text, i, out first, out last);
                if (currentLine.Trim().IsNullOrEmpty())
                    break;
                endOfLastLine = last;
                i = text.IndexOf('\n', last) + 1;
            }

            start = startOfFirstLine;
            length = endOfLastLine - startOfFirstLine + 1;
            return text.Substring(start, length);
        }

        private string GetLine(string text, int position, out int startOfLine, out int endOfLine)
        {
            startOfLine = GetStartOfLine(text, position);
            endOfLine = GetEndOfLine(text, position);
            return text.Substring(startOfLine, endOfLine - startOfLine + 1);
        }

        private int GetStartOfLine(string text, int position)
        {
            int n = position - 1;
            while (n >= 0 && text[n] != '\n')
            {
                n--;
            }
            return n + 1;
        }

        private int GetEndOfLine(string text, int position)
        {
            int n = position;
            while (n < text.Length && text[n] != '\n')
            {
                n++;
            }
            if (text[n - 1] == '\r')
                n--;
            return n - 1;
        }

        private void WorksheetZoom(MouseWheelEventArgs e)
        {
            ChangeFontSize("WorksheetFontSize", e.Delta);
        }

        private void ResultsZoom(MouseWheelEventArgs e)
        {
            ChangeFontSize("ResultsFontSize", e.Delta);
        }

        private void OutputZoom(MouseWheelEventArgs e)
        {
            ChangeFontSize("OutputFontSize", e.Delta);
        }

        private static double _minFontSize = 8;
        private static double _maxFontSize = 32;

        private void ChangeFontSize(string settingName, int delta)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                double fontSize = GetSetting<double>(settingName);
                if (delta > 0 && fontSize < _maxFontSize)
                {
                    fontSize++;
                }
                else if (delta < 0 && fontSize > _minFontSize)
                {
                    fontSize--;
                }
                else
                {
                    return;
                }
                Settings[settingName] = fontSize;
            }
        }

        #endregion

        #region Mediator message handlers

        private void OnConnectionStateChanged(object sender, ConnectionStateChangedMessage message)
        {
            if (message.Database == CurrentDatabase && !message.IsConnected)
            {
                CurrentDatabase = null;
            }
            else if (CurrentDatabase == null && message.IsConnected)
            {
                if (_mainWindow.CurrentWorksheet == this)
                {
                    CurrentDatabase = message.Database;
                }
            }
        }

        private void OnDatabaseRemoved(object sender, DatabaseRemovedMessage message)
        {
            if (message.Database == CurrentDatabase)
            {
                CurrentDatabase = null;
            }
        }

        #endregion
    }
}
