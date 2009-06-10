using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Millionaire.Model;

namespace Millionaire.ViewModel
{
    public class FiftyFiftyViewModel : JokerViewModel
    {
        public FiftyFiftyViewModel(Joker joker, GameViewModel game)
            : base(joker, game)
        {
        }

        public override void Use(QuestionViewModel question)
        {
            base.Use(question);
            question.PlayShortSound(App.Current.GetSoundPath("FiftyFifty.wma"), new TimeSpan(0,0,3));
            foreach (int i in question.FiftyFifty)
            {
                question.SetAnswerVisible(i, false);
            }
        }
    }
}
