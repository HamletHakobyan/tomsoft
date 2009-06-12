using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Millionaire.Util
{
    /// <summary>
    /// Defines the contract for an object that has a parent object
    /// </summary>
    /// <typeparam name="P">Type of the parent object</typeparam>
    public interface IChildItem<P> where P : class
    {
        P Parent { get; set; }
    }
}
