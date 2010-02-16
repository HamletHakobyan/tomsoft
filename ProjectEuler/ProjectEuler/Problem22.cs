﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Developpez.Dotnet.IO;

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
                    .Select((n, i) => (i + 1) * Util.GetAlphaValue(n))
                    .Sum();
        }

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

        #endregion
    }
}
