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
            queen.SetProhibitedPositions(4,4);
            queen.printProhibitedPos();
        }
    }

    class Queen
    {
        const int dim = 8;
        bool[,] prohibitedPositions = new bool[dim, dim];

        public void SetProhibitedPositions(int ciY, int ciX)//current index
        {
            for (int y = 0; y < dim; y++)//вертикаль
            {
                for (int x = 0; x < dim; x++)//горизонталь
                {
                    if (x == ciX && y >= ciY && y+1 < dim) prohibitedPositions[y+1, x] = true; //закрываются вертикальные позиции
                    if (x == ciX && y >= ciY && x+1+y-ciY  < dim && y+1 <dim ) prohibitedPositions[y+1, x+1+y-ciY] = true; //положительная горизонталь
                    if (x == ciX && y >= ciY && x-1-y+ciY >= 0 && y+1 < dim) prohibitedPositions[y+1, x-1-y+ciY] = true; //отрицательная горизонталь
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
