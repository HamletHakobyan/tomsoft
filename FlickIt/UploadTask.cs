using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlickrNet;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media.Imaging;

namespace FlickIt
{
    public class UploadTask : INotifyPropertyChanged, ITask
    {
        private FlickrAccount _account;
        public FlickrAccount Account
        {
            get { return _account; }
            set
            {
                if (value != _account)
                {
                    _account = value;
                    OnPropertyChanged("Account");
                }
            }
        }

        #region Input image details

        protected Stream _stream = null;
        public Stream Stream
        {
            get { return _stream; }
            set
            {
                if (value != _stream)
                {
                    _stream = value;
                    OnPropertyChanged("Stream");
                    if (value != null)
                        Filename = null;
                }
            }
        }

        protected string _filename = null;
        public string Filename
        {
            get { return _filename; }
            set
            {
                if (value != _filename)
                {
                    _filename = value;
                    OnPropertyChanged("Filename");
                    if (value != null)
                        Stream = null;
                }
            }
        }

        protected string _title = null;
        public string Title
        {
            get { return _title; }
            set
            {
                if (value != _title)
                {
                    _title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        protected string _description = null;
        public string Description
        {
            get { return _description; }
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private string _tags;
        public string Tags
        {
            get { return _tags; }
            set
            {
                if (value != _tags)
                {
                    _tags = value;
                    OnPropertyChanged("Tags");
                }
            }
        }

        private bool _isPublic = true;
        public bool IsPublic
        {
            get { return _isPublic; }
            set
            {
                if (value != _isPublic)
                {
                    _isPublic = value;
                    OnPropertyChanged("IsPublic");
                }
            }
        }

        private bool _isFamily = false;
        public bool IsFamily
        {
            get { return _isFamily; }
            set
            {
                if (value != _isFamily)
                {
                    _isFamily = value;
                    OnPropertyChanged("IsFamily");
                }
            }
        }

        private bool _isFriend = false;
        public bool IsFriend
        {
            get { return _isFriend; }
            set
            {
                if (value != _isFriend)
                {
                    _isFriend = value;
                    OnPropertyChanged("IsFriend");
                }
            }
        }

        private ContentType _contentType = ContentType.None;
        public ContentType ContentType
        {
            get { return _contentType; }
            set
            {
                if (value != _contentType)
                {
                    _contentType = value;
                    OnPropertyChanged("ContentType");
                }
            }
        }

        private SafetyLevel _safetyLevel = SafetyLevel.None;
        public SafetyLevel SafetyLevel
        {
            get { return _safetyLevel; }
            set
            {
                if (value != _safetyLevel)
                {
                    _safetyLevel = value;
                    OnPropertyChanged("SafetyLevel");
                }
            }
        }

        private HiddenFromSearch _hiddenFromSearch = HiddenFromSearch.None;
        public HiddenFromSearch HiddenFromSearch
        {
            get { return _hiddenFromSearch; }
            set
            {
                if (value != _hiddenFromSearch)
                {
                    _hiddenFromSearch = value;
                    OnPropertyChanged("HiddenFromSearch");
                }
            }
        }

        #endregion

        #region Output image details

        protected PhotoInfo _photoInfo;
        public PhotoInfo PhotoInfo
        {
            get { return _photoInfo; }
            private set
            {
                if (value != _photoInfo)
                {
                    _photoInfo = value;
                    OnPropertyChanged("PhotoInfo");
                }
            }
        }

        #endregion

        #region ITask implementation

        public void Start()
        {
            session = App.Current.GetSession(Account);
            if (!session.LoggedIn)
            {
                bool? loggedIn = session.Login();
                if (loggedIn == false)
                {
                    throw new ApplicationException("Not logged in");
                }
                else if (loggedIn == null)
                {
                    return;
                }
            }

            bgw = new BackgroundWorker();
            bgw.WorkerReportsProgress = true;
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
            bgw.ProgressChanged += new ProgressChangedEventHandler(bgw_ProgressChanged);
            bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
            bgw.RunWorkerAsync();
        }

        public bool IsCancellable
        {
	        get { return false;}
        }

        public void Cancel()
        {
            throw new NotSupportedException("This task cannot be cancelled");
        }

        protected TaskStatus _status;
        public TaskStatus Status
        {
            get { return _status; }
            private set
            {
                if (value != _status)
                {
                    _status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        protected Exception _error;
        public Exception Error
        {
            get { return _error; }
            private set
            {
                if (value != _error)
                {
                    _error = value;
                    OnPropertyChanged("Error");
                }
            }
        }

        #endregion

        #region Transfer

        private BackgroundWorker bgw;
        private FlickrSession session = null;

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Error = null;
                Status = TaskStatus.Cancelled;
                PhotoInfo = null;
            }
            else if (e.Error != null)
            {
                Error = e.Error;
                Status = TaskStatus.Error;
                PhotoInfo = null;
            }
            else
            {
                Error = null;
                Status = TaskStatus.Complete;
                PhotoInfo = e.Result as PhotoInfo;
            }
        }

        void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Status = (TaskStatus)e.UserState;
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            using (Stream stream =
                    (_stream != null)
                    ? _stream
                    : new FileStream(_filename, FileMode.Open, FileAccess.Read))
            {
                string id = session.Flickr.UploadPicture(
                                    stream,
                                    _title,
                                    _description,
                                    _tags,
                                    _isPublic ? 1 : 0,
                                    _isFamily ? 1 : 0,
                                    _isFriend ? 1 : 0,
                                    _contentType,
                                    _safetyLevel,
                                    _hiddenFromSearch);
                
                e.Result = session.Flickr.PhotosGetInfo(id);
            }

        }

        #endregion

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
