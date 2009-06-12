using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Millionaire.Model;
using System.Windows.Input;
using Developpez.Dotnet.Windows.Input;

namespace Millionaire.ViewModel
{
    public class ScorePageViewModel : SlideViewModel
    {
        private ScorePage _scorePage;
        
        public ScorePageViewModel(ScorePage scorePage, GameViewModel game)
            : base(game)
        {
            this._scorePage = scorePage;
            if (!String.IsNullOrEmpty(_scorePage.SoundPath))
                this.Sound = App.Current.GetSoundPath(_scorePage.SoundPath);
            else
                this.Sound = null;

        }

        #region Public properties

        private bool _showScore;
        public bool ShowScore
        {
            get { return _showScore; }
            set
            {
                if (value != _showScore)
                {
                    _showScore = value;
                    OnPropertyChanged("ShowScore");
                }
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
                                if (ShowScore)
                                    Game.NextCommand.Execute(param);
                                else
                                    ShowScore = true;
                            },
                            (param) =>
                            {
                                if (ShowScore)
                                    return Game.NextCommand.CanExecute(param);
                                else
                                    return true;
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
                                if (ShowScore)
                                    ShowScore = false;
                                else
                                    Game.PreviousCommand.Execute(param);
                            },
                            (param) =>
                            {
                                if (ShowScore)
                                    return true;
                                else
                                    return Game.PreviousCommand.CanExecute(param);
                            });
                }
                return _previousCommand;
            }
        }

        #endregion
    }
}
