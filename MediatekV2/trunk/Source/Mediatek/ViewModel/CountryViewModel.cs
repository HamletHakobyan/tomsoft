using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Developpez.Dotnet.Windows.Input;
using Mediatek.Entities;
using Mediatek.Helpers;
using Mediatek.Properties;
using Mediatek.Service;

namespace Mediatek.ViewModel
{
    public class CountryViewModel : MediatekViewModelBase<Country>
    {
        public CountryViewModel()
        {
            Model = new Country();
        }

        public CountryViewModel(Country country)
        {
            Model = country;
        }

        public string Name
        {
            get { return Model.Name; }
            set
            {
                if (value != Model.Name)
                {
                    Model.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private BitmapSource _flag;
        public BitmapSource Flag
        {
            get
            {
                if (_flag == null && Model.FlagId.HasValue)
                {
                    Application.Current.Dispatcher.BeginInvoke(
                        () =>
                        {
                            _flag = Model.Flag.GetBitmapSource();
                            OnPropertyChanged("Flag");
                        },
                        DispatcherPriority.Background);
                    return null;
                }
                return _flag;
            }
            set
            {
                if (value != _flag)
                {
                    Model.Flag.SetImageData(value);
                    _flag = value;
                    OnPropertyChanged("Flag");
                }
            }
        }

        private DelegateCommand _chooseFlagCommand;
        public ICommand ChooseFlagCommand
        {
            get
            {
                if (_chooseFlagCommand == null)
                {
                    _chooseFlagCommand = new DelegateCommand(ChooseFlag);
                }
                return _chooseFlagCommand;
            }
        }

        private void ChooseFlag()
        {
            var service = GetService<IFileDialogService>();
            string fileName = null;
            var options = new FileDialogOptions { Filter = Resources.filadialog_image_filter };
            if (service.ShowOpen(ref fileName, options) == true)
            {
                var flag = Model.Flag ?? new Image();
                flag.SetImageData(fileName);
                flag.Name = Path.GetFileName(fileName);
            }
        }
    }
}
