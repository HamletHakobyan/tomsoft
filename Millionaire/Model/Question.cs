using System.Linq;
using System.Xml.Serialization;

namespace Millionaire.Model
{
    public class Question : Slide
    {
        public int Number { get; set; }

        public string Photo { get; set; }

        public string Text { get; set; }
        
        [XmlArrayItem("Answer")]
        public string[] Answers { get; set; }
        
        public int CorrectAnswer { get; set; }

        [XmlArrayItem("Hide")]
        public int[] FiftyFifty { get; set; }

        [XmlArrayItem("Percentage")]
        public int[] PublicChoice { get; set; }

        public string FriendToCall { get; set; }
    }
}
