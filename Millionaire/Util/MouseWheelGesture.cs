using System.Linq;
using System.Windows.Input;

namespace Millionaire.Util
{
    public class MouseWheelGesture : MouseGesture
    {
        private MouseWheelAction _mouseWheelAction;

        public MouseWheelAction MouseWheelAction
        {
            get { return _mouseWheelAction; }
            set { _mouseWheelAction = value; }
        }

        public MouseWheelGesture()
            : base(MouseAction.WheelClick)
        {
            _mouseWheelAction = MouseWheelAction.AllMovement;
        }

        public MouseWheelGesture(MouseWheelAction action)
            : this()
        {
            _mouseWheelAction = action;
        }

        public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
        {
            if (base.Matches(targetElement, inputEventArgs))
            {
                MouseWheelEventArgs wheelArgs = inputEventArgs as MouseWheelEventArgs;
                if (wheelArgs != null)
                {
                    if (MouseWheelAction == MouseWheelAction.AllMovement
                            || (MouseWheelAction == MouseWheelAction.WheelDown && wheelArgs.Delta < 0)
                            || MouseWheelAction == MouseWheelAction.WheelUp && wheelArgs.Delta > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static MouseWheelGesture _wheelDown;
        public static MouseWheelGesture WheelDown
        {
            get
            {
                if (_wheelDown == null)
                {
                    _wheelDown = new MouseWheelGesture(MouseWheelAction.WheelDown);
                }
                return _wheelDown;
            }
        }

        private static MouseWheelGesture _wheelUp;
        public static MouseWheelGesture WheelUp
        {
            get
            {
                if (_wheelUp == null)
                {
                    _wheelUp = new MouseWheelGesture(MouseWheelAction.WheelUp);
                }
                return _wheelUp;
            }
        }

        private static MouseWheelGesture _allMovement;
        public static MouseWheelGesture AllMovement
        {
            get
            {
                if (_allMovement == null)
                {
                    _allMovement = new MouseWheelGesture(MouseWheelAction.AllMovement);
                }
                return _allMovement;
            }
        }
    }

    public enum MouseWheelAction
    {
        AllMovement,
        WheelUp,
        WheelDown
    }
}
