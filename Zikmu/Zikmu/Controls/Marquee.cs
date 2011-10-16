using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Developpez.Dotnet.ComponentModel;

namespace Zikmu.Controls
{
    [TemplatePart(Name = "PART_Canvas", Type = typeof(Canvas))]
    [TemplatePart(Name = "PART_Presenter",Type = typeof(UIElement))]
    public class Marquee : ContentControl
    {
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(Marquee), new UIPropertyMetadata(Orientation.Horizontal, MarqueePropertyChanged));

        public static readonly DependencyProperty ScrollModeProperty =
            DependencyProperty.Register("ScrollMode", typeof(MarqueeScrollMode), typeof(Marquee), new UIPropertyMetadata(MarqueeScrollMode.Loop, MarqueePropertyChanged));

        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(double), typeof(Marquee), new UIPropertyMetadata(75.0, MarqueePropertyChanged));

        private Canvas _canvas;
        private UIElement _presenter;

        static Marquee()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Marquee), new FrameworkPropertyMetadata(typeof(Marquee)));
        }

        public Marquee()
        {
            Loaded += Marquee_Loaded;
        }

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public MarqueeScrollMode ScrollMode
        {
            get { return (MarqueeScrollMode)GetValue(ScrollModeProperty); }
            set { SetValue(ScrollModeProperty, value); }
        }

        public double Speed
        {
            get { return (double)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }

        void Marquee_Loaded(object sender, RoutedEventArgs e)
        {
            SetupAnimation();
        }


        private static void MarqueePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var marquee = d as Marquee;
            if (marquee == null)
                return;

            marquee.SetupAnimation();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _presenter = (UIElement)Template.FindName("PART_Presenter", this);
            _canvas = (Canvas) Template.FindName("PART_Canvas", this);
        }

        private void SetupAnimation()
        {
            if (!IsLoaded)
                return;
            DependencyProperty targetProperty;
            BindingBase animationFromBinding = null;
            BindingBase animationToBinding = null;
            string sizeProperty;
            bool autoReverse = false;
            IEasingFunction easingFunction = null;
            var animation = new DoubleAnimation();
            animation.From = 0.0;
            double pauseRatio = 0.0;

            switch (Orientation)
            {
                case Orientation.Vertical:
                    targetProperty = TranslateTransform.YProperty;
                    sizeProperty = "ActualHeight";
                    _canvas.SetBinding(MinWidthProperty, new Binding("ActualWidth") { Source = _presenter});
                    break;
                default:
                    targetProperty = TranslateTransform.XProperty;
                    sizeProperty = "ActualWidth";
                    _canvas.SetBinding(MinHeightProperty, new Binding("ActualHeight") { Source = _presenter });
                    break;
            }

            switch (ScrollMode)
            {
                case MarqueeScrollMode.BackAndForth:
                case MarqueeScrollMode.BackAndForthIfTooLarge:
                    animationToBinding = new MultiBinding
                    {
                        Bindings =
                            {
                                new Binding(sizeProperty) { Source = _canvas },
                                new Binding(sizeProperty) { Source = _presenter },
                            },
                        Converter = Singleton<BackAndForthOffsetConverter>.Instance,
                        ConverterParameter = ScrollMode
                    };
                    autoReverse = true;
                    easingFunction = new LinearEaseWithPause()
                    {
                        EasingMode = EasingMode.EaseIn,
                        InPause = 0.25,
                        OutPause = 0.25
                    };
                    pauseRatio = 0.6;
                    break;
                default:
                    animationFromBinding = new Binding(sizeProperty)
                    {
                        Source = _canvas
                    };
                    animationToBinding = new Binding(sizeProperty)
                    {
                        Source = _canvas,
                        Converter = Singleton<LoopOffsetConverter>.Instance
                    };
                    break;
            }

            animation.RepeatBehavior = RepeatBehavior.Forever;
            animation.AutoReverse = autoReverse;
            animation.EasingFunction = easingFunction;
            
            if (animationFromBinding != null)
                BindingOperations.SetBinding(animation, DoubleAnimation.FromProperty, animationFromBinding);
            if (animationToBinding != null)
                BindingOperations.SetBinding(animation, DoubleAnimation.ToProperty, animationToBinding);

            var durationBinding = new MultiBinding
            {
                Bindings =
                    {
                        new Binding("From") { Source = animation },
                        new Binding("To") { Source = animation },
                        new Binding("Speed") { Source = this },
                        new Binding { Source = pauseRatio }
                    },
                Converter = Singleton<ScrollDurationConverter>.Instance
            };
            BindingOperations.SetBinding(animation, Timeline.DurationProperty, durationBinding);

            var transform = _presenter.RenderTransform as TranslateTransform;
            if (transform == null)
            {
                transform = new TranslateTransform();
                _presenter.RenderTransform = transform;
            }
            transform.BeginAnimation(TranslateTransform.XProperty, null);
            transform.BeginAnimation(TranslateTransform.YProperty, null);
            transform.BeginAnimation(targetProperty, animation);
        }

        #region Nested type: BackAndForthOffsetConverter

        class BackAndForthOffsetConverter : IMultiValueConverter
        {
            #region Implementation of IMultiValueConverter

            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                double actualCanvasSize = (double) values[0];
                double actualContentSize = (double)values[1];
                MarqueeScrollMode scrollMode = (MarqueeScrollMode) parameter;
                if (scrollMode == MarqueeScrollMode.BackAndForthIfTooLarge &&
                    actualCanvasSize > actualContentSize)
                    return 0.0;
                return actualCanvasSize - actualContentSize;
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        #endregion

        #region Nested type: ScrollDurationConverter

        class ScrollDurationConverter : IMultiValueConverter
        {
            #region Implementation of IMultiValueConverter

            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                double from = (double) values[0];
                double to = (double) values[1];
                double speed = (double) values[2];
                double pauseRatio = 0.0;
                for (int i = 3; i < values.Length; i++)
                {
                    pauseRatio += (double) values[i];
                }
                if (pauseRatio >= 1.0)
                    pauseRatio = 0.9;
                double timeInSeconds = Math.Abs(to - from) / speed / (1 - pauseRatio);
                return new Duration(TimeSpan.FromSeconds(timeInSeconds));
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        #endregion

        #region Nested type: LinearEaseWithPause

        class LinearEaseWithPause : EasingFunctionBase
        {
            #region Overrides of Freezable

            protected override Freezable CreateInstanceCore()
            {
                return new LinearEaseWithPause();
            }

            #endregion

            #region Overrides of EasingFunctionBase

            protected override double EaseInCore(double normalizedTime)
            {
                if (normalizedTime < InPause)
                    return 0.0;
                if (normalizedTime >= 1.0 - OutPause)
                    return 1.0;

                // y = ax + b
                double a = 1 / (1 - InPause - OutPause);
                double b = -a * InPause;

                return a * normalizedTime + b;
            }

            #endregion

            // Using a DependencyProperty as the backing store for InPause.  This enables animation, styling, binding, etc...
            public static readonly DependencyProperty InPauseProperty =
                DependencyProperty.Register("InPause", typeof(double), typeof(LinearEaseWithPause), new UIPropertyMetadata(0.1, null, CoercePause));

            // Using a DependencyProperty as the backing store for OutPause.  This enables animation, styling, binding, etc...
            public static readonly DependencyProperty OutPauseProperty =
                DependencyProperty.Register("OutPause", typeof(double), typeof(LinearEaseWithPause), new UIPropertyMetadata(0.1, null, CoercePause));

            public double InPause
            {
                get { return (double)GetValue(InPauseProperty); }
                set { SetValue(InPauseProperty, value); }
            }

            public double OutPause
            {
                get { return (double)GetValue(OutPauseProperty); }
                set { SetValue(OutPauseProperty, value); }
            }

            private static object CoercePause(DependencyObject obj, object baseValue)
            {
                double d = (double) baseValue;
                if (d < 0.0)
                    return 0.0;
                if (d >= 1.0)
                    return 0.0;
                return d;
            }

        }

        #endregion

        #region Nested type: LoopOffsetConverter

        class LoopOffsetConverter : IValueConverter
        {
            #region Implementation of IValueConverter

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                double actualCanvasSize = (double)value;
                return -actualCanvasSize;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        #endregion
    }

    public enum MarqueeScrollMode
    {
        Loop,
        BackAndForth,
        BackAndForthIfTooLarge
    }
}
