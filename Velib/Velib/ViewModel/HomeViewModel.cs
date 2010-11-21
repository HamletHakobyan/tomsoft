using System.Collections.ObjectModel;
using System.Linq;
using Developpez.Dotnet.Windows.Input;
using Developpez.Dotnet.Windows.ViewModel;
using Velib.Model;
using Velib.Navigation;

namespace Velib.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel(INavigationService navigationService)
        {
            this._navigationService = navigationService;
            
            var networkViewModels =
                from n in App.Current.Config.Networks
                select new NetworkViewModel(navigationService, n);
            
            this.Networks = new ObservableCollection<NetworkViewModel>(networkViewModels);
            this.Networks.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Networks_CollectionChanged);
            this.DisplayName = "Vélib - Accueil";
        }

        private INavigationService _navigationService;

        #region Networks

        public ObservableCollection<NetworkViewModel> Networks { get; private set; }

        void Networks_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // TODO
        }

        #endregion

        #region New network properties

        private string _newNetworkUri;
        public string NewNetworkUri
        {
            get { return _newNetworkUri; }
            set
            {
                _newNetworkUri = value;
                OnPropertyChanged("NewNetworkUri");
            }
        }

        private string _newNetworkName;
        public string NewNetworkName
        {
            get { return _newNetworkName; }
            set
            {
                _newNetworkName = value;
                OnPropertyChanged("NewNetworkName");
            }
        }

        #endregion

        #region Commands

        private RelayCommand _showNetworkCommand = null;

        public RelayCommand ShowNetworkCommand
        {
            get
            {
                if (_showNetworkCommand == null)
                {
                    _showNetworkCommand =
                        new RelayCommand(
                            parameter =>
                            {
                                NetworkViewModel viewModel = parameter as NetworkViewModel;
                                //NetworkView view = new NetworkView();
                                //view.DataContext = viewModel;
                                _navigationService.Navigate(viewModel);
                            });
                }
                return _showNetworkCommand;
            }
        }

        private RelayCommand _addNetworkCommand;
        public RelayCommand AddNetworkCommand
        {
            get
            {
                if (_addNetworkCommand == null)
                {
                    _addNetworkCommand =
                        new RelayCommand(
                            parameter =>
                            {
                                Network network = new Network(NewNetworkName, NewNetworkUri);
                                App.Current.Config.Networks.Add(network);
                                this.Networks.Add(new NetworkViewModel(_navigationService, network));
                                NewNetworkName = "";
                                NewNetworkUri = "";
                            },
                            parameter =>
                            {
                                if (string.IsNullOrEmpty(NewNetworkUri) || string.IsNullOrEmpty(NewNetworkName))
                                    return false;
                                else
                                    return true;
                            });
                }
                return _addNetworkCommand;
            }
        }

        #endregion

    }
}
