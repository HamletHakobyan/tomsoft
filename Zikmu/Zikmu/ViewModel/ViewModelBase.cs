using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using Developpez.Dotnet.Reflection;
using Developpez.Dotnet.Windows.Input;
using Zikmu.Infrastructure;

namespace Zikmu.ViewModel
{
    public class ViewModelBase : Developpez.Dotnet.Windows.ViewModel.ViewModelBase
    {
        protected void InitCommands()
        {
            var methods =
                from m in GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly)
                let a = m.GetAttribute<CommandAttribute>()
                where a != null
                select new
                {
                    Method = m,
                    CommandName = string.IsNullOrEmpty(a.CommandName)
                                      ? m.Name + "Command"
                                      : a.CommandName
                };

            foreach (var m in methods)
            {
                if (!m.Method.ReturnType.Equals(typeof(void)))
                    throw new InvalidOperationException("Method must have void return type");

                var parameters = m.Method.GetParameters();
                ICommand command;
                object instance = m.Method.IsStatic ? null : this;
                if (parameters.Length == 0)
                {
                    var action = (Action)Delegate.CreateDelegate(typeof(Action), instance, m.Method);
                    command = new DelegateCommand(action);
                }
                else if (parameters.Length == 1)
                {
                    var actionType = typeof(Action).MakeGenericType(parameters[0].ParameterType);
                    var commandType = typeof(DelegateCommand<>).MakeGenericType(parameters[0].ParameterType);
                    var action = (Action)Delegate.CreateDelegate(actionType, instance, m.Method);
                    command = (ICommand) Activator.CreateInstance(commandType, action);
                }
                else
                {
                    throw new InvalidOperationException("Method must have 0 or 1 parameter");
                }
                var commandProperty = GetType().GetProperty(m.CommandName);
                if (commandProperty == null)
                    throw new InvalidOperationException(string.Format("No property named {0} was found", m.CommandName));

                commandProperty.SetValue(this, command, null);
            }
        }
    }
}
