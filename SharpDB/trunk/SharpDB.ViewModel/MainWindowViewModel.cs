using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Windows.Input;
using System.Windows.Input;
using SharpDB.Util.Dialogs;
using SharpDB.Util;

namespace SharpDB.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {

        public MainWindowViewModel()
        {
            _title = "SharpDB - Untitled1";
            _currentWorksheet = new WorksheetViewModel();
        }

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

        private DelegateCommand _testDialogCommand;
        public ICommand TestDialogCommand
        {
            get
            {
                if (_testDialogCommand == null)
                {
                    _testDialogCommand = new DelegateCommand(TestDialog);
                }
                return _testDialogCommand;
            }
        }

        private void TestDialog()
        {
            var service = GetService<IDialogService>();
            service.Show(new DummyViewModel());
        }

        class DummyViewModel : IDialogViewModel
        {
            private DialogButton[] _buttons;

            public DummyViewModel()
            {
                var sayHelloCommand = new DelegateCommand(() =>
                    {
                        var service = ServiceLocator.Instance.GetService<IMessageBoxService>();
                        service.Show("Hello, world !");
                    });

                _buttons = new[]
                {
                    new DialogButton { Text = "Say hello", Command = sayHelloCommand },
                    new DialogButton { Text = "OK", IsDefault = true, DialogResult = true },
                    new DialogButton { Text = "Cancel", IsCancel = false, DialogResult = false }
                };
            }

            public string DialogTitle
            {
                get { return "Hello there !"; }
            }

            public IEnumerable<DialogButton> Buttons
            {
                get { return _buttons; }
            }

            public bool Resizable
            {
                get { return true; }
            }

            public void OnShow()
            {
            }

            public void OnClose(bool? result)
            {
            }

            public event CloseRequestedEventHandler CloseRequested;
        }

    }
}
