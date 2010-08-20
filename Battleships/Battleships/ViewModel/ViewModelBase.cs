using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Developpez.Dotnet.Linq;
using Battleships.Service;

namespace Battleships.ViewModel
{
    public class ViewModelBase : Developpez.Dotnet.Windows.ViewModel.ViewModelBase
    {
        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> selector)
        {
            OnPropertyChanged(LinqHelper.GetPropertyName(selector));
        }

        protected virtual TService GetService<TService>()
        {
            return ServiceLocator.Instance.GetService<TService>();
        }

        protected virtual void Navigate(object destination)
        {
            GetService<INavigationService>().Navigate(destination);
        }
    }
}
