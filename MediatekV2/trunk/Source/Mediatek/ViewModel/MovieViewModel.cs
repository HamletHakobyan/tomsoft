using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Developpez.Dotnet.Windows.Input;
using Mediatek.Entities;
using System.Windows.Threading;
using Mediatek.Helpers;
using System.Windows;
using Developpez.Dotnet.Windows.Service;
using Mediatek.Messaging;

namespace Mediatek.ViewModel
{
    public class MovieViewModel : MediaViewModel
    {
        public MovieViewModel(Movie movie)
            : base(movie)
        {
        }

        #region Properties

        public Movie MovieModel
        {
            get { return (Movie)Model; }
            set { Model = value; }
        }

        public int? Year
        {
            get { return MovieModel.Year; }
            set
            {
                if (value != MovieModel.Year)
                {
                    MovieModel.Year = value;
                    OnPropertyChanged("Year");
                }
            }
        }

        public IEnumerable<string> DirectorNames
        {
            get
            {
                return MovieModel.Contributions
                    .Where(r => r.RoleId == Role.DirectorRoleId)
                    .Select(r => r.Person.DisplayName);
            }
        }

        #endregion

        #region Commands

        private DelegateCommand _showMeCommand;
        public ICommand ShowMeCommand
        {
            get
            {
                if (_showMeCommand == null)
                {
                    _showMeCommand = new DelegateCommand(ShowMe);
                }
                return _showMeCommand;
            }
        }

        #endregion

        private void ShowMe()
        {
            Mediator.Instance.Post(this, new NavigationMessage(this));
        }
    }
}
