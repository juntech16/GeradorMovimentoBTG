using System;

namespace GeradorMovimentoBTG.Models
{
    public class BrownianMotionModel
    {
        public static double[] GenerateBrownianMotion(double sigma, double mean, double initialPrice, int numDays)
        {
            Random rand = new();
            double[] prices = new double[numDays];
            prices[0] = initialPrice;

            for (int i = 1; i < numDays; i++)
            {
                double u1 = 1.0 - rand.NextDouble();
                double u2 = 1.0 - rand.NextDouble();
                double z = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);

                double retornoDiario = mean + sigma * z;
                prices[i] = prices[i - 1] * Math.Exp(retornoDiario);
            }

            return prices;
        }


        public static double[][] GenerateMultipleBrownianMotions(double sigma, double mean, double initialPrice, int numDays, int numSimulations)
        {
            double[][] allSimulations = new double[numSimulations][];
            
            for (int sim = 0; sim < numSimulations; sim++)
            {
                allSimulations[sim] = GenerateBrownianMotion(sigma, mean, initialPrice, numDays);
            }

            return allSimulations;
        }
    }
}
