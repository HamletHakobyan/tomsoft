using System.Linq;
using System.Windows;

namespace Millionaire.Util
{
    public class QuizBindingHelper : DependencyObject
    {


        public static double GetPollPercentage(DependencyObject obj)
        {
            return (double)obj.GetValue(PollPercentageProperty);
        }

        public static void SetPollPercentage(DependencyObject obj, double value)
        {
            obj.SetValue(PollPercentageProperty, value);
        }

        // Using a DependencyProperty as the backing store for PollPercentage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PollPercentageProperty =
            DependencyProperty.RegisterAttached("PollPercentage", typeof(double), typeof(QuizBindingHelper), new UIPropertyMetadata(0d));

    
    }
}
