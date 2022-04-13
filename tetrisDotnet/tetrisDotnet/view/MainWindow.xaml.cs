using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace tetrisDotnet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Rectangle t = new Rectangle() { Fill = Brushes.Red };

        public MainWindow()
        {
            InitializeComponent();
            Grid.SetColumn(t, 5);
            Grid.SetRow(t, 10);
            grid_plateau.Children.Add(t);

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left && Grid.GetColumn(t) != 0)
            {
                Grid.SetColumn(t,Grid.GetColumn(t)-1);

            }
            else if (e.Key == Key.Right && Grid.GetColumn(t) != 10)
            {
                Grid.SetColumn(t, Grid.GetColumn(t) + 1);

            }
        }
    }
}
