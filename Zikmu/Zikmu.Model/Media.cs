using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Zikmu.Data;

namespace Zikmu.Model
{
    public class Media
    {
        private Media()
        {
        }

        public string Title { get; private set; }
        public string Artist { get; private set; }
        public string Album { get; private set; }
        public uint Track { get; private set; }
        public uint TrackCount { get; private set; }
        public TimeSpan Duration { get; private set; }

        public string FileName { get; private set; }
        public bool IsInfoLoaded { get; private set; }

        public bool ReadMetadataFromFile()
        {
            IsInfoLoaded = false;
            if (File.Exists(FileName))
            {
                using (var file = TagLib.File.Create(FileName))
                {
                    Title = file.Tag.Title;
                    Artist = file.Tag.FirstPerformer;
                    Album = file.Tag.Album;
                    Track = file.Tag.Track;
                    TrackCount = file.Tag.TrackCount;
                    Duration = file.Properties.Duration;
                }
                IsInfoLoaded = true;
            }
            return IsInfoLoaded;
        }

        private bool ReadMetadataFromDB(IMediaRepository repository)
        {
            throw new NotImplementedException();
        }

        public static Media FromFile(string fileName)
        {
            return new Media { FileName = fileName };
        }
    }
}
