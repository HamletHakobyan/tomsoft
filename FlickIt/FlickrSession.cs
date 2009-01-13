using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlickrNet;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.IO;

namespace FlickIt
{
    public delegate bool? UserAuthenticationCallback(string flickrUrl);

    public class FlickrSession : INotifyPropertyChanged
    {
        public FlickrSession(FlickrAccount account, UserAuthenticationCallback callback)
        {
            this.Account = account;
            this.UserAuthenticationCallBack = callback;
        }

        public FlickrAccount Account { get; set; }

        private SessionStatus _status;
        public SessionStatus Status
        {
            get { return _status; }
            set
            {
                if (value != _status)
                {
                    _status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        private bool _loggedIn;
        public bool LoggedIn
        {
            get { return _loggedIn; }
            set
            {
                if (value != _loggedIn)
                {
                    _loggedIn = value;
                    OnPropertyChanged("LoggedIn");
                }
            }
        }

        private FoundUser _user;
        public FoundUser User
        {
            get { return _user; }
            set
            {
                if (value != _user)
                {
                    _user = value;
                    OnPropertyChanged("User");
                }
            }
        }

        private AuthLevel _permissions;
        public AuthLevel Permissions
        {
            get { return _permissions; }
            set
            {
                if (value != _permissions)
                {
                    _permissions = value;
                    OnPropertyChanged("Permissions");
                }
            }
        }

        public UserAuthenticationCallback UserAuthenticationCallBack { get; set; }

        private string _errorText;
        public string ErrorText
        {
            get { return _errorText; }
            private set
            {
                if (value != _errorText)
                {
                    _errorText = value;
                    OnPropertyChanged("ErrorText");
                }
            }
        }

        private Flickr flickr = null;
        public Flickr Flickr
        {
            get { return flickr; }
        }


        private Auth auth = null;

        public bool? Login()
        {
            string apikey = Account.ApiKey;
            string secret = Account.ApiSecret;
            string token = null;
            if (auth != null)
                token = auth.Token;
            if (token == null)
                token = Account.LastApiToken;

            if (flickr == null)
                flickr = new Flickr(apikey, secret);

            if (!string.IsNullOrEmpty(token))
                auth = flickr.AuthCheckToken(token);

            if (auth == null)
            {
                string frob = flickr.AuthGetFrob();
                string flickrUrl = flickr.AuthCalcUrl(frob, AuthLevel.Write);

                if (UserAuthenticationCallBack(flickrUrl) == true)
                {
                    auth = flickr.AuthGetToken(frob);
                    if (auth != null)
                        token = auth.Token;
                }
                else
                {
                    SetLoggedIn(null, SessionStatus.None, null);
                    return null;
                }
            }

            if (string.IsNullOrEmpty(token))
            {
                return SetLoggedIn(null, SessionStatus.Error, "Authentication failed");
            }

            Account.LastApiToken = token;
            flickr.AuthToken = token;

            return SetLoggedIn(auth, SessionStatus.LoggedIn, null);
        }

        public void Logout()
        {
            auth = null;
            flickr = null;
            SetLoggedIn(null, SessionStatus.None, null);
        }

        private bool SetLoggedIn(Auth auth, SessionStatus status, string errorText)
        {
            Status = status;
            ErrorText = errorText;

            if (auth == null)
            {
                LoggedIn = false;
                Permissions = AuthLevel.None;
                User = null;
                return false;
            }
            else
            {
                User = auth.User;
                Permissions = auth.Permissions;
                LoggedIn = true;
                return true;
            }
        }

        public void SendImage(BitmapSource imageSource)
        {

        }

        #region INotifyPropertyChanged implementation

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
