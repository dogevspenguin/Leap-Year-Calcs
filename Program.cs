using System;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Console.WriteLine("Real Year Length (Synodic) Input:");
        double RyL = GetPositiveDoubleInput("RyL");

        Console.WriteLine("Leap Year Length Input:");
        double LyL = GetPositiveDoubleInput("LyL");

        Console.WriteLine("Normal Year Length Input:");
        double NyL = GetPositiveDoubleInput("NyL");

        const double RN = 1e12;  // A very large value approaching infinity
        double bestDiff = double.PositiveInfinity;
        long bestA = 0, bestB = 0, bestC = 0;

        var calculateMethod = Math.Abs(RyL - LyL) < Math.Abs(RyL - NyL)
            ? (Func<long, long, long, double>)((a, b, c) => CalculateExpression(a, b, c, RyL, NyL, LyL, RN))
            : ((a, b, c) => CalculateExpressionReverse(a, b, c, RyL, NyL, LyL, RN));

        object lockObject = new object();
        Parallel.For(1, 1000, a =>
        {
            Console.WriteLine(a);
            for (long b = 1; b < 1000; b++)
            {
                for (long c = 1; c < 1000; c++)
                {
                    double exprValue = calculateMethod(a, b, c);
                    double diff = Math.Abs(exprValue - RyL);

                    lock (lockObject)
                    {
                        if (diff < bestDiff)
                        {
                            bestDiff = diff;
                            bestA = a;
                            bestB = b;
                            bestC = c;
                        }
                    }
                }
            }
        });

        Console.WriteLine($"Optimal values: a = {bestA}, b = {bestB}, c = {bestC}");
        Console.WriteLine($"Minimum difference: {bestDiff}");
    }

    private static double GetPositiveDoubleInput(string paramName)
    {
        while (true)
        {
            Console.Write($"Enter {paramName}: ");
            if (double.TryParse(Console.ReadLine(), out double value) && value > 0)
            {
                return value;
            }
            Console.WriteLine("Invalid input. Please enter a positive number.");
        }
    }

    private static long LCM(long a, long b)
    {
        return Math.Abs(a * b) / GCD(a, b);
    }

    private static long GCD(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    private static double CalculateExpression(long a, long b, long c, double RyL, double NyL, double LyL, double RN)
    {
        long term1 = (long)Math.Floor(RN / a);
        long term15 = (long)Math.Floor(RN / LCM(a, b));
        long term2 = (long)Math.Floor(RN / b);
        long term25 = (long)Math.Floor(RN / LCM(b, c));
        long term3 = (long)Math.Floor(RN / c);
        long term35 = (long)Math.Floor(RN / LCM(a, c));

        double term4 = ((term1 - term15 + term3 - term35)) * NyL + (RN - (term1 - term15 + term3 - term35)) * LyL;
        return term4 / RN;
    }

    private static double CalculateExpressionReverse(long a, long b, long c, double RyL, double NyL, double LyL, double RN)
    {
        long term1 = (long)Math.Floor(RN / a);
        long term15 = (long)Math.Floor(RN / LCM(a, b));
        long term2 = (long)Math.Floor(RN / b);
        long term25 = (long)Math.Floor(RN / LCM(b, c));
        long term3 = (long)Math.Floor(RN / c);
        long term35 = (long)Math.Floor(RN / LCM(a, c));

        double term4 = ((term1 - term15 + term3 - term35)) * LyL + (RN - (term1 - term15 + term3 - term35)) * NyL;
        return term4 / RN;
    }
}