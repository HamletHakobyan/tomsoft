using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Developpez.Dotnet;
using Developpez.Dotnet.Text;
using Developpez.Dotnet.Windows.Input;
using Zikmu.Behaviors;
using Zikmu.Infrastructure;

namespace Zikmu.ViewModel
{
    public class MainWindowViewModel : ViewModelBase, IMediaController
    {
        private readonly DispatcherTimer _positionRefreshTimer;

        public MainWindowViewModel()
        {
            InitCommands();
            _positionRefreshTimer = new DispatcherTimer();
            _positionRefreshTimer.Interval = TimeSpan.FromMilliseconds(100);
            _positionRefreshTimer.Tick += _positionRefreshTimer_Tick;
            _positionRefreshTimer.Start();

            CurrentSong = new SongViewModel
            {
                Uri = new Uri(@"D:\Mp3\MUSIC\Divers\A-B\Brad Mehldau - Paranoid Android.mp3"),
                Title = "Paranoid Android",
                Artist = "Brad Mehldau"
            };
            OpenMedia(CurrentSong.Uri.AbsolutePath);
        }

        #region Public properties

        private SongViewModel _currentSong;
        public SongViewModel CurrentSong
        {
            get { return _currentSong; }
            set
            {
                _currentSong = value;
                OnPropertyChanged("CurrentSong");
            }
        }

        public double DisplayVolume
        {
            get { return Volume * 100; }
            set
            {
                Volume = value / 100;
                OnPropertyChanged("DisplayVolume");
            }
        }

        private TimeSpan _currentDuration;
        public TimeSpan CurrentDuration
        {
            get { return _currentDuration; }
            set
            {
                _currentDuration = value;
                OnPropertyChanged("CurrentDuration");
            }
        }

        private TimeSpan _currentPosition;
        public TimeSpan CurrentPosition
        {
            get { return _currentPosition; }
            set
            {
                _currentPosition = value;
                NotifyPositionChanged();
            }
        }

        public double CurrentPositionUI
        {
            get { return _currentPosition.TotalSeconds; }
            set
            {
                Seek(TimeSpan.FromSeconds(value));
                NotifyPositionChanged();
            }
        }

        public string CurrentPositionText
        {
            get
            {
                var pos = _currentPosition;
                string minutes = ((int) pos.TotalMinutes).ToString("D2");
                string seconds = pos.Seconds.ToString("D2");
                return string.Format("{0}:{1}", minutes, seconds);
            }
        }


        #endregion

        #region Commands

        public ICommand CloseCommand { get; set; }

        public ICommand PlayCommand { get; private set; }
        public ICommand PauseCommand { get; private set; }
        public ICommand StopCommand { get; private set; }
        public ICommand NextTrackCommand { get; private set; }
        public ICommand PreviousTrackCommand { get; private set; }

        //private DelegateCommand _playCommand;
        //public ICommand PlayCommand
        //{
        //    get
        //    {
        //        if (_playCommand == null)
        //        {
        //            _playCommand = new DelegateCommand(Play);
        //        }
        //        return _playCommand;
        //    }
        //}

        //private DelegateCommand _pauseCommand;
        //public ICommand PauseCommand
        //{
        //    get
        //    {
        //        if (_pauseCommand == null)
        //        {
        //            _pauseCommand = new DelegateCommand(Pause);
        //        }
        //        return _pauseCommand;
        //    }
        //}

        //private DelegateCommand _stopCommand;
        //public ICommand StopCommand
        //{
        //    get
        //    {
        //        if (_stopCommand == null)
        //        {
        //            _stopCommand = new DelegateCommand(Stop);
        //        }
        //        return _stopCommand;
        //    }
        //}

        //private DelegateCommand _nextTrackCommand;
        //public ICommand NextTrackCommand
        //{
        //    get
        //    {
        //        if (_nextTrackCommand == null)
        //        {
        //            _nextTrackCommand = new DelegateCommand(NextTrack);
        //        }
        //        return _nextTrackCommand;
        //    }
        //}

        //private DelegateCommand _previousTrackCommand;
        //public ICommand PreviousTrackCommand
        //{
        //    get
        //    {
        //        if (_previousTrackCommand == null)
        //        {
        //            _previousTrackCommand = new DelegateCommand(PreviousTrack);
        //        }
        //        return _previousTrackCommand;
        //    }
        //}

        [Command]
        private void Close()
        {
            Application.Current.Shutdown();
        }

        [Command]
        private void Play()
        {
            PlayRequested.Raise(this);
        }

        [Command("PauseCommand")]
        private void Pause()
        {
            PauseRequested.Raise(this);
        }

        [Command]
        private void Stop()
        {
            StopRequested.Raise(this);
        }

        [Command]
        private void NextTrack()
        {
        }

        [Command]
        private void PreviousTrack()
        {
        }

        #endregion

        #region Private methods

        private void OpenMedia(string fileName)
        {
            var args = new MediaOpenEventArgs(new Uri(fileName));
            OpenMediaRequested.Raise(this, args);
        }

        private TimeSpan GetDuration()
        {
            var args = new MediaQueryDurationEventArgs();
            QueryDuration.Raise(this, args);
            return args.Duration;
        }

        private TimeSpan GetPosition()
        {
            var args = new MediaQueryDurationEventArgs();
            QueryPosition.Raise(this, args);
            return args.Duration;
        }

        private void NotifyPositionChanged()
        {
            OnPropertyChanged("CurrentPosition");
            OnPropertyChanged("CurrentPositionUI");
            OnPropertyChanged("CurrentPositionText");
        }

        private void Seek(TimeSpan offset)
        {
            var args = new MediaSeekEventArgs(SeekOrigin.Begin, offset);
            SeekRequested.Raise(this, args);
        }

        void _positionRefreshTimer_Tick(object sender, EventArgs e)
        {
            _currentPosition = GetPosition();
            NotifyPositionChanged();
        }

        #endregion

        #region Implementation of IMediaController

        public event EventHandler PlayRequested;
        public event EventHandler PauseRequested;
        public event EventHandler StopRequested;
        public event EventHandler CloseMediaRequested;
        public event EventHandler<MediaOpenEventArgs> OpenMediaRequested;
        public event EventHandler<MediaSeekEventArgs> SeekRequested;
        public event EventHandler<MediaQueryDurationEventArgs> QueryPosition;
        public event EventHandler<MediaQueryDurationEventArgs> QueryDuration;
        
        public void OnMediaOpened()
        {
            CurrentDuration = GetDuration();
        }

        public void OnMediaEnded()
        {
            // TODO : Next
        }

        public void OnMediaFailed(Exception exception)
        {
            // TODO : MessageBox via DialogService
            MessageBox.Show(exception.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private double _volume = 1.0;
        public double Volume
        {
            get { return _volume; }
            private set
            {
                _volume = value;
                OnPropertyChanged("Volume");
            }
        }

        private double _balance;
        public double Balance
        {
            get { return _balance; }
            private set
            {
                _balance = value;
                OnPropertyChanged("Balance");
            }
        }

        private bool _isMuted;
        public bool IsMuted
        {
            get { return _isMuted; }
            set
            {
                _isMuted = value;
                OnPropertyChanged("IsMuted");
            }
        }

        #endregion
    }
}
