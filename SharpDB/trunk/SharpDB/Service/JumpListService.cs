﻿using System;
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
        public JumpListService(Config config)
        {
            var jumpList = new JumpList(new List<JumpItem>(), false, true);
            JumpList.SetJumpList(App.Current, jumpList);

            foreach (var connection in config.RecentConnections)
            {
                string arguments = string.Format("/connect \"{0}\"", connection);
                AddTask(connection, Properties.Resources.jumplist_recent_databases, args: arguments);
            }

            foreach (var filename in config.RecentFiles)
            {
                AddRecent(filename);
            }
        }

        public void AddRecent(string path)
        {
            AddToCategory(path, Properties.Resources.jumplist_recent_files);
        }

        public void RemoveRecent(string path)
        {
            RemoveFromCategory(path, "Recent");
        }

        public void AddToCategory(string path, string category)
        {
            AddTask(Path.GetFileName(path), category, args: ProtectPath(path));
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

        private int _maxCountPerCategory = 6;
        public int MaxCountPerCategory
        {
            get { return _maxCountPerCategory; }
            set
            {
                _maxCountPerCategory = Math.Max(0, value);
                var jumpList = JumpList.GetJumpList(App.Current);
                if (jumpList == null)
                    return;
                TrimMaxItems(jumpList);
            }
        }

        private void TrimMaxItems(JumpList jumpList)
        {
            var toRemove = jumpList.JumpItems.GroupBy(j => j.CustomCategory)
                                    .Select(g => g.Skip(_maxCountPerCategory))
                                    .SelectMany(j => j)
                                    .ToArray();
            foreach (var item in toRemove)
            {
                jumpList.JumpItems.Remove(item);
            }
            jumpList.Apply();
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
