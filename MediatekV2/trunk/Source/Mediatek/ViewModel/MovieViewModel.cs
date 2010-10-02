using System.Collections.Generic;
using System.Linq;
using Mediatek.Entities;
using System.Windows.Threading;
using Mediatek.Helpers;
using System.Windows;

namespace Mediatek.ViewModel
{
    public class MovieViewModel : MediaViewModel
    {
        public MovieViewModel(Movie movie)
            : base(movie)
        {
        }

        public Movie MovieModel
        {
            get { return (Movie)Model; }
            set { Model = value; }
        }

        public string Title
        {
            get { return MovieModel.Title; }
            set
            {
                if (value != MovieModel.Title)
                {
                    MovieModel.Title = value;
                    OnPropertyChanged("Title");
                }
            }
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

        private string[] _directorNames;
        public IEnumerable<string> DirectorNames
        {
            get
            {
                if (_directorNames == null)
                {
                    Application.Current.Dispatcher.BeginInvoke(
                        () =>
                            {
                                var query = from c in MovieModel.Contributions
                                            where c.RoleId == Role.DirectorRoleId
                                            select c.LoadProperty(cc => cc.Person).Person.DisplayName;
                                _directorNames = query.ToArray();
                                OnPropertyChanged("DirectorNames");
                            },
                        DispatcherPriority.Background);
                }

                return _directorNames;
            }
        }
    }
}
