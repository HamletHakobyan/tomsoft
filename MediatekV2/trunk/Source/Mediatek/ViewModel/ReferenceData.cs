using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Mediatek.Service;

namespace Mediatek.ViewModel
{
    public class ReferenceData : MediatekViewModelBase
    {
        public IList<CountryViewModel> Countries
        {
            get
            {
                var rep = GetService<IViewModelRepository>();
                return rep.Countries;
            }
        }

    }
}
