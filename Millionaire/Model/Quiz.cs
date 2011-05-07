using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Millionaire.Util;

namespace Millionaire.Model
{
    public class Quiz
    {
        public Quiz()
        {
            this.Slides = new ChildItemCollection<Quiz, Slide>(this);
            this.Jokers = new List<Joker>();
            this.ContentPath = "";
        }

        [XmlElement("Question", typeof(Question))]
        [XmlElement("Photo", typeof(Photo))]
        [XmlElement("Video", typeof(Video))]
        [XmlElement("StartPage", typeof(StartPage))]
        [XmlElement("SlideShow", typeof(SlideShow))]
        [XmlElement("ScorePage", typeof(ScorePage))]
        public ChildItemCollection<Quiz,Slide> Slides { get; private set; }

        public List<Joker> Jokers { get; set; }

        [XmlIgnore]
        public string ContentPath { get; private set; }

        [XmlArrayItem("Score")]
        public int[] ScoreMap { get; set; }

        public static Quiz Load(string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Quiz));
            using (StreamReader reader = new StreamReader(path))
            {
                Quiz quiz = (Quiz)xs.Deserialize(reader);
                FileInfo f = new FileInfo(path);
                quiz.ContentPath = f.DirectoryName;
                return quiz;
            }
        }

        public static Quiz Load(Stream stream)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Quiz));
            using (StreamReader reader = new StreamReader(stream))
            {
                Quiz quiz = xs.Deserialize(reader) as Quiz;
                return quiz;
            }
        }
    }
}
