using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Millionaire.Model
{
    public class Game
    {
        private static Game _currentGame;
        public static Game Current
        {
            get { return _currentGame; }
        }

        protected Game(Quiz quiz)
        {
            this.Quiz = quiz;
            this.CurrentSlideIndex = -1;
            this.Score = 0;
        }

        public static void StartGame(Quiz quiz)
        {
            Game game = new Game(quiz);
            _currentGame = game;
        }

        public Quiz Quiz { get; private set; }
        public int CurrentSlideIndex { get; set; }
        public int Score { get; set; }
    }
}
