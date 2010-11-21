using Developpez.Dotnet.Windows.ViewModel;

namespace Velib.ViewModel
{
    public class StationStatusViewModel : ViewModelBase
    {
        public StationStatusViewModel(StationStatus status)
        {
            this._status = status;
        }

        private StationStatus _status;

        public string Ticket
        {
            get { return _status.Ticket ? "Oui" : "Non"; }
        }

        public int FreeSlots
        {
            get { return _status.FreeSlots; }
        }

        public int AvailableBikes
        {
            get { return _status.AvailableBikes; }
        }

        public int TotalSlots
        {
            get { return _status.TotalSlots; }
        }
    }
}
