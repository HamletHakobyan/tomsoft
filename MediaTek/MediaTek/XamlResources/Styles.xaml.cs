using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using MediaTek.Utilities;
using System.Windows.Controls;

namespace MediaTek.XamlResources
{
    public partial class Styles
    {
        void stlTextField_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidationService.Validate(sender as DependencyObject);
        }

        void stlComboField_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidationService.Validate(sender as DependencyObject);
        }

        void stlDateField_ValueChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            ValidationService.Validate(sender as DependencyObject);
        }
    }
}
