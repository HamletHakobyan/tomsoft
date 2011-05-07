using System;
using System.Linq;
using System.Windows.Input;
using Developpez.Dotnet.Windows.Input;
using Millionaire.Model;

namespace Millionaire.ViewModel
{
    public class PublicChoiceViewModel : JokerViewModel
    {
        public PublicChoiceViewModel(Joker joker, GameViewModel game)
            : base(joker, game)
        {
            this.Sound = App.Current.GetSoundPath("PublicChoice.wma");
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

        private bool _showResults;
        public bool ShowResults
        {
            get { return _showResults; }
            set
            {
                if (value != _showResults)
                {
                    _showResults = value;
                    OnPropertyChanged("ShowResults");
                }
            }
        }

        public string[] Percentage
        {
            get
            {
                if (Question != null)
                    return Question.PublicChoicePercentage.Select(p => String.Format("{0}%", p)).ToArray();
                return null;
            }
        }

        public double[] PercentageHeight
        {
            get
            {
                if (Question != null)
                    return Question.PublicChoicePercentage
                            .Select(i => _referenceHeight * i / 100).ToArray();
                return null;
            }
        }

        private double _referenceHeight;
        public double ReferenceHeight
        {
            get { return _referenceHeight; }
            set
            {
                if (value != _referenceHeight)
                {
                    _referenceHeight = value;
                    OnPropertyChanged("ReferenceHeight");
                    OnPropertyChanged("PercentageHeight");
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
                                if (ShowResults)
                                    Game.ChangeSlide(Question);
                                else
                                    ShowResults = true;
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
                                if (ShowResults)
                                    ShowResults = false;
                                else
                                    Game.ChangeSlide(Question);
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

    }
}
