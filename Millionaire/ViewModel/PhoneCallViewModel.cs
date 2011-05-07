using System.Linq;
using System.Windows.Input;
using Developpez.Dotnet.Windows.Input;
using Millionaire.Model;

namespace Millionaire.ViewModel
{
    public class PhoneCallViewModel : JokerViewModel
    {
        public PhoneCallViewModel(Joker joker, GameViewModel game)
            : base(joker, game)
        {
            this.Sound = App.Current.GetSoundPath("PhoneCall.wma");
        }

        #region Public properties

        public string QuestionText
        {
            get
            {
                if (Question != null)
                    return Question.QuestionText;
                return null;
            }
        }

        public string[] Answers
        {
            get
            {
                if (Question != null)
                    return Question.Answers;
                return null;
            }
        }

        public bool[] AnswerVisible
        {
            get
            {
                if (Question != null)
                    return Question.AnswerVisible;
                return null;
            }
        }

        public string FriendToCall
        {
            get
            {
                if (Question != null)
                    return Question.FriendToCall;
                return null;
            }
        }

        private CallState _state = CallState.Ringing;
        public CallState State
        {
            get { return _state; }
            set
            {
                if (value != _state)
                {
                    _state = value;
                    switch (_state)
                    {
                        case CallState.Ringing:
                            this.Sound = App.Current.GetSoundPath("PhoneCall.wma");
                            break;
                        case CallState.Talking:
                            this.Sound = null;
                            break;
                        case CallState.Countdown:
                            this.Sound = App.Current.GetSoundPath("Countdown.mp3");
                            break;
                        case CallState.TimesUp:
                            this.Sound = App.Current.GetSoundPath("TimesUp.mp3");
                            break;
                        default:
                            break;
                    }
                    OnPropertyChanged("State");
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
                                switch (State)
                                {
                                    case CallState.Ringing:
                                        State = CallState.Talking;
                                        break;
                                    case CallState.Talking:
                                        State = CallState.Countdown;
                                        break;
                                    case CallState.Countdown:
                                        State = CallState.TimesUp;
                                        break;
                                    case CallState.TimesUp:
                                        Game.ChangeSlide(Question);
                                        break;
                                    default:
                                        break;
                                }
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
                                switch (State)
                                {
                                    case CallState.Ringing:
                                        Game.ChangeSlide(Question);
                                        break;
                                    case CallState.Talking:
                                        State = CallState.Ringing;
                                        break;
                                    case CallState.Countdown:
                                        State = CallState.Talking;
                                        break;
                                    case CallState.TimesUp:
                                        State = CallState.Countdown;
                                        break;
                                    default:
                                        break;
                                }
                            });
                }
                return _previousCommand;
            }
        }

        #endregion

        #region Public methods

        public override void Use(QuestionViewModel question)
        {
            base.Use(question);
            Game.ChangeSlide(this);
        }

        #endregion

        public enum CallState
        {
            Ringing,
            Talking,
            Countdown,
            TimesUp
        }
    }
}
