using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Windows.Input;
using System.Windows.Input;

namespace Battleships.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        #region Properties

        

        #endregion

        #region Commands

        private DelegateCommand _newGameCommand;
        public ICommand NewGameCommand
        {
            get
            {
                if (_newGameCommand == null)
                {
                    _newGameCommand = new DelegateCommand(NewGame);
                }
                return _newGameCommand;
            }
        }

        #endregion

        #region Public methods

        public void NewGame()
        {
            Navigate(new GameViewModel());
        }

        #endregion
    }
}
