using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelTask
{
    class Program
    {
        static void Main(string[] args)
        {
            //Parallel.For(1, 10, Factorial);

           // Console.ReadLine();

            Console.WriteLine("The number of processors " +
    "on this computer is {0}.",
    Environment.ProcessorCount);
        }

        static void Factorial(int x)
        {
            int result = 1;

            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }
            Console.WriteLine($"Выполняется задача {Task.CurrentId}");
            Console.WriteLine($"Факториал числа {x} равен {result}");
            Thread.Sleep(3000);
        }
    }
}
