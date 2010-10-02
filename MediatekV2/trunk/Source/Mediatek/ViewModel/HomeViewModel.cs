using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Developpez.Dotnet.Windows.Input;
using Developpez.Dotnet.Windows.Service;
using Mediatek.Helpers;
using Mediatek.Messaging;
using Mediatek.Service;

namespace Mediatek.ViewModel
{
    public class HomeViewModel : MediatekViewModelBase
    {
        public HomeViewModel()
        {
            Mediator.Instance.Subscribe<EntityMessage<MediaViewModel>>(MediaMessageHandler);
        }

        private void MediaMessageHandler(object sender, EntityMessage<MediaViewModel> message)
        {
            if (message.Entity is MovieViewModel)
                OnPropertyChanged("MovieCount");
            else if (message.Entity is AlbumViewModel)
                OnPropertyChanged("AlbumCount");
            else if (message.Entity is BookViewModel)
                OnPropertyChanged("BookCount");
            
            _recentlyAdded = null;
            OnPropertyChanged("RecentlyAdded");
        }

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
                var rep = GetService<IViewModelRepository>();
                return rep.Medias.OfType<MovieViewModel>().Count();
            }
        }

        public int AlbumCount
        {
            get
            {
                var rep = GetService<IViewModelRepository>();
                return rep.Medias.OfType<AlbumViewModel>().Count();
            }
        }

        public int BookCount
        {
            get
            {
                var rep = GetService<IViewModelRepository>();
                return rep.Medias.OfType<BookViewModel>().Count();
            }
        }

        private ObservableCollection<MediaViewModel> _recentlyAdded;
        public ObservableCollection<MediaViewModel> RecentlyAdded
        {
            get
            {
                if (_recentlyAdded == null)
                {
                    var rep = GetService<IViewModelRepository>();
                    _recentlyAdded =
                        rep.Medias
                            .OrderByDescending(m => m.DateAdded)
                            .Take(6)
                            .ToObservableCollection();
                }
                return _recentlyAdded;
            }
        }

        private ObservableCollection<LoanViewModel> _loans;
        public ObservableCollection<LoanViewModel> Loans
        {
            get
            {
                if (_loans == null)
                {
                    var rep = GetService<IViewModelRepository>();
                    _loans =
                        rep.Loans
                            .Where(loan => !loan.ReturnDate.HasValue)
                            .OrderBy(loan => loan.LoanDate)
                            .ToObservableCollection();
                }
                return _loans;
            }
        }

        #endregion

        #region Commands

        private DelegateCommand<string> _navigateCommand;
        public ICommand NavigateCommand
        {
            get
            {
                if (_navigateCommand == null)
                {
                    _navigateCommand = new DelegateCommand<string>(Navigate);
                }
                return _navigateCommand;
            }
        }

        private void Navigate(string destination)
        {
            NavigationDestination dest;
            if (Enum.TryParse(destination, out dest))
            {
                Mediator.Instance.Post(this, new NavigationMessage(dest));
            }
        }

        #endregion
    }
}
