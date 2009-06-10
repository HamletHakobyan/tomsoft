using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Millionaire.ViewModel;
using System.Xml.Serialization;
using Millionaire.Model;
using System.IO;
using System.Configuration;

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
                    MessageBox.Show("Veuillez fournir en paramètre le nom du fichier Quiz à utiliser.");
                }
            }
            catch(Exception ex)
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

