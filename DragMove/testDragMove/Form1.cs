using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// On importe le namespace contenant DragMoveExtensions pour pouvoir
// utiliser ses méthodes d'extension.
using WinFormsDragMove;

namespace testDragMove
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            // Enable the DragMove behavior for label1. From now on,
            // no need to handle the MouseDown event for this control

#if FX_20
            // C# 2 usage (static method) :
            DragMoveExtensions.EnableDragMove(this, true);
            DragMoveExtensions.EnableDragMove(label1, true);
#else
            // C# 3 usage (extension method) :
            label1.EnableDragMove(true);
#endif

        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            // Automatic DragMove isn't enabled for label2, so we temporarily
            // activate it on the MouseDown event

#if FX_20
            // C# 2 usage (static method) :
            DragMoveExtensions.DragMove(label2);
#else
            // C# 3 usage (extension method) :
            label2.DragMove();

#endif

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Enable or disable DragMove fot the Form according to the checkbox

#if FX_20
            DragMoveExtensions.EnableDragMove(this, checkBox1.Checked);
#else
            this.EnableDragMove(checkBox1.Checked);
#endif

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
