using System;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Developpez.Dotnet.Windows.Input;
using Mediatek.Service;
using Developpez.Dotnet.Windows.Service;
using Mediatek.Messaging;

namespace Mediatek.ViewModel
{
    public class MoviesViewModel : MediatekViewModelBase
    {
        public MoviesViewModel()
        {
            var rep = GetService<IViewModelRepository>();
            this.Movies = new CollectionView(rep.Medias)
                              {
                                  Filter = o => o is MovieViewModel
                              };
        }

        #region Properties
        
        public ICollectionView Movies { get; private set; }

        #endregion

        #region Commands

        private DelegateCommand<MovieViewModel> _showMovieCommand;
        public ICommand ShowMovieCommand
        {
            get
            {
                if (_showMovieCommand == null)
                {
                    _showMovieCommand = new DelegateCommand<MovieViewModel>(ShowMovie);
                }
                return _showMovieCommand;
            }
        }

        #endregion

        #region Private methods

        private void ShowMovie(MovieViewModel movie)
        {
            Mediator.Instance.Post(this, new NavigationMessage(movie));
        }

        #endregion
    }
}
