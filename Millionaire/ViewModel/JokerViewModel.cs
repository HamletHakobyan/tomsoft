using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Windows.ViewModel;
using Millionaire.Model;

namespace Millionaire.ViewModel
{
    public class JokerViewModel : SlideViewModel
    {
        private Joker _joker;

        public JokerViewModel(Joker joker, GameViewModel game)
            : base(game)
        {
            this._joker = joker;
        }

        public JokerType Type
        {
            get { return _joker.Type; }
        }


        public bool IsUsed
        {
            get { return _joker.Used; }
            set
            {
                if (value != _joker.Used)
                {
                    _joker.Used = value;
                    OnPropertyChanged("IsUsed");
                }
            }
        }

        private QuestionViewModel _question;
        public QuestionViewModel Question
        {
            get { return _question; }
            private set
            {
                if (value != _question)
                {
                    _question = value;
                    OnPropertyChanged("Question");
                }
            }
        }

        public virtual void Use(QuestionViewModel question)
        {
            this.Question = question;
            IsUsed = true;
        }

    }
}
