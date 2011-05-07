using System;
using System.Linq;
using System.Windows.Input;
using Developpez.Dotnet.Windows.ViewModel;

namespace Millionaire.ViewModel
{
    public abstract class SlideViewModel : ViewModelBase
    {
        protected SlideViewModel(GameViewModel game)
        {
            this._game = game;
        }

        #region Public properties

        private GameViewModel _game;
        public GameViewModel Game
        {
            get { return _game; }
            set
            {
                if (value != _game)
                {
                    _game = value;
                    OnPropertyChanged("Game");
                }
            }
        }

        private Uri _sound;
        public Uri Sound
        {
            get { return _sound; }
            set
            {
                if (value != _sound)
                {
                    _sound = value;
                    OnPropertyChanged("Sound");
                }
            }
        }


        #endregion

        #region Commands

        public virtual ICommand NextCommand
        {
            get
            {
                return _game.NextCommand;
            }
        }

        public virtual ICommand PreviousCommand
        {
            get
            {
                return _game.PreviousCommand;
            }
        }


        #endregion

        #region Public methods

        public virtual void Load()
        {
        }

        public virtual void Unload()
        {
        }

        #endregion
    }
}
