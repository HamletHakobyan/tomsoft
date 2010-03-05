using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediatek.Entities
{
    public class Contribution
    {
        public virtual Guid MediaId { get; set; }
        public virtual Guid PersonId { get; set; }
        public virtual Guid RoleId { get; set; }
        public virtual string Comment { get; set; }

        public virtual Media Media { get; set; }
        public virtual Person Person { get; set; }
        public virtual Role Role { get; set; }
    }
}
