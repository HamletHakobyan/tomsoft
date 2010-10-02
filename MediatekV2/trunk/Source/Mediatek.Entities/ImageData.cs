using System;

namespace Mediatek.Entities
{
    public class ImageData : IEntity
    {
        public virtual Guid ImageId { get; set; }
        public virtual byte[] Bytes { get; set; }

        public virtual Image Image { get; set; }
    }
}
