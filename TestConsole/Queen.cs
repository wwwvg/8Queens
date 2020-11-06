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
    class NodeData
    {
        public List<Pair> ProhibitedPositions = new List<Pair>();
        public Pair Pair { get; set; }

    }

    class Queen
    {
        TreeNode<NodeData> allPositions = new TreeNode<NodeData>(new NodeData());
        int dim;//размерность

        public Queen(int dim)
        {
            this.dim = dim;
        }

        /*  1) Каждый узел изначально получает список запрещенных позиций
            2) На основании этого списка получает разрешенные позиции в след. ряду
            3) Делает ходы по всем позициям (записывает позицию в свои данные)
            4) Добавляет на основании своего хода запрещенные позиции и передает их дочернему узлу
        
            1) Создаем лес из dim узлов, где узлы расставляеются по-порядку
            2) Делают ход (записывает позицию в свои данные)
            3) Записывают запрещенные позиции
            4) Получают доступные позиции для детей
            5) Создают детей, в соответствии с доступными позициями. Детям передают координаты и список запрещенных позиций
            6) Дети уже сами смотрят возможные ходы на основании полученного списка запрещенных позиций
        */

        public void FindQueens()
        {
            int y = 0;
            var forest = new List<TreeNode<NodeData>>(dim);
            for (int x = 0; x < dim; x++)
            {
                var pair = new Pair { Y = y, X = x };
                var nodeData = new NodeData { Pair = pair };
                SetProhibitedPositions(nodeData.Pair.Y, nodeData.Pair.X, ref nodeData.ProhibitedPositions);
                var treeNode = new TreeNode<NodeData>(nodeData);
                forest.Add(treeNode);
            }

            for (int x = 0; x < forest.Count; x++)
            {
                
                getAllowedPositionsInRow(y, forest[x].Data.ProhibitedPositions);

                //AddChildren(y+1, forest[x]);


                //var allowedPositions = new List<Pair>();
                //allowedPositions = getAllowedPositionsInRow(y, ref nodeData.ProhibitedPositions);
                //for (int i = 0; i < allowedPositions.Count; i++)
                //{
                //    forest[x].AddChild(new NodeData { new Pair { Y = allowedPositions[i].Y, X = allowedPositions[i].X } }); ;
                //}
                
            }
        }

        /*--------------------------------------------------расстановка фигур-------------------------------------------------------------------------------------------*/
 /*       public void SetQueens(int y, List<Pair> prohibitedPositions = null)
        {
            if(prohibitedPositions == null) 
                prohibitedPositions = new List<Pair>();
            List<Pair> allowedPositions = getAllowedPositionsInRow(y, ref prohibitedPositions);
            if (allowedPositions.Count == 0) return;
Console.WriteLine( allowedPositions.Count);
//Console.ReadKey();

            foreach (var position in allowedPositions)
            {

//Console.WriteLine($"{position.Y} {position.X}");
                var pair = new Pair { Y = position.Y, X = position.X };
                var nodeData = new NodeData { Pair = pair, ProhibitedPositions = prohibitedPositions };
                allPositions.AddChild(nodeData);
// PrintProhibitedPositions(nodeData.ProhibitedPositions);
 //Console.ReadKey();
                SetProhibitedPositions(nodeData.Pair.Y, nodeData.Pair.X, ref nodeData.ProhibitedPositions);
                if (y + 1 < dim)
                    SetQueens(y+1, ref nodeData.ProhibitedPositions);
            }

        }//*/
        /*--------------------------------------------------все доступные позиции в ряду-------------------------------------------------------------------------------------------*/
        List<Pair> getAllowedPositionsInRow(int y, List<Pair> prohibitedPositions)
        {
            var allowedPositions = new List<Pair>();
            
            for (int x = 0; x < dim; x++)//горизонталь
            {
                foreach (var item in prohibitedPositions)
                    if (x == item.X && y == item.Y)
                        continue;
                var pair = new Pair { Y = y, X = x };
                allowedPositions.Add(pair);
                
                foreach (var item in allowedPositions)    
                    Console.Write($"{x} ");
                Console.WriteLine();           
            }
            return allowedPositions;
        }
        /*--------------------------------------------------закрыть позиции которые бьет фигура-------------------------------------------------------------------------------------*/
        public void SetProhibitedPositions(int ciY, int ciX, ref List<Pair> prohibitedPositions)
        {
            var pair = new Pair() { Y = ciY, X = ciX };
            prohibitedPositions.Add(pair);

            for (int y = 0; y < dim; y++)
            {
                pair = new Pair() { Y = y, X = ciX };
                prohibitedPositions.Add(pair);
            }

            for (int x = 0; x < dim; x++)
            {
                pair = new Pair() { Y = ciY, X = x }; 
                prohibitedPositions.Add(pair);
            }

            for (int y = 0; y < dim; y++)//вертикаль
            {
                for (int x = 0; x < dim; x++)//горизонталь
                {
                    if (x == ciX && y >= ciY && x + 1 + y - ciY < dim && y + 1 < dim)
                    {
                        pair = new Pair() { Y = y + 1, X = x + 1 + y - ciY };
                        prohibitedPositions.Add(pair); //закрывается положительная диагональ
                    }
                    if (x == ciX && y >= ciY && x - 1 - y + ciY >= 0 && y + 1 < dim)
                    {
                        pair = new Pair() { Y = y + 1, X = x - 1 - y + ciY };
                        prohibitedPositions.Add(pair);//закрывается отрицательная диагональ
                    }
                }
            }
        }
                /*--------------------------------------------------обнуление всех закрытых позиций-------------------------------------------------------------------------------------------*/
        //void resetProhibitedPositions()//
        //{
        //    for (int y = 0; y < dim; y++)//вертикаль
        //    {
        //        for (int x = 0; x < dim; x++)//горизонталь
        //        {
        //            prohibitedPositions[y, x] = false;
        //        }
        //    }
        //}
        /*--------------------------------------------------печать закрытых позиций-------------------------------------------------------------------------------------*/
        public void PrintProhibitedPositions(bool[,] prohibitedPositions)
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
