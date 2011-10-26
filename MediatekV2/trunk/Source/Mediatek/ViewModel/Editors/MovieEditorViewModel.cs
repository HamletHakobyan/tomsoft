using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Windows.Service;
using Mediatek.Entities;
using Mediatek.Helpers;
using Mediatek.Messaging;
using Mediatek.Service;

namespace Mediatek.ViewModel.Editors
{
    public class MovieEditorViewModel : EditorViewModelBase
    {
        private readonly MovieViewModel _movie;

        public MovieEditorViewModel(MovieViewModel movie)
        {
            _movie = movie;
            this.DialogTitle = Properties.Resources.edit_movie;
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        private string _originalTitle;
        public string OriginalTitle
        {
            get { return _originalTitle; }
            set
            {
                _originalTitle = value;
                OnPropertyChanged("OriginalTitle");
            }
        }

        private int? _year;
        public int? Year
        {
            get { return _year; }
            set
            {
                _year = value;
                OnPropertyChanged("Year");
            }
        }

        private PersonViewModel _director;
        public PersonViewModel Director
        {
            get { return _director; }
            set
            {
                _director = value;
                OnPropertyChanged("Director");
            }
        }

        public ICollection<PersonViewModel> Persons { get; private set; }

        protected override void Load()
        {
            base.Load();
            _title = _movie.Title;
            _originalTitle = _movie.OriginalTitle;
            _year = _movie.Year;
            var rep = GetService<IViewModelRepository>();
            var directorContrib =
                _movie.Contributions
                    .Where(c => c.Model.RoleId == Role.DirectorRoleId)
                    .FirstOrDefault();
            if (directorContrib != null)
                _director = rep.Persons.FirstOrDefault(d => d.Id == directorContrib.Model.PersonId);
            Persons = rep.Persons;
        }

        protected override void Save()
        {
            base.Save();
            _movie.Title = _title;
            _movie.OriginalTitle = _originalTitle;
            _movie.Year = _year;
            var directorContrib =
                _movie.Contributions
                    .Where(c => c.Model.RoleId == Role.DirectorRoleId)
                    .FirstOrDefault();
            if (directorContrib != null && _director != null)
                directorContrib.Model.Person = _director.Model;
        }

    }
}
