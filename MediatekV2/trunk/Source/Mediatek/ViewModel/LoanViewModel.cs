using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet.Windows.ViewModel;
using Mediatek.Entities;
using Mediatek.Service;

namespace Mediatek.ViewModel
{
    public class LoanViewModel : MediatekViewModelBase<Loan>
    {
        #region Properties

        private MediaViewModel _media;
        public MediaViewModel Media
        {
            get
            {
                if (_media == null)
                {
                    var rep = GetService<IViewModelRepository>();
                    _media = rep.Medias.SingleOrDefault(m => m.Id == Model.MediaId);
                }
                return _media;
            }
        }

        private PersonViewModel _person;

        public LoanViewModel(Loan loan)
        {
            this.Model = loan;
        }
        public PersonViewModel Person
        {
            get
            {
                if (_person == null)
                {
                    var rep = GetService<IViewModelRepository>();
                    _person = rep.Persons.SingleOrDefault(p => p.Id == Model.PersonId);
                }
                return _person;
            }
        }

        public DateTime LoanDate
        {
            get { return Model.LoanDate; }
            set { Model.LoanDate = value; }
        }

        public DateTime? ReturnDate
        {
            get { return Model.ReturnDate; }
            set { Model.ReturnDate = value; }
        }

        #endregion

        #region Commands

        #endregion
    }
}
