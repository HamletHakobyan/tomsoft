using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace SharpDB.Controls
{
    [TemplatePart(Name = "PART_CloseButton", Type = typeof(Button))]
    public class TabDocumentContainerItem : ContentControl
    {
        private TabDocumentContainer _parent;
        private Button _closeButton;

        static TabDocumentContainerItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabDocumentContainer), new FrameworkPropertyMetadata(typeof(TabDocumentContainerItem)));
        }

        public TabDocumentContainerItem(TabDocumentContainer parent)
        {
            _parent = parent;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _closeButton = this.Template.FindName("PART_CloseButton", this) as Button;
            if (_closeButton != null)
            {
                _closeButton.Click += closeButton_Click;
            }
        }

        void closeButton_Click(object sender, RoutedEventArgs e)
        {
            _parent.CloseTab(this);
        }

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(TabDocumentContainerItem), new UIPropertyMetadata(false));

    }
}
