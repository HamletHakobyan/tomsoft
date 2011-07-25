using System;
using Mediatek.Entities;
using Developpez.Dotnet;

namespace Mediatek.ViewModel
{
    public class PersonViewModel : MediatekViewModelBase<Person>
    {
        public PersonViewModel(Person person)
        {
            Model = person;
        }

        public Guid Id
        {
            get { return Model.Id; }
        }

        public string PersonDisplayName
        {
            get
            {
                if (!Model.DisplayName.IsNullOrEmpty())
                    return Model.DisplayName;
                if (!Model.FirstName.IsNullOrEmpty() && !Model.LastName.IsNullOrEmpty())
                    return string.Format("{0} {1}", Model.FirstName, Model.LastName);
                if (!Model.FirstName.IsNullOrEmpty())
                    return Model.FirstName;
                if (!Model.LastName.IsNullOrEmpty())
                    return Model.LastName;
                return string.Empty;
            }
        }

        public string FirstName
        {
            get { return Model.FirstName; }
            set
            {
                if (value != Model.FirstName)
                {
                    Model.FirstName = value;
                    OnPropertyChanged("FirstName");
                }
            }
        }

        public string LastName
        {
            get { return Model.LastName; }
            set
            {
                if (value != Model.LastName)
                {
                    Model.LastName = value;
                    OnPropertyChanged("LastName");
                }
            }
        }

        public new string DisplayName
        {
            get { return Model.DisplayName; }
            set
            {
                if (value != Model.DisplayName)
                {
                    Model.DisplayName = value;
                    OnPropertyChanged("DisplayName");
                }
            }
        }

        public string NickName
        {
            get { return Model.NickName; }
            set
            {
                if (value != Model.NickName)
                {
                    Model.NickName = value;
                    OnPropertyChanged("NickName");
                }
            }
        }

        public bool IsGroup
        {
            get { return Model.IsGroup; }
            set
            {
                if (value != Model.IsGroup)
                {
                    Model.IsGroup = value;
                    OnPropertyChanged("IsGroup");
                }
            }
        }

    }
}