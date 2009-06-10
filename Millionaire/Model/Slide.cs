using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Millionaire.Util;
using System.Xml.Serialization;

namespace Millionaire.Model
{
    public abstract class Slide : IChild<Quiz>
    {
        [XmlIgnore]
        public Quiz Quiz { get; set; }

        #region IChild<Quiz> Members

        Quiz IChild<Quiz>.Parent
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
