﻿using System;
using System.ComponentModel;
using Developpez.Dotnet;
using Developpez.Dotnet.Windows.ViewModel;
using Microsoft.Practices.Unity;

namespace Mediatek.ViewModel
{
    public class MediatekViewModelBase : ViewModelBase
    {
        public override TService GetService<TService>(string name)
        {
            if (name.IsNullOrEmpty())
                return App.UnityContainer.Resolve<TService>();
            return App.UnityContainer.Resolve<TService>(name);
        }
    }

    public class MediatekViewModelBase<TModel> : ViewModelBase<TModel>
    {
        public override TService GetService<TService>(string name)
        {
            if (name.IsNullOrEmpty())
                return App.UnityContainer.Resolve<TService>();
            return App.UnityContainer.Resolve<TService>(name);
        }
    }
}
