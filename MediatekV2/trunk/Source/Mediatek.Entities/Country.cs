using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediatek.Entities
{
    public class Country
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual byte[] Flag { get; set; }
        public virtual bool Predefined { get; set; }
            
        public virtual ICollection<Language> Languages { get; set; }
        public virtual ICollection<Person> Persons { get; set; }
        public virtual ICollection<Media> Medias { get; set; }
    }
}
