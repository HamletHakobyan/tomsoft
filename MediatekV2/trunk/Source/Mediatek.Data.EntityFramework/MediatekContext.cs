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
            this.ContextOptions.ProxyCreationEnabled = false;
            this.ContextOptions.LazyLoadingEnabled = true;

            _languages = CreateObjectSet<Language>()
                            .Include("Countries")
                            .Include("Medias")
                            .Include("Flag");
            _countries = CreateObjectSet<Country>()
                             .Include("Language")
                             .Include("Persons")
                             .Include("Medias")
                             .Include("Flag");
            _roles = CreateObjectSet<Role>()
                         .Include("Contributions")
                         .Include("Symbol");
            _persons = CreateObjectSet<Person>()
                           .Include("Countries")
                           .Include("Contributions")
                           .Include("Loans")
                           .Include("Picture");
            _medias = CreateObjectSet<Media>()
                          .Include("Language")
                          .Include("Countries")
                          .Include("Contributions")
                          .Include("Loans")
                          .Include("Picture");
            _contributions = CreateObjectSet<Contribution>()
                                 .Include("Media")
                                 .Include("Person")
                                 .Include("Role");
            _loans = CreateObjectSet<Loan>()
                         .Include("Media")
                         .Include("Person");
            _images = CreateObjectSet<Image>();
            _imageData = CreateObjectSet<ImageData>();
            _dbProperties = CreateObjectSet<DbProperties>();
        }

        #endregion

        #region Private data

        private static readonly string _defaultContainerName = "MediatekDataEntities";

        private readonly ObjectQuery<Country> _countries;
        private readonly ObjectQuery<Language> _languages;
        private readonly ObjectQuery<Role> _roles;
        private readonly ObjectQuery<Person> _persons;
        private readonly ObjectQuery<Media> _medias;
        private readonly ObjectQuery<Contribution> _contributions;
        private readonly ObjectQuery<Loan> _loans;
        private readonly ObjectQuery<Image> _images;
        private readonly ObjectQuery<ImageData> _imageData;
        private readonly ObjectQuery<DbProperties> _dbProperties;

        private bool _dbPropertiesLoaded;
        private string _name;
        private string _description;
        private string _culture;

        #endregion

        #region IEntityRepository implementation

        public IQueryable<Language> Languages
        {
            get { return _languages; }
        }

        public IQueryable<Country> Countries
        {
            get { return _countries; }
        }

        public IQueryable<Media> Medias
        {
            get { return _medias; }
        }

        public IQueryable<Role> Roles
        {
            get { return _roles; }
        }

        public IQueryable<Person> Persons
        {
            get { return _persons; }
        }

        public IQueryable<Contribution> Contributions
        {
            get { return _contributions; }
        }

        public IQueryable<Loan> Loans
        {
            get { return _loans; }
        }

        public IQueryable<Image> Images
        {
            get { return _images; }
        }

        public IQueryable<ImageData> ImageData
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

        public void AddContribution(Contribution contribution)
        {
            AddObject(_contributions.Name, contribution);
        }

        public void AddCountry(Country country)
        {
            AddObject(_countries.Name, country);
        }

        public void AddLanguage(Language language)
        {
            AddObject(_languages.Name, language);
        }

        public void AddLoan(Loan loan)
        {
            AddObject(_loans.Name, loan);
        }

        public void AddMedia(Media media)
        {
            AddObject(_medias.Name, media);
        }

        public void AddPerson(Person person)
        {
            AddObject(_persons.Name, person);
        }

        public void AddRole(Role role)
        {
            AddObject(_roles.Name, role);
        }

        public void AddImage(Image image)
        {
            AddObject(_images.Name, image);
        }

        public void AddImageData(ImageData imageData)
        {
            AddObject(_imageData.Name, imageData);
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
                    AddObject(_dbProperties.Name, dbProperties);
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
