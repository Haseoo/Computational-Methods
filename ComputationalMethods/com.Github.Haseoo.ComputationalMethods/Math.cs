using System;
using System.Diagnostics;
using System.Linq;

using static com.Github.Haseoo.ComputationalMethods.MathUtils;

namespace com.Github.Haseoo.ComputationalMethods
{
    public static class Math
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
        
        public static double[] LagrangeInterpolation(double[] x, double[] y)
        {
            var result = new double[x.Length];
            for (var i = 0; i < x.Length; i++)
            {
                double[] w = { 1.0 };
                for (var j = 0; j < x.Length; j++)
                {
                    if (j == i) continue;
                    w = MultiplyPolynomial(w, new []{ 1.0, -x[j] });
                    for(var k = 0; k < w.Length; k++)
                    {
                        w[k] = w[k] / (x[i] - x[j]);
                    }
                }
                for (var k = 0; k < w.Length; k++)
                {
                    result[k] += y[i] * w[k];
                }
            }
            return result;
        }
    }
}