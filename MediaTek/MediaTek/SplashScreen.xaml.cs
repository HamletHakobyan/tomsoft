using System;
using System.Windows;
using System.ComponentModel;

namespace MediaTek
{
    public partial class SplashScreen : Window
    {
        BackgroundWorker bgwLoad;

        public SplashScreen()
        {
            InitializeComponent();
            bgwLoad = new BackgroundWorker();
            bgwLoad.DoWork += new DoWorkEventHandler(bgwLoad_DoWork);
            bgwLoad.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwLoad_RunWorkerCompleted);
            bgwLoad.RunWorkerAsync();
        }

        void bgwLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(ShowMainWindow));
        }

        private void ShowMainWindow()
        {
            MainWindow main = new MainWindow();
            App.Current.MainWindow = main;
            main.Show();
            this.Close();
        }

        void bgwLoad_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                App.Current.OpenDatabase(args[1]);
            }
        }

    }
}