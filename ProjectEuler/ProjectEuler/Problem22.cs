using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectEuler
{
    class Problem22 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            var names = LoadNames();
            return names
                    .OrderBy(n => n)
                    .Select((n, i) => (i + 1) * GetAlphaValue(n))
                    .Sum();
        }

        #endregion

        private List<string> LoadNames()
        {
            string inputFile = @"Data\p22_names.txt";
            using (StreamReader reader = new StreamReader(inputFile))
            {
                var allNames = new List<string>();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var names = line.ToUpper().Split(',').Select(n => n.Trim('"'));
                    allNames.AddRange(names);
                }
                return allNames;
            }
        }

        public static long GetAlphaValue(string s)
        {
            return s.Select(c => (long)c - 64).Sum();
        }
    }
}
