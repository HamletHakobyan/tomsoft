﻿using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Millionaire.View
{
    /// <summary>
    /// Interaction logic for StartPageView.xaml
    /// </summary>
    public partial class StartPageView : UserControl
    {
        public StartPageView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(this);
        }
    }
}
