using System;
using System.IO;

namespace Zikmu.Behaviors
{
    public interface IMediaController
    {
        event EventHandler PlayRequested;
        event EventHandler PauseRequested;
        event EventHandler StopRequested;
        event EventHandler CloseMediaRequested;
        event EventHandler<MediaOpenEventArgs> OpenMediaRequested;
        event EventHandler<MediaSeekEventArgs> SeekRequested;
        event EventHandler<MediaQueryDurationEventArgs> QueryPosition;
        event EventHandler<MediaQueryDurationEventArgs> QueryDuration;

        void OnMediaOpened();
        void OnMediaEnded();
        void OnMediaFailed(Exception exception);

        double Volume { get; }
        double Balance { get; }
        bool IsMuted { get; }
    }

    public class MediaSeekEventArgs : EventArgs
    {
        private readonly SeekOrigin _origin;
        private readonly TimeSpan _offset;

        public MediaSeekEventArgs(SeekOrigin origin, TimeSpan offset)
        {
            _origin = origin;
            _offset = offset;
        }

        public TimeSpan Offset
        {
            get { return _offset; }
        }

        public SeekOrigin Origin
        {
            get { return _origin; }
        }
    }

    public class MediaQueryDurationEventArgs : EventArgs
    {
        public TimeSpan Duration { get; set; }
        public bool Handled { get; set; }
    }

    public class MediaOpenEventArgs : EventArgs
    {
        private readonly Uri _mediaUri;

        public MediaOpenEventArgs(Uri mediaUri)
        {
            _mediaUri = mediaUri;
        }

        public Uri MediaUri
        {
            get { return _mediaUri; }
        }
    }
}
