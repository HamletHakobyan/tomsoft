using MVVMLib.Input;
using MVVMLib.ViewModel;
using Velib.Navigation;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;

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

        public long Number
        {
            get { return _station.Number; }
        }

        private static Regex regexName =
            new Regex(@"^\s*[0-9]+\s*-\s*", RegexOptions.Compiled);

        private string _name = null;
        public string Name
        {
            get
            {
                if (_name == null)
                {
                    _name = regexName.Replace(_station.Name, "", 1);
                }
                return _name;
            }
        }

        public string FullName
        {
            get { return _station.Name; }
        }


        public string Address
        {
            get { return _station.Address; }
        }

        public string FullAddress
        {
            get { return _station.FullAddress; }
        }

        private StationStatusViewModel _status;
        public StationStatusViewModel Status
        {
            get
            {
                if (_status == null)
                {
                    GetStatusAsync();
                }
                return _status;
            }
            private set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        private bool _isDataReady = false;
        public bool IsDataReady
        {
            get { return _isDataReady; }
            private set
            {
                _isDataReady = value;
                OnPropertyChanged("IsDataReady");
                OnPropertyChanged("LoadingPanelVisibility");
            }
        }

        public Visibility LoadingPanelVisibility
        {
            get
            {
                return _isDataReady ? Visibility.Hidden : Visibility.Visible;
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
                                GetStatusAsync();
                            });
                }
                return _refreshStatusCommand;
            }
        }

        #endregion Commands

        bool _gettingStatus = false;
        private void GetStatusAsync()
        {
            if (!_gettingStatus)
            {
                _gettingStatus = true;
                IsDataReady = false;
                ThreadPool.QueueUserWorkItem(GetStatus);
            }
        }

        private void GetStatus(object state)
        {
            Status = new StationStatusViewModel(_station.GetStatus());
            IsDataReady = true;
            _gettingStatus = false;
        }



    }
}
