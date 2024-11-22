using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Prakt4.Fractals
{
    /// <summary>
    /// Логика взаимодействия для CantorSet.xaml
    /// </summary>
    public partial class CantorSet : Window
    {
        private const int LineGap = 20;
        private double _fractalLength;
        private int _iterationsCount;
        private byte _colorR;
        private byte _colorG;
        private byte _colorB;

        public CantorSet()
        {
            InitializeComponent();

            MaxHeight = SystemParameters.PrimaryScreenHeight;
            MaxWidth = SystemParameters.PrimaryScreenWidth;
            MinHeight = MaxHeight / 2;
            MinWidth = MaxWidth / 2;
        }

        private void DrawLines(double length, double x1, double y, int iteration = 0)
        {
            if (iteration >= _iterationsCount)
                return;

            canvas.Children.Add(new Line
            {
                X1 = x1,
                X2 = x1 + length,
                Y1 = y,
                Y2 = y,
                Stroke = new SolidColorBrush(Color.FromRgb(_colorR, _colorG, _colorB)),
                StrokeThickness = LineGap / 2
            });

            length /= 3;
            iteration++;
            DrawLines(length, x1,              y + LineGap, iteration);
            DrawLines(length, x1 + length * 2, y + LineGap, iteration);
        }

        private void tbFractalSettings_LostFocus(object sender, RoutedEventArgs e)
        {
            // обработка некорректных данных
            if (tbFractalLength.Text == "")
            {
                _fractalLength = 0;
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

            if (!(double.TryParse(tbFractalLength.Text, out _fractalLength)
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
            DrawLines(_fractalLength, LineGap, LineGap);
        }
    }
}
