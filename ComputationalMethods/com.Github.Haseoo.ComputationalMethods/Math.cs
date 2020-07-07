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
        
        private static double Fibonacci(int n) {
            var a = 0;
            var b = 1;
            for (var i = 0; i < n; i++)
            {
                var temp = a;
                a = b;
                b = temp + b;
            }
            return a;
        }
        
        public static double FindMin(Func<double, double> f, double a, double b, double eta) {
            var max = (b - a) / eta;
            var k = 0.0;
            var it = 0;
            while(true)
            {
                if (Fibonacci(it) > max) {
                    k = it;
                    break;
                }
                it++;
            }
            var c = b - (Fibonacci((int)(k - 1)) / Fibonacci((int)k)) * (b - a);
            var d = a + b - c;
            for (var i = 0; i < k - 4; i++) {
                if (f(c) < f(d)) {
                    b = d;
                } else {
                    a = c;
                }
                c = b - (Fibonacci((int)(k - i - 2)) / Fibonacci((int)(k - i - 1))) * (b - a);
                d = a + b - c;
            }
            return c;
        }

        public static double[] RunyKutty(Func<double, double, double> f, double x0, double xn, double y0, double h)
        {
            var n = (int) (System.Math.Abs(x0 - xn) / h + 1);
            var retVal = new double[n];
            var y = y0;
            var x = x0;

            for (var i = 0; i < n; ++i)
            {
                var k = h * f(x, y);
                var dy = h;
                k = h * f((x + 0.5) * h, (y + 0.5) * k);
                dy += 2 * k;
                k = h * f((x + 0.5) * h, (y + 0.5) * k);
                dy += 2 * k;
                k = h * f(x + h, y + k);
                dy += k;
                dy /= 6.0;
                retVal[i] = dy;
                y += dy;
                x += h;
            }
            return retVal;
        }
    }
}