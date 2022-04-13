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

namespace tetrisDotnet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Rectangle t = new Rectangle() { Fill = Brushes.Red };
        private static System.Timers.Timer timer;

        public MainWindow()
        {
            SetTimer();

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
            if (e.Key == Key.Left && Grid.GetColumn(t) != 0)
            {
                Grid.SetColumn(t,Grid.GetColumn(t)-1);

            }
            else if (e.Key == Key.Right && Grid.GetColumn(t) != 10)
            {
                Grid.SetColumn(t, Grid.GetColumn(t) + 1);

            }
            else if (e.Key == Key.Up && Grid.GetRow(t) != 19)
            {
                Grid.SetRow(t, 19);

            }
            else if (e.Key == Key.Down && Grid.GetRow(t) != 19)
            {
                Grid.SetRow(t, Grid.GetRow(t) + 1);

                // Reset du timer
                timer.Stop();
                timer.Start();

            }
        }
        private void SetTimer()
        {
            // Create a timer with a two second interval.
            timer = new Timer(1500);
            // Hook up the Elapsed event for the timer. 
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            synchronize(() =>
            {
                if (Grid.GetRow(t) != 19)
                {
                    Grid.SetRow(t, Grid.GetRow(t) + 1);
                }
            });
        }

        private static void synchronize(Action a)
        {
            Application app = Application.Current;
            if (app != null && app.Dispatcher != null)
            {
                Application.Current.Dispatcher.Invoke(a);
            }
        }
    }
}

