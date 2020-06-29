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
        
        public static double[,] GetDiffTable(double[] x, double[] y)
        {
            var diffTable = new double[x.Length, y.Length];
            for (var i = 0; i < y.Length; i++)
            {
                diffTable[i, 0] = y[i];
            }
            for (var i = 1; i <= y.Length - 1; i++)
            {
                for (var j = y.Length - 1; j >= i; j--)
                {
                    diffTable[j, i] = diffTable[j, i - 1] - diffTable[j - 1, i - 1];
                }
            }
            return diffTable;
        }
    }
}