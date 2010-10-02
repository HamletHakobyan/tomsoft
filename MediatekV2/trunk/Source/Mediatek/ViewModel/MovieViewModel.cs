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
    }
}
