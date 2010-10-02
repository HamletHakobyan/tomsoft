using System.Collections.Generic;
using System.Linq;
using System.Text;
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
