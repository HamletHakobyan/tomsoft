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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

namespace SharpDB.Controls
{
    [TemplatePart(Name = "PART_DropDownButton", Type = typeof(ButtonBase))]
    public class SplitButton : Button
    {
        static SplitButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SplitButton), new FrameworkPropertyMetadata(typeof(SplitButton)));
        }

        #region Dependency properties

        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(object), typeof(SplitButton), new UIPropertyMetadata(null));

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(SplitButton), new UIPropertyMetadata(null));

        public Style ItemContainerStyle
        {
            get { return (Style)GetValue(ItemContainerStyleProperty); }
            set { SetValue(ItemContainerStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemContainerStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemContainerStyleProperty =
            DependencyProperty.Register("ItemContainerStyle", typeof(Style), typeof(SplitButton), new UIPropertyMetadata(null));



        public Style ButtonStyle
        {
            get { return (Style)GetValue(ButtonStyleProperty); }
            set { SetValue(ButtonStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonStyleProperty =
            DependencyProperty.Register("ButtonStyle", typeof(Style), typeof(SplitButton), new UIPropertyMetadata(null));

        #endregion

        #region Routed events

        public event ItemClickEventHandler ItemClick
        {
            add { this.AddHandler(ItemClickEvent, value); }
            remove { this.RemoveHandler(ItemClickEvent, value); }
        }

        public static RoutedEvent ItemClickEvent =
            EventManager.RegisterRoutedEvent(
                "ItemClick",
                RoutingStrategy.Bubble,
                typeof(ItemClickEventHandler),
                typeof(SplitButton));

        #endregion

        private ButtonBase _dropDownButton;
        private ContextMenu _contextMenu;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _dropDownButton = Template.FindName("PART_DropDownButton", this) as ButtonBase;
            _dropDownButton.Click += dropDownButton_Click;
            _contextMenu = _dropDownButton.ContextMenu as ContextMenu;
            if (_contextMenu != null)
            {
                _contextMenu.PlacementTarget = this;
                _contextMenu.Placement = PlacementMode.Bottom;
                _contextMenu.AddHandler(MenuItem.ClickEvent, (RoutedEventHandler)MenuItem_Click);
            }
        }

        void dropDownButton_Click(object sender, RoutedEventArgs e)
        {
            if (_contextMenu != null)
            {
                _contextMenu.IsOpen = true;
            }
        }

        void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (_contextMenu != null)
            {
                _contextMenu.IsOpen = false;
            }

            var menuItem = e.OriginalSource as MenuItem;
            if (menuItem == null)
                return;

            var item = menuItem.DataContext;
            if (item == null)
                return;

            var args = new ItemClickEventArgs(item, this);
            RaiseEvent(args);
            if (Command != null && Command.CanExecute(item))
            {
                Command.Execute(item);
            }
        }
    }

    public delegate void ItemClickEventHandler(object sender, ItemClickEventArgs e);

    public class ItemClickEventArgs : RoutedEventArgs
    {
        public ItemClickEventArgs(object item)
            : this(item, null)
        {
        }

        public ItemClickEventArgs(object item, object source)
            : base(SplitButton.ItemClickEvent, source)
        {
            this.Item = item;
        }

        public object Item { get; private set; }
    }
}
