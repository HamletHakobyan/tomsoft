using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace WinFormsDragMove
{
    /// <summary>
    /// Cette classe fournit des méthodes d'extension pour les contrôles Windows Forms
    /// </summary>
    public static partial class WinFormExtensions
    {
        #region DragMove extension

        private static Dictionary<Control, DraggedControl> draggedControls = new Dictionary<Control, DraggedControl>();

        /// <summary>
        /// Met temporairement le contrôle en mode "DragMove".
        /// Cette méthode doit être appelée à partir du gestionnaire de l'évènement MouseDown du contrôle.
        /// </summary>
        /// <param name="ctl">Le contrôle à déplacer</param>
        /// <param name="e">Les informations sur l'évènement MouseDown</param>
#if FX_20
        public static void DragMove(Control ctl, MouseEventArgs e)
#else
        public static void DragMove(this Control ctl, MouseEventArgs e)
#endif
        {
            new DraggedControl(ctl, e.X, e.Y);
        }

        /// <summary>
        /// Enregistre le contrôle pour la gestion par le DragMove.
        /// Ce contrôle pourra être déplacé par la souris sans aucun code supplémentaire.
        /// </summary>
        /// <param name="ctl">Le contrôle à enregistrer</param>
#if FX_20
        public static void RegisterForDragMove(Control ctl)
#else
        public static void RegisterForDragMove(this Control ctl)
#endif
        {
            draggedControls.Add(ctl, new DraggedControl(ctl));
        }

        /// <summary>
        /// Désenregistre le contrôle spécifié pour la gestion par le DragMove.
        /// Ce contrôle ne sera plus automatiquement déplacé par la souris.
        /// </summary>
        /// <param name="ctl">Le contrôle à désenregistrer</param>
#if FX_20
        public static void UnregisterForDragMove(Control ctl)
#else
        public static void UnregisterForDragMove(this Control ctl)
#endif
        {
            if (draggedControls.ContainsKey(ctl))
            {
                draggedControls[ctl].Dispose();
                draggedControls.Remove(ctl);
            }
        }

        /// <summary>
        /// Vérifie si le contrôle est enregistré pour la gestion par le DragMove
        /// </summary>
        /// <param name="ctl">Le contrôle à vérifier</param>
        /// <returns>true si le contrôle est enregistré; sinon, false</returns>
#if FX_20
        public static bool IsRegisteredForDragMove(Control ctl)
#else
        public static bool IsRegisteredForDragMove(this Control ctl)
#endif
        {
            return draggedControls.ContainsKey(ctl);
        }

        private class DraggedControl : IDisposable
        {
            public Control Target { get; private set; }
            public int XStart { get; private set; }
            public int YStart { get; private set; }
            public bool IsMoving { get; private set; }
            public bool IsTemporary { get; set; }

            public DraggedControl(Control target)
            {
                Target = target;
                IsMoving = false;
                IsTemporary = false;
                Target.MouseDown += Target_MouseDown;
                Target.MouseMove += Target_MouseMove;
                Target.MouseUp += Target_MouseUp;
                Target.Disposed += Target_Disposed;
            }

            public DraggedControl(Control target, int xStart, int yStart)
                : this(target)
            {
                XStart = xStart;
                YStart = yStart;
                IsMoving = true;
                IsTemporary = true;
            }

            void Target_MouseDown(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    IsMoving = true;
                    XStart = e.X;
                    YStart = e.Y;
                }
            }

            void Target_MouseUp(object sender, MouseEventArgs e)
            {
                IsMoving = false;
                if (IsTemporary)
                {
                    Dispose();
                }
            }

            private void Target_MouseMove(object sender, MouseEventArgs e)
            {
                if (IsMoving)
                {
                    int x = Target.Location.X + e.X - XStart;
                    int y = Target.Location.Y + e.Y - YStart;
                    Target.Location = new Point(x, y);
                }
            }

            void Target_Disposed(object sender, EventArgs e)
            {
#if FX_20
                UnregisterForDragMove(Target);
#else
                Target.UnregisterForDragMove();
#endif
            }

            public void Dispose()
            {
                Target.MouseDown -= Target_MouseDown;
                Target.MouseMove -= Target_MouseMove;
                Target.MouseUp -= Target_MouseUp;
                Target.Disposed -= Target_Disposed;
                Target = null;
            }
        }

        #endregion

    }
}