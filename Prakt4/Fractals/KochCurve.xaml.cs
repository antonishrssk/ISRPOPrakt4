using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Prakt4.Fractals
{
    /// <summary>
    /// Логика взаимодействия для KochCurve.xaml
    /// </summary>
    public partial class KochCurve : Window
    {
        private double _fractalSize;
        private int _iterationsCount;
        private byte _colorR;
        private byte _colorG;
        private byte _colorB;

        public KochCurve()
        {
            InitializeComponent();

            MaxHeight = SystemParameters.PrimaryScreenHeight;
            MaxWidth = SystemParameters.PrimaryScreenWidth;
            MinHeight = MaxHeight / 2;
            MinWidth = MaxWidth / 2;
        }

        private void DrawLines(double x1, double y1, double x2, double y2, int iteration = 0)
        {
            if (iteration >= _iterationsCount)
            {
                var line = new Line
                {
                    X1 = x1,
                    Y1 = y1,
                    X2 = x2,
                    Y2 = y2,
                    Stroke = new SolidColorBrush(Color.FromRgb(_colorR, _colorG, _colorB)),
                    StrokeThickness = 1
                };
                canvas.Children.Add(line);
                return;
            }

            // расстояния между координатами
            double dx = (x2 - x1) / 3;
            double dy = (y2 - y1) / 3;

            // координаты вершин треугольника
            double xA = x1 + dx;
            double yA = y1 + dy;
            double xB = x2 - dx;
            double yB = y2 - dy;
            double xC = (x1 + x2) / 2 + Math.Sqrt(3) / 2 * dy;
            double yC = (y1 + y2) / 2 - Math.Sqrt(3) / 2 * dx;

            iteration++;
            DrawLines(x1, y1, xA, yA, iteration);
            DrawLines(xA, yA, xC, yC, iteration);
            DrawLines(xC, yC, xB, yB, iteration);
            DrawLines(xB, yB, x2, y2, iteration);
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
            double y = _fractalSize / 3;
            DrawLines(0, y, _fractalSize, y);
        }
    }
}
