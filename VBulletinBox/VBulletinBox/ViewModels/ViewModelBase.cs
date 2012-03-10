using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Linq.Expressions;
using System.Reflection;

namespace VBulletinBox.ViewModels
{
    /// <summary>
    /// Provides common functionality for ViewModel classes
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged<T>(Expression<Func<T>> expression)
        {
            if (expression.NodeType == ExpressionType.Lambda && expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                PropertyInfo prop = (expression.Body as MemberExpression).Member as PropertyInfo;
                if (prop != null)
                {
                    OnPropertyChanged(prop.Name);
                    return;
                }
            }
            throw new ArgumentException("expression", "Not a property expression");
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }

    public abstract class ViewModelBase<T> : ViewModelBase
    {
        public abstract T Model { get; }
    }
}
