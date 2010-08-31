using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Windows.ViewModel;

namespace SOFlairNotifier.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            _config = new ConfigViewModel();
            _flair = new FlairViewModel();
        }

        private ConfigViewModel _config;
        public ConfigViewModel Config
        {
            get { return _config; }
            set
            {
                _config = value;
                OnPropertyChanged("Config");
            }
        }

        private FlairViewModel _flair;
        public FlairViewModel Flair
        {
            get { return _flair; }
            set
            {
                _flair = value;
                OnPropertyChanged("Flair");
            }
        }
    }
}
