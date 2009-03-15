using MVVMLib;
using MVVMLib.ViewModel;
using Velib.Navigation;
using MVVMLib.Input;

namespace Velib.ViewModel
{
    public class StationViewModel : ViewModelBase
    {
        public StationViewModel(INavigationService navigationService, Station station)
        {
            this._navigationService = navigationService;
            this._station = station;
        }

        private INavigationService _navigationService;
        private Station _station;

        #region Properties

        public string Number
        {
            get { return _station.Number; }
        }

        public string Name
        {
            get { return _station.Name; }
        }

        public string Address
        {
            get { return _station.Address; }
        }

        private StationStatusViewModel _status;
        public StationStatusViewModel Status
        {
            get
            {
                if (_status == null)
                {
                    _status = new StationStatusViewModel(_station.GetStatus());
                }
                return _status;
            }
        }

        #endregion Properties

        #region Commands

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
                                _status = new StationStatusViewModel(_station.GetStatus());
                                OnPropertyChanged("Status");
                            });
                }
                return _refreshStatusCommand;
            }
        }

        #endregion Commands

    }
}
