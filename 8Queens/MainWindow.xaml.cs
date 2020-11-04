using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _8Queens
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowChessBoard();
        }

        void ShowChessBoard()
        {
            chessBoard.Children.Clear();
            int dim;
            if(!int.TryParse(dimension.Text, out dim)) return;

            chessBoard.Rows = dim;
            chessBoard.Columns = dim;
            
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    var cell1 = new Rectangle { Fill = Brushes.LightGray };
                    var cell2 = new Rectangle { Fill = Brushes.Brown };

                    if(i % 2 == 0)
                    {
                        if(j % 2 == 0)
                            chessBoard.Children.Add(cell1);
                        else
                            chessBoard.Children.Add(cell2);
                    }
                    else
                    {
                        if (j % 2 == 0)
                            chessBoard.Children.Add(cell2);
                        else
                            chessBoard.Children.Add(cell1);
                    }
                }
            }
        }

        private void dimension_LostFocus(object sender, RoutedEventArgs e)
        {
            ShowChessBoard();
        }
    }
}
