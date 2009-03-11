using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVMLib.ViewModel;
using Velib.Model;
using System.Collections.ObjectModel;
using MVVMLib;
using System.Windows.Navigation;
using Velib.View;

namespace Velib.ViewModel
{
    public class NetworkRepositoryViewModel : ViewModelBase
    {
        public NetworkRepositoryViewModel(NavigationWindow navigationWindow, NetworkRepository repository)
        {
            this._navigationWindow = navigationWindow;
            this._repository = repository;
            
            var networkViewModels =
                from n in repository.Networks
                select new NetworkViewModel(navigationWindow, n);
            
            this.Networks = new ObservableCollection<NetworkViewModel>(networkViewModels);
            this.Networks.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Networks_CollectionChanged);
        }

        private NavigationWindow _navigationWindow;
        private NetworkRepository _repository;

        #region Networks

        public ObservableCollection<NetworkViewModel> Networks { get; private set; }

        void Networks_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // TODO
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
                                NetworkView view = new NetworkView();
                                view.DataContext = viewModel;
                                _navigationWindow.Navigate(view);
                            });
                }
                return _showNetworkCommand;
            }
        }


        #endregion

    }
}
