using System;
using Developpez.Dotnet.Windows.ViewModel;
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

        
    }
}