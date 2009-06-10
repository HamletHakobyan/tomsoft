using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Windows.ViewModel;
using Millionaire.Model;
using Developpez.Dotnet.Windows.Input;
using System.Windows.Input;

namespace Millionaire.ViewModel
{
    public class GameViewModel : ViewModelBase
    {
        static GameViewModel()
        {
        }

        private Game _game;

        public GameViewModel(Game game)
        {
            this._game = game;
            this._quizVM = new QuizViewModel(_game.Quiz, this);
        }

        #region Public properties

        private QuizViewModel _quizVM;
        public QuizViewModel Quiz
        {
            get
            {
                return _quizVM;
            }
        }

        public int Score
        {
            get
            {
                int s = _game.Score;
                if (s < 0)
                    s = 0;
                if (s >= _quizVM.ScoreMap.Length)
                    s = _quizVM.ScoreMap.Length - 1;
                return _quizVM.ScoreMap[s];
            }
        }

        private SlideViewModel _currentSlide;
        public SlideViewModel CurrentSlide
        {
            get
            {
                return _currentSlide;
            }
            private set
            {
                if (value != _currentSlide)
                {
                    _currentSlide = value;
                    OnPropertyChanged("CurrentSlide");
                }
            }
        }

        public List<JokerViewModel> Jokers
        {
            get
            {
                return _quizVM.Jokers;
            }
        }

        #endregion

        #region Commands

        private ICommand _quitCommand;
        public ICommand QuitCommand
        {
            get
            {
                if (_quitCommand == null)
                {
                    _quitCommand =
                        new RelayCommand(
                            (param) =>
                            {
                                App.Current.Shutdown();
                            });
                }
                return _quitCommand;
            }
        }


        private ICommand _nextCommand;
        public ICommand NextCommand
        {
            get
            {
                if (_nextCommand == null)
                {
                    _nextCommand =
                        new RelayCommand(
                            (param) =>
                            {
                                NextSlide();
                            },
                            (param) =>
                            {
                                return _game.CurrentSlideIndex < _game.Quiz.Slides.Count - 1;
                            });
                }
                return _nextCommand;
            }
        }

        private ICommand _previousCommand;
        public ICommand PreviousCommand
        {
            get
            {
                if (_previousCommand == null)
                {
                    _previousCommand =
                        new RelayCommand(
                            (param) =>
                            {
                                PreviousSlide();
                            },
                            (param) =>
                            {
                                return _game.CurrentSlideIndex > 0;
                            });
                }
                return _previousCommand;
            }
        }

        #endregion

        #region Public methods

        public void NextSlide()
        {
            ChangeSlide(_game.CurrentSlideIndex + 1);
        }

        public void PreviousSlide()
        {
            ChangeSlide(_game.CurrentSlideIndex - 1);
        }

        public void ChangeSlide(SlideViewModel newSlide)
        {
            QuestionViewModel question = _currentSlide as QuestionViewModel;
            if (question != null)
            {
                question.Answered -= question_Answered;
                question.Unanswered -= question_Unanswered;
            }

            question = newSlide as QuestionViewModel;
            if (question != null)
            {
                question.Answered += question_Answered;
                question.Unanswered += question_Unanswered;
            }

            if (_currentSlide != null)
            {
                _currentSlide.Unload();
            }
            if (newSlide != null)
            {
                newSlide.Load();
            }
            CurrentSlide = newSlide;
        } 

        #endregion
        
        private SlideViewModel GetSlide(int index)
        {
            return _quizVM.Slides[index];
        }

        private void ChangeSlide(int newSlideIndex)
        {
            SlideViewModel newSlide = null;
            if (newSlideIndex >= 0 && newSlideIndex < _game.Quiz.Slides.Count)
            {
                newSlide = GetSlide(newSlideIndex);
            }
            ChangeSlide(newSlide);
            _game.CurrentSlideIndex = newSlideIndex;
        }

        void question_Answered(object sender, QuestionViewModel.AnswerEventArgs e)
        {
            if (e.CorrectAnswer)
            {
                _game.Score++;
                OnPropertyChanged("Score");
            }
        }

        void question_Unanswered(object sender, QuestionViewModel.AnswerEventArgs e)
        {
            if (e.CorrectAnswer)
            {
                _game.Score--;
                OnPropertyChanged("Score");
            }
        }

        
    }
}
