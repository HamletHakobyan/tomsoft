using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Mediatek.Entities;
using System.Data.EntityClient;

namespace Mediatek.Data.EntityFramework
{
    public class MediatekContext : ObjectContext, IEntityRepository
    {
        #region Constructors

        public MediatekContext()
            : this("name=MediatekDataEntities", _defaultContainerName)
        {
        }

        public MediatekContext(string connectionString, string defaultContainerName)
            : base(connectionString, defaultContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            _languages = CreateObjectSet<Language>();
            _countries = CreateObjectSet<Country>();
            _roles = CreateObjectSet<Role>();
            _persons = CreateObjectSet<Person>();
            _medias = CreateObjectSet<Media>();
            _contributions = CreateObjectSet<Contribution>();
            _loans = CreateObjectSet<Loan>();
            _images = CreateObjectSet<Image>();
            _imageData = CreateObjectSet<ImageData>();
        }

        #endregion

        #region Private data

        private static readonly string _defaultContainerName = "MediatekDataEntities";

        private ObjectSet<Country> _countries;
        private ObjectSet<Language> _languages;
        private ObjectSet<Role> _roles;
        private ObjectSet<Person> _persons;
        private ObjectSet<Media> _medias;
        private ObjectSet<Contribution> _contributions;
        private ObjectSet<Loan> _loans;
        private ObjectSet<Image> _images;
        private ObjectSet<ImageData> _imageData;

        #endregion

        #region Public properties

        public ObjectSet<Language> Languages
        {
            get { return _languages; }
        }

        public ObjectSet<Country> Countries
        {
            get { return _countries; }
        }

        public ObjectSet<Media> Medias
        {
            get { return _medias; }
        }

        public ObjectSet<Role> Roles
        {
            get { return _roles; }
        }

        public ObjectSet<Person> Persons
        {
            get { return _persons; }
        }

        public ObjectSet<Contribution> Contributions
        {
            get { return _contributions; }
        }

        public ObjectSet<Loan> Loans
        {
            get { return _loans; }
        }

        public ObjectSet<Image> Images
        {
            get { return _images; }
        }

        public ObjectSet<ImageData> ImageData
        {
            get { return _imageData; }
        }


        #endregion

        #region Public methods

        public static MediatekContext GetContext(string providerName, string providerConnectionString)
        {
            var ecsb = new EntityConnectionStringBuilder();
            ecsb.Provider = providerName;
            ecsb.ProviderConnectionString = providerConnectionString;
            ecsb.Metadata = Properties.Resources.EntityMetaData;
            return new MediatekContext(ecsb.ConnectionString, _defaultContainerName);
        }

        #endregion

        #region IMediatekEntityProvider implementation

        IQueryable<Contribution> IEntityRepository.Contributions
        {
            get { return this.Contributions; }
        }

        IQueryable<Country> IEntityRepository.Countries
        {
            get { return this.Countries; }
        }

        IQueryable<Language> IEntityRepository.Languages
        {
            get { return this.Languages; }
        }

        IQueryable<Loan> IEntityRepository.Loans
        {
            get { return this.Loans; }
        }

        IQueryable<Media> IEntityRepository.Medias
        {
            get { return this.Medias; }
        }

        IQueryable<Person> IEntityRepository.Persons
        {
            get { return this.Persons; }
        }

        IQueryable<Role> IEntityRepository.Roles
        {
            get { return this.Roles; }
        }

        #endregion
    }
}
