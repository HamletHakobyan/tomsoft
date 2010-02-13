using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    class Problem19 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            int nSundays = 0;
            for (int year = 1901; year <= 2000; year++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    if (new DateTime(year, month, 1).DayOfWeek == DayOfWeek.Sunday)
                        nSundays++;
                }
            }
            return nSundays;
        }

        #endregion
    }
}
