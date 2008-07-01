using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MediaTek
{
    public static class ModalServices
    {
        public delegate void ModalReturnEventHandler(bool? modalResult);

        private static Stack<ModalReturnEventHandler> modalReturnHandlers = new Stack<ModalReturnEventHandler>();

        public static void ShowModal(this Control ctl, Panel modalParent, ModalReturnEventHandler returnHandler)
        {
            foreach (UIElement elt in modalParent.Children)
            {
                elt.IsEnabled = false;
                elt.Opacity /= 2;
            }
            modalReturnHandlers.Push(returnHandler);
            modalParent.Children.Add(ctl);
        }

        public static void ReturnModal(this Control ctl, bool? modalResult)
        {
            Panel modalParent = ctl.Parent as Panel;
            modalParent.Children.Remove(ctl);
            foreach (UIElement elt in modalParent.Children)
            {
                elt.IsEnabled = true;
                elt.Opacity *= 2;
            }

            ModalReturnEventHandler handler = modalReturnHandlers.Pop();
            if (handler != null)
            {
                handler(modalResult);
            }
        }
    }
}
