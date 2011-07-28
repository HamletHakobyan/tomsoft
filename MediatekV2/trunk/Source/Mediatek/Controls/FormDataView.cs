using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mediatek.Controls
{
    public class FormDataView : ItemsControl
    {
        static FormDataView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FormDataView),
                                                     new FrameworkPropertyMetadata(typeof(FormDataView)));
        }

        public FormDataView()
        {
            this.Loaded += FormDataView_Loaded;
        }

        void FormDataView_Loaded(object sender, RoutedEventArgs e)
        {
            AdjustHeaderWidth();
        }

        private bool _adjustingHeaderWidth;
        internal void AdjustHeaderWidth()
        {
            if (_adjustingHeaderWidth)
                return;

            try
            {
                _adjustingHeaderWidth = true;
                double maxWidth = 0;
                var headers = new List<FrameworkElement>();
                foreach (FormField field in Items.OfType<FormField>())
                {
                    var header = field.Template.FindName("PART_Header", field) as FrameworkElement;
                    if (header != null)
                    {
                        maxWidth = Math.Max(maxWidth, header.ActualWidth);
                        headers.Add(header);
                    }
                }
                foreach (var header in headers)
                {
                    header.Width = maxWidth;
                }
            }
            finally
            {
                _adjustingHeaderWidth = false;
            }
        }

        public bool IsInEditMode
        {
            get { return (bool)GetValue(IsInEditModeProperty); }
            set { SetValue(IsInEditModeProperty, value); }
        }

        public static readonly DependencyProperty IsInEditModeProperty =
            DependencyProperty.Register("IsInEditMode", typeof(bool), typeof(FormDataView), new UIPropertyMetadata(false));
    }
}
