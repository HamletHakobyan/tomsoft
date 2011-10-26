using System;
using System.Linq;
using System.Windows.Input;
using Developpez.Dotnet.Windows.Input;
using Mediatek.Entities;
using Mediatek.Helpers;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Developpez.Dotnet.Windows.Service;
using Mediatek.Messaging;
using System.Windows.Data;
using System.ComponentModel;
using Mediatek.Service;
using Mediatek.ViewModel.Editors;

namespace Mediatek.ViewModel
{
    public abstract class MediaViewModel : MediatekViewModelBase<Media>
    {
        protected MediaViewModel(Media media)
        {
            this.Model = media;
            Mediator.Instance.Subscribe<EntityMessage<ContributionViewModel>>(ContributionMessageHandler);
            Mediator.Instance.Subscribe<EntityMessage<LoanViewModel>>(LoanMessageHandler);
        }

        private static void LoanMessageHandler(object sender, EntityMessage<LoanViewModel> message)
        {
            // TODO
        }

        private void ContributionMessageHandler(object sender, EntityMessage<ContributionViewModel> message)
        {
            switch (message.Action)
            {
                case EntityAction.Created:
                    Contributions.Add(message.Entity);
                    break;
                case EntityAction.Deleted:
                    Contributions.Remove(message.Entity);
                    break;
            }
        }

        #region Properties

        public Guid Id
        {
            get { return Model.Id; }
        }

        public DateTime? DateAdded
        {
            get { return Model.DateAdded; }
            set
            {
                Model.DateAdded = value;
                OnPropertyChanged("DateAdded");
            }
        }

        private BitmapSource _picture;
        public BitmapSource Picture
        {
            get
            {
                if (_picture == null && Model.PictureId.HasValue)
                {
                    Application.Current.Dispatcher.BeginInvoke(
                        () =>
                        {
                            _picture = Model.Picture.GetBitmapSource();
                            OnPropertyChanged("Picture");
                        },
                        DispatcherPriority.Background);
                    return null;
                }
                return _picture;
            }
            set
            {
                if (value != _picture)
                {
                    Model.Picture.SetImageData(value);
                    _picture = value;
                    OnPropertyChanged("Picture");
                }
            }
        }

        public string Title
        {
            get { return Model.Title; }
            set
            {
                if (value != Model.Title)
                {
                    Model.Title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        public string OriginalTitle
        {
            get { return Model.OriginalTitle; }
            set
            {
                if (value != Model.OriginalTitle)
                {
                    Model.OriginalTitle = value;
                    OnPropertyChanged("OriginalTitle");
                }
            }
        }

        public byte Rating
        {
            get { return Model.Rating ?? 0; }
            set
            {
                if (value != (Model.Rating ?? 0))
                {
                    Model.Rating = value;
                    OnPropertyChanged("Rating");
                    OnModified();
                }
            }
        }

        private ObservableCollection<ContributionViewModel> _contributions;
        public ICollection<ContributionViewModel> Contributions
        {
            get
            {
                if (_contributions == null)
                {
                    _contributions =
                        Model.Contributions
                            .Select(c => new ContributionViewModel(c))
                            .ToObservableCollection();
                    var view = CollectionViewSource.GetDefaultView(_contributions);
                    view.SortDescriptions.Add(new SortDescription("RoleName", ListSortDirection.Ascending));
                    view.SortDescriptions.Add(new SortDescription("ContributorName", ListSortDirection.Ascending));
                    view.GroupDescriptions.Add(new PropertyGroupDescription("RoleName"));
                }
                return _contributions;
            }
        }

        private ObservableCollection<LoanViewModel> _loans;
        public ObservableCollection<LoanViewModel> Loans
        {
            get
            {
                if (_loans == null)
                {
                    _loans = Model.Loans
                        .Select(l => new LoanViewModel(l))
                        .ToObservableCollection();
                }
                return _loans;
            }
        }


        #endregion

        #region Commands

        private DelegateCommand _editCommand;
        private DelegateCommand _showMeCommand;

        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                {
                    _editCommand = new DelegateCommand(Edit);
                }
                return _editCommand;
            }
        }

        public ICommand ShowMeCommand
        {
            get
            {
                if (_showMeCommand == null)
                {
                    _showMeCommand = new DelegateCommand(ShowMe);
                }
                return _showMeCommand;
            }
        }

        public int? Year
        {
            get { return Model.Year; }
            set
            {
                if (value != Model.Year)
                {
                    Model.Year = value;
                    OnPropertyChanged("Year");
                }
            }
        }

        protected virtual void Edit()
        {
            var editor = GetEditor();
            if (GetService<IDialogService>().Show(editor) == true)
            {
                OnModified();
            }
        }

        protected virtual void OnModified()
        {
            OnPropertyChanged(string.Empty);
            Mediator.Instance.Post(this, new EntityMessage<MediaViewModel>(this, EntityAction.Modified));
        }

        protected abstract IDialogViewModel GetEditor();

        protected virtual void Delete()
        {
        }

        private void ShowMe()
        {
            Mediator.Instance.Post(this, new NavigationMessage(this));
        }

        #endregion

    }
}
