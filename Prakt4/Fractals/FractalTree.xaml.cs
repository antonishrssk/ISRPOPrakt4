using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Prakt4.Fractals
{
    /// <summary>
    /// Логика взаимодействия для FractalTree.xaml
    /// </summary>
    public partial class FractalTree : Window
    {
        private double _fractalSize;
        private double _angleOfInclination;
        private int _iterationsCount;
        private byte _colorR;
        private byte _colorG;
        private byte _colorB;

        public FractalTree()
        {
            InitializeComponent();

            MaxHeight = SystemParameters.PrimaryScreenHeight;
            MaxWidth = SystemParameters.PrimaryScreenWidth;
            MinHeight = MaxHeight / 2;
            MinWidth = MaxWidth / 2;
        }

        private void DrawLines(double x1, double y1, double length, double angle = -90, int iteration = 0)
        {
            if (iteration >= _iterationsCount)
                return;

            double x2 = x1 + length * Math.Cos(angle * Math.PI / 180);
            double y2 = y1 + length * Math.Sin(angle * Math.PI / 180);

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

            iteration++;
            DrawLines(x2, y2, length * 0.7, angle - 45 + _angleOfInclination, iteration);
            DrawLines(x2, y2, length * 0.7, angle + 45 + _angleOfInclination, iteration);
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
            else if (tbAngleOfInclination.Text == "")
            {
                _angleOfInclination = 0;
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
                && double.TryParse(tbAngleOfInclination.Text, out _angleOfInclination)
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
            DrawLines(_fractalSize * 2.5, _fractalSize * 3.5, _fractalSize);
        }
    }
}
