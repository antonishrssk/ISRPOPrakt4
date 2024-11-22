using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Prakt4.Fractals
{
    /// <summary>
    /// Логика взаимодействия для SierpinskiCarpet.xaml
    /// </summary>
    public partial class SierpinskiCarpet : Window
    {
        private double _fractalSize;
        private int _iterationsCount;
        private byte _colorR;
        private byte _colorG;
        private byte _colorB;

        public SierpinskiCarpet()
        {
            InitializeComponent();

            MaxHeight = SystemParameters.PrimaryScreenHeight;
            MaxWidth = SystemParameters.PrimaryScreenWidth;
            MinHeight = MaxHeight / 2;
            MinWidth = MaxWidth / 2;
        }

        private void DrawSquares(double size, double x, double y, int iteration = 0)
        {
            if (iteration >= _iterationsCount)
            {
                var square = new Rectangle
                {
                    Height = size,
                    Width = size,
                    Fill = new SolidColorBrush(Color.FromRgb(_colorR, _colorG, _colorB))
                };
                Canvas.SetLeft(square, x);
                Canvas.SetTop(square, y);
                canvas.Children.Add(square);
                return;
            }

            size /= 3;
            iteration++;
            DrawSquares(size, x,            y,            iteration);
            DrawSquares(size, x + size,     y,            iteration);
            DrawSquares(size, x + size * 2, y,            iteration);
            DrawSquares(size, x,            y + size,     iteration);
            DrawSquares(size, x + size * 2, y + size,     iteration);
            DrawSquares(size, x,            y + size * 2, iteration);
            DrawSquares(size, x + size,     y + size * 2, iteration);
            DrawSquares(size, x + size * 2, y + size * 2, iteration);
        }

        private void tbFractalSettings_LostFocus(object sender, RoutedEventArgs e)
        {
            // обработка некорректных данных
            if (tbFractalSize.Text == "")
            {
                _fractalSize = 0;
                canvas.Children.Clear();
                return;
            }
            else if (tbIterationsCount.Text == "")
            {
                _iterationsCount = 0;
                canvas.Children.Clear();
                return;
            }
            else if (tbColorR.Text == "")
            {
                _colorR = 0;
                canvas.Children.Clear();
                return;
            }
            else if (tbColorG.Text == "")
            {
                _colorG = 0;
                canvas.Children.Clear();
                return;
            }
            else if (tbColorB.Text == "")
            {
                _colorB = 0;
                canvas.Children.Clear();
                return;
            }
            
            if (!(double.TryParse(tbFractalSize.Text, out _fractalSize)
                && int.TryParse(tbIterationsCount.Text, out _iterationsCount)
                && byte.TryParse(tbColorR.Text, out _colorR)
                && byte.TryParse(tbColorG.Text, out _colorG)
                && byte.TryParse(tbColorB.Text, out _colorB)
                ))
            {
                MessageBox.Show("Введите правильные значения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // рисование фрактала
            canvas.Children.Clear();
            DrawSquares(_fractalSize, 0, 0);
        }
    }
}
