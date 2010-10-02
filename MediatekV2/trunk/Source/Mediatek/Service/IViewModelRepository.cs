using System.Collections.Generic;
using Mediatek.ViewModel;

namespace Mediatek.Service
{
    interface IViewModelRepository
    {
        IList<MediaViewModel> Medias { get; }
        IList<PersonViewModel> Persons { get; }
        IList<LoanViewModel> Loans { get; }
    }
}
