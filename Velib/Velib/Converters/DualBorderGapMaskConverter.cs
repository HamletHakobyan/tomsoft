using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Velib.Converters
{
    public class DualBorderGapMaskConverter : IMultiValueConverter
    {
        // Methods
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double num4;
            Type type = typeof(double);
            if (parameter == null
                || values == null
                || values.Length != 4
                || values[0] == null
                || values[1] == null
                || values[2] == null
                || values[3] == null
                || !type.IsAssignableFrom(values[0].GetType())
                || !type.IsAssignableFrom(values[1].GetType())
                || !type.IsAssignableFrom(values[2].GetType())
                || !type.IsAssignableFrom(values[3].GetType()))
            {
                return DependencyProperty.UnsetValue;
            }
            Type c = parameter.GetType();
            if (!type.IsAssignableFrom(c) && !typeof(string).IsAssignableFrom(c))
            {
                return DependencyProperty.UnsetValue;
            }
            double pixels1 = (double)values[0];
            double pixels2 = (double)values[1];
            double width = (double)values[2];
            double height = (double)values[3];
            if ((width == 0.0) || (height == 0.0))
            {
                return null;
            }
            if (parameter is string)
            {
                num4 = double.Parse((string)parameter, NumberFormatInfo.InvariantInfo);
            }
            else
            {
                num4 = (double)parameter;
            }
            Grid visual = new Grid();
            visual.Width = width;
            visual.Height = height;
            ColumnDefinition colDefinition1 = new ColumnDefinition();
            ColumnDefinition colDefinition2 = new ColumnDefinition();
            ColumnDefinition colDefinition3 = new ColumnDefinition();
            ColumnDefinition colDefinition4 = new ColumnDefinition();
            ColumnDefinition colDefinition5 = new ColumnDefinition();
            colDefinition1.Width = new GridLength(num4);
            colDefinition2.Width = new GridLength(pixels1);
            colDefinition3.Width = new GridLength(1.0, GridUnitType.Star);
            colDefinition4.Width = new GridLength(pixels2);
            colDefinition5.Width = new GridLength(num4);
            visual.ColumnDefinitions.Add(colDefinition1);
            visual.ColumnDefinitions.Add(colDefinition2);
            visual.ColumnDefinitions.Add(colDefinition3);
            visual.ColumnDefinitions.Add(colDefinition4);
            visual.ColumnDefinitions.Add(colDefinition5);
            RowDefinition rowDefinition1 = new RowDefinition();
            RowDefinition rowDefinition2 = new RowDefinition();
            rowDefinition1.Height = new GridLength(height / 2.0);
            rowDefinition2.Height = new GridLength(1.0, GridUnitType.Star);
            visual.RowDefinitions.Add(rowDefinition1);
            visual.RowDefinitions.Add(rowDefinition2);
            Rectangle rectangle1 = new Rectangle();
            Rectangle rectangle2 = new Rectangle();
            Rectangle rectangle3 = new Rectangle();
            Rectangle rectangle4 = new Rectangle();
            Rectangle rectangle5 = new Rectangle();
            rectangle1.Fill = Brushes.Black;
            rectangle2.Fill = Brushes.Black;
            rectangle3.Fill = Brushes.Black;
            rectangle4.Fill = Brushes.Black;
            rectangle5.Fill = Brushes.Black;
            Grid.SetRowSpan(rectangle1, 2);
            Grid.SetRow(rectangle1, 0);
            Grid.SetColumn(rectangle1, 0);
            Grid.SetRow(rectangle2, 1);
            Grid.SetColumn(rectangle2, 1);
            Grid.SetRowSpan(rectangle3, 2);
            Grid.SetRow(rectangle3, 0);
            Grid.SetColumn(rectangle3, 2);
            Grid.SetRow(rectangle4, 1);
            Grid.SetColumn(rectangle4, 3);
            Grid.SetRowSpan(rectangle5, 2);
            Grid.SetRow(rectangle5, 0);
            Grid.SetColumn(rectangle5, 4);
            visual.Children.Add(rectangle1);
            visual.Children.Add(rectangle2);
            visual.Children.Add(rectangle3);
            visual.Children.Add(rectangle4);
            visual.Children.Add(rectangle5);
            return new VisualBrush(visual);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] { Binding.DoNothing };
        }
    }
}
