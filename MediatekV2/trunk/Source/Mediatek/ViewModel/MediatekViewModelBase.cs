using System;
using System.ComponentModel;
using Developpez.Dotnet;
using Developpez.Dotnet.Windows.ViewModel;

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

    public class MediatekViewModelBase<TModel> : ViewModelBase<TModel> //, IEditableObject
    {
        public override TService GetService<TService>(string name)
        {
            if (name.IsNullOrEmpty())
                return App.UnityContainer.Resolve<TService>();
            return App.UnityContainer.Resolve<TService>(name);
        }

        //#region Implementation of IEditableObject

        //private bool _isInEditMode;
        //public bool IsInEditMode
        //{
        //    get { return _isInEditMode; }
        //}

        //public void BeginEdit()
        //{
        //    if (_isInEditMode)
        //        throw new InvalidOperationException("The object is already being edited.");
        //    BeginEditCore();
        //    _isInEditMode = true;
        //}

        //public void EndEdit()
        //{
        //    if (!_isInEditMode)
        //        throw new InvalidOperationException("The object is not being edited.");
        //    EndEditCore();
        //    _isInEditMode = false;
        //}

        //public void CancelEdit()
        //{
        //    if (!_isInEditMode)
        //        throw new InvalidOperationException("The object is not being edited.");
        //    CancelEditCore();
        //    _isInEditMode = false;
        //}

        //#endregion

        //protected virtual void BeginEditCore()
        //{
        //}

        //protected virtual void EndEditCore()
        //{
        //}

        //protected virtual void CancelEditCore()
        //{
        //}
    }
}
