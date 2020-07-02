using System;
using System.Diagnostics;
using System.Linq;

using static com.Github.Haseoo.ComputationalMethods.MathUtils;
using static System.Math;

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
        
        public static double TaylorDifferentiation(double[] x, double[] y, double h)
        {
            var diffTable = GetDiffTable(x, y);
            var retVal = 0.0;
            for (var i = 1; i < y.Length; i++)
            {
                retVal += (1.0 / i) * diffTable[y.Length - 1, i];
            }
            return retVal * (1.0 / h);
        }
        
        public static double[] GaussElimination(double[,] inMatrix) {
            var n = inMatrix.GetLength(0);
            var retVal = new double[n];
            for (var i = 0; i < n - 1; i++) {
                for (var j = i + 1; j < n; j++) {
                    if (Abs(inMatrix[i,i]) < 0.0) return null;
                    var m = -inMatrix[j, i] / inMatrix[i, i];
                    for (var k = i + 1; k <= n; k++)
                        inMatrix[j, k] += m * inMatrix[i, k];
                }
            }
            for (var i = n - 1; i >= 0; i--) {
                var s = inMatrix[i, n];
                for (var j = n - 1; j >= i + 1; j--)
                    s -= inMatrix[i, j] * retVal[j];
                if (Abs(inMatrix[i, i]) < 0.0) return null;
                retVal[i] = s / inMatrix[i, i];
            }
            return retVal;
        }
        
        public static double? FindRootBisection(Func<double, double> f, double a, double b, double eps) {
            if (f(a) * f(b) > 0) {
                return null;
            }
            var c = 0.0;
            while (b - a > eps) {
                c = (a + b) / 2;
                if (f(a) * f(c) < 0)
                    b = c;
                else
                    a = c;
            }
            return c;
        }
    }
}