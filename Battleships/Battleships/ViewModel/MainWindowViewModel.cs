using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet;
using Developpez.Dotnet.ComponentModel;
using Battleships.Service;

namespace Battleships.ViewModel
{
    public class MainWindowViewModel : ViewModelBase, INavigationService
    {
        public MainWindowViewModel()
        {
            Current = new HomeViewModel();
        }

        private ViewModelBase _current;
        public ViewModelBase Current
        {
            get { return _current; }
            set
            {
                _current = value;
                OnPropertyChanged("Current");
            }
        }

        #region INavigationService members

        object INavigationService.Current
        {
            get { return this.Current; }
        }

        void INavigationService.Navigate(object destination)
        {
            Current = (ViewModelBase)destination;
        }

        #endregion
    }
}
