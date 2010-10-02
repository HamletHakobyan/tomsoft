using System;

namespace Mediatek.Entities
{
    public class Image : IEntity
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Comment { get; set; }

        public virtual ImageData Data { get; set; }
    }
}
