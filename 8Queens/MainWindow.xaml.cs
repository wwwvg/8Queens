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

/*
    1) При создании шахматной доски помещать ссылки на клетки в двумерный массив для удобства расстановки ферзей. При изменении размерности его пересоздавать.
    2) В классе ферзя добавить метод возвращайщий из списка решений список очередных позиций
    2) После нахождения ферзей сразу вывести первое решение
    4) Отобразить в статусной строке статистическую информацию
    5) Кнопка "след. решение" изначально д.б. выключена, включаться должна только при наличии очередного решения

    Желательно:
    1) Поиск осуществляется в отдельном потоке с возможностью его остановки
    2) Возможность выводить первое решение по мере обнаружения (поток поиска должен информировать и передавать UI это решение и продолжать поиск дальше)
    3) Выводить минимально возможное кол-во ферзей

 */

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
            if (chessBoard == null) return;
            chessBoard.Children.Clear();

            if(!int.TryParse(dimension.Text, out int dim)) return;

            chessBoard.Rows = dim;
            chessBoard.Columns = dim;

            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    var cell1 = new Grid { Style = TryFindResource("grayGridCell") as Style};
                    var cell2 = new Grid { Style = TryFindResource("redGridCell") as Style };

                    //var gridRedCellStyle = new Style();


                    //тестовая установка ферзя в ячейки
                    #region использование векторного ферзя
                    var path = new Path()
                    {
                        Fill = Brushes.DarkOrange,
                        Data = (Geometry)this.TryFindResource("queenGeometry"),
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        Stretch = Stretch.UniformToFill,
                        Margin = new Thickness(5, 5, 5, 5),
                    };
                    var viewBox = new Viewbox();
                    viewBox.Child = path;
                    cell1.Children.Add(viewBox);
                    #endregion

                    if (i % 2 == 0)
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

        private void dimension_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowChessBoard();
        }
    }
}
