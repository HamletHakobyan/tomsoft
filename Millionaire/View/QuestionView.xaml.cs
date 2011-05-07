using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
