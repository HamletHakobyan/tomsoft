using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MediaTek.Filtering
{
    public enum StringComparisonOperator
    {
        Equals,
        Contains,
        StartsWith,
        EndsWith
    }
    
    public class StringFilter : Filter
    {
        public bool CaseSensitive
        {
            get { return (bool)GetValue(CaseSensitiveProperty); }
            set { SetValue(CaseSensitiveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CaseSensitive.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CaseSensitiveProperty =
            DependencyProperty.Register("CaseSensitive", typeof(bool), typeof(StringFilter), new UIPropertyMetadata(true));



        public StringComparisonOperator Operator
        {
            get { return (StringComparisonOperator)GetValue(OperatorProperty); }
            set { SetValue(OperatorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Operator.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OperatorProperty =
            DependencyProperty.Register("Operator", typeof(StringComparisonOperator), typeof(StringFilter), new UIPropertyMetadata(StringComparisonOperator.Equals));


        public string ComparedValue
        {
            get { return (string)GetValue(ComparedValueProperty); }
            set { SetValue(ComparedValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ComparedValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ComparedValueProperty =
            DependencyProperty.Register("ComparedValue", typeof(string), typeof(StringFilter), new UIPropertyMetadata(null));

        protected override bool EvaluateInternal(object propertyValue)
        {
            bool result = false;
            if (propertyValue == null)
            {
                result = (this.ComparedValue == null);
            }
            else if (propertyValue is string)
            {
                string s = propertyValue as string;
                string comp = this.ComparedValue;
                if (this.ComparedValue != null && !CaseSensitive)
                {
                    comp = comp.ToLower();
                }
                switch (this.Operator)
                {
                    case StringComparisonOperator.Equals:
                        result = s.Equals(comp);
                        break;
                    case StringComparisonOperator.Contains:
                        result = s.Contains(comp);
                        break;
                    case StringComparisonOperator.StartsWith:
                        result = s.StartsWith(comp);
                        break;
                    case StringComparisonOperator.EndsWith:
                        result = s.EndsWith(comp);
                        break;
                    default:
                        throw new ArgumentException("This is not a valid string operator");
                }
            }
            else
            {
                throw new ArgumentException("This is not a string");
            }
            if (this.Negate)
                return !result;
            else
                return result;
        }
    }
}
