using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// On importe le namespace contenant FormExtensionMethods pour pouvoir
// utiliser ses méthodes d'extension.
using WinFormsDragMove;

namespace testDragMove
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            // On active le déplacement automatique pour la Form et le label1
            // Plus besoin de se préoccuper des évènements souris pour ces 2 éléments

#if FX_20
            // Utilisation avec C# 2 (méthode statique) :
            WinFormExtensions.RegisterForDragMove(this);
            WinFormExtensions.RegisterForDragMove(label1);
#else
            // Utilisation avec C# 3 (méthode d'extension) :
            this.RegisterForDragMove();
            label1.RegisterForDragMove();
#endif

        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            // La gestion du déplacement automatique n'est pas activée pour le label2
            // Quand le bouton de la souris est enfoncé, on déclenche le déplacement

#if FX_20
            // Utilisation avec C# 2 (méthode statique) :
            WinFormExtensions.DragMove(label2, e);
#else
            // Utilisation avec C# 3 (méthode d'extension) :
            label2.DragMove(e);

#endif

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
