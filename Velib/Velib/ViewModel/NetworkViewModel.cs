using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVMLib.ViewModel;
using System.Windows.Navigation;
using System.Collections.ObjectModel;

namespace Velib.ViewModel
{
    public class NetworkViewModel : ViewModelBase
    {

        public NetworkViewModel(NavigationWindow navigationWindow, Network network)
        {
            this._navigationWindow = navigationWindow;
            this._network = network;

            var stationViewModels =
                from s in network.Stations
                select new StationViewModel(navigationWindow, s);

            this.Stations = new ObservableCollection<StationViewModel>(stationViewModels);
            this.Stations.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Stations_CollectionChanged);
        }

        private NavigationWindow _navigationWindow;
        private Network _network;

        public string Name
        {
            get { return _network.Name; }
            set
            {
                _network.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public ObservableCollection<StationViewModel> Stations { get; private set; }

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

    }
}
