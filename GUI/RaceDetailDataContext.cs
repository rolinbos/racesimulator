using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Model;

namespace GUI
{
    public class RaceDetailDataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _trackName;
        public string TrackName {
            get
            {
                return _trackName;
            }
            set
            {
                _trackName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TrackName"));
            }
        }

        private ObservableCollection<string> _participants;
        public ObservableCollection<string> participants
        {
            get
            {
                return _participants;
            }
            set
            {
                _participants = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("participants"));
            }
        }

        private ObservableCollection<string> _racelijst;
        public ObservableCollection<string> racelijst
        {
            get
            {
                return _racelijst;
            }
            set
            {
                _racelijst = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("racelijst"));
            }
        }

        public string LabelName { get; set; }

        private ObservableCollection<string> _leaderSchip;
        public ObservableCollection<string> leaderSchip
        {
            get
            {
                return _leaderSchip;
            }
            set
            {
                _leaderSchip = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TrackName"));
            }
        }
    }
}
