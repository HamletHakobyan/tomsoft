﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;

namespace SharpDB.Util.Service
{
    public class DialogButton : INotifyPropertyChanged
    {
        private bool? _dialogResult;
        public bool? DialogResult
        {
            get { return _dialogResult; }
            set
            {
                if (value != _dialogResult)
                {
                    _dialogResult = value;
                    OnPropertyChanged("DialogResult");
                }
            }
        }

        private bool _isDefault;
        public bool IsDefault
        {
            get { return _isDefault; }
            set
            {
                if (value != _isDefault)
                {
                    _isDefault = value;
                    OnPropertyChanged("IsDefault");
                }
            }
        }

        private bool _isCancel;
        public bool IsCancel
        {
            get { return _isCancel; }
            set
            {
                if (value != _isCancel)
                {
                    _isCancel = value;
                    OnPropertyChanged("IsCancel");
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
                }
            }
        }

        private bool _isVisible = true;
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (value != _isVisible)
                {
                    _isVisible = value;
                    OnPropertyChanged("IsVisible");
                }
            }
        }

        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (value != _isEnabled)
                {
                    _isEnabled = value;
                    OnPropertyChanged("IsEnabled");
                }
            }
        }

        private ICommand _command;
        public ICommand Command
        {
            get { return _command; }
            set
            {
                if (value != _command)
                {
                    _command = value;
                    OnPropertyChanged("Command");
                }
            }
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
