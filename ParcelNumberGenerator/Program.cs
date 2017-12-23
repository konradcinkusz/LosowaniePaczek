using ParcelNumberGenerator.Base;
using ParcelNumberGenerator.OthersImplementations;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelNumberGenerator
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("How many numbers do you want to draw?");
            string numbersCountToDrawText = Console.ReadLine();
            int numbersCount = 0;
            Int32.TryParse(numbersCountToDrawText, out numbersCount);
            double fastest = double.MaxValue;
            string log = string.Empty;
            List<INumberPoolGenerator> listOfGenerators = new List<INumberPoolGenerator>();
            listOfGenerators.Add(new NumberPoolDBv2());
            listOfGenerators.Add(new NumberPoolDatabase());
            listOfGenerators.Add(new NumberPoolDBFunc());
            listOfGenerators.Add(new NumberPoolDBv2WithRangeOff(new Tuple<int, int>(30000, 40000)));
            listOfGenerators.Add(new NumberPoolDBv2WithUBS() { Mode = Mode.Iterative });
            listOfGenerators.Add(new NumberPoolDBv2WithUBS() { Mode = Mode.Recursive });
            listOfGenerators.ForEach(numberPoolGenerator =>
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                for (int i = 0; i < numbersCount; i++)
                    numberPoolGenerator.Generate();
                stopWatch.Stop();
                string message = $"{numberPoolGenerator.GetType().Name}:\t{stopWatch.Elapsed}\t{numberPoolGenerator.Mode}";
                if (fastest > stopWatch.Elapsed.TotalMilliseconds)
                {
                    fastest = stopWatch.Elapsed.TotalMilliseconds;
                    log = message;
                }                
                Console.WriteLine(message);
            });
            Console.WriteLine($"The fastest representation is {log}");
        }
    }
}
