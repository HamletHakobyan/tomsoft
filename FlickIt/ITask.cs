using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlickIt
{
    public interface ITask
    {
        bool IsCancellable { get; }
        TaskStatus Status { get; }
        Exception Error { get; }
        void Start();
        void Cancel();
    }
}
