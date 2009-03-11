using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVMLib.ViewModel;
using System.Windows.Navigation;
using MVVMLib;

namespace Velib.ViewModel
{
    public class StationViewModel : ViewModelBase
    {
        public StationViewModel(NavigationWindow navigationWindow, Station station)
        {
            this._navigationWindow = navigationWindow;
            this._station = station;
        }

        private NavigationWindow _navigationWindow;
        private Station _station;

        public string Name
        {
            get { return _station.Name; }
        }

        private StationStatus _status;
        public StationStatus Status
        {
            get
            {
                if (_status == null)
                    _status = _station.GetStatus();
                return _status;
            }
        }

        private RelayCommand _refreshStatusCommand = null;

        public RelayCommand RefreshStatusCommand
        {
            get
            {
                if (_refreshStatusCommand == null)
                {
                    _refreshStatusCommand =
                        new RelayCommand(
                            parameter =>
                            {
                                _status = _station.GetStatus();
                                OnPropertyChanged("Status");
                            });
                }
                return _refreshStatusCommand;
            }
        }

    }
}
