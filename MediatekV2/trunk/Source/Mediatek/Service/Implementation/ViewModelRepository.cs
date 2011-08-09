using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Mediatek.Entities;
using Mediatek.Helpers;
using Mediatek.Messaging;
using Mediatek.Properties;
using Mediatek.ViewModel;
using Developpez.Dotnet.Windows.Service;

namespace Mediatek.Service.Implementation
{
    class ViewModelRepository : IViewModelRepository
    {
        public ViewModelRepository()
        {
            Mediator.Instance.Subscribe<EntityMessage<MediaViewModel>>(MediaMessageHandler);
        }

        #region Medias

        private ObservableCollection<MediaViewModel> _medias;
        public IList<MediaViewModel> Medias
        {
            get
            {
                if (_medias == null)
                {
                    _medias = BuildViewModels(App.Repository.Medias, CreateViewModel);
                }
                return _medias;
            }
        }

        private static MediaViewModel CreateViewModel(Media media)
        {
            if (media is Movie)
            {
                return new MovieViewModel((Movie)media);
            }
            if (media is Album)
            {
                return new AlbumViewModel((Album)media);
            }
            if (media is Book)
            {
                return new BookViewModel((Book)media);
            }
            throw new ArgumentException(Resources.unknown_media_type, "media");
        }

        private void MediaMessageHandler(object sender, EntityMessage<MediaViewModel> message)
        {
            bool saveNeeded = false;
            switch (message.Action)
            {
                case EntityAction.Created:
                    _medias.Add(message.Entity);
                    App.Repository.AddMedia(message.Entity.Model);
                    saveNeeded = true;
                    break;
                case EntityAction.Deleted:
                    _medias.Remove(message.Entity);
                    App.Repository.DeleteObject(message.Entity.Model);
                    saveNeeded = true;
                    break;
                case EntityAction.Modified:
                    saveNeeded = true;
                    break;
            }
            if (saveNeeded)
                App.Repository.SaveChanges();
        }

        #endregion

        #region Persons

        private ObservableCollection<PersonViewModel> _persons;
        public IList<PersonViewModel> Persons
        {
            get
            {
                if (_persons == null)
                {
                    _persons = BuildViewModels(App.Repository.Persons, p => new PersonViewModel(p));
                }
                return _persons;
            }
        }

        #endregion

        #region Loans

        private ObservableCollection<LoanViewModel> _loans;
        public IList<LoanViewModel> Loans
        {
            get
            {
                if (_loans == null)
                {
                    _loans = BuildViewModels(App.Repository.Loans, loan => new LoanViewModel(loan));
                }
                return _loans;
            }
        }

        #endregion

        #region Countries

        private ObservableCollection<CountryViewModel> _countries;
        public IList<CountryViewModel> Countries
        {
            get
            {
                if (_countries == null)
                {
                    _countries = BuildViewModels(App.Repository.Countries, country => new CountryViewModel(country));
                }
                return _countries;
            }
        }


        #endregion

        #region Utility methods

        private ObservableCollection<TViewModel> BuildViewModels<TModel, TViewModel>(IEnumerable<TModel> models, Func<TModel, TViewModel> projection)
        {
            return models.Select(projection).ToObservableCollection();
        }

        #endregion
    }
}