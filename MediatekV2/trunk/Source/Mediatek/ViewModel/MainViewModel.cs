using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Windows.ViewModel;
using Mediatek.Service;
using System.Windows.Input;
using Developpez.Dotnet.Windows.Input;
using System.Collections.ObjectModel;
using Mediatek.Entities;
using Mediatek.Helpers;
using Developpez.Dotnet.Windows.Service;
using Mediatek.Messaging;

namespace Mediatek.ViewModel
{
    public class MainViewModel : ViewModelBase, INavigationService
    {
        private readonly LinkedList<object> _history;
        private LinkedListNode<object> _currentNode;

        public MainViewModel()
        {
            Mediator.Instance.Subscribe<NavigationMessage>(NavigationMessageHandler);

            _history = new LinkedList<object>();
            HomeVM = new HomeViewModel();
            MoviesVM = new MoviesViewModel();
            AlbumsVM = new AlbumsViewModel();
            BooksVM = new BooksViewModel();
            Navigate(HomeVM);
        }

        private void NavigationMessageHandler(object sender, NavigationMessage message)
        {
            switch (message.Destination)
            {
                case NavigationDestination.Movies:
                    ShowMovies();
                    break;
                case NavigationDestination.Albums:
                    ShowAlbums();
                    break;
                case NavigationDestination.Books:
                    ShowBooks();
                    break;
                default:
                    Navigate(HomeVM);
                    break;
            }
        }

        #region INavigationService implementation

        public object Current
        {
            get
            {
                if (_currentNode != null)
                    return _currentNode.Value;
                return null;
            }
        }

        public bool Navigate(object dest)
        {
            while (_currentNode != null && _currentNode.Next != null)
                _history.RemoveLast();
            _currentNode = _history.AddLast(dest);
            OnNavigationPropertiesChanged();
            return true;
        }

        public bool GoBack()
        {
            if (_currentNode == null || _currentNode.Previous == null)
                return false;
            _currentNode = _currentNode.Previous;
            OnNavigationPropertiesChanged();
            return true;
        }

        public bool GoForward()
        {
            if (_currentNode == null || _currentNode.Next == null)
                return false;
            _currentNode = _currentNode.Next;
            OnNavigationPropertiesChanged();
            return true;
        }

        public bool CanGoBack
        {
            get
            {
                return _currentNode != null && _currentNode.Previous != null;
            }
        }

        public bool CanGoForward
        {
            get
            {
                return _currentNode != null && _currentNode.Next != null;
            }
        }

        private void OnNavigationPropertiesChanged()
        {
            OnPropertyChanged("Current");
            OnPropertyChanged("CanGoBack");
            OnPropertyChanged("CanGoForward");
        }

        #endregion

        #region Properties

        public HomeViewModel HomeVM { get; private set; }
        public MoviesViewModel MoviesVM { get; private set; }
        public AlbumsViewModel AlbumsVM { get; private set; }
        public BooksViewModel BooksVM { get; private set; }

        public IList<MediaViewModel> Medias
        {
            get { return App.GetService<IViewModelRepository>().Medias; }
        }

        #endregion

        #region Commands

        private DelegateCommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new DelegateCommand(ExitApp);
                }
                return _closeCommand;
            }
        }

        private DelegateCommand _goForwardCommand;
        public ICommand GoForwardCommand
        {
            get
            {
                if (_goForwardCommand == null)
                {
                    _goForwardCommand = new DelegateCommand(
                        () => GoForward(),
                        () => CanGoForward);
                }
                return _goForwardCommand;
            }
        }

        private DelegateCommand _goBackCommand;
        public ICommand GoBackCommand
        {
            get
            {
                if (_goBackCommand == null)
                {
                    _goBackCommand = new DelegateCommand(
                        () => GoBack(),
                        () => CanGoBack);
                }
                return _goBackCommand;
            }
        }

        #endregion

        #region Public methods

        public void ShowMovies()
        {
            Navigate(MoviesVM);
        }

        public void ShowAlbums()
        {
            Navigate(AlbumsVM);
        }

        public void ShowBooks()
        {
            Navigate(BooksVM);
        }

        #endregion

        #region Private methods

        private void ExitApp()
        {
            App.Current.Shutdown();
        }

        #endregion
    }
}
