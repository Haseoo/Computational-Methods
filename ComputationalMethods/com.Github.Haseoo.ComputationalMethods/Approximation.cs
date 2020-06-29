using System;
using System.IO;
using System.Linq;

using static com.Github.Haseoo.ComputationalMethods.Approximation.EquationsCoefficients;

namespace com.Github.Haseoo.ComputationalMethods
{
    public class Approximation
    {
        internal enum EquationsCoefficients {
            A = 0, B= 1, D = 1, E = 2, C = 3, F = 4
        }
        
        private static readonly string GNU_PLOT_COMMAND_FORMAT =
            "F(x)={0}\nset xrange[{1}:{2}]\n" +
            "set terminal svg size 800,600 enhanced fname 'arial' butt solid\n" +
            "set output 'out.svg'\n" +
            "set key inside bottom right\n" +
            "set xlabel 'x'\n" +
            "set ylabel 'y'\n" +
            "set title 'Approximation'\n" +
            "plot F(x) notitle, \"{3}\" using 1:2 notitle";

        private double[][] Points { get; }
        private double[] FunctionCoefficients { get; }

        public Approximation(double[][] points)
        {
            this.Points = points;
            FunctionCoefficients = new double[2];
            var s = new double[5];
            for (var i = 0; i < Points[0].Length; ++i)
            {
                ++s[0];
                s[1] += Points[0][i];
                s[2] += Points[0][i] * Points[0][i];
                s[3] += Points[1][i];
                s[4] += Points[0][i] * Points[1][i];
            }

            var w = s[(int) A] * s[(int) E] - s[(int) B] * s[(int) D];
            var wy = s[(int) C] * s[(int) E] - s[(int) B] * s[(int) F];
            var wx = s[(int) A] * s[(int) F] - s[(int) C] * s[(int) D];
            FunctionCoefficients[0] = wx / w;
            FunctionCoefficients[1] = wy / w;
        }

        public string GetApproximationFunction()
        {
            return $"{FunctionCoefficients[0]}*x+{FunctionCoefficients[1]}";
        }

        public void WritePoints(string dataFilePath, string gnuPlotFilePath)
        {
            using StreamWriter data = new StreamWriter(dataFilePath, false);
            for (var i = 0; i < Points[0].Length; ++i)
            {
                data.WriteLine("{0}\t{1}", Points[0][i], Points[1][i]);
            }

            using var commands = new StreamWriter(gnuPlotFilePath, false);
            commands.Write(string.Format(GNU_PLOT_COMMAND_FORMAT, GetApproximationFunction(),
                Points[0].Min() - 1, Points[0].Max() + 1, dataFilePath));
            ;
        }
    }
}