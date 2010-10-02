using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Developpez.Dotnet.Windows;

namespace Mediatek.Controls
{
    public class AnimatedGifImage : Image
    {

        public Uri UriSource
        {
            get { return (Uri)GetValue(UriSourceProperty); }
            set { SetValue(UriSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UriSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UriSourceProperty =
            DependencyProperty.Register("UriSource", typeof(Uri), typeof(AnimatedGifImage), new UIPropertyMetadata(null, UriSourceChanged));

        private GifBitmapDecoder _decoder;
        private AnimationClock _clock;
        private static void UriSourceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            AnimatedGifImage image = obj as AnimatedGifImage;
            if (image == null)
                return;

            if (image._clock != null && image._clock.Controller != null)
            {
                image._clock.Controller.Stop();
                image._clock = null;
            }
            image._decoder = null;
            var e2 = e.OfType<Uri>();
            if (e2.NewValue != null)
            {
                SetupAnimation(image, e2.NewValue);
            }
        }

        private int FrameIndex
        {
            get { return (int)GetValue(FrameIndexProperty); }
        }

// ReSharper disable InconsistentNaming
        private static readonly DependencyProperty FrameIndexProperty =
// ReSharper restore InconsistentNaming
            DependencyProperty.Register(
                "FrameIndex",
                typeof(int),
                typeof(AnimatedGifImage),
                new FrameworkPropertyMetadata(
                    0,
                    FrameIndexChanged));

        private static void FrameIndexChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            AnimatedGifImage image = obj as AnimatedGifImage;
            if (image == null)
                return;

            if (image._decoder != null)
            {
                image.SetCurrentValue(SourceProperty, image._decoder.Frames[image.FrameIndex]);
                image.InvalidateVisual();
            }
        }

        private static void SetupAnimation(AnimatedGifImage image, Uri uri)
        {
            image._decoder = new GifBitmapDecoder(uri, BitmapCreateOptions.None, BitmapCacheOption.Default);
            int frameCount = image._decoder.Frames.Count;
            if (frameCount > 1)
            {
                //var animation = new Int32AnimationUsingKeyFrames();
                var animation = new Int32Animation();
                animation.From = 0;
                animation.To = frameCount - 1;
                animation.Duration = new Duration(TimeSpan.FromSeconds(1));
                animation.RepeatBehavior = RepeatBehavior.Forever;
                //TimeSpan timeSpan = TimeSpan.Zero;
                //TimeSpan timeStep = TimeSpan.FromSeconds(animation.Duration.TimeSpan.TotalSeconds);
                //for (int i = 0; i < image._decoder.Frames.Count; i++)
                //{
                //    var frame = new LinearInt32KeyFrame(i, KeyTime.FromTimeSpan(timeSpan));
                //    animation.KeyFrames.Add(frame);
                //    timeSpan += timeStep;
                //}
                image._clock = animation.CreateClock();
                image.Source = image._decoder.Frames[0];
                image.ApplyAnimationClock(FrameIndexProperty, image._clock);
            }
        }
    }
}
