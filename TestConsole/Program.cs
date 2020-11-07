using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Queen queen = new Queen(8);
            queen.FindQueens();

            Console.WriteLine($"Найдено решений: {queen.GetQueensCount()}");
            Console.WriteLine($"Всего комбинаций: {queen.GetAllCombinationsCount()}");
            Console.WriteLine($"Затраченное время: {queen.GetSearchTime()} секунд");

            int i = 0;
            foreach (var list in queen.GetFoundQueens())
            {
                Console.Write($"{++i}) ");
                foreach (var item in list)
                {
                    Console.Write($"{item} ");
                }
                Console.WriteLine("\n");
            }
        }
    }
}
