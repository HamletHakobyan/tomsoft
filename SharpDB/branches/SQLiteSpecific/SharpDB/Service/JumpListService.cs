using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDB.Util.Service;
using System.Windows.Shell;
using System.Windows;
using Developpez.Dotnet;
using System.IO;
using SharpDB.Model;

namespace SharpDB.Service
{
    class JumpListService : IJumpListService
    {
        private Config _config;

        public JumpListService(Config config)
        {
            _config = config;

            var jumpList = new JumpList(new List<JumpItem>(), false, true);
            JumpList.SetJumpList(App.Current, jumpList);

            foreach (var connection in config.RecentConnections)
            {
                string arguments = string.Format("/connect \"{0}\"", connection);
                AddTask(jumpList, false, connection, Properties.Resources.jumplist_recent_databases, args: arguments);
            }

            foreach (var filename in config.RecentFiles)
            {
                AddRecent(jumpList, false, filename);
            }

            jumpList.Apply();
        }

        public void AddRecent(string path)
        {
            var jumpList = JumpList.GetJumpList(Application.Current);
            if (jumpList == null)
                return;

            AddToCategory(jumpList, true, path, Properties.Resources.jumplist_recent_files);
        }

        private void AddRecent(JumpList jumpList, bool apply, string path)
        {
            AddToCategory(jumpList, apply, path, Properties.Resources.jumplist_recent_files);
        }

        public void RemoveRecent(string path)
        {
            RemoveFromCategory(path, "Recent");
        }

        public void AddToCategory(string path, string category)
        {
            var jumpList = JumpList.GetJumpList(Application.Current);
            if (jumpList == null)
                return;

            AddToCategory(jumpList, true, path, category);
        }

        private void AddToCategory(JumpList jumpList, bool apply, string path, string category)
        {
            AddTask(jumpList, apply, Path.GetFileName(path), category, args: ProtectPath(path));
        }

        public void RemoveFromCategory(string path, string category)
        {
            RemoveTask(Path.GetFileName(path), category);
        }

        public void AddTask(string title, string category, string appPath = null, string args = null, string workingDirectory = null, string description = null, string iconResourcePath = null, int iconResourceIndex = 0)
        {
            var jumpList = JumpList.GetJumpList(Application.Current);
            if (jumpList == null)
                return;

            AddTask(jumpList, true, title, category, appPath, args, workingDirectory, description, iconResourcePath, iconResourceIndex);
        }

        private void AddTask(JumpList jumpList, bool apply, string title, string category, string appPath = null, string args = null, string workingDirectory = null, string description = null, string iconResourcePath = null, int iconResourceIndex = 0)
        {
            var item = FindTask(title, category) ?? new JumpTask
            {
                Title = title,
                CustomCategory = category
            };

            item.Description = description;
            item.ApplicationPath = appPath;
            item.Arguments = args;
            item.WorkingDirectory = workingDirectory;
            item.IconResourcePath = iconResourcePath;
            item.IconResourceIndex = iconResourceIndex;

            jumpList.JumpItems.Remove(item);
            jumpList.JumpItems.Insert(0, item);
            TrimMaxItems(jumpList);
            if (apply)
                jumpList.Apply();
        }

        public void RemoveTask(string title, string category)
        {
            var jumpList = JumpList.GetJumpList(Application.Current);
            if (jumpList == null)
                return;
            var item = FindTask(title, category);
            if (item != null)
            {
                jumpList.JumpItems.Remove(item);
                jumpList.Apply();
            }
        }

        public void ClearCategory(string category)
        {
            var jumpList = JumpList.GetJumpList(Application.Current);
            if (jumpList == null)
                return;

            jumpList.JumpItems.RemoveAll(item => item.CustomCategory == category);
            jumpList.Apply();
        }

        public void Clear()
        {
            var jumpList = JumpList.GetJumpList(Application.Current);
            if (jumpList == null)
                return;

            jumpList.JumpItems.Clear();
            jumpList.Apply();
        }

        public IEnumerable<JumpItem> GetJumpItems()
        {
            var jumpList = JumpList.GetJumpList(App.Current);
            if (jumpList == null)
                return Enumerable.Empty<JumpItem>();
            return jumpList.JumpItems;
        }

        private void TrimMaxItems(JumpList jumpList)
        {
            // Clear oldest items in each category
            var toRemove = jumpList.JumpItems.GroupBy(j => j.CustomCategory)
                                    .Select(g => g.Skip(_config.MaxRecentItems))
                                    .SelectMany(j => j)
                                    .ToArray();
            foreach (var item in toRemove)
            {
                jumpList.JumpItems.Remove(item);
            }
        }

        private JumpTask FindTask(string title, string category)
        {
            var jumpList = JumpList.GetJumpList(Application.Current);
            if (jumpList == null)
                return null;
            var item = jumpList.JumpItems
                               .OfType<JumpTask>()
                               .Where(jp => jp.Title == title &&
                                            jp.CustomCategory == category)
                               .FirstOrDefault();
            return item;
        }

        private static string ProtectPath(string path)
        {
            return string.Format("\"{0}\"", path);
        }

    }
}
