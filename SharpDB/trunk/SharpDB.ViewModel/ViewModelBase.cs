using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Developpez.Dotnet;
using System.Linq.Expressions;
using Developpez.Dotnet.Linq;
using System.Diagnostics;
using SharpDB.Util;

namespace SharpDB.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
#if DEBUG
            if (this.GetType().GetProperty(propertyName) == null)
            {
                Debug.Print("[OnPropertyChanged] No such property : {0}", propertyName);
            }
#endif
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertySelector)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(LinqHelper.GetPropertyName(propertySelector)));
        }

        #endregion

        #region GetService methods

        protected virtual object GetService(Type serviceType, string name)
        {
            return ServiceLocator.Instance.GetService(serviceType, name);
        }

        protected object GetService(Type serviceType)
        {
            return GetService(serviceType, string.Empty);
        }

        protected T GetService<T>(string name)
        {
            return (T)GetService(typeof(T), name);
        }

        protected T GetService<T>()
        {
            return (T)GetService(typeof(T), string.Empty);
        }

        #endregion
    }
}
