using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using TestConsole;
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
        Grid[,] cells;
        Queen queen;
     //   int current

        public MainWindow()
        {
            InitializeComponent();
            ShowChessBoard();
        }

        #region Показать шахматную доску
        void ShowChessBoard()
        {
            if (chessBoard == null) return;
            chessBoard.Children.Clear();

            if(!int.TryParse(dimension.Text, out int dim)) return;

            chessBoard.Rows = dim;
            chessBoard.Columns = dim;

            cells = new Grid[dim, dim];
            for (int y = 0; y < dim; y++)
            {
                for (int x = 0; x < dim; x++)
                {
                    cells[y, x] = AddCell(y, x);
                }
            }
        }
        #endregion

        #region Установить в ней ячейки
        Grid AddCell(int y, int x)
        {
            var cell1 = new Grid { Style = TryFindResource("grayGridCell") as Style };
            var cell2 = new Grid { Style = TryFindResource("redGridCell") as Style };

            Grid cell;
            if (y % 2 == 0)
            {
                if (x % 2 == 0)
                    cell = AddCell(cell1);
                else
                    cell = AddCell(cell2);
            }
            else
            {
                if (x % 2 == 0)
                    cell = AddCell(cell2);
                else
                    cell = AddCell(cell1);
            }
            return cell;
        }

        Grid AddCell(Grid cell)
        {
            chessBoard.Children.Add(cell);
            return cell;
        }
        #endregion

        #region Если изменилась размерность то перерисовать доску
        private void dimension_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowChessBoard();
        }
        #endregion

        #region Если нажата кнопка искать, то искать решения, а потом вывести первое решение на экран
        void solveTask_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(dimension.Text, out int dim)) return;
            if (chessBoard == null) return;

            for (int y = 0; y < dim; y++)
            {
                for (int x = 0; x < dim; x++)
                {
                    cells[y, x].Children.Clear();
                }
            }

            statusBar.Text = "";
            
            queen = new Queen(dim);
            queen.FindQueens();
            List<List<int>> list = queen.GetFoundQueens();
            if (list.Count == 0) return;

            int yy = 0;
            foreach (var x in list[new Random().Next(0, list.Count)])
            {
                var path = new Path()
                {
                    Fill = Brushes.Black,
                    Data = (Geometry)this.TryFindResource("queenGeometry"),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Stretch = Stretch.UniformToFill,
                    Margin = new Thickness(5, 5, 5, 5),
                    Name = "queen",
                };

                var viewBox = new Viewbox();
                viewBox.Child = path;
                cells[yy++, x].Children.Add(viewBox);
                
            }
            statusBar.Text = $"Найдено решений: {queen.GetQueensCount()}    " +
                             $"Всего комбинаций: {queen.GetAllCombinationsCount()}    " +
                             $"Затраченное время: {queen.GetSearchTime()} секунд";

        }
        #endregion
    }
}
