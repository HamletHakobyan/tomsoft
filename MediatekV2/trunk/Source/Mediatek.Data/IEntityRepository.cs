using Mediatek.Entities;
using System.Linq;
using System;
using System.Linq.Expressions;

namespace Mediatek.Data
{
    public interface IEntityRepository : IDisposable
    {
        IQueryable<Contribution> Contributions { get; }
        IQueryable<Country> Countries { get; }
        IQueryable<Language> Languages { get; }
        IQueryable<Loan> Loans { get; }
        IQueryable<Media> Medias { get; }
        IQueryable<Person> Persons { get; }
        IQueryable<Role> Roles { get; }
        IQueryable<Image> Images { get; }
        IQueryable<ImageData> ImageData { get; }

        string Name { get; set; }
        string Description { get; set; }
        string Culture { get; set; }

        void LoadProperty(object entity, string navigationProperty);
        void LoadProperty<TEntity>(TEntity entity, Expression<Func<TEntity, object>> selector);
        int SaveChanges();

        void AddContribution(Contribution contribution);
        void AddCountry(Country country);
        void AddLanguage(Language language);
        void AddLoan(Loan loan);
        void AddMedia(Media media);
        void AddPerson(Person person);
        void AddRole(Role role);
        void AddImage(Image image);
        void AddImageData(ImageData imageData);

        void DeleteObject(object entity);
    }
}
