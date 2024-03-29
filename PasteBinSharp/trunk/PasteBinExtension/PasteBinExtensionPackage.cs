﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using PasteBinSharp;
using PasteBinSharp.UI;

namespace ThomasLevesque.PasteBinExtension
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the informations needed to show the this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.GuidPasteBinExtensionPkgString)]
    public sealed class PasteBinExtensionPackage : Package
    {
        // Overriden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initilaization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Trace.WriteLine("Initializing PasteBinExtensionPackage");
            base.Initialize();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                // Create the command for the menu item.
                CommandID menuCommandID = new CommandID(GuidList.GuidPasteBinExtensionCmdSet, (int)PkgCmdIDList.PostToPasteBinCommandId);
                MenuCommand menuItem = new MenuCommand(MenuItemCallback, menuCommandID);
                mcs.AddCommand(menuItem);
            }
        }
        #endregion

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            var entry = GetEntryFromActiveDocument();
            var window = new SendWindow(entry);
            window.CenterWindowCallback =
                hWnd =>
                    {
                        IVsUIShell uiShell = (IVsUIShell) GetService(typeof(SVsUIShell));
                        if (uiShell != null)
                            uiShell.CenterDialogOnWindow(hWnd, IntPtr.Zero);
                    };
            window.ShowDialog();
        }

        PasteBinEntry GetEntryFromActiveDocument()
        {
            PasteBinEntry entry = new PasteBinEntry();
            entry.Text = string.Empty;
            entry.Title = string.Empty;
            
            DTE dte = GetService(typeof(SDTE)) as DTE;
            if (dte != null)
            {
                var document = dte.ActiveDocument;
                if (document != null)
                {
                    entry.Title = document.Name;
                    entry.Format = PasteBinUtil.FormatFromFileName(document.FullName);

                    var selection = (TextSelection) document.Selection;
                    if (selection != null)
                        entry.Text = selection.Text;
                }
            }
            return entry;
        }
    }

}
