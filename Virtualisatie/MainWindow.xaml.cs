using Controller;
using Model;
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

namespace Virtualisatie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Competition competition = new Competition();
            Data.Initialize(competition);
            Data.NextRace();
            Initialize();
        }

        public void Initialize()
        {
            CacheImages.ClearCache();
            Data.CurrentRace.DriverChanged += OnDriversChanged;
            this.Dispatcher.Invoke(() =>
            {
                //Data.CurrentRace.DriverChanged += ((MainDataContext)this.DataContext).OnDriversChanged;
            });

            Data.CurrentRace.NextRace += NextRace;
            //ImageCache.ClearCache();
        }

        public void OnDriversChanged(Object sender, DriversChangedEventArgs e)
        {
            //TrackImage.Dispatcher.BeginInvoke(
            this.firstImage.Dispatcher.BeginInvoke(
                System.Windows.Threading.DispatcherPriority.Render,
                new Action(() =>
                {
                    this.firstImage.Source = null;
                    this.firstImage.Source = Visualisation.DrawTrack(Data.CurrentRace.Track);
                }
            ));
        }

        public void NextRace(Object source, RaceStartEventArgs e)
        {
            Initialize();
        }
    }
}
