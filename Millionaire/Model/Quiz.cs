using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Millionaire.Util;
using System.IO;

namespace Millionaire.Model
{
    public class Quiz
    {
        public Quiz()
        {
            this.Slides = new ChildCollection<Quiz, Slide>(this);
            this.Jokers = new List<Joker>();
            this.ContentPath = "";
        }

        [XmlElement("Question", typeof(Question))]
        [XmlElement("Photo", typeof(Photo))]
        [XmlElement("StartPage", typeof(StartPage))]
        [XmlElement("SlideShow", typeof(SlideShow))]
        public ChildCollection<Quiz,Slide> Slides { get; set; }

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
                Quiz quiz = xs.Deserialize(reader) as Quiz;
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
