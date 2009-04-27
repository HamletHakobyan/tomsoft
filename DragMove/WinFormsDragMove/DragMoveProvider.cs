using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace WinFormsDragMove
{
    /// <summary>
    /// Ajoute aux contrôles une propriété AutoDragMove, qui permet d'activer un comportement de
    /// glisser/déplacer automatique.
    /// </summary>
    [ProvideProperty("AutoDragMove", typeof(Control))]
    public partial class DragMoveProvider : Component, IExtenderProvider
    {
        /// <summary>
        /// Initialise une nouvelle instance de DragMoveProvider sans spécifier de conteneur
        /// </summary>
        public DragMoveProvider()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialise une nouvelle instance de DragMoveProvider avec le conteneur spécifié
        /// <param name="container">Un IContainer qui représente le conteneur de ce DragMoveProvider.</param>
        /// </summary>
        public DragMoveProvider(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #region IExtenderProvider Members

        /// <summary>
        /// Renvoie true si le DragMoveProvider peut fournir une propriété d'extension à l'objet cible spécifié.
        /// </summary>
        /// <param name="extendee">L'objet cible auquel ajouter une propriété d'extension.</param>
        /// <returns>true si le DragMoveProvider peut fournir une ou plusieurs propriété d'extensions ; sinon, false.</returns>
        public bool CanExtend(object extendee)
        {
            return (extendee is Control);
        }

        #endregion

        /// <summary>
        /// Renvoie une valeur qui indique si le comportement AutoDragMove est activé pour le contrôle spécifié
        /// </summary>
        /// <param name="control">Le contrôle pour lequel on veut récupérer la propriété AutoDragMove.</param>
        /// <returns>true si le comportement AutoDragMove est activé pour ce contrôle ; sinon, false.</returns>
        [DefaultValue(false)]
        public bool GetAutoDragMove(Control control)
        {
#if FX_20
            return WinFormExtensions.IsRegisteredForDragMove(control);
#else
            return control.IsRegisteredForDragMove();
#endif
        }

        /// <summary>
        /// Active ou désactive le comportement AutoDragMove pour un contrôle
        /// </summary>
        /// <param name="control">Le contrôle pour lequel le comportement AutoDragMove doit être activé ou désactivé.</param>
        /// <param name="autoDragMove">true pour active le comportement AutoDragMove, false pour le désactiver.</param>
        public void SetAutoDragMove(Control control, bool autoDragMove)
        {
#if FX_20
            if (WinFormExtensions.IsRegisteredForDragMove(control) != autoDragMove)
            {
                if (autoDragMove)
                    WinFormExtensions.RegisterForDragMove(control);
                else
                    WinFormExtensions.UnregisterForDragMove(control);
            }
#else
            if (control.IsRegisteredForDragMove() != autoDragMove)
            {
                if (autoDragMove)
                    control.RegisterForDragMove();
                else
                    control.UnregisterForDragMove();
            }
#endif
        }
    }
}
