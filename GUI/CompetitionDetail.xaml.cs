using Controller;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Model;
using System.Linq;

namespace GUI
{
    /// <summary>
    /// Interaction logic for CompetitionDetail.xaml
    /// </summary>
    public partial class CompetitionDetail : Window
    {
        public CompetitionDetail()
        {
            InitializeComponent();
            this.UpdateCompetitionScherm();
        }

        public void UpdateCompetitionScherm()
        {
            this.Dispatcher.Invoke(() =>
            {
                var dataContext = (RaceDetailDataContext)this.RaceGrid.DataContext;
                dataContext.TrackName = "Competitie";
                dataContext.participants = new System.Collections.ObjectModel.ObservableCollection<string>();

                int index = 1;
                foreach (var item in Data.Competition.Participants.OrderByDescending(participant => participant.Points))
                {
                    var str = $"{index}. {item.Name} heeft {item.Points} punten";
                    dataContext.participants.Add(str);
                    index++;
                }
            });
        }
    }
}
