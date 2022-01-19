using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Controller;
using Model;

namespace GUI
{
    /// <summary>
    /// Interaction logic for RaceDetail.xaml
    /// </summary>
    public partial class RaceDetail : Window
    {
        public RaceDetail()
        {
            InitializeComponent();
            Update();
            Data.CurrentRace.DriverChanged += OnDriversChangedStats;
        }

        public void OnDriversChangedStats(object sender, DriversChangedEventArgs e)
        {
            Update();
        }

        public void Update()
        {
            this.Dispatcher.Invoke(() =>
            {
                var dataContext = (RaceDetailDataContext)this.RaceGrid.DataContext;
                dataContext.TrackName = Data.CurrentRace.Track.Name;
                dataContext.Participants = new System.Collections.ObjectModel.ObservableCollection<string>();
                foreach (var item in Data.CurrentRace.Participants)
                {
                    dataContext.Participants.Add(item.Name);
                    dataContext.Participants.Add(item.Laps.ToString());
                }
            });
        }
    }
}
