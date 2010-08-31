using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Windows.ViewModel;
using SOFlairNotifier.Model;
using Developpez.Dotnet.Windows.Input;
using System.Windows.Input;
using System.Threading.Tasks;

namespace SOFlairNotifier.ViewModel
{
    public class FlairViewModel : ViewModelBase
    {
        public FlairViewModel()
        {
            Refresh();
        }

        #region Private data

        private SOFlair _flair;

        #endregion

        #region Properties

        public string DisplayName
        {
            get { return _flair.DisplayName; }
        }

        public int Reputation
        {
            get { return _flair.Reputation; }
        }

        public int GoldBadges
        {
            get { return GoldBadges; }
        }

        public int SilverBadges
        {
            get { return _flair.SilverBadges; }
        }

        public int BronzeBadges
        {
            get { return _flair.BronzeBadges; }
        }

        public Uri ProfileUrl
        {
            get { return _flair.ProfileUrl; }
        }

        public Uri GravatarUrl
        {
            get { return _flair.GravatarUrl; }
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged("IsRefreshing");
            }
        }

        private bool? _status;
        public bool? Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        private string _statusText;
        public string StatusText
        {
            get { return _statusText; }
            set
            {
                _statusText = value;
                OnPropertyChanged("StatusText");
            }
        }

        #endregion

        #region Commands

        private DelegateCommand _refreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                if (_refreshCommand == null)
                {
                    _refreshCommand = new DelegateCommand(Refresh);
                }
                return _refreshCommand;
            }
        }

        #endregion

        #region Public methods

        public void Refresh()
        {
            Task.Factory.StartNew(DoRefresh);
        }

        #endregion

        #region Private methods

        private void DoRefresh()
        {
            IsRefreshing = true;
            Status = null;
            StatusText = "Refreshing...";
            try
            {
                var flair = SOFlair.GetFlair(Properties.Settings.Default.UserId);
                _flair = flair;
                Status = true;
                OnPropertyChanged(null);
                StatusText = "OK";
            }
            catch (Exception ex)
            {
                Status = false;
                StatusText = "Error: " + ex.Message;
            }
            IsRefreshing = false;
        }

        #endregion
    }
}
