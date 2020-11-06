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
            DateTime time1 = DateTime.Now;
            Queen queen = new Queen(8);
            queen.FindQueens();
            DateTime time2 = DateTime.Now;
            TimeSpan span = time2 - time1;

            Console.WriteLine($"Найдено решений: {queen.foundQueens.Count} за {span.TotalSeconds} секунд");
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
