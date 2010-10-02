
namespace Mediatek.Entities
{
    public class Album : Media
    {
        public virtual int? Duration { get; set; }
        public virtual int? NumberOfTracks { get; set; }
    }
}
