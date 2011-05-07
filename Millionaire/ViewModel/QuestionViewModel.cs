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
    public class QuestionViewModel : SlideViewModel
    {
        private readonly Question _question;

        public QuestionViewModel(Question question, GameViewModel game)
            : base(game)
        {
            this._question = question;
            _answerSelected = new bool[] { false, false, false, false};
            _answerVisible = new bool[] { true, true, true, true};
            _correctAnswerVisible = new bool[] { false, false, false, false };
            this.Sound = App.Current.GetSoundPath("Question.wma");
        }

        #region Public properties

        public int QuestionNumber
        {
            get { return _question.Number; }
        }

        private ImageSource _photo;
        public ImageSource Photo
        {
            get
            {
                if (_photo == null && !String.IsNullOrEmpty(_question.Photo))
                {
                    if (!string.IsNullOrEmpty(_question.Quiz.ContentPath))
                    {
                        string fullPath = Path.Combine(_question.Quiz.ContentPath, _question.Photo);
                        BitmapImage bmp = new BitmapImage();
                        bmp.BeginInit();
                        bmp.UriSource = new Uri(fullPath);
                        bmp.EndInit();
                        _photo= bmp;
                    }
                }
                return _photo;
            }
        }

        public string QuestionText
        {
            get { return _question.Text; }
        }

        public string[] Answers
        {
            get { return _question.Answers; }
        }

        private readonly bool[] _answerSelected;
        public bool[] AnswerSelected
        {
            get { return _answerSelected; }
        }

        private int _showAnswers;
        private readonly bool[] _answerVisible;
        public bool[] AnswerVisible
        {
            get
            {
                return _answerVisible.Select((b, index) => b & (index < _showAnswers)).ToArray();
            }
        }

        private readonly bool[] _correctAnswerVisible;
        public bool[] CorrectAnswerVisible
        {
            get
            {
                return _correctAnswerVisible;
            }
        }

        public int[] PublicChoicePercentage
        {
            get
            {
                bool[] answerVisible = AnswerVisible;
                int[] tmp = _question.PublicChoice.Select((percentage, index) => answerVisible[index] ? percentage : 0).ToArray();
                int sum = tmp.Sum();
                return tmp.Select(p => (int)Math.Round((double)p * 100 / sum)).ToArray();
            }
        }

        public string FriendToCall
        {
            get { return _question.FriendToCall; }
        }

        public int[] FiftyFifty
        {
            get { return _question.FiftyFifty; }
        }

        #endregion

        #region Commands

        private RelayCommand _answerCommand;
        public ICommand AnswerCommand
        {
            get
            {
                if (_answerCommand == null)
                {
                    _answerCommand =
                        new RelayCommand(
                            (param) =>
                            {
                                if (_showAnswers < 4 || _validated)
                                    return;

                                int currentAnswerIndex = GetSelectedAnswer();
                                if (currentAnswerIndex >= 0)
                                {
                                    if (_validated)
                                    {
                                        bool correct = (_question.CorrectAnswer == -1) || (currentAnswerIndex == _question.CorrectAnswer);
                                        OnUnanswered(correct);
                                        _validated = false;
                                    }
                                }
                                SetCorrectAnswerVisible(-1);
                                
                                int newAnswerIndex = Convert.ToInt32(param);
                                SetSelectedAnswer(newAnswerIndex);
                            });
                }
                return _answerCommand;
            }
        }

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
                                if (_showAnswers < 4)
                                {
                                    _showAnswers++;
                                    OnPropertyChanged("AnswerVisible");
                                }
                                else if (_showAnswers == 4)
                                {
                                    if (GetSelectedAnswer() >= 0)
                                    {
                                        _showAnswers++;
                                        ValidateAnswer();
                                    }
                                }
                                else
                                {
                                    BaseNextCommand.Execute(param);
                                }
                            },
                            (param) =>
                            {
                                if (_showAnswers <= 4)
                                    return true;
                                return BaseNextCommand.CanExecute(param);
                            });
                }
                return _nextCommand;
            }
        }

        // To avoid compiler warning on using 'base' in lambra expression
        private ICommand BaseNextCommand
        {
            get { return base.NextCommand; }
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
                                if (_showAnswers > 4)
                                {
                                    _showAnswers--;
                                    int currentAnswerIndex = GetSelectedAnswer();
                                    if (_validated)
                                    {
                                        bool correct = (_question.CorrectAnswer == -1) || (currentAnswerIndex == _question.CorrectAnswer);
                                        OnUnanswered(correct);
                                        _validated = false;
                                    }
                                    SetCorrectAnswerVisible(-1);
                                }

                                if (GetSelectedAnswer() >= 0)
                                {
                                    SetSelectedAnswer(-1);
                                }
                                else if (_showAnswers > 0)
                                {
                                    _showAnswers--;
                                    OnPropertyChanged("AnswerVisible");
                                }
                                else
                                {
                                    BasePreviousCommand.Execute(param);
                                }
                            },
                            (param) =>
                            {
                                if (_showAnswers > 0)
                                    return true;
                                return BasePreviousCommand.CanExecute(param);
                            });
                }
                return _previousCommand;
            }
        }

        // To avoid compiler warning on using 'base' in lambra expression
        private ICommand BasePreviousCommand
        {
            get { return base.PreviousCommand; }
        }

        private RelayCommand _jokerCommand;
        public ICommand JokerCommand
        {
            get
            {
                if (_jokerCommand == null)
                {
                    _jokerCommand =
                        new RelayCommand(
                            (param) =>
                            {
                                if (_showAnswers < 4 || _validated)
                                    return;

                                try
                                {
                                    JokerType jokerType = (JokerType)Enum.Parse(typeof(JokerType), param.ToString());
                                    JokerViewModel jvm = Game.Jokers.FirstOrDefault(j => j.Type == jokerType && !j.IsUsed);
                                    if (jvm != null)
                                    {
                                        jvm.Use(this);
                                    }
                                }
                                catch
                                {
                                }
                            },
                            (param) =>
                            {
                                try
                                {
                                    JokerType jokerType = (JokerType)Enum.Parse(typeof(JokerType), param.ToString());
                                    return Game.Jokers.Any(j => j.Type == jokerType && !j.IsUsed);
                                }
                                catch
                                {
                                    return false;
                                }
                            });
                }
                return _jokerCommand;
            }
        }

        private ICommand _removeFiftyFiftyCommand;
        public ICommand RemoveFiftyFiftyCommand
        {
            get {
                if (_removeFiftyFiftyCommand == null)
                {
                    _removeFiftyFiftyCommand =
                        new RelayCommand(
                            (param) =>
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    SetAnswerVisible(i, true);
                                }

                                JokerViewModel joker = (from j in Game.Jokers
                                                       where j.Type == JokerType.FiftyFifty
                                                             && j.IsUsed
                                                             && j.Question == this
                                                       select j)
                                                       .FirstOrDefault();
                                if (joker != null)
                                    joker.IsUsed = false;
                            });
                }
                return _removeFiftyFiftyCommand;
            }
        }


        #endregion

        #region Public events

        public class AnswerEventArgs : EventArgs
        {
            public AnswerEventArgs(bool correctAnswer)
            {
                this.CorrectAnswer = correctAnswer;
            }

            public bool CorrectAnswer { get; private set; }
        }

        public event EventHandler<AnswerEventArgs> Answered;

        protected void OnAnswered(bool correctAnswer)
        {
            if (correctAnswer)
            {
                this.Sound = App.Current.GetSoundPath("CorrectAnswer.wma");
            }
            else
            {
                this.Sound = App.Current.GetSoundPath("WrongAnswer.mp3");
            }

            var handler = Answered;
            if (handler != null)
                handler(this, new AnswerEventArgs(correctAnswer));
        }

        public event EventHandler<AnswerEventArgs> Unanswered;

        protected void OnUnanswered(bool correctAnswer)
        {
            var handler = Unanswered;
            if (handler != null)
                handler(this, new AnswerEventArgs(correctAnswer));
        }
        #endregion

        #region Public methods

        public void SetAnswerVisible(int index, bool value)
        {
            _answerVisible[index] = value;
            OnPropertyChanged("AnswerVisible");
        }

        public void SetCorrectAnswerVisible(int index)
        {
            for (int i = 0; i < 4; i++)
            {
                if (i == index)
                    _correctAnswerVisible[i] = true;
                else
                    _correctAnswerVisible[i] = false;
            }
            OnPropertyChanged("CorrectAnswerVisible");
        }

        public int GetSelectedAnswer()
        {
            for (int i = 0; i < 4; i++)
            {
                if (_answerSelected[i])
                    return i;
            }
            return -1;
        }
        
        public void SetSelectedAnswer(int index)
        {
            for (int i = 0; i < 4; i++)
            {
                _answerSelected[i] = (i == index);
            }
            this.Sound = null;
            if (index >= 0)
            {
                this.Sound = App.Current.GetSoundPath("Selection.wma");
            }
            else
            {
                this.Sound = App.Current.GetSoundPath("Question.wma");
            }
            OnPropertyChanged("AnswerSelected");
        }

        #endregion

        private bool _validated;
        private void ValidateAnswer()
        {
            int answerIndex = GetSelectedAnswer();
            bool correct = (_question.CorrectAnswer == -1) || (answerIndex == _question.CorrectAnswer);
            if (_question.CorrectAnswer == -1)
            {
                SetCorrectAnswerVisible(answerIndex);
            }
            else
            {
                SetCorrectAnswerVisible(_question.CorrectAnswer);
            }
            OnAnswered(correct);
            _validated = true;
        }

        private DispatcherTimer _timer;
        public void PlayShortSound(Uri sound, TimeSpan duration)
        {
            if (_timer == null)
            {
                _timer = new DispatcherTimer();
                _timer.Tick += _timer_Tick;
            }
            _timer.IsEnabled = false;
            _timer.Interval = duration;
            this.Sound = sound;
            _timer.Start();
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();
            this.Sound = App.Current.GetSoundPath("Question.wma");
        }
    }
}
