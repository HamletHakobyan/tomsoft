using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Millionaire.Util;
using System.Xml.Serialization;

namespace Millionaire.Model
{
    public abstract class Slide : IChildItem<Quiz>
    {
        [XmlIgnore]
        public Quiz Quiz { get; private set; }

        #region IChildItem<Quiz> Members

        Quiz IChildItem<Quiz>.Parent
        {
            get
            {
                return this.Quiz;
            }
            set
            {
                this.Quiz = value;
            }
        }

        #endregion

    }
}
