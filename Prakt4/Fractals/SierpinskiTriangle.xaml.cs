using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Prakt4.Fractals
{
    /// <summary>
    /// Логика взаимодействия для SierpinskiTriangle.xaml
    /// </summary>
    public partial class SierpinskiTriangle : Window
    {
        private double _fractalSize;
        private int _iterationsCount;
        private byte _colorR;
        private byte _colorG;
        private byte _colorB;

        public SierpinskiTriangle()
        {
            InitializeComponent();

            MaxHeight = SystemParameters.PrimaryScreenHeight;
            MaxWidth = SystemParameters.PrimaryScreenWidth;
            MinHeight = MaxHeight / 2;
            MinWidth = MaxWidth / 2;
        }

        private void DrawTriangles(double size, double x, double y, int iteration = 0)
        {
            if (iteration >= _iterationsCount)
            {
                var triangle = new Polygon
                {
                    Stroke = new SolidColorBrush(Color.FromRgb(_colorR, _colorG, _colorB))
                };
                triangle.Points.Add(new Point(x,            y                          )); // верхний угол
                triangle.Points.Add(new Point(x - size / 2, y + Math.Sqrt(3) / 2 * size)); // левый угол
                triangle.Points.Add(new Point(x + size / 2, y + Math.Sqrt(3) / 2 * size)); // правый угол
                canvas.Children.Add(triangle);
                return;
            }

            size /= 2;
            iteration++;
            DrawTriangles(size, x,            y,                           iteration);
            DrawTriangles(size, x - size / 2, y + Math.Sqrt(3) / 2 * size, iteration);
            DrawTriangles(size, x + size / 2, y + Math.Sqrt(3) / 2 * size, iteration);
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
            DrawTriangles(_fractalSize, _fractalSize / 2, 0);
        }
    }
}
