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
using Mediatek.ViewModel;

namespace Mediatek.View
{
    /// <summary>
    /// Interaction logic for MediaContributionsView.xaml
    /// </summary>
    public partial class MediaContributionsView : UserControl
    {
        public MediaContributionsView()
        {
            InitializeComponent();
        }



        public IEnumerable<ContributionViewModel> Contributions
        {
            get { return (IEnumerable<ContributionViewModel>)GetValue(ContributionsProperty); }
            set { SetValue(ContributionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Contributions.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContributionsProperty =
            DependencyProperty.Register("Contributions", typeof(IEnumerable<ContributionViewModel>), typeof(MediaContributionsView), new UIPropertyMetadata(null));


    }
}
