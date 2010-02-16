using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ProjectEuler
{
    class Problem42 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            var triangleNumbers = new SortedSet<long>(Enumerable.Range(1, 50).Select(n => (long)n * (n - 1) / 2));
            var words = LoadWords();
            return words.Count(w => triangleNumbers.Contains(Util.GetAlphaValue(w)));
        }

        #endregion

        private List<string> LoadWords()
        {
            string inputFile = @"Data\p42_words.txt";
            using (StreamReader reader = new StreamReader(inputFile))
            {
                var allWords = new List<string>();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var words = line.ToUpper().Split(',').Select(n => n.Trim('"'));
                    allWords.AddRange(words);
                }
                return allWords;
            }
        }
    }
}
