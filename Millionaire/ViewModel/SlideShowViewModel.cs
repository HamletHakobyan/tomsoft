using System;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Developpez.Dotnet.Windows.Input;
using Millionaire.Model;

namespace Millionaire.ViewModel
{
    public class SlideShowViewModel : SlideViewModel
    {
        private readonly SlideShow _slideShow;

        public SlideShowViewModel(SlideShow slideShow, GameViewModel game)
            : base(game)
        {
            this._slideShow = slideShow;
            if (!String.IsNullOrEmpty(_slideShow.SoundPath))
            {
                if (!String.IsNullOrEmpty(_slideShow.Quiz.ContentPath))
                {
                    string fullPath = Path.Combine(_slideShow.Quiz.ContentPath, _slideShow.SoundPath);
                    this.Sound = new Uri(fullPath);
                }
            }
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(_slideShow.Interval);
            _timer.Tick += _timer_Tick;
        }

        private readonly DispatcherTimer _timer;
        void _timer_Tick(object sender, EventArgs e)
        {
            if (_currentIndex + 1 < _slideShow.Photos.Count)
            {
                _currentIndex++;
                OnPropertyChanged("CurrentPhoto");
            }
            else
                _timer.Stop();
        }

        #region Public properties

        private int _currentIndex;
        public ImageSource CurrentPhoto
        {
            get
            {
                if (_currentIndex >= 0 && _currentIndex < _slideShow.Photos.Count)
                {
                    string photoPath = _slideShow.Photos[_currentIndex];
                    if (!String.IsNullOrEmpty(_slideShow.Quiz.ContentPath))
                    {
                        string fullPath = Path.Combine(_slideShow.Quiz.ContentPath, photoPath);
                        BitmapImage bmp = new BitmapImage();
                        bmp.BeginInit();
                        bmp.UriSource = new Uri(fullPath);
                        bmp.EndInit();
                        return bmp;
                    }
                }
                return null;
            }
        }

        #endregion

        #region Commands

        private RelayCommand _nextCommand;
        public override ICommand NextCommand
        {
            get
            {
                if (_nextCommand == null)
                {
                    _nextCommand =
                        new RelayCommand(
                            (param) =>
                            {
                                if (_currentIndex + 1 < _slideShow.Photos.Count)
                                {
                                    _currentIndex++;
                                    OnPropertyChanged("CurrentPhoto");
                                }
                                else
                                    Game.NextSlide();
                            });
                }
                return _nextCommand;
            }
        }

        private RelayCommand _previousCommand;
        public override ICommand PreviousCommand
        {
            get
            {
                if (_previousCommand == null)
                {
                    _previousCommand =
                        new RelayCommand(
                            (param) =>
                            {
                                if (_currentIndex > 0)
                                {
                                    _currentIndex--;
                                    OnPropertyChanged("CurrentPhoto");
                                }
                                else
                                    Game.PreviousSlide();
                            });
                }
                return _previousCommand;
            }
        }

        #endregion

        #region Public methods

        public override void Load()
        {
            base.Load();
            _timer.Start();
        }

        public override void Unload()
        {
            base.Unload();
            _timer.Stop();
        }

        #endregion
    }
}
