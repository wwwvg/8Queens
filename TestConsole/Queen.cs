using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*  
            1) Создаем лес из dim узлов, где узлы расставляеются по-порядку
            2) Делают ход (записывает позицию в свои данные)
            3) Записывают запрещенные позиции
            4) Получают доступные позиции для детей
            5) Создают детей, в соответствии с доступными позициями. Детям передают координаты и список запрещенных позиций
            6) Дети уже сами смотрят возможные ходы на основании полученного списка запрещенных позиций

         
            1) Каждый узел изначально получает список запрещенных позиций
            2) На основании этого списка получает разрешенные позиции в след. ряду
            3) Делает ходы по всем позициям (записывает позицию в свои данные)
            4) Добавляет на основании своего хода запрещенные позиции и передает их дочернему узлу 
*/

namespace TestConsole
{
    #region NodeData
    class NodeData
    {
        public bool[,] ProhibitedPositions { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public NodeData(int y, int x, int dim)
        {
            Y = y;
            X = x;
            ProhibitedPositions = new bool[dim,dim];
        }
        public NodeData(int y, int x, int dim, bool[,] prohibitedPositions) : this(y, x, dim)
        {
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    ProhibitedPositions[i, j] = prohibitedPositions[i, j];
                }
            }
            ;
        }
        public override string ToString()
        {
            return $"{Y }:{X }";
        }
    }
    #endregion

    public class Queen
    {
        List<TreeNode<NodeData>> forest; 
        List<TreeNode<NodeData>> foundQueens = new List<TreeNode<NodeData>>();
        int childCounter;
        DateTime timeStart;
        DateTime timeStop;

        public int Dim { get; }//размерность
        public int FindNotMoreThan { get; }

        public Queen(int dim = 8, int findNotMoreThan = int.MaxValue)
        {
            Dim = dim;
            FindNotMoreThan = findNotMoreThan;
        }

        #region Возврат результата и статистики
        public int GetQueensCount() => foundQueens.Count;

        public int GetAllCombinationsCount() => childCounter + Dim;

        public double GetSearchTime() => (timeStop - timeStart).TotalSeconds;

        public List<List<int>> GetFoundQueens()
        {
            var list = new List<List<int>>();   
            foreach (var item in foundQueens)  // item = TreeNode<NodeData>
            {
                var treeNode = item;
                var listX = new List<int>();
                do
                {
                    listX.Add(treeNode.Data.X);
                    treeNode = treeNode.Parent;
                }
                while (treeNode != null);

                list.Add(listX);
            }
            return list;
        }
        #endregion


        public void FindQueens()
        {
            timeStart = DateTime.Now;
            int y = 0;
            forest = new List<TreeNode<NodeData>>(Dim);
            for (int x = 0; x < Dim; x++)   //добавление корней в первый ряд
            {
                var nodeData = new NodeData(y, x, Dim);     //установка координат фигуры и размерности для запрещ. позиций
                SetProhibitedPositions(nodeData);   //установка запрещенных позиций
                var treeNode = new TreeNode<NodeData>(nodeData);    //создание корня
                forest.Add(treeNode);   //добавление корня
            }

            AddRoots(0, forest.Count);

            timeStop = DateTime.Now;
        }

        void AddRoots(int begin, int end)
        {
            int y = 0;
            for (int x = begin; x < end; x++)
            {

                if (y + 1 < Dim)
                {
                    var allowedPos = nextRowAllowedPosition(y + 1, forest[x].Data); // получение доступных позиций в след. ряду
                    {
                        foreach (var itemX in allowedPos)   // для каждой доступной позиции добавляем ребенка
                        {
                            AddChildren(y + 1, itemX, forest[x]); // в рекурсивную функцию передаем координаты и запрет. позиции для создания детей
                        }
                    }
                }
            }
        }
        /*--------------------------------------------------рекурсивная функция заполняющая дерево всех решений-------------------------------------------------------------------------------------*/
        void AddChildren(int y, int x, TreeNode<NodeData> parent)
        {            
            NodeData nodeData = new NodeData(y, x, Dim, parent.Data.ProhibitedPositions);      
            SetProhibitedPositions(nodeData);   //установка запрещенных позиций
            
            var child = parent.AddChild(nodeData);   //добавление ребенка if (y + 1 < dim)

/*=====>*/  if (y + 1 == Dim) foundQueens.Add(child); //добавил решение
             
            if (FindNotMoreThan == GetQueensCount()) return; //если указано, что искать не более N решений

            if (y + 1 < Dim)
            {
                childCounter++;
                var allowedPos = nextRowAllowedPosition(y + 1, child.Data); // получение доступных позиций в след. ряду
                if (allowedPos.Count == 0) return;
                {
                    foreach (var itemX in allowedPos)   // для каждой доступной позиции добавляем ребенка
                    {
                        AddChildren(y + 1, itemX, child); // в рекурсивную функцию передаем координаты и запрет. позиции для создания детей      
                    }
                }
            }
        }
/*--------------------------------------------------дать открытые позиции в след. ряду-------------------------------------------------------------------------------------*/
        List<int> nextRowAllowedPosition(int y, NodeData nodeData)
        {
            var allowedPos = new List<int>();
            for (int x = 0; x < Dim; x++)//горизонталь
            {
                if (!nodeData.ProhibitedPositions[y, x])
                    allowedPos.Add(x);
            }
            return allowedPos;
        }

/*--------------------------------------------------закрыть позиции которые бьет фигура-------------------------------------------------------------------------------------*/
        void SetProhibitedPositions(NodeData nodeData)
        {
            int ciY = nodeData.Y;
            int ciX = nodeData.X;

            nodeData.ProhibitedPositions[ciY, ciX] = true;

            for (int y = 0; y < Dim; y++)
                nodeData.ProhibitedPositions[y, ciX] = true;

            for (int x = 0; x < Dim; x++)
                nodeData.ProhibitedPositions[ciY, x] = true;

            for (int y = 0; y < Dim; y++)//вертикаль
            {
                for (int x = 0; x < Dim; x++)//горизонталь
                {
                    if (x == ciX && y >= ciY && x + 1 + y - ciY < Dim && y + 1 < Dim) nodeData.ProhibitedPositions[y + 1, x + 1 + y - ciY] = true; //закрывается положительная диагональ
                    if (x == ciX && y >= ciY && x - 1 - y + ciY >= 0 && y + 1 < Dim) nodeData.ProhibitedPositions[y + 1, x - 1 - y + ciY] = true; //закрывается отрицательная диагональ
                }
            }
        }

/*--------------------------------------------------печать закрытых позиций-------------------------------------------------------------------------------------*/
        void PrintProhibitedPositions(bool[,] prohibitedPositions)
        {
            for (int y = 0; y < Dim; y++)
            {
                for (int x = 0; x < Dim; x++)
                {
                    string isProhibited = prohibitedPositions[y, x] ? "#" : ".";
                    Console.Write($"{isProhibited}    ");
                }
                Console.WriteLine("\n");
            }
            Console.WriteLine("\n");
           // Console.ReadKey();
        }
    }
}



//Parallel.Invoke(() => AddRoots(0, 0),
//                () => AddRoots(1, 1),
//                () => AddRoots(2, 2),
//                () => AddRoots(3, 3),
//                () => AddRoots(4, 4),
//                () => AddRoots(5, 8)
//                );


//for (int x = 0; x < forest.Count; x++)
//{

//    if (y + 1 < Dim)
//    {
//        var allowedPos = nextRowAllowedPosition(y + 1, forest[x].Data); // получение доступных позиций в след. ряду
//        {
//            foreach (var itemX in allowedPos)   // для каждой доступной позиции добавляем ребенка
//            {
//                AddChildren(y + 1, itemX, forest[x]); // в рекурсивную функцию передаем координаты и запрет. позиции для создания детей
//            }
//        }
//    }
//}