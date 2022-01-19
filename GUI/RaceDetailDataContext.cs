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
        public ObservableCollection<string> Participants
        {
            get
            {
                return _participants;
            }
            set
            {
                _participants = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Particpants"));
            }
        }
    }
}
