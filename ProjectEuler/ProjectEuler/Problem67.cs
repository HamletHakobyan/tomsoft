using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ProjectEuler
{
    class Problem67 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            int[][] triangle = LoadTriangle();

            long[][] sumGrid = new long[triangle.Length][];
            for (int i = sumGrid.Length - 1; i >= 0; i--)
            {
                sumGrid[i] = new long[triangle[i].Length];
                for (int j = 0; j < sumGrid[i].Length; j++)
                {
                    sumGrid[i][j] = triangle[i][j];
                    if (i + 1 < sumGrid.Length)
                        sumGrid[i][j] += Math.Max(sumGrid[i + 1][j], sumGrid[i + 1][j + 1]);
                }
            }
            return sumGrid[0][0];
        }

        private int[][] LoadTriangle()
        {
            using (var reader = File.OpenText(@"Data\p67_triangle.txt"))
            {
                var rows = new List<int[]>();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Length == 0)
                        continue;
                    var row = line.Split(' ').Select(s => int.Parse(s)).ToArray();
                    rows.Add(row);
                }
                return rows.ToArray();
            }
        }

        #endregion
    }
}
