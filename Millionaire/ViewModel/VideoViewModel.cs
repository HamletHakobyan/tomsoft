using System;
using System.IO;
using Millionaire.Model;

namespace Millionaire.ViewModel
{
    public class VideoViewModel : SlideViewModel
    {
        private readonly Video _video;
        private readonly Uri _videoUri;

        public VideoViewModel(Video video, GameViewModel game) : base(game)
        {
            _video = video;
            if (!String.IsNullOrEmpty(_video.Quiz.ContentPath))
            {
                if (!String.IsNullOrEmpty(_video.Path))
                {
                    string fullPath = Path.Combine(_video.Quiz.ContentPath, _video.Path);
                    _videoUri = new Uri(fullPath);
                }
                if (!String.IsNullOrEmpty(_video.SoundPath))
                {
                    string fullPath = Path.Combine(_video.Quiz.ContentPath, _video.SoundPath);
                    this.Sound = new Uri(fullPath);
                }
            }
        }

        public Uri Video
        {
            get { return _videoUri; }
        }
    }
}