using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediatek.Entities
{
    public class Role
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual bool Predefined { get; set; }
        public Guid? SymbolId { get; set; }

        public virtual ICollection<Contribution> Contributions { get; set; }
        public virtual Image Symbol { get; set; }
    }
}
