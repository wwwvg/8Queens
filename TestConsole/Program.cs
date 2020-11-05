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
            queen.SetQueens();
            queen.PrintQueensPositions();
            

            //queen.SetProhibitedPositions(7, 4);
            //queen.PrintProhibitedPositions();
        }
    }
}
