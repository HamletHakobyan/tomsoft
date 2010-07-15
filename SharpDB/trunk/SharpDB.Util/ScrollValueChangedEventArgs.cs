using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace SharpDB.Util
{
    public class ScrollValueChangedEventArgs : EventArgs
    {
        public ScrollValueChangedEventArgs(Orientation orientation, double newValue, double oldValue, double minimum, double maximum)
        {
            this.Minimum = minimum;
            this.Maximum = maximum;
            this.OldValue = oldValue;
            this.NewValue = newValue;
            this.Orientation = orientation;
        }

        public double Minimum { get; private set; }
        public double Maximum { get; private set; }
        public double OldValue { get; private set; }
        public double NewValue { get; private set; }
        public Orientation Orientation { get; private set; }
    }
}
