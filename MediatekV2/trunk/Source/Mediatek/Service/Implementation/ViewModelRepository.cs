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
                    _medias = App.Repository.Medias
                            .Select(CreateViewModel)
                            .ToObservableCollection();
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
            switch (message.Action)
            {
                case EntityAction.Created:
                    _medias.Add(message.Entity);
                    break;
                case EntityAction.Deleted:
                    _medias.Remove(message.Entity);
                    break;
            }
        }

        #endregion
    }
}