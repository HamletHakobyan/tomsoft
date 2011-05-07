using System;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using Millionaire.Model;
using Millionaire.ViewModel;

namespace Millionaire
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var bounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            this.Left = 0;
            this.Top = 0;
            this.Width = bounds.Width;
            this.Height = bounds.Height;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Quiz quiz = null;
            try
            {
                var args = Environment.GetCommandLineArgs();
                if (args.Length > 1)
                {
                    string filename = args[1];
                    quiz = Quiz.Load(filename);
                }
                else
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = "Fichiers quiz XML|*.xml";
                    ofd.Title = "Choisissez un fichier quiz à utiliser";
                    if (ofd.ShowDialog() == true)
                    {
                        quiz = Quiz.Load(ofd.FileName);
                    }
                    else
                    {
                        App.Current.Shutdown();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Application.Current.Shutdown();
            }

            Game.StartGame(quiz);
            GameViewModel gameVM = new GameViewModel(Game.Current);
            this.DataContext = gameVM;
            gameVM.NextSlide();

        }
    }
}

