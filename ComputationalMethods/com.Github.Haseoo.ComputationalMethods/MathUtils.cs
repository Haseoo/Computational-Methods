namespace com.Github.Haseoo.ComputationalMethods
{
    public static class MathUtils
    {
        public static double[] MultiplyPolynomial(double[] first, double[] second)
        {
            var result = new double[first.Length + second.Length - 1];
            for (var i = 0; i < first.Length; i++)
            {
                for (var j = 0; j < second.Length; j++)
                {
                    result[i + j] += first[i] * second[j];
                }
            }
            return result;
        }
    }
}