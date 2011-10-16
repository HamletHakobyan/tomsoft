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
    [TemplatePart(Name = "PART_Presenter",Type = typeof(FrameworkElement))]
    public class Marquee : ContentControl
    {
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(Marquee), new UIPropertyMetadata(Orientation.Horizontal, MarqueePropertyChanged));

        public static readonly DependencyProperty ScrollModeProperty =
            DependencyProperty.Register("ScrollMode", typeof(MarqueeScrollMode), typeof(Marquee), new UIPropertyMetadata(MarqueeScrollMode.Loop, MarqueePropertyChanged));

        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(double), typeof(Marquee), new UIPropertyMetadata(75.0, MarqueePropertyChanged));

        public static readonly DependencyProperty PauseDurationProperty =
            DependencyProperty.Register("PauseDuration", typeof(TimeSpan), typeof(Marquee), new UIPropertyMetadata(TimeSpan.FromSeconds((1)), MarqueePropertyChanged));

        private Canvas _canvas;
        private FrameworkElement _presenter;

        static Marquee()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Marquee), new FrameworkPropertyMetadata(typeof(Marquee)));
        }

        public Marquee()
        {
            Loaded += Marquee_Loaded;
            SizeChanged += Marquee_SizeChanged;
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

        public TimeSpan PauseDuration
        {
            get { return (TimeSpan)GetValue(PauseDurationProperty); }
            set { SetValue(PauseDurationProperty, value); }
        }

        void Marquee_Loaded(object sender, RoutedEventArgs e)
        {
            SetupAnimation();
        }

        private void Marquee_SizeChanged(object sender, EventArgs e)
        {
            SetupAnimation();
        }

        void _presenter_SizeChanged(object sender, SizeChangedEventArgs e)
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
            if (_presenter != null)
                _presenter.SizeChanged -= _presenter_SizeChanged;

            base.OnApplyTemplate();
            _presenter = (FrameworkElement)Template.FindName("PART_Presenter", this);
            _canvas = (Canvas) Template.FindName("PART_Canvas", this);
            _presenter.SizeChanged += _presenter_SizeChanged;
        }

        private void SetupAnimation()
        {
            if (!IsLoaded)
                return;

            DoubleAnimationBase animation;
            DependencyProperty targetProperty;
            string sizePropertyName;
            SetupOrientation(out targetProperty, out sizePropertyName);
            
            switch (ScrollMode)
            {
                case MarqueeScrollMode.BackAndForth:
                case MarqueeScrollMode.BackAndForthIfTooLarge:
                    animation = SetupBackAndForthAnimation(sizePropertyName);
                    break;
                default:
                    animation = SetupLoopAnimation(sizePropertyName);
                    break;
            }

            var transform = _presenter.RenderTransform as TranslateTransform;
            if (transform != null)
            {
                // Stop previous animation
                transform.BeginAnimation(TranslateTransform.XProperty, null);
                transform.BeginAnimation(TranslateTransform.YProperty, null);
            }
            else if (animation != null)
            {
                transform = new TranslateTransform();
                _presenter.RenderTransform = transform;
            }

            if (animation != null)
            {
                animation.RepeatBehavior = RepeatBehavior.Forever;
                transform.BeginAnimation(targetProperty, animation);
            }
        }

        private void SetupOrientation(out DependencyProperty targetProperty, out string sizePropertyName)
        {
            switch (Orientation)
            {
                case Orientation.Vertical:
                    targetProperty = TranslateTransform.YProperty;
                    sizePropertyName = "ActualHeight";
                    _canvas.SetBinding(MinWidthProperty, new Binding("ActualWidth") { Source = _presenter });
                    break;
                default:
                    targetProperty = TranslateTransform.XProperty;
                    sizePropertyName = "ActualWidth";
                    _canvas.SetBinding(MinHeightProperty, new Binding("ActualHeight") { Source = _presenter });
                    break;
            }
        }

        private DoubleAnimationBase SetupBackAndForthAnimation(string sizePropertyName)
        {
            var animation = new DoubleAnimationUsingKeyFrames();
            animation.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.Zero)));
            animation.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(PauseDuration)));
            animation.KeyFrames.Add(new LinearDoubleKeyFrame());
            animation.KeyFrames.Add(new LinearDoubleKeyFrame());

            var animationFromBinding = new MultiBinding
            {
                Bindings =
                    {
                        new Binding(sizePropertyName) { Source = _canvas },
                        new Binding(sizePropertyName) { Source = _presenter },
                        new Binding("ScrollMode") { Source = this }
                    },
                Converter = Singleton<BackAndForthOffsetConverter>.Instance,
                ConverterParameter = false // "From"
            };

            var animationToBinding = new MultiBinding
            {
                Bindings =
                    {
                        new Binding(sizePropertyName) { Source = _canvas },
                        new Binding(sizePropertyName) { Source = _presenter },
                        new Binding("ScrollMode") { Source = this }
                    },
                Converter = Singleton<BackAndForthOffsetConverter>.Instance,
                ConverterParameter = true // "To"
            };

            BindingOperations.SetBinding(
                animation.KeyFrames[0],
                DoubleKeyFrame.ValueProperty,
                animationFromBinding);

            BindingOperations.SetBinding(
                animation.KeyFrames[1],
                DoubleKeyFrame.ValueProperty,
                animationFromBinding);

            BindingOperations.SetBinding(
                animation.KeyFrames[2],
                DoubleKeyFrame.ValueProperty,
                animationToBinding);

            BindingOperations.SetBinding(
                animation.KeyFrames[3],
                DoubleKeyFrame.ValueProperty,
                animationToBinding);

            var keyTime2Binding = new MultiBinding
            {
                Bindings =
                    {
                        new Binding("Value") { Source = animation.KeyFrames[0] },
                        new Binding("Value") { Source = animation.KeyFrames[3] },
                        new Binding("Speed") { Source = this },
                        new Binding("PauseDuration") { Source = this }
                    },
                Converter = Singleton<ScrollDurationConverter>.Instance,
                ConverterParameter = 1
            };

            var keyTime3Binding =  new MultiBinding
            {
                Bindings =
                    {
                        new Binding("Value") { Source = animation.KeyFrames[0] },
                        new Binding("Value") { Source = animation.KeyFrames[3] },
                        new Binding("Speed") { Source = this },
                        new Binding("PauseDuration") { Source = this }
                    },
                Converter = Singleton<ScrollDurationConverter>.Instance,
                ConverterParameter = 2
            };

            BindingOperations.SetBinding(
                animation.KeyFrames[2],
                DoubleKeyFrame.KeyTimeProperty,
                keyTime2Binding);

            BindingOperations.SetBinding(
                animation.KeyFrames[3],
                DoubleKeyFrame.KeyTimeProperty,
                keyTime3Binding);

            animation.AutoReverse = true;
            return animation;
        }

        private DoubleAnimationBase SetupLoopAnimation(string sizePropertyName)
        {
            var animation = new DoubleAnimation();

            var animationFromBinding = new Binding(sizePropertyName)
            {
                Source = _canvas
            };
            var animationToBinding = new Binding(sizePropertyName)
            {
                Source = _canvas,
                Converter = Singleton<LoopOffsetConverter>.Instance
            };

            var durationBinding = new MultiBinding
            {
                Bindings =
                    {
                        new Binding("From") { Source = animation },
                        new Binding("To") { Source = animation },
                        new Binding("Speed") { Source = this },
                    },
                Converter = Singleton<ScrollDurationConverter>.Instance
            };

            BindingOperations.SetBinding(
                animation,
                DoubleAnimation.FromProperty,
                animationFromBinding);

            BindingOperations.SetBinding(
                animation,
                DoubleAnimation.ToProperty,
                animationToBinding);

            BindingOperations.SetBinding(
                animation,
                Timeline.DurationProperty,
                durationBinding);

            return animation;
        }

        #region Nested type: BackAndForthOffsetConverter

        class BackAndForthOffsetConverter : IMultiValueConverter
        {
            #region Implementation of IMultiValueConverter

            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                double actualCanvasSize = (double) values[0];
                double actualContentSize = (double)values[1];
                var scrollMode = (MarqueeScrollMode) values[2];
                bool to = (bool) parameter;

                if (scrollMode == MarqueeScrollMode.BackAndForthIfTooLarge &&
                    actualCanvasSize > actualContentSize)
                    return (actualCanvasSize - actualContentSize) / 2.0;
                
                if (to)
                    return actualCanvasSize - actualContentSize;

                return 0.0;
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                throw new NotSupportedException();
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

                TimeSpan pauseDuration = TimeSpan.Zero;
                if (values.Length > 3)
                    pauseDuration = (TimeSpan) values[3];
                int nPauses = 0;
                if (parameter != null)
                    nPauses = (int) parameter;
                var totalPause = TimeSpan.Zero;
                for (int i = 0; i < nPauses; i++)
                {
                    totalPause += pauseDuration;
                }

                double timeInSeconds = totalPause.TotalSeconds + Math.Abs(to - from) / speed;
                var ts = TimeSpan.FromSeconds(timeInSeconds);
                if (targetType == typeof(Duration))
                    return new Duration(ts);
                if (targetType == typeof(KeyTime))
                    return KeyTime.FromTimeSpan(ts);
                return ts;
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                throw new NotSupportedException();
            }

            #endregion
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
                throw new NotSupportedException();
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
