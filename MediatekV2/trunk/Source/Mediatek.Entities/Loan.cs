using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediatek.Entities
{
    public class Loan : IEntity
    {
        public virtual Guid Id { get; set; }
        public virtual Guid MediaId { get; set; }
        public virtual Guid PersonId { get; set; }
        public virtual DateTime LoanDate { get; set; }
        public virtual DateTime? ReturnDate { get; set; }
        public virtual string Comment { get; set; }

        public virtual Media Media { get; set; }
        public virtual Person Person { get; set; }
    }
}
