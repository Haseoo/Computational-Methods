using System;
using System.Linq;

namespace com.Github.Haseoo.ComputationalMethods
{
    public class Math
    {
        public static double Horners(double[] polynomialParameters,
            double argument)
        {
            double value = 0;
            polynomialParameters.Take(polynomialParameters.Length - 1)
                .ToList()
                .ForEach(param => value = (value + param) * argument);
            return value + polynomialParameters.Last();
        }
    }
}