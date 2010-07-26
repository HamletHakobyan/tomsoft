using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shell;

namespace SharpDB.Util.Service
{
    public interface IJumpListService
    {
        void AddRecent(string path);
        void RemoveRecent(string path);
        void AddToCategory(string path, string category);
        void RemoveFromCategory(string path, string category);
        void AddTask(string title, string category, string appPath = null, string args = null, string workingDirectory = null, string description = null, string iconResourcePath = null, int iconResourceIndex = 0);
        void RemoveTask(string title, string category);
        void ClearCategory(string category);
        void Clear();
        IEnumerable<JumpItem> GetJumpItems();
    }
}
