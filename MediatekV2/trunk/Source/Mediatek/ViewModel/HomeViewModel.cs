using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Windows.ViewModel;
using Mediatek.Entities;
using Developpez.Dotnet.Windows.Input;
using System.Windows.Input;
using Mediatek.Messaging;
using Mediatek.Service;
using Developpez.Dotnet.Windows.Service;

namespace Mediatek.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        #region Properties

        public string DbName
        {
            get { return App.Repository.Name; }
            set
            {
                if (value != App.Repository.Name)
                {
                    App.Repository.Name = value;
                    OnPropertyChanged("DbName");
                }
            }
        }

        public string DbDescription
        {
            get { return App.Repository.Description; }
            set
            {
                if (value != App.Repository.Description)
                {
                    App.Repository.Description = value;
                    OnPropertyChanged("DbDescription");
                }
            }
        }

        public int MovieCount
        {
            get
            {
                var rep = App.GetService<IViewModelRepository>();
                return rep.Medias.OfType<MovieViewModel>().Count();
            }
        }

        public int AlbumCount
        {
            get
            {
                var rep = App.GetService<IViewModelRepository>();
                return rep.Medias.OfType<AlbumViewModel>().Count();
            }
        }

        public int BookCount
        {
            get
            {
                var rep = App.GetService<IViewModelRepository>();
                return rep.Medias.OfType<BookViewModel>().Count();
            }
        }

        #endregion

        #region Commands

        private DelegateCommand _showMoviesCommand;
        public ICommand ShowMoviesCommand
        {
            get
            {
                if (_showMoviesCommand == null)
                {
                    _showMoviesCommand = new DelegateCommand(
                        () => Mediator.Instance.Post(this, new NavigationMessage(NavigationDestination.Movies))
                    );
                }
                return _showMoviesCommand;
            }
        }

        private DelegateCommand _showAlbumsCommand;
        public ICommand ShowAlbumsCommand
        {
            get
            {
                if (_showAlbumsCommand == null)
                {
                    _showAlbumsCommand = new DelegateCommand(
                        () => Mediator.Instance.Post(this, new NavigationMessage(NavigationDestination.Albums))
                    );
                }
                return _showAlbumsCommand;
            }
        }

        private DelegateCommand _showBooksCommand;
        public ICommand ShowBooksCommand
        {
            get
            {
                if (_showBooksCommand == null)
                {
                    _showBooksCommand = new DelegateCommand(
                        () => Mediator.Instance.Post(this, new NavigationMessage(NavigationDestination.Books))
                    );
                }
                return _showBooksCommand;
            }
        }

        #endregion
    }
}
