using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls.Primitives;

namespace MediaTek.Utilities
{
    public static class ModalServices
    {
        public delegate void ModalReturnEventHandler(bool? modalResult);

        private static Stack<ModalContext> modalStack = new Stack<ModalContext>();

        private class ModalContext
        {
            public ModalContext()
            {
                DisabledElements = new List<UIElement>();
            }
            public ModalReturnEventHandler ReturnHandler { get; set; }
            public List<UIElement> DisabledElements { get; private set; }
            
            public Selector OldFocusedElement { get; set; }
        }

        public static void ShowModal(this Control ctl, Panel modalParent, ModalReturnEventHandler returnHandler)
        {
            ModalContext ctx = new ModalContext
            {
                ReturnHandler = returnHandler,
            };

            if (Keyboard.FocusedElement is ListBoxItem)
            {
                ListBoxItem lbi = Keyboard.FocusedElement as ListBoxItem;
                Selector s = lbi.FindAncestor<Selector>();
                ctx.OldFocusedElement = s;
            }

            foreach (UIElement elt in modalParent.Children)
            {
                if (elt.IsEnabled)
                {
                    ctx.DisabledElements.Add(elt);
                    elt.IsEnabled = false;
                    elt.Opacity /= 2;
                }
            }
            modalStack.Push(ctx);
            modalParent.Children.Add(ctl);
        }

        public static void ReturnModal(this Control ctl, bool? modalResult)
        {
            ModalContext ctx = modalStack.Pop();
            Panel modalParent = ctl.Parent as Panel;
            modalParent.Children.Remove(ctl);
            foreach (UIElement elt in ctx.DisabledElements)
            {
                elt.IsEnabled = true;
                elt.Opacity *= 2;
            }

            if (ctx.ReturnHandler != null)
            {
                ctx.ReturnHandler(modalResult);
            }

            if (ctx.OldFocusedElement != null)
            {
                Keyboard.Focus(ctx.OldFocusedElement);
            }
        }

    }
}
