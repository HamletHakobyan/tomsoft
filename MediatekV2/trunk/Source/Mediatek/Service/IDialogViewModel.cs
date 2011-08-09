using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Developpez.Dotnet.Windows.ViewModel;

namespace Mediatek.Service
{
    public interface IDialogViewModel : IWindowViewModel
    {
        string DialogTitle { get; }
        IEnumerable<DialogButton> Buttons { get; }
    }

    public class DialogButton : INotifyPropertyChanged
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged("Text");
            }
        }

        private bool _isDefault;
        public bool IsDefault
        {
            get { return _isDefault; }
            set
            {
                _isDefault = value;
                OnPropertyChanged("IsDefault");
            }
        }

        private bool _isCancel;
        public bool IsCancel
        {
            get { return _isCancel; }
            set
            {
                _isCancel = value;
                OnPropertyChanged("IsCancel");
            }
        }

        private bool? _dialogResult;
        public bool? DialogResult
        {
            get { return _dialogResult; }
            set
            {
                _dialogResult = value;
                OnPropertyChanged("DialogResult");
            }
        }

        private ICommand _command;
        public ICommand Command
        {
            get { return _command; }
            set
            {
                _command = value;
                OnPropertyChanged("Command");
            }
        }

        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}