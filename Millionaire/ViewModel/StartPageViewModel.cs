using System.Linq;
using Millionaire.Model;

namespace Millionaire.ViewModel
{
    public class StartPageViewModel : SlideViewModel
    {
        private readonly StartPage _startPage;

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
