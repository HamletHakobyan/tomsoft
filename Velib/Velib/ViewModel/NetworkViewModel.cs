using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVMLib.ViewModel;
using System.Windows.Navigation;
using System.Collections.ObjectModel;
using Velib.Model;
using MVVMLib;

namespace Velib.ViewModel
{
    public class NetworkViewModel : ViewModelBase
    {

        public NetworkViewModel(NavigationWindow navigationWindow, Network network)
        {
            this._navigationWindow = navigationWindow;
            this._network = network;
        }

        private NavigationWindow _navigationWindow;
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
                OnPropertyChanged("BaseUri");
            }
        }

        private ObservableCollection<StationViewModel> _stations = null;
        public ObservableCollection<StationViewModel> Stations
        {
            get
            {
                if (_stations == null)
                {
                    var stationViewModels =
                        from s in _network.Data.Stations
                        select new StationViewModel(_navigationWindow, s);
                    _stations = new ObservableCollection<StationViewModel>(stationViewModels);
                    _stations.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Stations_CollectionChanged);
                }
                return _stations;
            }
        }

        void Stations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // TODO
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

        #endregion Commands
    }
}
