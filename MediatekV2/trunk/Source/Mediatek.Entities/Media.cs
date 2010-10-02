using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mediatek.Entities
{
    public abstract class Media : IEntity
    {
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string OriginalTitle { get; set; }
        public virtual Guid? LanguageId { get; set; }
        public virtual int? Year { get; set; }
        public virtual string Description { get; set; }
        public virtual string Comment { get; set; }
        public virtual byte? Rating { get; set; }
        public virtual Guid? PictureId { get; set; }
        public virtual DateTime? DateAdded { get; set; }

        public virtual Language Language { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
        public virtual ICollection<Contribution> Contributions { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
        public virtual Image Picture { get; set; }
    }
}
