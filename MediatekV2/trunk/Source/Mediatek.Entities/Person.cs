using System;
using System.Collections.Generic;

namespace Mediatek.Entities
{
    public class Person : IEntity
    {
        public virtual Guid Id { get; set; }
        public virtual bool IsGroup { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string NickName { get; set; }
        public virtual DateTime? DateOfBirth { get; set; }
        public virtual DateTime? DateOfDeath { get; set; }
        public virtual string Biography { get; set; }
        public virtual string Comment { get; set; }
        public virtual Guid? PictureId { get; set; }

        public virtual ICollection<Country> Countries { get; set; }
        public virtual ICollection<Contribution> Contributions { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
        public virtual Image Picture { get; set; }

    }
}
