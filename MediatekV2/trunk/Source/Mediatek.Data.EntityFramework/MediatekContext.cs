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
            _dbProperties = CreateObjectSet<DbProperties>();
        }

        #endregion

        #region Private data

        private static readonly string _defaultContainerName = "MediatekDataEntities";

        private readonly ObjectSet<Country> _countries;
        private readonly ObjectSet<Language> _languages;
        private readonly ObjectSet<Role> _roles;
        private readonly ObjectSet<Person> _persons;
        private readonly ObjectSet<Media> _medias;
        private readonly ObjectSet<Contribution> _contributions;
        private readonly ObjectSet<Loan> _loans;
        private readonly ObjectSet<Image> _images;
        private readonly ObjectSet<ImageData> _imageData;
        private readonly ObjectSet<DbProperties> _dbProperties;

        private bool _dbPropertiesLoaded;
        private string _name;
        private string _description;
        private string _culture;

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

        public string Name
        {
            get
            {
                EnsureDbPropertiesLoaded();
                return _name;
            }
            set
            {
                EnsureDbPropertiesLoaded();
                _name = value;
            }
        }

        public string Description
        {
            get
            {
                EnsureDbPropertiesLoaded();
                return _description;
            }
            set
            {
                EnsureDbPropertiesLoaded();
                _description = value;
            }
        }

        public string Culture
        {
            get
            {
                EnsureDbPropertiesLoaded();
                return _culture;
            }
            set
            {
                EnsureDbPropertiesLoaded();
                _culture = value;
            }
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

        public override int SaveChanges(SaveOptions options)
        {
            if (_dbPropertiesLoaded)
            {
                var dbProperties = _dbProperties.SingleOrDefault();
                if (dbProperties == null)
                {
                    dbProperties = CreateObject<DbProperties>();
                    dbProperties.Id = 1;
                    dbProperties.Name = _name;
                    dbProperties.Description = _description;
                    dbProperties.Culture = _culture;
                    _dbProperties.AddObject(dbProperties);
                }
                else
                {
                    dbProperties.Name = _name;
                    dbProperties.Description = _description;
                    dbProperties.Culture = _culture;
                }
            }
            return base.SaveChanges(options);
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

        IQueryable<Image> IEntityRepository.Images
        {
            get { return this.Images; }
        }

        IQueryable<ImageData> IEntityRepository.ImageData
        {
            get { return this.ImageData; }
        }

        #endregion

        #region Private methods

        private void EnsureDbPropertiesLoaded()
        {
            if (!_dbPropertiesLoaded)
            {
                var dbProperties = _dbProperties.SingleOrDefault();
                if (dbProperties != null)
                {
                    _name = dbProperties.Name;
                    _description = dbProperties.Description;
                    _culture = dbProperties.Culture;
                }
                _dbPropertiesLoaded = true;
            }
        }

        #endregion
    }

    public class DbProperties
    {
        public virtual short Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Culture { get; set; }
    }
}
