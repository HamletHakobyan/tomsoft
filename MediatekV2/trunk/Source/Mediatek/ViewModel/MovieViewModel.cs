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
using Mediatek.Service;
using Mediatek.ViewModel.Editors;

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

        protected override IDialogViewModel GetEditor()
        {
            return new MovieEditorViewModel(this);
        }
    }
}
