using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using MVVMLib.Input;
using MVVMLib.ViewModel;
using Velib.Model;
using Velib.Navigation;
using System.Threading;
using System.Windows;

namespace Velib.ViewModel
{
    public class NetworkViewModel : ViewModelBase
    {

        public NetworkViewModel(INavigationService navigationService, Network network)
        {
            this._navigationService = navigationService;
            this._network = network;
        }

        private INavigationService _navigationService;
        private Network _network;

        #region Properties

        public string Name
        {
            get { return _network.Name; }
            set
            {
                _network.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public string BaseUri
        {
            get { return _network.BaseUri; }
            set
            {
                _network.BaseUri = value;
                _network.InvalidateData();
                _stations = null;
                OnPropertyChanged("BaseUri");
                OnPropertyChanged("Stations");
            }
        }

        private ObservableCollection<StationViewModel> _stations = null;
        public ObservableCollection<StationViewModel> Stations
        {
            get
            {
                if (_stations == null)
                {
                    CreateStationsAsync(false);
                }
                return _stations;
            }
            set
            {
                _stations = value;
                OnPropertyChanged("Stations");
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

        private StationViewModel _selectedStation;
        public StationViewModel SelectedStation
        {
            get { return _selectedStation; }
            set
            {
                if (value != _selectedStation)
                {
                    _selectedStation = value;
                    OnPropertyChanged("SelectedStation");
                }
            }
        }

        private bool _isInEditMode = false;
        public bool IsInEditMode
        {
            get { return _isInEditMode; }
            set
            {
                _isInEditMode = value;
                OnPropertyChanged("IsInEditMode");
            }
        }

        private string _newName;
        public string NewName
        {
            get { return _newName; }
            set
            {
                _newName = value;
                OnPropertyChanged("NewName");
            }
        }

        private string _newBaseUri;
        public string NewBaseUri
        {
            get { return _newBaseUri; }
            set
            {
                _newBaseUri = value;
                OnPropertyChanged("NewBaseUri");
            }
        }

        private string _searchText = "";
        public string SearchText
        {
            set
            {
                _searchText = value.ToLower();
                SetStationFilter();
            }
        }


        #endregion Properties

        #region Commands

        private RelayCommand _beginEditCommand;
        public RelayCommand BeginEditCommand
        {
            get
            {
                if (_beginEditCommand == null)
                {
                    _beginEditCommand = new RelayCommand(
                        parameter =>
                        {
                            NewName = Name;
                            NewBaseUri = BaseUri;
                            IsInEditMode = true;
                        });
                }
                return _beginEditCommand;
            }
        }

        private RelayCommand _commitEditCommand;
        public RelayCommand CommitEditCommand
        {
            get
            {
                if (_commitEditCommand == null)
                {
                    _commitEditCommand = new RelayCommand(
                        parameter =>
                        {
                            Name = NewName;
                            BaseUri = NewBaseUri;
                            IsInEditMode = false;
                        },
                        parameter =>
                        {
                            if (string.IsNullOrEmpty(NewName) || string.IsNullOrEmpty(NewBaseUri))
                                return false;
                            else
                                return true;
                        });
                }
                return _commitEditCommand;
            }
        }

        private RelayCommand _cancelEditCommand;
        public RelayCommand CancelEditCommand
        {
            get
            {
                if (_cancelEditCommand == null)
                {
                    _cancelEditCommand = new RelayCommand(
                        parameter =>
                        {
                            NewName = Name;
                            NewBaseUri = BaseUri;
                            IsInEditMode = false;
                        });
                }
                return _cancelEditCommand;
            }
        }

        private RelayCommand _refreshStationsCommand;
        public RelayCommand RefreshStationsCommand
        {
            get
            {
                if (_refreshStationsCommand == null)
                {
                    _refreshStationsCommand = new RelayCommand(
                        parameter =>
                        {
                            CreateStationsAsync(true);
                        });
                }
                return _refreshStationsCommand;
            }
        }

        private RelayCommand _sortCommand;
        public RelayCommand SortCommand
        {
            get
            {
                if (_sortCommand == null)
                {
                    _sortCommand = new RelayCommand(
                        parameter =>
                        {
                            SortStationsBy(parameter as string);
                        });
                }
                return _sortCommand;
            }
        }

        private void SortStationsBy(string propertyName)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(_stations);
            ListSortDirection direction = ListSortDirection.Ascending;
            if (view.SortDescriptions.Count > 0)
            {
                SortDescription currentSort = view.SortDescriptions[0];
                if (currentSort.PropertyName == propertyName)
                {
                    if (currentSort.Direction == ListSortDirection.Ascending)
                        direction = ListSortDirection.Descending;
                    else
                        direction = ListSortDirection.Ascending;
                }
                view.SortDescriptions.Clear();
            }
            if (!string.IsNullOrEmpty(propertyName))
            {
                view.SortDescriptions.Add(new SortDescription(propertyName, direction));
            }
        }


        #endregion Commands

        private bool _creatingStations = false;
        private void CreateStationsAsync(bool refresh)
        {
            if (!_creatingStations)
            {
                _creatingStations = true;
                IsDataReady = false;
                ThreadPool.QueueUserWorkItem(CreateStations, refresh);
            }
        }

        private void CreateStations(object oRefresh)
        {
            bool refresh = (bool)oRefresh;
            
            if (refresh)
                _network.RefreshData();

            var stationViewModels =
                from s in _network.Data.Stations
                select new StationViewModel(_navigationService, s);

            Stations = new ObservableCollection<StationViewModel>(stationViewModels);

            SetStationFilter();

            IsDataReady = true;
            _creatingStations = false;
        }

        private void SetStationFilter()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(_stations);
            view.Filter = (item) =>
            {
                StationViewModel station = item as StationViewModel;
                if (station != null)
                {
                    if (station.FullName.ToLower().Contains(_searchText))
                        return true;
                    if (station.Address.ToLower().Contains(_searchText))
                        return true;
                    if (station.FullAddress.ToLower().Contains(_searchText))
                        return true;
                }
                return false;
            };
        }

    }
}
