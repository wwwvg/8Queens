using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
  
        static void Main(string[] args)
        {
            Queen queen = new Queen();
            queen.SetProhibitedPositions(2);
            queen.printProhibitedPos();
        }
    }

    class Queen
    {
        const int dim = 8;
        bool[,] prohibitedPositions = new bool[dim, dim];

        public void SetProhibitedPositions(int mi)//main index
        {
            for (int y = 0; y < dim; y++)//вертикаль
            {
                for (int x = 0; x < dim; x++)//горизонталь
                {
                    if (x == mi && y+1 < dim) prohibitedPositions[y+1, x] = true; //закрываются вертикальные позиции
                    if (x == mi && x+1+y  < dim && y+1 <dim ) prohibitedPositions[y+1, x+1+y] = true; //1-я горизонталь
                    if (x == mi && x-1-y >= 0 && y+1 < dim) prohibitedPositions[y+1, x-1-y] = true; //2-я горизонталь
                }
            }
        }

        public void printProhibitedPos()
        {
            for (int y = 0; y < dim; y++)
            {
                for (int x = 0; x < dim; x++)
                {
                    string isProhibited = prohibitedPositions[y, x] ? "#" : ".";
                    Console.Write($"{isProhibited}    ");
                }
                Console.WriteLine("\n");
            }
        }
    }
}
