using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Zikmu.Behaviors
{
    public static class MediaElementBehavior
    {
        [AttachedPropertyBrowsableForType(typeof(MediaElement))]
        public static IMediaController GetController(MediaElement obj)
        {
            return (IMediaController)obj.GetValue(ControllerProperty);
        }

        public static void SetController(MediaElement obj, IMediaController value)
        {
            obj.SetValue(ControllerProperty, value);
        }

        public static readonly DependencyProperty ControllerProperty =
            DependencyProperty.RegisterAttached(
              "Controller",
              typeof(IMediaController),
              typeof(MediaElementBehavior),
              new UIPropertyMetadata(
                null,
                ControllerChanged));

        private static MediaAdapter GetMediaAdapter(MediaElement obj)
        {
            return (MediaAdapter)obj.GetValue(_mediaAdapterProperty);
        }

        private static void SetMediaAdapter(MediaElement obj, MediaAdapter value)
        {
            obj.SetValue(_mediaAdapterProperty, value);
        }

        private static readonly DependencyProperty _mediaAdapterProperty =
            DependencyProperty.RegisterAttached("MediaAdapter", typeof(MediaAdapter), typeof(MediaElement), new UIPropertyMetadata(null));

        private static void ControllerChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var mediaElement = o as MediaElement;
            if (mediaElement == null)
                return;

            var oldValue = (IMediaController)e.OldValue;
            var newValue = (IMediaController)e.NewValue;

            if (oldValue != null)
                DetachController(mediaElement);
            
            if (newValue != null)
                AttachController(mediaElement, newValue);
        }

        private static void DetachController(MediaElement mediaElement)
        {
            var adapter = GetMediaAdapter(mediaElement);
            if (adapter != null)
                adapter.Dispose();
            SetMediaAdapter(mediaElement, null);
        }

        private static void AttachController(MediaElement mediaElement, IMediaController controller)
        {
            var adapter = new MediaAdapter(mediaElement, controller);
            SetMediaAdapter(mediaElement, adapter);
        }

        class MediaAdapter : IDisposable
        {
            private readonly MediaElement _mediaElement;
            private readonly IMediaController _controller;

            public MediaAdapter(MediaElement mediaElement, IMediaController controller)
            {
                _mediaElement = mediaElement;
                _controller = controller;

                Initialize();
            }

            private void Initialize()
            {
                _mediaElement.MediaOpened += MediaElementMediaOpened;
                _mediaElement.MediaEnded += MediaElementMediaEnded;
                _mediaElement.MediaFailed += MediaElementMediaFailed;

                _controller.OpenMediaRequested += ControllerOpenMediaRequested;
                _controller.CloseMediaRequested += ControllerCloseMediaRequested;
                _controller.PlayRequested += ControllerPlayRequested;
                _controller.PauseRequested += ControllerPauseRequested;
                _controller.StopRequested += ControllerStopRequested;
                _controller.SeekRequested += ControllerSeekRequested;
                _controller.QueryPosition += ControllerQueryPosition;
                _controller.QueryDuration += ControllerQueryDuration;

                _mediaElement.SetBinding(MediaElement.BalanceProperty, new Binding("Balance") { Source = _controller });
                _mediaElement.SetBinding(MediaElement.VolumeProperty, new Binding("Volume") { Source = _controller });
                _mediaElement.SetBinding(MediaElement.IsMutedProperty, new Binding("IsMuted") { Source = _controller });
            }

            void MediaElementMediaOpened(object sender, RoutedEventArgs e)
            {
                _controller.OnMediaOpened();
            }

            void MediaElementMediaEnded(object sender, RoutedEventArgs e)
            {
                _controller.OnMediaEnded();
            }

            void MediaElementMediaFailed(object sender, ExceptionRoutedEventArgs e)
            {
                _controller.OnMediaFailed(e.ErrorException);
            }

            void ControllerOpenMediaRequested(object sender, MediaOpenEventArgs e)
            {
                _mediaElement.Source = e.MediaUri;
            }

            void ControllerCloseMediaRequested(object sender, EventArgs e)
            {
                _mediaElement.Close();
            }

            void ControllerPlayRequested(object sender, EventArgs e)
            {
                _mediaElement.Play();
            }

            void ControllerPauseRequested(object sender, EventArgs e)
            {
                _mediaElement.Pause();
            }

            void ControllerStopRequested(object sender, EventArgs e)
            {
                _mediaElement.Stop();
            }

            void ControllerSeekRequested(object sender, MediaSeekEventArgs e)
            {
                switch (e.Origin)
                {
                    case System.IO.SeekOrigin.Begin:
                        _mediaElement.Position = e.Offset;
                        break;
                    case System.IO.SeekOrigin.Current:
                        _mediaElement.Position += e.Offset;
                        break;
                    case System.IO.SeekOrigin.End:
                        _mediaElement.Position = _mediaElement.NaturalDuration.TimeSpan - e.Offset;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("e", "Invalid origin");
                }
            }

            void ControllerQueryPosition(object sender, MediaQueryDurationEventArgs e)
            {
                e.Duration = _mediaElement.Position;
                e.Handled = true;
            }

            void ControllerQueryDuration(object sender, MediaQueryDurationEventArgs e)
            {
                e.Duration = _mediaElement.NaturalDuration.TimeSpan;
                e.Handled = true;
            }

            public void Dispose()
            {
                _mediaElement.MediaOpened -= MediaElementMediaOpened;
                _mediaElement.MediaEnded -= MediaElementMediaEnded;
                _mediaElement.MediaFailed -= MediaElementMediaFailed;

                _controller.OpenMediaRequested -= ControllerOpenMediaRequested;
                _controller.CloseMediaRequested -= ControllerCloseMediaRequested;
                _controller.PlayRequested -= ControllerPlayRequested;
                _controller.PauseRequested -= ControllerPauseRequested;
                _controller.StopRequested -= ControllerStopRequested;
                _controller.SeekRequested -= ControllerSeekRequested;
                _controller.QueryPosition -= ControllerQueryPosition;
                _controller.QueryDuration -= ControllerQueryDuration;
            }
        }
    }
}
