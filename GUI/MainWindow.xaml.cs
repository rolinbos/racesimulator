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
using System.Windows.Threading;
using Controller;
using Model;
using RaceSimulator;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Direction CurrentDirection = Direction.East;

        public MainWindow()
        {
            InitializeComponent();

            // Create a new competition
            Competition competition = new Competition();
            Data.Initialize(competition);
            Data.NextRace();

            Initialize();
        }

        public void Initialize()
        {
            Image.ClearCache();
            Data.CurrentRace.DriverChanged += DriversChanged;
            Data.CurrentRace.NextRace += NextRace;
        }

        //Add the new events (intialize) to the new race when previous race ends
        public void NextRace(Object source, RaceStartEventArgs e)
        {
            Initialize();
        }

        public void DriversChanged(Object sender, DriversChangedEventArgs args)
        {
            this.CompleteTrack.Dispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new Action(() =>
                {
                    this.CompleteTrack.Source = null;
                    this.CompleteTrack.Source = GUIVisualisatie.DrawTrack(Data.CurrentRace.Track);
                }));
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }
    }
}
