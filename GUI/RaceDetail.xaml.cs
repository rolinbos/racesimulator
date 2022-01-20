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
                dataContext.participants = new System.Collections.ObjectModel.ObservableCollection<string>();

                foreach (var item in Data.CurrentRace.Participants)
                {
                    var str = $"{item.Name} heeft {item.Laps} lap gereden";
                    dataContext.participants.Add(str);
                }

                dataContext.racelijst = new System.Collections.ObjectModel.ObservableCollection<string>();
                //Voeg alle tracknamen toe aan de lijst
                dataContext.racelijst.Add(Data.CurrentRace.Track.Name);
                foreach (var item in Data.CurrentRace.Track.Sections)
                {
                    dataContext.racelijst.Add(item.SectionType.ToString());
                }
            });
        }
    }
}
