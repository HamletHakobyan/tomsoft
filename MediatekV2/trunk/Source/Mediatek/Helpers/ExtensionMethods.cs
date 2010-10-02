using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mediatek.Helpers
{
    static class ExtensionMethods
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            return new ObservableCollection<T>(source);
        }
    }
}
