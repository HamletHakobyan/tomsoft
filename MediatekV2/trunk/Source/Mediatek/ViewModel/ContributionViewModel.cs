using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mediatek.Entities;

namespace Mediatek.ViewModel
{
    public class ContributionViewModel : MediatekViewModelBase<Contribution>
    {
        public ContributionViewModel(Contribution contribution)
        {
            Model = contribution;
        }

        public string ContributorName
        {
            get { return Model.Person.DisplayName; }
        }

        public string RoleName
        {
            get { return Model.Role.Name; }
        }

        public string MediaTitle
        {
            get { return Model.Media.Title; }
        }

    }
}
