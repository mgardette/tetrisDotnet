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
using System.Timers;
using tetrisDotnet.view_model;

namespace tetrisDotnet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Rectangle t = new Rectangle() { Fill = Brushes.Red };
        ViewModel viewModel = new ViewModel();


        public MainWindow()
        {
            this.DataContext = viewModel;
            InitializeComponent();
            Grid.SetColumn(t, 5);
            Grid.SetRow(t, 0);

            //Ajout de l'image à la Grid
            //Grid.Children.Add(img);
            //Ceci à la colonne 0
            //Grid.SetColumn(img, 0);
            //Grid.SetRow(t, 5);
            gridPlateau.Children.Add(t);

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                viewModel.moveBlockSide("left");
            }
            else if (e.Key == Key.Right)
            {
                viewModel.moveBlockSide("right");
            }
            else if (e.Key == Key.Up && Grid.GetRow(t) != 19)
            {
                //TODO piece qui tombe d'un coup
            }
            else if (e.Key == Key.Down && Grid.GetRow(t) != 19)
            {
                viewModel.moveBlockSide("down");
            }
        }
        
    }
}

