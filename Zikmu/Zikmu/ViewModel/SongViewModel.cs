using System;
using Developpez.Dotnet.Text;

namespace Zikmu.ViewModel
{
    public class SongViewModel : ViewModelBase
    {
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

        private string _artist;
        public string Artist
        {
            get { return _artist; }
            set
            {
                _artist = value;
                OnPropertyChanged("Artist");
            }
        }

        private Uri _uri;
        public Uri Uri
        {
            get { return _uri; }
            set
            {
                _uri = value;
                OnPropertyChanged("Uri");
            }
        }

        private string _displayFormat = "{Artist} - {Title}";
        private StringTemplate _displayTemplate;
        public string DisplayFormat
        {
            get { return _displayFormat; }
            set
            {
                _displayFormat = value;
                _displayTemplate = null;
                    
                OnPropertyChanged("DisplayFormat");
                OnPropertyChanged("DisplayText");
            }
        }

        public string DisplayText
        {
            get
            {
                if (_displayTemplate == null && _displayFormat != null)
                    _displayTemplate = new StringTemplate(_displayFormat);

                if (_displayTemplate == null)
                    return string.Empty;

                return _displayTemplate.Format(this);
            }
        }
        
    }
}
