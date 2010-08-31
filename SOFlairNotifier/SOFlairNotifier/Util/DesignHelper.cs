using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;

namespace SOFlairNotifier.Util
{
    static class DesignHelper
    {
        private static DependencyObject _dummy = new DependencyObject();

        public static bool DesignMode
        {
            get
            {
                return DesignerProperties.GetIsInDesignMode(_dummy);
            }
        }
    }
}
