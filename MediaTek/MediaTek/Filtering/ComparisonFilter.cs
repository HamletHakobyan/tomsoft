using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MediaTek.Filtering
{
    public enum ComparisonOperator
    {
        Equal,
        NotEqual,
        GreaterThan,
        LessThan,
        GreaterOrEqual,
        LessOrEqual
    }

    public class ComparisonFilter : Filter
    {

        public object ComparedValue
        {
            get { return (object)GetValue(ComparedValueProperty); }
            set { SetValue(ComparedValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ComparedValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ComparedValueProperty =
            DependencyProperty.Register("ComparedValue", typeof(object), typeof(ComparisonFilter), new UIPropertyMetadata(null));

        public ComparisonOperator Operator
        {
            get { return (ComparisonOperator)GetValue(OperatorProperty); }
            set { SetValue(OperatorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Operator.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OperatorProperty =
            DependencyProperty.Register("Operator", typeof(ComparisonOperator), typeof(ComparisonFilter), new UIPropertyMetadata(ComparisonOperator.Equal));

        protected override bool EvaluateInternal(object propertyValue)
        {
            bool result = false;
            if (propertyValue == null)
            {
                result = (this.ComparedValue == null);
            }
            else if (propertyValue is IComparable)
            {
                IComparable value = propertyValue as IComparable;

                switch (this.Operator)
                {
                    case ComparisonOperator.Equal:
                        result = (value.CompareTo(this.ComparedValue) == 0);
                        break;
                    case ComparisonOperator.NotEqual:
                        result = (value.CompareTo(this.ComparedValue) != 0);
                        break;
                    case ComparisonOperator.GreaterThan:
                        result = (value.CompareTo(this.ComparedValue) > 0);
                        break;
                    case ComparisonOperator.LessThan:
                        result = (value.CompareTo(this.ComparedValue) < 0);
                        break;
                    case ComparisonOperator.GreaterOrEqual:
                        result = (value.CompareTo(this.ComparedValue) >= 0);
                        break;
                    case ComparisonOperator.LessOrEqual:
                        result = (value.CompareTo(this.ComparedValue) <= 0);
                        break;
                    default:
                        throw new ArgumentException("This is not a valid comparison operator");
                }
            }
            else
            {
                throw new NotSupportedException("The type of the property must implement IComparable");
            }

            if (this.Negate)
                return !result;
            else
                return result;
        }
    }
}
