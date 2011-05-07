using System.Linq;
using System.Xml.Serialization;
using Millionaire.Util;

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
