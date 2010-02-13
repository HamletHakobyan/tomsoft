using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Developpez.Dotnet;

namespace ProjectEuler
{
    class Problem15 : IEulerProblem
    {
        #region IEulerProblem Members

        public object GetSolution()
        {
            return NumberOfRoutes(20, 20);
        }

        #endregion

        Func<int, int, long> _numberOfRoutesCached = null;
        long NumberOfRoutes(int width, int height)
        {
            if (_numberOfRoutesCached == null)
            {
                Func<int, int, long> numberOfRoutes = NumberOfRoutes;
                _numberOfRoutesCached = numberOfRoutes.AsCached();
            }

            if (width == 0 || height == 0)
                return 1;
            long n = 0;
            if (width > 0)
                n += _numberOfRoutesCached(width - 1, height);
            if (height > 0)
                n += _numberOfRoutesCached(width, height - 1);
            return n;
        }
    }
}
