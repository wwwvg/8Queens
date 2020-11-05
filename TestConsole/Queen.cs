using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    struct Pair
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    class Queen
    {
        bool[,] prohibitedPositions;
        TreeNode<Pair> queensPositions;

        int dim;//размерность

        public Queen(int dim)
        {
            prohibitedPositions = new bool[dim, dim];
            this.dim = dim;
        }
        /*--------------------------------------------------обнуление всех закрытых позиций-------------------------------------------------------------------------------------------*/
        void resetProhibitedPositions()//
        {
            for (int y = 0; y < dim; y++)//вертикаль
            {
                for (int x = 0; x < dim; x++)//горизонталь
                {
                    prohibitedPositions[y, x] = false;
                }
            }
        }
        /*--------------------------------------------------расстановка фигур-------------------------------------------------------------------------------------------*/
        public void SetQueens()
        {



            //for (int y = 0; y < dim; y++)//вертикаль
            //{
            //    for (int x = 0; x < dim; x++)//горизонталь
            //    {
            //        var (allowedFound, posY, posX) = nextAllowedPosition();
            //        if (allowedFound)
            //        {
            //            queensPositions.Add(new Pair { Y = posY, X = posX });
            //            queensPositionsForPrint[posY, posX] = true;
            //            SetProhibitedPositions(posY, posX);
            //            PrintProhibitedPositions();
            //            continue;
            //        }
            //    }
            //}


        }

        /*--------------------------------------------------следущая доступная позиция-------------------------------------------------------------------------------------------*/
        (bool, int, int) nextAllowedPosition()
        {
            for (int y = 0; y < dim; y++)//вертикаль
            {
                for (int x = 0; x < dim; x++)//горизонталь
                {
                    if (!prohibitedPositions[y, x]) Console.WriteLine($"{y} {x}");

                    if (!prohibitedPositions[y, x]) return (true, y, x);
                }
            }
            return (false, -1, -1);
        }
        /*--------------------------------------------------все доступные позиции в ряду-------------------------------------------------------------------------------------------*/
        List<Pair> getAllowedPositionsInRow(int y)
        {
            var allowedPositions = new List<Pair>();

            for (int x = 0; x < dim; x++)//горизонталь
            {
                if (!prohibitedPositions[y, x])
                {
                    var pair = new Pair() { Y = y, X = x };
                    allowedPositions.Add(pair);
                    //Console.WriteLine($"{y} {x}");
                }
            }
            return allowedPositions;
        }
        /*--------------------------------------------------закрыть позиции которые бьет фигура-------------------------------------------------------------------------------------*/
        public void SetProhibitedPositions(int ciY, int ciX)
        {
            prohibitedPositions[ciY, ciX] = true;

            for (int y = 0; y < dim; y++)
                prohibitedPositions[y, ciX] = true;

            for (int x = 0; x < dim; x++)
                prohibitedPositions[ciY, x] = true;

            for (int y = 0; y < dim; y++)//вертикаль
            {
                for (int x = 0; x < dim; x++)//горизонталь
                {
                    if (x == ciX && y >= ciY && x + 1 + y - ciY < dim && y + 1 < dim) prohibitedPositions[y + 1, x + 1 + y - ciY] = true; //закрывается положительная диагональ
                    if (x == ciX && y >= ciY && x - 1 - y + ciY >= 0 && y + 1 < dim) prohibitedPositions[y + 1, x - 1 - y + ciY] = true; //закрывается отрицательная диагональ
                }
            }
        }
        /*--------------------------------------------------печать закрытых позиций-------------------------------------------------------------------------------------*/
        public void PrintProhibitedPositions()
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
            Console.WriteLine("\n");
        }

        /*--------------------------------------------------печать ферзей-------------------------------------------------------------------------------------*/
        public void PrintQueensPositions()
        {
            
        }
    }
}
