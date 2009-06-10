using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Millionaire.Model;

namespace Millionaire.ViewModel
{
    public class StartPageViewModel : SlideViewModel
    {
        private StartPage _startPage;

        public StartPageViewModel(StartPage startPage, GameViewModel game)
            : base(game)
        {
            this._startPage = startPage;
            this.Sound = App.Current.GetSoundPath("QVGDM.mp3");
        }

        public string Place
        {
            get { return _startPage.Place; }
        }

        public string Date
        {
            get { return _startPage.Date; }
        }

        public string Footer
        {
            get { return _startPage.Footer; }
        }
    }
}
