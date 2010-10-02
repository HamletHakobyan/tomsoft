using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediatek.Entities
{
    public class Language : IEntity
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual Guid? FlagId { get; set; }
        public virtual bool Predefined { get; set; }

        public virtual ICollection<Media> Medias { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
        public virtual Image Flag { get; set; }

    }
}
