using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Velib.Util
{
    public static class TreeHelperExtensions
    {
        #region Visual tree helper extensions

        public static DependencyObject GetVisualParent(this DependencyObject reference)
        {
            return VisualTreeHelper.GetParent(reference);
        }

        public static DependencyObject GetVisualChild(this DependencyObject reference, int childIndex)
        {
            return VisualTreeHelper.GetChild(reference, childIndex);
        }

        public static int GetVisualChildrenCount(this DependencyObject reference)
        {
            return VisualTreeHelper.GetChildrenCount(reference);
        }

        public static IEnumerable<DependencyObject> GetVisualChildren(this DependencyObject reference)
        {
            int nChildren = VisualTreeHelper.GetChildrenCount(reference);
            for (int i = 0; i < nChildren; i++)
            {
                yield return VisualTreeHelper.GetChild(reference, i);
            }
        }

        public static T GetVisualAncestor<T>(this DependencyObject reference) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(reference);
            while (!(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            if (parent != null)
                return (T)parent;
            else
                return null;
        }
        
        #endregion

        #region Logical tree helper extensions

        public static DependencyObject GetLogicalParent(this DependencyObject reference)
        {
            return LogicalTreeHelper.GetParent(reference);
        }

        public static IEnumerable<DependencyObject> GetLogicalChildren(this DependencyObject reference)
        {
            return LogicalTreeHelper.GetChildren(reference).Cast<DependencyObject>();
        }

        public static T GetLogicalAncestor<T>(this DependencyObject reference) where T : DependencyObject
        {
            DependencyObject parent = LogicalTreeHelper.GetParent(reference);
            while (!(parent is T))
            {
                parent = LogicalTreeHelper.GetParent(parent);
            }
            if (parent != null)
                return (T)parent;
            else
                return null;
        }

        #endregion
    }
}
