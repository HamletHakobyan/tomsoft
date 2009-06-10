using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Millionaire.ViewModel;

namespace Millionaire.View
{
    /// <summary>
    /// Interaction logic for QuestionView.xaml
    /// </summary>
    public partial class QuestionView : UserControl
    {
        public QuestionView()
        {
            InitializeComponent();
        }

        void QuestionView_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(this);
            //player.Source = App.Current.GetSoundPath("Question.wma");
        }

        private void btnDebug_Click(object sender, RoutedEventArgs e)
        {
            
        }

    }
}
