using System.Windows;
using Prakt4.Fractals;

namespace Prakt4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnFractalTree_Click(object sender, RoutedEventArgs e)
            => new FractalTree().Show();

        private void btnKochCurve_Click(object sender, RoutedEventArgs e)
            => new KochCurve().Show();

        private void btnSierpinskiCarpet_Click(object sender, RoutedEventArgs e)
            => new SierpinskiCarpet().Show();

        private void btnSierpinskiTriangle_Click(object sender, RoutedEventArgs e)
            => new SierpinskiTriangle().Show();

        private void btnCantorSet_Click(object sender, RoutedEventArgs e)
            => new CantorSet().Show();
    }
}