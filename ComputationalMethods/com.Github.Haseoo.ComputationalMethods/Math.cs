using System;
using System.Diagnostics;
using System.Linq;

namespace com.Github.Haseoo.ComputationalMethods
{
    public class Math
    {
        public static double Horners(
            double[] polynomialParameters,
            double argument)
        {
            double value = 0;
            polynomialParameters.Take(polynomialParameters.Length - 1)
                .ToList()
                .ForEach(param => value = (value + param) * argument);
            return value + polynomialParameters.Last();
        }
        
        public static double CalculateIntegralTrapezoidal(
            Func<double, double> function,
            double begin,
            double end,
            int accuracy)
        {
            Debug.Assert(begin <= end);
            var h = (end - begin) / accuracy;
            var returnValue = (function(begin) + function(end)) / 2.0;
            while (begin <= end - h)
            {
                returnValue += function(begin += h);
            }
            return returnValue * h;
        }
    }
}