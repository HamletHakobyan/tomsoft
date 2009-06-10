using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Windows.ViewModel;
using Millionaire.Model;

namespace Millionaire.ViewModel
{
    public class QuizViewModel : ViewModelBase
    {
        private Quiz _quiz;

        public QuizViewModel(Quiz quiz, GameViewModel game)
        {
            this._quiz = quiz;
            this._game = game;
            this.Slides = (from s in _quiz.Slides
                           select GetViewModel(s)).ToList();
            this.Jokers = (from j in _quiz.Jokers
                           select GetViewModel(j)).ToList();

            if (_quiz.ScoreMap != null && _quiz.ScoreMap.Length > 0)
            {
                this.ScoreMap = _quiz.ScoreMap;
            }
            else
            {
                this.ScoreMap = new int[]
                {
                    0,
                    200,
                    300,
                    500,
                    800,
                    1500,
                    3000,
                    6000,
                    12000,
                    24000,
                    48000,
                    72000,
                    100000,
                    150000,
                    300000,
                    1000000
                };
            }
        }

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

        private SlideViewModel GetViewModel(Slide s)
        {
            if (s.GetType() == typeof(Question))
            {
                return new QuestionViewModel(s as Question, _game);
            }
            else if (s.GetType() == typeof(Photo))
            {
                return new PhotoViewModel(s as Photo, _game);
            }
            else if (s.GetType() == typeof(StartPage))
            {
                return new StartPageViewModel(s as StartPage, _game);
            }
            else if (s.GetType() == typeof(SlideShow))
            {
                return new SlideShowViewModel(s as SlideShow, _game);
            }
            else
            {
                throw new ArgumentException("Type de slide inconnu", "s");
            }
        }

        private JokerViewModel GetViewModel(Joker j)
        {
            switch (j.Type)
            {
                case JokerType.FiftyFifty:
                    return new FiftyFiftyViewModel(j, _game);
                    break;
                case JokerType.PublicChoice:
                    return new PublicChoiceViewModel(j, _game);
                    break;
                case JokerType.PhoneCall:
                    return new PhoneCallViewModel(j, _game);
                    break;
                default:
                    throw new ArgumentException("Type de joker inconnu", "j");
                    break;
            }
        }

        public List<SlideViewModel> Slides { get; private set; }

        public List<JokerViewModel> Jokers { get; private set; }

        public int[] ScoreMap { get; private set; }
    }
}
