using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Millionaire.ViewModel;

namespace Millionaire.View
{
    /// <summary>
    /// Interaction logic for ScoreScreenView.xaml
    /// </summary>
    public partial class ScorePageView : UserControl
    {
        public ScorePageView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(this);
        }
        
        private ScorePageViewModel _vm;
        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (_vm != null)
            {
                _vm.PropertyChanged -= _vm_PropertyChanged;
            }
            _vm = DataContext as ScorePageViewModel;
            if (_vm != null)
            {
                _vm.PropertyChanged += _vm_PropertyChanged;
            }
        }

        void _vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ShowScore")
            {
                if (_vm.ShowScore)
                {
                    double endPos = 651.0 - GetScorePosition() * (651.0 - 171.0) / (QuizViewModel.DefaultScoreMap.Length - 1);
                    DoubleAnimation anim = new DoubleAnimation();
                    anim.From = 651;
                    anim.To = endPos;
                    anim.Duration = new Duration(TimeSpan.FromSeconds(2.5));
                    imgSelector.BeginAnimation(Canvas.TopProperty, anim);
                }
                else
                {
                    Canvas.SetTop(imgSelector, 651);
                }
            }
        }

        private double GetScorePosition()
        {
            int[] scoreMap = QuizViewModel.DefaultScoreMap;
            int score = _vm.Game.Score;
            int index = 0;
            for (int i = 0; i < scoreMap.Length; i++)
            {
                if (scoreMap[i] == score)
                {
                    index = i;
                    break;
                }
                if (scoreMap[i] < score)
                {
                    if (i + 1 < scoreMap.Length)
                    {
                        if (scoreMap[i + 1] > score)
                        {
                            if (scoreMap[i + 1] - score < scoreMap[i] - score)
                                index = i + 1;
                            else
                                index = i;
                            break;
                        }
                    }
                }
            }
            if (index == 0)
                index = scoreMap.Length - 1;

            return index;
        }
    }
}
