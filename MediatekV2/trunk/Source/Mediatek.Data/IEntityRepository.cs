using Mediatek.Entities;
using System.Linq;

namespace Mediatek.Data
{
    public interface IEntityRepository
    {
        IQueryable<Contribution> Contributions { get; }
        IQueryable<Country> Countries { get; }
        IQueryable<Language> Languages { get; }
        IQueryable<Loan> Loans { get; }
        IQueryable<Media> Medias { get; }
        IQueryable<Person> Persons { get; }
        IQueryable<Role> Roles { get; }

        int SaveChanges();
    }
}
