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

        protected override void Load()
        {
            base.Load();
            _title = _movie.Title;
            _originalTitle = _movie.OriginalTitle;
            _year = _movie.Year;
        }

        protected override void Save()
        {
            base.Save();
            _movie.Title = _title;
            _movie.OriginalTitle = _originalTitle;
            _movie.Year = _year;
        }

    }
}
