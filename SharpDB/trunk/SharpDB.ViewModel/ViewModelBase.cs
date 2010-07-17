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
using System.Resources;
using System.Windows;
using Developpez.Dotnet.Reflection;
using System.Configuration;

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

        protected void OnPropertyChanged<T>(Expression<Func<T>> propertySelector)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(LinqHelper.GetPropertyName(propertySelector)));
        }

        protected void OnPropertyChanged()
        {
            OnPropertyChanged(string.Empty);
        }

        #endregion

        #region Design mode

        private static readonly DependencyObject _dummyDependencyObject = new DependencyObject();
        protected bool IsInDesignMode
        {
            get
            {
                return DesignerProperties.GetIsInDesignMode(_dummyDependencyObject);
            }
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

        #region Resources related members

        protected virtual ResourceManager ResourceManager
        {
            get
            {
                if (!IsInDesignMode)
                    return GetService<ResourceManager>();
                return new DummyResourceManager();
            }
        }

        protected virtual T GetResource<T>(string name)
        {
            object resource = ResourceManager.GetObject(name);
            if (resource != null)
                return (T)resource;
            if (typeof(T) == typeof(string))
                return (T)(object)name;
            return default(T);
        }

        #endregion

        #region Settings related members

        protected virtual ApplicationSettingsBase Settings
        {
            get
            {
                return GetService<ApplicationSettingsBase>();
            }
        }

        protected virtual T GetSetting<T>(string name)
        {
            object settingValue = Settings[name];
            if (settingValue != null)
                return (T)settingValue;
            if (typeof(T) == typeof(string))
                return (T)(object)name;
            return default(T);
        }

        #endregion
    }
}
