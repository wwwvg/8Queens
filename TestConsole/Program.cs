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
        static int counter = 0;
        static void Main(string[] args)
        {
            Queen queen = new Queen(8);
            queen.FindQueens();
            Console.WriteLine(Queen.childCounter);
            for (int i = 0; i < queen.forest.Count; i++)
            {
                foreach (var node in queen.forest[i])
                    if (node.Data.Y == queen.dim - 1)
                        printQueens(queen.dim, node);
            }

            //queen.SetProhibitedPositions(7, 4);
            //queen.PrintProhibitedPositions();
        }

        static void printQueens(int dim, TreeNode<NodeData> treeNode)
        {
            StringBuilder sb = new StringBuilder();
            do
            {
                Console.WriteLine(treeNode);
                treeNode = treeNode.Parent;
            }
            while (treeNode != null) ;
            Console.WriteLine("---");
        }
    }
}
