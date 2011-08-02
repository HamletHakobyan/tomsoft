using System;

namespace PasteBinSharp
{
    public class PasteBinListEntry
    {
        public string Key { get; internal set; }
        public Uri Url { get; internal set; }

        public string Title { get; internal set; }
        public string LongFormat { get; internal set; }
        public string ShortFormat { get; internal set; }
        public bool Private { get; internal set; }
        public DateTime CreationDate { get; internal set; }
        public DateTime? ExpirationDate { get; internal set; }
        public int Size { get; internal set; }
        public int Hits { get; internal set; }
    }
}