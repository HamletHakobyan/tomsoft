using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace LocalizedResourceExtension
{
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [ProvideAutoLoad(UIContextGuids80.SolutionExists)]
    // This attribute is used to register the informations needed to show the this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.GuidLocalizedResourceExtensionPkgString)]
    public class LocalizedResourceExtensionPackage : Package, IVsSelectionEvents
    {
        private MenuCommand _addLocalizedResourceCommand;

        private static readonly HashSet<string> _allCultures =
            new HashSet<string>(
                CultureInfo.GetCultures(CultureTypes.AllCultures)
                    .Select(c => c.Name),
                StringComparer.InvariantCultureIgnoreCase);

        protected override void Initialize()
        {
            base.Initialize();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                // Create the command for the menu item.
                CommandID menuCommandID = new CommandID(GuidList.GuidLocalizedResourceExtensionCmdSet, (int)PkgCmdIDList.AddLocalizedResourceCommandId);
                _addLocalizedResourceCommand = new MenuCommand(MenuItemCallback, menuCommandID);
                mcs.AddCommand(_addLocalizedResourceCommand);
            }

            var monitorSelection = GetService(typeof(IVsMonitorSelection)) as IVsMonitorSelection;
            if (monitorSelection != null)
            {
                uint cookie;
                monitorSelection.AdviseSelectionEvents(this, out cookie);
            }
        }

        private ProjectItem _selectedProjectItem;

        private void MenuItemCallback(object sender, EventArgs e)
        {
            var selectedProjectItem = _selectedProjectItem;
            if (selectedProjectItem == null)
                return;

            string rootName;
            string culture;
            GetRootNameAndCulture(selectedProjectItem.Name, out rootName, out culture);

            var usedCultures = GetUsedCultures(selectedProjectItem, rootName);
            var window = new AddLocalizedResourceWindow(rootName, usedCultures);
            if (window.ShowDialog() == true)
            {
                string newName = rootName + "." + window.TargetCultureCode + ".resx";
                string selectedPath = selectedProjectItem.Properties.GetProperty<string>("FullPath");
                string newPath = Path.Combine(Path.GetDirectoryName(selectedPath), newName);

                if (window.CopyNeutralResources)
                {
                    File.Copy(selectedPath, newPath, true);
                }
                else
                {
                    var doc = XDocument.Load(selectedPath);
                    if (doc.Root != null)
                    {
                        var elements = doc.Root.Elements("data").ToArray();
                        foreach (var elt in elements)
                            elt.Remove();
                    }
                    doc.Save(newPath);
                }

                if (window.AddAsSubItem)
                {
                    selectedProjectItem.ProjectItems.AddFromFile(newPath);
                }
                else
                {
                    selectedProjectItem.Collection.AddFromFile(newPath);
                }
            }
        }

        private static HashSet<string> GetUsedCultures(ProjectItem projectItem, string rootName)
        {
            var usedCultures = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
            var allResx =
                projectItem.Collection.OfType<ProjectItem>().Concat(projectItem.ProjectItems.OfType<ProjectItem>())
                .Where(p => string.Equals(Path.GetExtension(p.Name), ".resx", StringComparison.InvariantCultureIgnoreCase));
            foreach (ProjectItem item in allResx)
            {
                string itemRootName;
                string culture;
                GetRootNameAndCulture(item.Name, out itemRootName, out culture);
                if (string.Equals(rootName, itemRootName, StringComparison.InvariantCultureIgnoreCase))
                    usedCultures.Add(culture);
            }
            return usedCultures;
        }

        private static void GetRootNameAndCulture(string path, out string rootName, out string culture)
        {
            string fileNameNoExtension = Path.GetFileNameWithoutExtension(path);
            string cultureExt = Path.GetExtension(fileNameNoExtension) ?? string.Empty;
            cultureExt = cultureExt.TrimStart('.');
            if (!string.IsNullOrEmpty(cultureExt) && _allCultures.Contains(cultureExt))
            {
                rootName = Path.GetFileNameWithoutExtension(fileNameNoExtension);
                culture = cultureExt;
            }
            else
            {
                rootName = fileNameNoExtension;
                culture = string.Empty;
            }
        }

        #region Implementation of IVsSelectionEvents

        public int OnSelectionChanged(
            IVsHierarchy pHierOld,
            uint itemidOld,
            IVsMultiItemSelect pMISOld,
            ISelectionContainer pSCOld,
            IVsHierarchy pHierNew,
            uint itemidNew,
            IVsMultiItemSelect pMISNew,
            ISelectionContainer pSCNew)
        {

            if (pHierNew != null)
            {
                ProjectItem item = pHierNew.GetProjectItem(itemidNew);
                if (item != null)
                {
                    string extension = Path.GetExtension(item.Name);
                    string rootName;
                    string culture;
                    GetRootNameAndCulture(item.Name, out rootName, out culture);
                    if (string.Equals(extension, ".resx", StringComparison.InvariantCultureIgnoreCase) &&
                            string.IsNullOrEmpty(culture))
                    {
                        _selectedProjectItem = item;
                        _addLocalizedResourceCommand.Visible = true;
                        return VSConstants.S_OK;
                    }
                }
            }

            _selectedProjectItem = null;
            _addLocalizedResourceCommand.Visible = false;
            return VSConstants.S_OK;
        }

        public int OnElementValueChanged(uint elementid, object varValueOld, object varValueNew)
        {
            return VSConstants.S_OK;
        }

        public int OnCmdUIContextChanged(uint dwCmdUICookie, int fActive)
        {
            return VSConstants.S_OK;
        }

        #endregion
    }
}
